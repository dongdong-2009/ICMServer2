using Equin.ApplicationFramework;
using ICMServer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICMServer
{
    public partial class FormDeviceManagement : Form
    {
        ICMDBContext m_DB = new ICMDBContext();

        public FormDeviceManagement()
        {
            InitializeComponent();
            ComboBoxDevTypeForPageDevInfo.Items.Add("全部");
            ComboBoxDevTypeForPageDevInfo.Items.AddRange(m_DeviceTypes);
            ComboBoxQueryTypeForPageDevMgmt.Items.Add("全部");
            ComboBoxQueryTypeForPageDevMgmt.Items.AddRange(m_DeviceTypes);
        }

        private readonly string[] m_DeviceTypes = 
        {
            "服务器",
            "别墅门口机",
            "单元门口机",
            "楼栋门口机",
            "小区门口机",
            "室内机",
            "管理机",
            "室内机 (SD)",
            "手机",
            "公共门铃机",
            "IP摄像头"
        };

        private void FormDeviceManagement_Load(object sender, EventArgs e)
        {
            ComboBoxDevTypeForPageDevInfo.SelectedIndex = 0;
            ComboBoxDevStatus.SelectedIndex = 0;
            ComboBoxQueryTypeForPageDevMgmt.SelectedIndex = 0;
            RefreshSIPAccountsList();
            RefreshDevicesManList();
            RefreshDevicesList();
            timer.Enabled = true;

            listViewSIPAccount.View = View.Details;
            listViewSIPAccount.GridLines = false;
            listViewSIPAccount.FullRowSelect = true;
            listViewSIPAccount.Scrollable = true;
            listViewSIPAccount.CheckBoxes = true;
            listViewSIPAccount.Columns.Add("Status", 100);
            listViewSIPAccount.Columns.Add("Account", 100);
            listViewSIPAccount.Columns.Add("Room Address", 180);
            listViewSIPAccount.Columns.Add("Group", 100);
            BtnSIPAccountRefresh_Click(this, e);
        }

        private async void RefreshDevicesManList()
        {
            bindingSourceDevicesForDeviceManagement.DataSource = await GetDevicesAsync(false);
        }

        private async void RefreshDevicesList()
        {
            bindingSourceDevices.DataSource = await GetDevicesAsync(true);
        }

        private void RefreshSIPAccountsList()// async void RefreshSIPAccountsList()
        {
            listViewSIPAccount.Items.Clear();
            
            using (var db = new ICMDBContext())
            {
                foreach (var dev in db.Sipaccounts)
                {
                    string room = DevicesAddressConverter.RoToChStr(dev.C_room);
                    List<string> sublist = new List<string>();
                    int online = (int)(dev.C_registerstatus ?? 0);
                    if (online == 0)
                        sublist.Add(strings.offline);//"離線");
                    else
                        sublist.Add(strings.online);//"在線");
                    sublist.Add(dev.C_user);
                    sublist.Add(room);
                    sublist.Add(dev.C_usergroup);
                    listViewSIPAccount.Items.Add(new ListViewItem(sublist.ToArray()));
                }
            }
            //bindingSourceSIPAccounts.DataSource = await GetSIPAccountsAsync();
            //this.dataGridViewSIPAccounts.Invalidate();
        }

        #region Async Database Operations

        public Task<BindingListView<Device>> GetDevicesAsync(bool reload)
        {
            return Task.Run(() => { return GetDevices(reload); });
        }

        public BindingListView<Device> GetDevices(bool reload)
        {
            BindingListView<Device> Devices = null;
            try
            {
                if (reload)
                {
                    using (var db = new ICMDBContext())
                    {
                        Devices = new BindingListView<Device>(db.Devices.ToList());
                    }
                }
                else
                {
                    Devices = new BindingListView<Device>(m_DB.Devices.ToList());
                }
            }
            catch (Exception) { }


            return Devices;
        }

        public static Task<BindingListView<sipaccount>> GetSIPAccountsAsync()
        {
            return Task.Run(() => { return GetSIPAccounts(); });
        }

        public static BindingListView<sipaccount> GetSIPAccounts()
        {
            BindingListView<sipaccount> sipaccounts = null;

            using (var db = new ICMDBContext())
            {
                sipaccounts = new BindingListView<sipaccount>(db.Sipaccounts.ToList());
            }

            return sipaccounts;
        }
        #endregion

        private void BtnImportAddressBook_Click(object sender, EventArgs e)
        {
            DialogAddressBookImport dlg = new DialogAddressBookImport();
            dlg.ShowDialog();
            SetLocalIPAsync();
            RefreshDevicesManList();
            RefreshDevicesList();
        }

        private async void SetLocalIPAsync()
        {
            using (var db = new ICMDBContext())
            {
                var controlServer = await (from Device in db.Devices
                                           where Device.type == 0
                                           select Device).FirstOrDefaultAsync();
                if (controlServer != null)
                {
                    Config.Instance.LocalIP = controlServer.ip;
                    Config.Instance.LocalGateway = controlServer.gw;
                    Config.Instance.LocalSubnetMask = controlServer.sm;
                }
            }
        }

        private void BtnRefreshDevMan_Click(object sender, EventArgs e)
        {
            RefreshDevicesManList();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            //RefreshDevicesList();
            BtnSearch_Click(sender, e);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            //RefreshDevicesList();
            BtnSearch_Click(sender, e);
        }

        private void DataGridViewDevDataTable_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null)
                return;

            if (e.ColumnIndex == dgvTxtBoxColOnlineStatus.Index)
            {
                bool online = ((int)e.Value) != 0;
                e.Value = (!online) ? strings.offline : strings.online;
                e.CellStyle.BackColor = (!online) ? Color.Red : Color.LimeGreen;
            }
            else if (e.ColumnIndex == dgvTxtBoxColDeviceId.Index)
            {
                string DeviceId = (string)e.Value;
                e.Value = DevicesAddressConverter.RoToChStr(DeviceId);
            }
            else if (e.ColumnIndex == dgvComboBoxColDeviceType.Index)
            {
                int DeviceType = (int)e.Value;
                if (DeviceType < m_DeviceTypes.Count())
                {
                    e.Value = m_DeviceTypes[DeviceType];
                }
            }
        }

        private void BtnShowDeviceInfo_Click(object sender, EventArgs e)
        {
            if (bindingSourceDevices.Current == null)
                return;
            
            var Device = ((ObjectView<Device>)bindingSourceDevices.Current).Object;
            if (Device != null)
            {
                DialogDevAlter dlg = new DialogDevAlter(null, Device, true);
                dlg.ShowDialog();
            }
        }

        private async void BtnSearch_Click(object sender, EventArgs e)
        {
            using (var db = new ICMDBContext())
            { 
                int DeviceType = ComboBoxDevTypeForPageDevInfo.SelectedIndex - 1;
                int onlineStatus = ComboBoxDevStatus.SelectedIndex - 1;

                //bindingSourceDevices.DataSource = await Task.Run(() => { return new BindingListView<Device>(db.Devices.ToList()); });
                //BindingListView<Device> Devices = (BindingListView<Device>)bindingSourceDevices.DataSource;

                if (DeviceType > -1 && onlineStatus == -1)
                {
                    bindingSourceDevices.DataSource = await Task.Run(() => 
                    { 
                        return new BindingListView<Device>((from d in db.Devices
                                where d.type == DeviceType
                                select d).ToList()); 
                    });
                    //Devices.Filter = new PredicateItemFilter<Device>(Device => Device.type == DeviceType);
                }
                else if (DeviceType == -1 && onlineStatus > -1)
                {
                    bindingSourceDevices.DataSource = await Task.Run(() =>
                    {
                        return new BindingListView<Device>((from d in db.Devices
                                where d.online == (onlineStatus == 0 ? 0 : 1)
                                                            select d).ToList());
                    });
                    //Devices.Filter = new PredicateItemFilter<Device>(Device => Device.online == (onlineStatus == 0 ? 0 : 1));
                }
                else if (DeviceType > -1 && onlineStatus > -1)
                {
                    bindingSourceDevices.DataSource = await Task.Run(() =>
                    {
                        return new BindingListView<Device>(
                            (from d in db.Devices
                                where (d.type == DeviceType) && (d.online == (onlineStatus == 0 ? 0 : 1))
                                select d).ToList());
                    });
                    //Devices.Filter = new PredicateItemFilter<Device>(Device => (Device.type == DeviceType) && (Device.online == (onlineStatus == 0 ? 0 : 1)));
                }
                else
                {
                    bindingSourceDevices.DataSource = await Task.Run(() => { 
                        return new BindingListView<Device>(db.Devices.ToList()); });
                    //Devices.Filter = null;
                }
            }
        }

        private void DataGridViewSIPAccounts_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dgvTxtBoxColSIPAccountStatus.Index)
                e.Value = "離線";
            else if (e.ColumnIndex == dgvTxtBoxColSIPAccountDeviceId.Index)
            {
                string DeviceId = (string)e.Value;
                DevicesAddressConverter.RoToChStr(DeviceId);
            }
        }

        private void BtnSIPAccountRefresh_Click(object sender, EventArgs e)
        {
            RefreshSIPAccountsList();
        }

        private void BtnAccountManager_Click(object sender, EventArgs e)
        {
            //DialogSIPManagement sipman = new DialogSIPManagement();
            //sipman.ShowDialog();
            DialogSIPMan sipman = new DialogSIPMan();
            sipman.ShowDialog();
        }

        private void BtnModifyDev_Click(object sender, EventArgs e)
        {
            if (bindingSourceDevicesForDeviceManagement.Current == null)
                return;

            var Device = ((ObjectView<Device>)bindingSourceDevicesForDeviceManagement.Current).Object;

            if (Device != null)
            {
                DialogDevAlter AlterForm = new DialogDevAlter(m_DB, Device, false);
                if (AlterForm.ShowDialog() == DialogResult.OK)
                    BtnRefreshDevMan_Click(this, e);
            }
            else
                MessageBox.Show("Choose the item you want to modify");
        }

        private void BtnRefreshOnline_Click(object sender, EventArgs e)
        {
            int nCount = listViewSIPAccount.Items.Count;
            for (int i = 0; i < nCount; i++)
            {
                if (listViewSIPAccount.Items[i].Checked)
                {
                        string SipServerAndPort = Config.Instance.SIPServerIP + ":" + Config.Instance.SIPServerPort.ToString();
                    string account = listViewSIPAccount.Items[i].SubItems[1].Text;
                    string httpPush = "http://" + SipServerAndPort + "/QuerySipAccountOnline?account=" + account;
                    WebRequest request = (HttpWebRequest)WebRequest.Create(httpPush);
                    request.Method = WebRequestMethods.Http.Get;
                    request.ContentType = "";
                    request.Timeout = 1000 * 5;
                    try
                    {
                        var resp = new StreamReader(request.GetResponse().GetResponseStream()).ReadToEnd();
                        if (resp.ToString() == "100")
                        {
                            listViewSIPAccount.Items[i].SubItems[0].Text = strings.online;//"在線";
                            var QueryAccount = (from sip in m_DB.Sipaccounts
                                                where sip.C_user == account
                                                select sip).FirstOrDefault();
                            if (QueryAccount != null)
                                QueryAccount.C_registerstatus = 1;

                        }
                        else if (resp.ToString() == "200")
                        {
                            listViewSIPAccount.Items[i].SubItems[0].Text = strings.offline;// "離線";
                            var QueryAccount = (from sip in m_DB.Sipaccounts
                                                where sip.C_user == account
                                                select sip).FirstOrDefault();
                            if (QueryAccount != null)
                                QueryAccount.C_registerstatus = 0;
                        }
                        else
                        {
                            listViewSIPAccount.Items[i].SubItems[0].Text = strings.unExpectedError;//"未预期错误";
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Time out!");
                    }
                    m_DB.SaveChanges();
                }
            }
        }

        private void BtnPushTest_Click(object sender, EventArgs e)
        {
            int nCount = listViewSIPAccount.Items.Count;
            for (int i = 0; i < nCount; i++)
            {
                if (listViewSIPAccount.Items[i].Checked)
                {
                    string SipServerAndPort = Config.Instance.SIPServerIP + ":" + Config.Instance.SIPServerPort.ToString();
                    string httpPush = "http://" + SipServerAndPort + "/PushSipAccount?from_account=ICMServer&to_account=" + listViewSIPAccount.Items[i].SubItems[1].Text;
                    WebRequest request = (HttpWebRequest)WebRequest.Create(httpPush);
                    request.Method = WebRequestMethods.Http.Get;
                    request.ContentType = "";
                    request.Timeout = 1000 * 5;
                    try
                    {
                        var resp = new StreamReader(request.GetResponse().GetResponseStream()).ReadToEnd();
                        if (resp.ToString() != "0")
                        {
                            MessageBox.Show("Push Message Failed!");
                            //return;
                        }
                        else
                            MessageBox.Show("Push Message Success!");
                    }
                    catch
                    {
                        MessageBox.Show("Time out!");
                    }
                }
            }
        }

        private void BtnAddDev_Click(object sender, EventArgs e)
        {
            DialogDevAdd AddDevDlg = new DialogDevAdd();
            if (AddDevDlg.ShowDialog() == DialogResult.OK)
                BtnRefreshDevMan_Click(this, e);
        }

        private void ButtonQuery_Click(object sender, EventArgs e)
        {
            m_DB = new ICMDBContext();
            bindingSourceDevicesForDeviceManagement.DataSource = new BindingListView<Device>(m_DB.Devices.ToList());

            int DeviceType = ComboBoxQueryTypeForPageDevMgmt.SelectedIndex - 1;
            BindingListView<Device> Devices = (BindingListView<Device>)bindingSourceDevicesForDeviceManagement.DataSource;
            if (DeviceType == -1)
            {
                Devices.Filter = null;
            }
            else if (DeviceType != -1)
            {
                Devices.Filter = new PredicateItemFilter<Device>(Device => (Device.type == DeviceType));
            }
        }

        private void BtnDelDev_Click(object sender, EventArgs e)
        {
            if (dataGridViewDevManager.SelectedRows.Count > 1)
            {
                var Devices = (from DataGridViewRow row in dataGridViewDevManager.SelectedRows
                               select ((ObjectView<Device>)row.DataBoundItem).Object as Device).ToList();

                //foreach (Device Device in Devices)
                {
                    //Device Device = ((ObjectView<Device>)row.DataBoundItem).Object as Device;
                    m_DB.Devices.RemoveRange(Devices);
                }
                m_DB.SaveChanges();
                BtnRefreshDevMan_Click(this, e);
                return;
            }
            if (MessageBox.Show(
                String.Format(strings.AreYouSureDelThis),//"您确定要刪除此笔资料？"),
                "",
                MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                if (bindingSourceDevicesForDeviceManagement.Current == null)
                    return;


                if (((ObjectView<Device>)bindingSourceDevicesForDeviceManagement.Current).Object is Device Device)
                {
                    //var d 
                    m_DB.Devices.Remove(Device);
                    m_DB.SaveChanges();
                    BtnRefreshDevMan_Click(this, e);
                }
                else
                    MessageBox.Show("Choose the item you want to delete");
            }
        }

        private void DataGridViewDevManager_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dataGridViewTextBoxColumn4.Index)
            {
                string DeviceId = e.Value as string;
                e.Value = DevicesAddressConverter.RoToChStr(DeviceId);
            }
            else if (e.ColumnIndex == dataGridViewTextBoxColumn3.Index)
            {
                int DeviceType = (int)e.Value;
                if (DeviceType < m_DeviceTypes.Count())
                {
                    e.Value = m_DeviceTypes[DeviceType];
                }
            }
        }

        private void BtnUpdateAddressBook_Click(object sender, EventArgs e)
        {
            DialogDevSelect dlg = new DialogDevSelect();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                List<Device> Devices = dlg.GetReturnResult();
                DialogUpdateAddressBookBox dlgUpdate = new DialogUpdateAddressBookBox(Devices);
                dlgUpdate.ShowDialog();
            }
        }

        private void BtnExportAddressBook_Click(object sender, EventArgs e)
        {
            DialogAddressBookExport dlg = new DialogAddressBookExport();
            dlg.ShowDialog();
            System.Diagnostics.Process.Start("explorer.exe", Path.GetAddressBookTempFolderPath());
        }

        private void ComboBoxDevTypeForPageDevInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            BtnSearch_Click(sender, e);
        }

        private void ComboBoxDevStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            BtnSearch_Click(sender, e);
        }
    }
}

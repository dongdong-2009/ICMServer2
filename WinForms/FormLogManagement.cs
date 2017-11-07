using ICMServer.Models;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICMServer
{
    public partial class FormLogManagement : Form
    {
        ICMDBContext m_db = new ICMDBContext();
        DialogEventWarn m_DlgDisplayAlarms = new DialogEventWarn();

        public FormLogManagement()
        {
            InitializeComponent();
        }

        private void FormLogManagement_Load(object sender, EventArgs e)
        {
            ICMServer.Net.HttpServer.Instance.ReceivedAlarmEvent += HttpServer_ReceivedAlarmEvent;
            ICMServer.Net.HttpServer.Instance.ReceivedCallOutEvent += HttpServer_ReceivedCallOutEvent;
            ICMServer.Net.HttpServer.Instance.ReceivedCommonEvent += HttpServer_ReceivedCommonEvent;
            ICMServer.Net.HttpServer.Instance.ReceivedOpenDoorEvent += HttpServer_ReceivedOpenDoorEvent;

            // Alarm log tabpage
            ComboBoxHandleState.SelectedIndex = 0;
            RefreshAlarmList();
            //Alarm log tabpage

            //Normal log tabpage
            ComboBoxNormalStatus.SelectedIndex = 0;
            listViewNormalEvent.View = View.Details;
            listViewNormalEvent.GridLines = false;
            listViewNormalEvent.FullRowSelect = true;
            listViewNormalEvent.Scrollable = true;
            listViewNormalEvent.CheckBoxes = true;
            listViewNormalEvent.Columns.Add("发生位置", 120);
            listViewNormalEvent.Columns.Add("发生时间", 120);
            listViewNormalEvent.Columns.Add("事件內容", 120);
            listViewNormalEvent.Columns.Add("处理状态", 120);
            listViewNormalEvent.Columns.Add("处理时间", 120);
            listViewNormalEvent.Columns.Add("处理人", 100);
            listViewNormalEvent.Columns.Add("备註", 100);
            BtnNormalRefresh_Click(this, e);
            //Normal log tabpage

            //Session log tabpage
            ComboBoxSessionlog.SelectedIndex = 0;
            listViewSessionLog.View = View.Details;
            listViewSessionLog.GridLines = false;
            listViewSessionLog.FullRowSelect = true;
            listViewSessionLog.Scrollable = true;
            listViewSessionLog.CheckBoxes = true;
            listViewSessionLog.Columns.Add("主叫方", 120);
            listViewSessionLog.Columns.Add("被叫方", 120);
            listViewSessionLog.Columns.Add("開始时间", 120);
            listViewSessionLog.Columns.Add("結束时间", 120);
            listViewSessionLog.Columns.Add("來电状态", 120);
            listViewSessionLog.Columns.Add("", 0);
            listViewSessionLog.Columns.Add("处理人", 100);
            listViewSessionLog.Columns.Add("备註", 100);
            BtnRefreshDial_Click(this, e);
            //Session log tabpage

            //Openlock log tabpage
            BtnRefreshOpen_Click(this, e);
            //Openlock log tabpage

            BtnDiviceLogRefresh_Click(this, e);
            //Device status log tabpage
        }

        void HttpServer_ReceivedOpenDoorEvent(object sender, EventArgs e)
        {
            RefreshOpendoorList();
        }

        void HttpServer_ReceivedCommonEvent(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        void HttpServer_ReceivedCallOutEvent(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        void HttpServer_ReceivedAlarmEvent(object sender, ICMServer.Net.HttpReceivedAlarmEventArgs e)
        {
            RefreshAlarmList();
            if (e.Alarms.Count > 0)
            {
                ShowEventWarnDialog();
            }
        }

        protected void ShowEventWarnDialog()
        {
            if (this.InvokeRequired)
            {
                this.Invoke((System.Action)(() => { ShowEventWarnDialog(); }));
            }
            else
            {
                if (m_DlgDisplayAlarms.IsDisplaying)
                    m_DlgDisplayAlarms.RefreshAlarmsList();
                else
                    m_DlgDisplayAlarms.ShowDialog();
            }
        }

        private async void RefreshAlarmList()
        {
            if (this.InvokeRequired)
            {
                this.Invoke((System.Action)(() => { RefreshAlarmList(); }));
            }
            else
            {
                using (var db = new ICMDBContext())
                {
                    var alarms = from a in db.Eventwarns
                                 orderby a.time descending
                                 select a;

                    eventwarnBindingSource.DataSource = await Task.Run(() => { return alarms.ToList(); });
                }
            }
        }

        private void BtnRefreshAlram_Click(object sender, EventArgs e)
        {
            RefreshAlarmList();
        }

        private void BtnNormalRefresh_Click(object sender, EventArgs e)
        {
            listViewNormalEvent.Items.Clear();
            foreach (var normal in m_db.Eventcommons)
            {
                List<string> sublist = new List<string>
                {
                    normal.srcaddr,
                    normal.time.ToString(),
                    normal.content
                };
                if (normal.handlestatus == 1)
                    sublist.Add(strings.hashandle);
                else
                    sublist.Add(strings.nothandle);
                sublist.Add(normal.handletime.ToString());
                sublist.Add(normal.handler);
                sublist.Add(normal.action);
                listViewNormalEvent.Items.Add(new ListViewItem(sublist.ToArray()));
            }
        }

        private void BtnRefreshDial_Click(object sender, EventArgs e)
        {
            listViewSessionLog.Items.Clear();
            foreach (var call in m_db.Eventcallouts.OrderBy(t => t.time))
            {
                List<string> sublist = new List<string>
                {
                    DevicesAddressConverter.RoToChStr(call.from),
                    DevicesAddressConverter.RoToChStr(call.to),
                    call.time.ToString(),
                    call.time.ToString(),
                    call.action,
                    call.type.ToString(),
                    call.owner
                };
                listViewSessionLog.Items.Add(new ListViewItem(sublist.ToArray()));
                checkBoxAllSession.Checked = false;
            }
        }

        private void BtnRefreshOpen_Click(object sender, EventArgs e)
        {
            RefreshOpendoorList();
        }

        private async void RefreshOpendoorList()
        {
            if (this.InvokeRequired)
            {
                this.Invoke((System.Action)(() => { RefreshOpendoorList(); }));
            }
            else
            {
                using (var db = new ICMDBContext())
                {
                    var opendoor = from a in db.Eventopendoors
                                   where a.C_from.StartsWith(textBoxLobby.Text)
                                   orderby a.C_time descending
                                   select a;
                    eventopendoorbindingSource.DataSource = await Task.Run(() => { return opendoor.ToList(); });
                }
            }
        }

        private async void BtnDiviceLogRefresh_Click(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((System.Action)(() => { RefreshOpendoorList(); }));
            }
            else
            {
                using (var db = new ICMDBContext())
                {
                    var devstatus = from a in db.Heartbeatlogs
                                   orderby a.C_id
                                   select a;
                    eventDevStatusbindingSource.DataSource = await Task.Run(() => { return devstatus.ToList(); });
                }
            }
        }

        private async void BtnQuery_Click(object sender, EventArgs e)
        {
            using (var db = new ICMDBContext())
            {
                if (ComboBoxHandleState.SelectedIndex == 0)
                {
                    var alarms = from a in m_db.Eventwarns
                                 where dateTimePickerStart.Value <= a.time && a.time <= dateTimePickerEnd.Value
                                 orderby a.time descending
                                 select a;
                    eventwarnBindingSource.DataSource = await Task.Run(() => { return alarms.ToList(); });
                }
                else
                {
                    int handle = ComboBoxHandleState.SelectedIndex - 1;

                    var alarms = from a in m_db.Eventwarns
                                 where dateTimePickerStart.Value <= a.time && a.time <= dateTimePickerEnd.Value && a.handlestatus == handle
                                 orderby a.time descending
                                 select a;
                    eventwarnBindingSource.DataSource = await Task.Run(() => { return alarms.ToList(); });
                }
            }
        }

        private void BtnDeal_Click(object sender, EventArgs e)
        {
            if (eventwarnBindingSource.Current is eventwarn alarm)
            {
                DialogHandleAlarm dlg = new DialogHandleAlarm(alarm);
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    RefreshAlarmList();
                }
            }
        }

        private void BtnDel_Click(object sender, EventArgs e)
        {
            if (eventwarnDataGridView.SelectedRows.Count > 0)
            {
                using (var db = new ICMDBContext())
                {
                    var alarms = (from DataGridViewRow row in eventwarnDataGridView.SelectedRows
                                  select (db.Eventwarns.Attach(row.DataBoundItem as eventwarn))).ToList();

                    db.Eventwarns.RemoveRange(alarms);
                    db.SaveChanges();
                }
                RefreshAlarmList();
            }
        }

        private void BtnDrop_Click(object sender, EventArgs e)
        {
            if (eventwarnDataGridView.SelectedRows.Count > 0)
            { 
                var alarms = (from DataGridViewRow row in eventwarnDataGridView.SelectedRows
                              select new { DeviceId = (row.DataBoundItem as eventwarn).srcaddr }).Distinct();
                using (var db = new ICMDBContext())
                {
                    var Devices = (from d in db.Devices
                                   select d).ToList();
                    Devices = (from d in Devices
                               join a in alarms on d.roomid equals a.DeviceId
                               select d).ToList();
                    if (Devices.Count() > 0)
                    {
                        DialogSendDisableSafties dlg = new DialogSendDisableSafties(Devices);
                        dlg.ShowDialog();
                    }
                }
            }
        }

        private void DataGridViewToExcel(DataGridView grid)
        {
            Microsoft.Office.Interop.Excel.Application xla = new Microsoft.Office.Interop.Excel.Application();
            Workbook wb = xla.Workbooks.Add(XlSheetType.xlWorksheet);
            Worksheet ws = (Worksheet)xla.ActiveSheet;

            xla.Visible = true;

            for (int i = 0; i < grid.ColumnCount; ++i)
            {
                ws.Cells[1, i + 1] = grid.Columns[i].HeaderText;
            }

            for (int i = 0; i < grid.RowCount; ++i)
            {
                for (int j = 0; j < grid.ColumnCount; ++j)
                {
                    ws.Cells[i + 2, j + 1] = grid.Rows[i].Cells[j].FormattedValue;
                }
            }
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            DataGridViewToExcel(eventwarnDataGridView);
        }

        private void BtnQueryNormal_Click(object sender, EventArgs e)
        {
            listViewNormalEvent.Items.Clear();
            if (m_db != null)
            {
                if (ComboBoxNormalStatus.SelectedIndex == 0)
                {
                    var Query = from aa in m_db.Eventcommons
                                where (aa.time >= dateTimePickerNormalStart.Value && aa.time <= dateTimePickerNormalEnd.Value)
                                select aa;
                    foreach (var normal in Query)
                    {
                        List<string> sublist = new List<string>
                        {
                            normal.srcaddr,
                            normal.time.ToString(),
                            normal.content
                        };
                        if (normal.handlestatus == 1)
                            sublist.Add("已处理");
                        else
                            sublist.Add("未处理");
                        sublist.Add(normal.handletime.ToString());
                        //sublist.Add(normal.id.ToString());
                        sublist.Add(normal.handler);
                        sublist.Add(normal.action);
                        listViewNormalEvent.Items.Add(new ListViewItem(sublist.ToArray()));
                    }
                }
                else
                {
                    int NormalHanle = (ComboBoxNormalStatus.SelectedIndex == 1) ? 1 : 0;
                    var Query = from aa in m_db.Eventcommons
                                where aa.time >= dateTimePickerNormalStart.Value && aa.time <= dateTimePickerNormalEnd.Value && aa.handlestatus == NormalHanle
                                select aa;
                    foreach (var normal in Query)
                    {
                        List<string> sublist = new List<string>
                        {
                            normal.srcaddr,
                            normal.time.ToString(),
                            normal.content
                        };
                        if (normal.handlestatus == 1)
                            sublist.Add("已处理");
                        else
                            sublist.Add("未处理");
                        sublist.Add(normal.handletime.ToString());
                        //sublist.Add(normal.id.ToString());
                        sublist.Add(normal.handler);
                        sublist.Add(normal.action);
                        listViewNormalEvent.Items.Add(new ListViewItem(sublist.ToArray()));
                    }
                }
            }
        }

        private void BtnDelNormal_Click(object sender, EventArgs e)
        {
            int nCount = listViewNormalEvent.Items.Count;
            for (int i = 0; i < nCount; i++)
            {
                if (listViewNormalEvent.Items[i].Checked)
                {
                    DateTime queryID = Convert.ToDateTime(listViewNormalEvent.Items[i].SubItems[1].Text);
                    if (m_db != null)
                    {
                        var QueryDel = from aa in m_db.Eventcommons
                                       where aa.time == queryID
                                       select aa;
                        foreach (var del in QueryDel)
                        {
                            m_db.Eventcommons.Remove(del);
                        }
                        m_db.SaveChanges();
                    }
                }
            }
            checkBoxNormalAll.Checked = false;
            BtnNormalRefresh_Click(this, e);
        }

        private void BtnDealNormal_Click(object sender, EventArgs e)
        {
            int nCount = listViewNormalEvent.Items.Count;
            for (int i = 0; i < nCount; i++)
            {
                if (listViewNormalEvent.Items[i].Checked)
                {
                    DateTime queryID = Convert.ToDateTime(listViewNormalEvent.Items[i].SubItems[1].Text);
                    if (m_db != null)
                    {
                        var QueryDel = from aa in m_db.Eventcommons
                                       where aa.time == queryID
                                       select aa;
                        foreach (var deal in QueryDel)
                        {
                            deal.handlestatus = 1;
                        }
                        m_db.SaveChanges();
                    }
                }
            }
            checkBoxNormalAll.Checked = false;
            BtnNormalRefresh_Click(this, e);
        }

        private void BtnPrintNormal_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application xla = new Microsoft.Office.Interop.Excel.Application();
            Workbook wb = xla.Workbooks.Add(XlSheetType.xlWorksheet);
            Worksheet ws = (Worksheet)xla.ActiveSheet;

            xla.Visible = true;

            ws.Cells[1, 1] = "发生位置";
            ws.Cells[1, 2] = "发生时间";
            ws.Cells[1, 3] = "事件內容";
            ws.Cells[1, 4] = "处理状态";
            ws.Cells[1, 5] = "处理时间";
            ws.Cells[1, 6] = "处理人";
            ws.Cells[1, 7] = "备註";

            int nCount = listViewNormalEvent.Items.Count;
            for (int i = 0; i < nCount; i++)
            {
                ws.Cells[i + 2, 1] = listViewNormalEvent.Items[i].SubItems[0].Text;
                ws.Cells[i + 2, 2] = listViewNormalEvent.Items[i].SubItems[1].Text;
                ws.Cells[i + 2, 3] = listViewNormalEvent.Items[i].SubItems[2].Text;
                ws.Cells[i + 2, 4] = listViewNormalEvent.Items[i].SubItems[3].Text;
                ws.Cells[i + 2, 5] = listViewNormalEvent.Items[i].SubItems[4].Text;
                ws.Cells[i + 2, 6] = listViewNormalEvent.Items[i].SubItems[5].Text;
                ws.Cells[i + 2, 7] = listViewNormalEvent.Items[i].SubItems[6].Text;
            }
        }

        private void CheckBoxNormalAll_CheckedChanged(object sender, EventArgs e)
        {
            int nCount = listViewNormalEvent.Items.Count;
            for (int i = 0; i < nCount; i++)
            {
                listViewNormalEvent.Items[i].Checked = checkBoxNormalAll.Checked;
            }
        }

        private void CheckBoxAllSession_CheckedChanged(object sender, EventArgs e)
        {
            int nCount = listViewSessionLog.Items.Count;
            for (int i = 0; i < nCount; i++)
            {
                listViewSessionLog.Items[i].Checked = checkBoxAllSession.Checked;
            }
        }

        private void BtnQueryDial_Click(object sender, EventArgs e)
        {
            listViewSessionLog.Items.Clear();
            if (m_db != null)
            {
                var Query = from aa in m_db.Eventcallouts
                            where ((aa.time >= dateTimePickerCallStart.Value && aa.time <= dateTimePickerCallEnd.Value) || (aa.time >= dateTimePickerCallStart.Value && aa.time <= dateTimePickerCallEnd.Value))
                            orderby aa.time
                            select aa;
                foreach (var calllog in Query)
                {
                    List<string> sublist = new List<string>
                    {
                        DevicesAddressConverter.RoToChStr(calllog.from),
                        DevicesAddressConverter.RoToChStr(calllog.to),
                        calllog.time.ToString(),
                        calllog.time.ToString()
                    };
                    listViewSessionLog.Items.Add(new ListViewItem(sublist.ToArray()));
                }
            }
        }

        private void BtnDelDial_Click(object sender, EventArgs e)
        {
            int nCount = listViewSessionLog.Items.Count;
            for (int i = 0; i < nCount; i++)
            {
                if (listViewSessionLog.Items[i].Checked)
                {
                    DateTime queryID = Convert.ToDateTime(listViewSessionLog.Items[i].SubItems[2].Text);
                    if (m_db != null)
                    {
                        var QueryDel = from aa in m_db.Eventcallouts
                                       where aa.time == queryID
                                       select aa;
                        foreach (var del in QueryDel)
                        {
                            m_db.Eventcallouts.Remove(del);
                        }
                        m_db.SaveChanges();
                    }
                }
            }
            checkBoxAllSession.Checked = false;
            BtnRefreshDial_Click(this, e);
        }

        private void BtnPrintDial_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application xla = new Microsoft.Office.Interop.Excel.Application();
            Workbook wb = xla.Workbooks.Add(XlSheetType.xlWorksheet);
            Worksheet ws = (Worksheet)xla.ActiveSheet;

            xla.Visible = true;

            ws.Cells[1, 1] = "主叫方";
            ws.Cells[1, 2] = "被叫方";
            ws.Cells[1, 3] = "開始时间";
            ws.Cells[1, 4] = "結束时间";
            ws.Cells[1, 5] = "來电状态";
            ws.Cells[1, 6] = "处理人";
            ws.Cells[1, 7] = "备註";

            int nCount = listViewSessionLog.Items.Count;
            for (int i = 0; i < nCount; i++)
            {
                try
                {
                    ws.Cells[i + 2, 1] = listViewSessionLog.Items[i].SubItems[0].Text;
                    ws.Cells[i + 2, 2] = listViewSessionLog.Items[i].SubItems[1].Text;
                    ws.Cells[i + 2, 3] = listViewSessionLog.Items[i].SubItems[2].Text;
                    ws.Cells[i + 2, 4] = listViewSessionLog.Items[i].SubItems[3].Text;
                    ws.Cells[i + 2, 5] = listViewSessionLog.Items[i].SubItems[4].Text;
                    ws.Cells[i + 2, 6] = listViewSessionLog.Items[i].SubItems[5].Text;
                    ws.Cells[i + 2, 7] = listViewSessionLog.Items[i].SubItems[6].Text;
                }
                catch (Exception) { }
            }
        }

        private void BtnChooseDev_Click(object sender, EventArgs e)
        {
            DialogAreaAddress dlg = new DialogAreaAddress();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBoxLobby.Text = dlg.ReturnValue;
            }
        }

        private async void BtnQueryOpen_Click(object sender, EventArgs e)
        {
            using (var db = new ICMDBContext())
            {
                var opendoor = from a in db.Eventopendoors
                               where a.C_from.StartsWith(textBoxLobby.Text)
                               orderby a.C_time descending
                               select a;
                eventopendoorbindingSource.DataSource = await Task.Run(() => { return opendoor.ToList(); });
            }
        }

        //private void listViewOpenLog_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    IsChanged = true;
        //}

        //private void listViewOpenLog_MouseClick(object sender, MouseEventArgs e)
        //{
        //    DateTime queryID = new DateTime();
        //    if (IsChanged)
        //    {
        //        if (m_db != null)
        //        {
        //            foreach (ListViewItem tmp in listViewOpenLog.Items)
        //            {
        //                if (tmp.Selected == true)
        //                {
        //                    queryID = Convert.ToDateTime(tmp.SubItems[3].Text);
        //                }
        //            }
        //            var queryPic = from zz in m_db.photographs//.eventopendoors
        //                           where zz.C_time == queryID
        //                           select zz;
        //            foreach (var pic in queryPic)
        //            {
        //                pictureBoxSnap.Image = ByteToImage(pic.C_img);//Image.FromFile(pic.C_img);//.C_img);//@".\data\snapshot\image" + ID + ".jpg");
        //            }
        //        }
        //    }
        //    IsChanged = false;
        //}

        private Image ByteToImage(byte[] src)
        {
            if (src.Length > 0)
            {
                try
                {
                    MemoryStream mStream = new MemoryStream();
                    byte[] pData = src;
                    mStream.Write(pData, 0, Convert.ToInt32(pData.Length));
                    Bitmap bm = new Bitmap(mStream, false);
                    mStream.Dispose();
                    return bm;
                }
                catch (Exception) { }
            }
            //else
            //    MessageBox.Show("Empty Image");
            return null;
        }

        private readonly string[] m_HandleStatus =
        {
            "未处理",
            "处理中",
            "已处理"
        };

        private void EventwarnDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dgvTextBoxAlarmSrc.Index)
            {
                string DeviceId = e.Value as string;
                e.Value = DevicesAddressConverter.RoToChStr(DeviceId);
            }
            else if (e.ColumnIndex == dgvTextBoxAlarmHandleStatus.Index)
            {
                int status = e.Value != null ? (int)e.Value : 0;
                if (status < m_HandleStatus.Count())
                {
                    e.Value = m_HandleStatus[status];
                }
            }
            else if (e.ColumnIndex == dgvTextBoxAlarmAction.Index)
            {
                if (e.Value is string action)
                {
                    if (action == "trig")
                    {
                        e.CellStyle.BackColor = Color.Red;
                        e.Value = "事件触发";
                    }
                    else if (action == "unalarm")
                    {
                        e.CellStyle.BackColor = Color.LimeGreen;
                        e.Value = "事件解除";
                    }
                    else if (action == "enable")
                        e.Value = "布防";
                    else if (action == "disable")
                        e.Value = "撤防";
                    else
                        e.Value = "";
                }
            }
        }

        private void EventopendoordataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dgvTextBoxOpenDoorOpenObject.Index)
            {
                string OpenId = (string)e.Value;
                if(OpenId[2] == '-')
                    e.Value = DevicesAddressConverter.RoToChStr(OpenId);
            }
            else if (e.ColumnIndex == dgvTextBoxOpenDoorFrom.Index)
            {
                string FromId = (string)e.Value;
                e.Value = DevicesAddressConverter.RoToChStr(FromId);
            }
            else if (e.ColumnIndex == dgvTextBoxOpenDoorStatus.Index)
            {
                bool suc = Convert.ToBoolean(e.Value);
                e.Value = (suc) ? "成功" : "失敗";
            }
        }

        private void EventopendoordataGridView_SelectionChanged(object sender, EventArgs e)
        {
            eventopendoor opendoor = eventopendoorbindingSource.Current as eventopendoor;
            if (opendoor == null)
                return;

            var photo = (from p in m_db.Photographs
                         where p.C_time == opendoor.C_time
                         select p).FirstOrDefault();
            if (photo != null)
                pictureBoxSnap.Image = ByteToImage(photo.C_img);
            else
                pictureBoxSnap.Image = null;
        }

        private void FormLogManagement_FormClosing(object sender, FormClosingEventArgs e)
        {
            ICMServer.Net.HttpServer.Instance.ReceivedAlarmEvent -= HttpServer_ReceivedAlarmEvent;
            ICMServer.Net.HttpServer.Instance.ReceivedCallOutEvent -= HttpServer_ReceivedCallOutEvent;
            ICMServer.Net.HttpServer.Instance.ReceivedCommonEvent -= HttpServer_ReceivedCommonEvent;
            ICMServer.Net.HttpServer.Instance.ReceivedOpenDoorEvent -= HttpServer_ReceivedOpenDoorEvent;
        }

        private void BtnDelOpen_Click(object sender, EventArgs e)
        {
            if (eventopendoordataGridView.SelectedRows.Count > 0)
            {
                using (var db = new ICMDBContext())
                {
                    var opens = (from DataGridViewRow row in eventopendoordataGridView.SelectedRows
                                 select (db.Eventopendoors.Attach(row.DataBoundItem as eventopendoor))).ToList();

                    db.Eventopendoors.RemoveRange(opens);
                    db.SaveChanges();
                }
                RefreshOpendoorList();
                return;
            }
        }
    }
}

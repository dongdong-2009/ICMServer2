using ICMServer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICMServer
{
    public partial class DialogSelectIP : Form
    {
        string m_FilePath;
        int m_DeviceType;
        List<Device> m_Devices = new List<Device>();

        public DialogSelectIP(string filePath, int DeviceType)
        {
            InitializeComponent();
            m_FilePath = filePath;
            m_DeviceType = DeviceType;
        }

        public List<Device> Devices 
        {
            get { return m_Devices; }
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            this.Devices.Clear();
            switch (this.ComboBoxUpgradeType.SelectedIndex)
            {
            case 0: // by IP
                {
                    string ip = this.ComboBoxUpgradeIPs.Text;
                    using (var db = new ICMDBContext())
                    {
                        var Device = (from d in db.Devices
                                      where d.ip == ip
                                      select d).FirstOrDefault();
                        if (Device != null)
                            this.Devices.Add(Device);
                        else
                            MessageBox.Show(string.Format("不存在 IP 地址为 {0} 的设备", ip));
                    }
                }
                break;

            case 1: // by Device Address
                using (var db = new ICMDBContext())
                {
                    List<Device> Devices;
                    if (m_DeviceType > 0)
                    {
                        Devices = (from Device in db.Devices
                                   where Device.roomid.StartsWith(this.textBoxDeviceAddress.Text)
                                      && (int)Device.type == m_DeviceType
                                  select Device).ToList();
                    }
                    else
                    {
                        Devices = (from Device in db.Devices
                                   where Device.roomid.StartsWith(this.textBoxDeviceAddress.Text)
                                   select Device).ToList();
                    }
                    this.Devices.AddRange(Devices);
                }
                break;
            }
            this.DialogResult = DialogResult.OK;
        }

        private async void RefillIPList()
        {
            using (var db = new ICMDBContext())
            {
                if (m_DeviceType <= 0)
                {
                    DeviceBindingSource.DataSource = await Task.Run(() => { 
                        return (from Device in db.Devices
                                select Device).AsEnumerable().
                                OrderBy(Device => Device.ip, new IPComparer()).ToList(); 
                    });
                }
                else
                {
                    DeviceBindingSource.DataSource = await Task.Run(() => { 
                        return (from Device in db.Devices
                                where Device.type == m_DeviceType
                                select Device).AsEnumerable().
                                OrderBy(Device => Device.ip, new IPComparer()).ToList(); 
                    });
                }
            }
        }

        private void DialogSelectIP_Load(object sender, EventArgs e)
        {
            //Device Devices;
            this.ComboBoxUpgradeType.SelectedIndex = 0;
            RefillIPList();
            if (this.ComboBoxUpgradeIPs.Items.Count > 0)
                this.ComboBoxUpgradeIPs.SelectedIndex = 0;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnSelectDeviceAddress_Click(object sender, EventArgs e)
        {
            DialogAreaAddress dlg = new DialogAreaAddress();
            if (dlg.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            this.textBoxDeviceAddress.Text = dlg.ReturnValue;
        }

        private void ComboBoxUpgradeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.ComboBoxUpgradeType.SelectedIndex)
            {
            case 0: // by IP
                this.ComboBoxUpgradeIPs.Visible = true;
                this.textBoxDeviceAddress.Visible =
                this.BtnSelectDeviceAddress.Visible = false;
                break;

            case 1: // by Device Address
                this.ComboBoxUpgradeIPs.Visible = false;
                this.textBoxDeviceAddress.Visible =
                this.BtnSelectDeviceAddress.Visible = true;
                break;
            }
        }
    }
}

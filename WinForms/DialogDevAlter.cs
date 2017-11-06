using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using ICMServer.NET.Models;
using ICMServer.Models;

namespace ICMServer
{
    public partial class DialogDevAlter : Form
    {
        ICMDBContext m_DB;
        Device m_Device;
        readonly bool m_ReadOnly;

        public DialogDevAlter(ICMDBContext db, Device Device, bool readOnly)
        {
            InitializeComponent();
            m_DB = db;
            m_Device = Device;
            m_ReadOnly = readOnly;
            
            ComboBoxDeviceType.Enabled = 
            DeviceAddressControl.Enabled =
            //textBoxDeviceAddr.Enabled = 
            ipAddressControlDevIP.Enabled = 
            textBoxAlias.Enabled =
            ipAddressControlGW.Enabled = 
            ipAddressControlSM.Enabled =
            ComboBoxGroupIp.Enabled = 
            textBoxDeviceMac.Enabled = 
            textBoxCamID.Enabled = 
            textBoxCamPW.Enabled = !m_ReadOnly;
        }

        private void DialogDevAlter_Load(object sender, EventArgs e)
        {
            //using (var db = new ICMServer.Models.ICMDBContext())
            {
                /**
                 * Query modify item
                 **/
                var dev = m_Device;
                if (dev != null)
                {
                    ComboBoxDeviceType.SelectedIndex = (int)dev.type;
                    DeviceAddressControl.Text = dev.roomid;
                    //textBoxDeviceAddr.Text = dev.roomid;
                    ipAddressControlDevIP.Text = dev.ip;
                    textBoxAlias.Text = dev.Alias;
                    ipAddressControlGW.Text = dev.gw;
                    ipAddressControlSM.Text = dev.sm;
                    int GroupIpIndex = ComboBoxGroupIp.FindStringExact(dev.group);
                    ComboBoxGroupIp.SelectedIndex = GroupIpIndex;
                    textBoxDeviceMac.Text = dev.mac;
                    textBoxCamID.Text = dev.cameraid;
                    textBoxCamPW.Text = dev.camerapw;
                }
            }
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            string addr = DeviceAddressControl.Text;
            //string addr = textBoxDeviceAddr.Text;
            string AddrIP = ipAddressControlDevIP.Text;
            string gate = AddrIP;
            int index = gate.LastIndexOf(".");
            if (index > 0)
                gate = gate.Substring(0, index + 1) + 1;
            /**
             * Start fetch user input and save
             **/
            //List<string> xmlrow = new List<string>();
            //ListViewItem item;
            //using (var db = new ICMDBContext())
            {
                var dev = m_Device;
                if (dev != null)
                {
                    dev.ip = AddrIP;
                    dev.roomid = addr;
                    dev.Alias = textBoxAlias.Text;
                    try { dev.group = ComboBoxGroupIp.SelectedItem.ToString(); }
                    catch { dev.group = "NO"; }
                    if (textBoxDeviceMac.Text == "  :  :  :  :  :")
                        dev.mac = "";
                    //try { dev.mac = textBoxDeviceMac.Text; }
                    //catch { dev.mac = ""; }
                    dev.type = ComboBoxDeviceType.SelectedIndex;
                    dev.sm = "255.255.255.0";
                    dev.gw = gate;
                    if (m_DB != null)
                    {
                        m_DB.SaveChanges();
                        DialogResult = DialogResult.OK;
                    }
                }
            }
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ComboBoxDeviceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBoxDeviceType.SelectedIndex == 5 || ComboBoxDeviceType.SelectedIndex == 7)
            {
                labelGroupIp.Visible = false;
                ComboBoxGroupIp.Visible = false;
            }
            else
            {
                labelGroupIp.Visible = true;
                ComboBoxGroupIp.Visible = true;
            }
        }
    }
}

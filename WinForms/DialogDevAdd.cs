using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ICMServer.Models;

namespace ICMServer
{
    public partial class DialogDevAdd : Form
    {
        public DialogDevAdd()
        {
            InitializeComponent();
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            string addr = "";
            addr = DevicesAddressConverter.ChStrToRo(textBoxDeviceAddr.Text);
            string AddrIP = ipAddressControlDevIP.Text;
            string gate = AddrIP;
            int index = gate.LastIndexOf(".");
            if (index > 0)
                gate = gate.Substring(0, index + 1) + 1;
            /**
             * Start fetch user input and save
             **/
            using (var db = new ICMDBContext())
            {
                Device dev = new Device();
                dev.ip = AddrIP;
                dev.roomid = addr;
                dev.Alias = textBoxAlias.Text;
                try { dev.group = ComboBoxGroupIp.SelectedItem.ToString(); }
                catch { dev.group =""; }
                if (textBoxDeviceMac.Text == "  :  :  :  :  :")
                    dev.mac = "";
                //try { dev.mac = textBoxDeviceMac.Text; }
                //catch { dev.mac = ""; }
                dev.type = ComboBoxDeviceType.SelectedIndex;
                dev.sm = "255.255.255.0";
                dev.gw = gate;
                db.Devices.Add(dev);
                try
                {
                    db.SaveChanges();
                    DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.InnerException.Message);
                }
            }
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DialogDevAdd_Load(object sender, EventArgs e)
        {

        }

        private void ComboBoxDeviceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ComboBoxDeviceType.SelectedIndex == 5 || ComboBoxDeviceType.SelectedIndex == 7)
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

using ICMServer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICMServer
{
    public partial class DialogWatchSelectDev : Form
    {
        ICMDBContext db = new ICMDBContext();
        FormVideoTalk pass = null;
        public DialogWatchSelectDev(FormVideoTalk PASS)
        {
            InitializeComponent();
            this.pass = PASS;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (ComboBoxSelectDev.SelectedItem == null)
            {
                MessageBox.Show(strings.CantBeNull);
                return;
            }
            string ip = "", Addr = "", Group = "";
            int index = ComboBoxSelectDev.SelectedIndex;
            var QueryLive = from Device in db.Devices
                            where (Device.Alias == ComboBoxSelectDev.SelectedItem.ToString())
                                    && (Device.online == 1)
                            select Device;
            foreach (var dev in QueryLive)
            {
                ip = dev.ip;
                string[] addrsplit = dev.roomid.Split('-');
                string addr = string.Join("",addrsplit);
                Addr = addr;
                Group = dev.group;
            }
            this.pass.PassIP = ip;
            this.pass.PassADDR = Addr;
            this.pass.PassGroup = Group;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void WatchSelectDev_Load(object sender, EventArgs e)
        {
            // Search for living Devices
            var QueryLive = from Device in db.Devices
                            where (Device.online == 1)
                               && Device.type >= (int)DeviceType.Door_Camera
                               && Device.type <= (int)DeviceType.Lobby_Phone_Area
                            select Device;
            foreach (var dev in QueryLive)
            {
                // Add Device alias to ComboBox
                ComboBoxSelectDev.Items.Add(dev.Alias);
                ComboBoxIPMap.Items.Add(dev.ip);
            }
        }
    }
}

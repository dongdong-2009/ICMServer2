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
    public partial class DialogIPCall : Form
    {
        public DialogIPCall()
        {
            InitializeComponent();
        }

        public string ReturnIP { get; set; }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (!CheckIPCouldCall(ipAddressControl.Text))
                return;
            this.ReturnIP = ipAddressControl.Text;
            this.DialogResult = DialogResult.OK;
        }

        private bool CheckIPCouldCall(string ip)
        {
            ICMDBContext db = new ICMDBContext();
            var Dev = (from dev in db.Devices
                           where dev.ip == ip
                           select dev).FirstOrDefault();
            if(Dev!=null)
            {
                if (Dev.type != 2 && Dev.type != 5 && Dev.type != 6 && Dev.type != 7)
                {
                    MessageBox.Show(strings.TheIPAddrIsNotBelongToIndoorAdminDoorbell);
                    return false;
                }
                return true;
            }
            MessageBox.Show(strings.IPNotExist);
            return false;
        }
    }
}

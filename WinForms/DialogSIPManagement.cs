using DeviceAddressControlLib;
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
    public partial class DialogSIPManagement : Form
    {
        public DialogSIPManagement()
        {
            InitializeComponent();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void BtnQuery_Click(object sender, EventArgs e)
        {
            string DeviceId = textBoxDeviceId.Text;
            using (var db = new ICMDBContext())
            {
                var sipAccounts = from account in db.Sipaccounts
                                  where account.C_room == DeviceId
                                  select account;
                this.sipaccountBindingSource.DataSource = await sipAccounts.ToListAsync();
            }
        }
    }
}

using ICMServer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICMServer
{
    public partial class DialogSIPAdd : Form
    {
        int randomcode = 0;

        public DialogSIPAdd(string strTextBox, string strGroup)
        {
            InitializeComponent();
            textBoxRoomID.Text = strTextBox;
            textBoxGroup.Text = strGroup;
        }

        private void AddSIP_Load(object sender, EventArgs e)
        {
            textBoxPasswd.Text = GeneratePassword();
        }

        private string GeneratePassword()
        {
            Random random = new Random();
            randomcode = random.Next() % 10000;
            DateTime time = DateTime.Now;
            string time2 = time.ToString() + randomcode.ToString();
            MD5 md5 = MD5.Create();
            byte[] source = Encoding.Default.GetBytes(time2);
            byte[] crypto = md5.ComputeHash(source);
            return Convert.ToBase64String(crypto).Substring(0, 10);
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (textBoxAccount.Text=="")
            {
                MessageBox.Show(strings.UserCannotBeNull);//"用户名不得为空");
                return;
            }
            using (var db = new ICMDBContext())
            {
                sipaccount SIP = new sipaccount();
                string account = textBoxAccount.Text;
                var checkNameExist = (from check in db.Sipaccounts
                                      where check.C_user == account
                                      select check).FirstOrDefault();
                if (checkNameExist != null)
                {
                    MessageBox.Show("Account already exitst!\nPlease choose another name.");
                    return;
                }
                SIP.C_user = textBoxAccount.Text;
                SIP.C_password = textBoxPasswd.Text;
                SIP.C_room = DevicesAddressConverter.ChStrToRo(textBoxRoomID.Text);
                SIP.C_usergroup = textBoxGroup.Text;
                SIP.C_updatetime = DateTime.Now;
                SIP.C_sync = 0;
                SIP.C_registerstatus = 0;
                SIP.C_randomcode = randomcode.ToString();
                db.Sipaccounts.Add(SIP);
                db.SaveChanges();
            }
            this.Close();
        }
    }
}

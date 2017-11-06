using ICMServer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICMServer
{
    public partial class DialogLogin : Form
    {
        /// <summary>
        /// 用于視窗拖曳移动
        /// </summary>
        int m_PX;
        int m_PY;
        bool m_IsDragging;

        public DialogLogin()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            String username = ComboBoxUserName.Text;
            String password = textBoxUserPassword.Text;
            var user = ICMDBContext.GetUserByName(username);
            if (user != null)
            {
                //if (ICMServer.Security.MD5Encode(password) == user.C_password)
                if (password == user.C_password)
                {
                    this.DialogResult = DialogResult.OK;  // open mdiparent main form
                    this.Close();
                }
                else
                    MessageBox.Show("密码错误");
            }
            else
                MessageBox.Show(String.Format("用户名 {0} 不存在", username));
        }

        private void BtnLogin_MouseDown(object sender, MouseEventArgs e)
        {
            this.BtnLogin.BackgroundImage = ICMServer.DialogLoginResource.BtnLoginClickedBackgroundImage;
        }

        private void BtnLogin_MouseUp(object sender, MouseEventArgs e)
        {
            this.BtnLogin.BackgroundImage = ICMServer.DialogLoginResource.BtnLoginBackgroundImage;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnClose_MouseDown(object sender, MouseEventArgs e)
        {
            this.BtnClose.BackgroundImage = ICMServer.DialogLoginResource.BtnCloseClickedBackgroundImage;
        }

        private void BtnClose_MouseUp(object sender, MouseEventArgs e)
        {
            this.BtnClose.BackgroundImage = ICMServer.DialogLoginResource.BtnCloseBackgroundImage;
        }

        private void DialogLogin_Load(object sender, EventArgs e)
        {
            if (Config.Instance.AppLanaguage == "2")
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN");
                BackgroundImage = ICMServer.DialogLoginResource.dlgLoginBackgroundImage;
                this.BtnLogin.BackgroundImage = ICMServer.DialogLoginResource.BtnLoginBackgroundImage;
                this.BtnClose.BackgroundImage = ICMServer.DialogLoginResource.BtnCloseBackgroundImage;
            }
            using (var db = new ICMDBContext())
            {
                foreach (var user in db.Users)
                    ComboBoxUserName.Items.Add(user.C_username);
                ComboBoxUserName.SelectedIndex = 0;
            }
            Task.Run(() => { SetLocalNetwork(); });
        }

        void SetLocalNetwork()
        {
            using (var db = new ICMDBContext())
            {
                var Device = (from d in db.Devices
                              where d.type == 0
                              select d).FirstOrDefault();
                if (Device != null)
                {
                    if (IsValidIPAddress(Device.ip)
                     && IsValidIPAddress(Device.gw)
                     && IsValidIPAddress(Device.sm))
                    {
                        Config.Instance.LocalIP = Device.ip;
                        Config.Instance.LocalGateway = Device.gw;
                        Config.Instance.LocalSubnetMask = Device.sm;
                    }
                }
            }
        }

        private bool IsValidIPAddress(string ip)
        {
            return Regex.Match(ip, @"^[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}$").Length > 0;
        }

        private void DialogLogin_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                m_PX = e.X;
                m_PY = e.Y;
                m_IsDragging = true;
            }
        }

        private void DialogLogin_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_IsDragging)
            {
                this.Location = new Point(this.Left + e.X - m_PX, this.Top + e.Y - m_PY);
            }
        }

        private void DialogLogin_MouseUp(object sender, MouseEventArgs e)
        {
            m_IsDragging = false;
        }
    }
}

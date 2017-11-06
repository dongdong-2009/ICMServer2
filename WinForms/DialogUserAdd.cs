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
    public partial class DialogUserAdd : Form
    {
        ICMDBContext m_db = new ICMDBContext();
        int m_id;

        public DialogUserAdd(int id)
        {
            InitializeComponent();
            m_id = id;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormAddUser_Load(object sender, EventArgs e)
        {
            foreach (var zz in m_db.Authorities)
                cmbBoxAuthority.Items.Add(zz.C_name);
            if (m_id != 0)
            {
                var query = from user in m_db.Users
                            where user.C_id == m_id
                            select user;
                foreach (var user in query)
                {
                    txtBoxName.Text = user.C_username;
                    txtBoxNum.Text = user.C_userno;
                    cmbBoxAuthority.Text = user.C_powerid.ToString();
                    //var queryAuthGroup = from zz in db.Authority
                    //                     where zz.id == a.powerid
                    //                     select zz;
                    //foreach (var z in queryAuthGroup)
                    //    ComboBoxAutho.Text = z.authoName;
                }
            }
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (txtBoxPassword.Text != "")
            {
                if (txtBoxPassword.Text == txtBoxPassword2.Text)
                {
                    if (m_id == 0)
                    {
                        user users = new Models.user();
                        //User user = new User();
                        users.C_username = txtBoxName.Text;
                        users.C_userno = txtBoxNum.Text;
                        users.C_password = txtBoxPassword.Text;
                        users.C_powerid = cmbBoxAuthority.SelectedIndex;//.SelectedItem.ToString();
                        m_db.Users.Add(users);
                    }
                    else
                    {
                        var query = from users in m_db.Users
                                    where users.C_id == m_id
                                    select users;
                        foreach (var user in query)
                        {
                            user.C_username = txtBoxName.Text;
                            user.C_userno = txtBoxNum.Text;
                            user.C_password = txtBoxPassword.Text;
                            user.C_powerid = cmbBoxAuthority.SelectedIndex;//.SelectedItem.ToString();
                        }
                    }
                    m_db.SaveChanges();
                    this.Close();
                }
                else
                    MessageBox.Show(strings.PwEnterNotTheSame);//"兩次输入的密码不一致！");
            }
            else
                MessageBox.Show(strings.plzEnterPw);//"请输入密码");
        }
    }
}

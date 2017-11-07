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
    public partial class DialogAdminSet : Form
    {
        public DialogAdminSet()
        {
            InitializeComponent();
        }

        private void AdminSet_Load(object sender, EventArgs e)
        {
            listViewAutho.View = View.Details;
            listViewAutho.GridLines = false;
            listViewAutho.FullRowSelect = true;
            listViewAutho.Scrollable = true;
            listViewAutho.CheckBoxes = true;
            listViewAutho.Columns.Add(strings.OperatorPermissionGroup, 120);//"操作人员权限组", 120);
            listViewAutho.Columns.Add("", 120);
            listViewAutho.Columns.Add("", 0);
            listViewAutho.Columns.Add("", 120);

            listViewAdmin.View = View.Details;
            listViewAdmin.GridLines = false;
            listViewAdmin.FullRowSelect = true;
            listViewAdmin.Scrollable = true;
            listViewAdmin.CheckBoxes = true;
            listViewAdmin.Columns.Add(strings.OperatorID, 120);//"操作人员編号", 120);
            listViewAdmin.Columns.Add(strings.OperatorName, 120);//"操作人员姓名", 120);
            listViewAdmin.Columns.Add("", 0);
            listViewAdmin.Columns.Add(strings.PermissionGroup, 120);//"权限组", 120);
            
            listViewRefresh();
        }

        private void listViewRefresh()
        {
            ICMDBContext db = new ICMDBContext();
            listViewAdmin.Items.Clear();
            listViewAutho.Items.Clear();

            foreach (var user in db.Users)
            {
                List<string> sublist = new List<string>
                {
                    user.C_userno,
                    user.C_username,
                    user.C_id.ToString(),
                    user.C_powerid.ToString()
                };
                listViewAdmin.Items.Add(new ListViewItem(sublist.ToArray()));
            }

            foreach (var us in db.Authorities)
            {
                List<string> sublist = new List<string>
                {
                    us.C_name,
                    "",
                    us.C_id.ToString()
                };
                listViewAutho.Items.Add(new ListViewItem(sublist.ToArray()));
            }
        }

        private void BtnAddAutho_Click(object sender, EventArgs e)
        {
            DialogAdminMan adminMan = new DialogAdminMan(0);
            adminMan.ShowDialog();
            listViewRefresh();
        }

        private void BtnModAutho_Click(object sender, EventArgs e)
        {
            int ID = 0;
            int nCount = listViewAutho.Items.Count;
            for (int i = 0; i < nCount; i++)
            {
                if (listViewAutho.Items[i].Checked)
                {
                    ID = int.Parse(listViewAutho.Items[i].SubItems[2].Text);
                }
            }
            if (ID != 0)
            {
                DialogAdminMan AlterForm = new DialogAdminMan(ID);
                AlterForm.ShowDialog();
                listViewRefresh();
            }
            else
                MessageBox.Show("Choose the item you want to modify");
        }

        private void BtnDelAutho_Click(object sender, EventArgs e)
        {
            int ID = 0;
            int nCount = listViewAutho.Items.Count;
            for (int i = 0; i < nCount; i++)
            {
                if (listViewAutho.Items[i].Checked)
                {
                    ID = int.Parse(listViewAutho.Items[i].SubItems[2].Text);
                }
            }
            if (ID != 0)
            {
                ICMDBContext db = new ICMDBContext();
                var queryDel = from zz in db.Authorities
                               where zz.C_id == ID
                               select zz;
                foreach (var a in queryDel)
                {
                    db.Authorities.Remove(a);
                }
                db.SaveChanges();
                listViewRefresh();
            }
            else
                MessageBox.Show("Choose the item you want to delete");
        }

        private void BtnAddAdmin_Click(object sender, EventArgs e)
        {
            DialogUserAdd addOp = new DialogUserAdd(0);
            addOp.ShowDialog();
            listViewRefresh();
        }

        private void BtnModAdmin_Click(object sender, EventArgs e)
        {
            int ID = 0;
            int nCount = listViewAdmin.Items.Count;
            for (int i = 0; i < nCount; i++)
            {
                if (listViewAdmin.Items[i].Checked)
                {
                    ID = int.Parse(listViewAdmin.Items[i].SubItems[2].Text);
                }
            }
            if (ID != 0)
            {
                DialogUserAdd ModOp = new DialogUserAdd(ID);
                ModOp.ShowDialog();
                listViewRefresh();
            }
            else
                MessageBox.Show("Choose the item you want to modify");
        }

        private void BtnDelAdmin_Click(object sender, EventArgs e)
        {
            int ID = 0;
            int nCount = listViewAdmin.Items.Count;
            for (int i = 0; i < nCount; i++)
            {
                if (listViewAdmin.Items[i].Checked)
                {
                    ID = int.Parse(listViewAdmin.Items[i].SubItems[2].Text);
                }
            }
            if (ID != 0)
            {
                ICMDBContext db = new ICMDBContext();
                var queryDel = from user in db.Users
                               where user.C_id == ID
                               select user;
                foreach (var user in queryDel)
                {
                    db.Users.Remove(user);
                }
                db.SaveChanges();
                listViewRefresh();
            }
            else
                MessageBox.Show("Choose the item you want to delete");
        }
    }
}

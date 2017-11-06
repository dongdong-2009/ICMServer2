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
    public partial class DialogAdminMan : Form
    {
        ICMDBContext db = new ICMDBContext();
        authority auth = new authority();
        int ID;

        public DialogAdminMan(int id)
        {
            InitializeComponent();
            ID = id;
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (ID == 0)
            {
                auth.C_name = textBoxAuthoName.Text;
                auth.C_authority = CalcLevel();
                db.Authorities.Add(auth);
            }
            else
            {
                var query = from zz in db.Authorities
                            where zz.C_id == ID
                            select zz;
                foreach (var a in query)
                {
                    a.C_name = textBoxAuthoName.Text;
                    a.C_authority = CalcLevel();
                }
            }
            db.SaveChanges();
            this.Close();
        }

        private int CalcLevel()
        {
            int calc = 0;
            if (checkBoxArea.Checked)
                calc = Convert.ToInt32(checkBoxArea.Tag);
            if (checkBoxAutho1.Checked)
                calc += Convert.ToInt32(checkBoxAutho1.Tag);
            if (checkBoxDev.Checked)
                calc += Convert.ToInt32(checkBoxDev.Tag);
            if (checkBoxEvent.Checked)
                calc += Convert.ToInt32(checkBoxEvent.Tag);
            if (checkBoxLock.Checked)
                calc += Convert.ToInt32(checkBoxLock.Tag);
            if (checkBoxPhone.Checked)
                calc += Convert.ToInt32(checkBoxPhone.Tag);
            return calc;
        }

        private void checkBoxSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxArea.Checked = checkBoxSelectAll.Checked;
            checkBoxAutho1.Checked = checkBoxSelectAll.Checked;
            checkBoxDev.Checked = checkBoxSelectAll.Checked;
            checkBoxEvent.Checked = checkBoxSelectAll.Checked;
            checkBoxLock.Checked = checkBoxSelectAll.Checked;
            checkBoxPhone.Checked = checkBoxSelectAll.Checked;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AdminMan_Load(object sender, EventArgs e)
        {
            int level;
            if (ID != 0)
            {
                var query = from zz in db.Authorities
                            where zz.C_id == ID
                            select zz;
                foreach (var a in query)
                {
                    textBoxAuthoName.Text = a.C_name;
                    level = (int)a.C_authority;
                    checkBoxDev.Checked = ((level & 1) == 1);
                    level >>= 1;
                    checkBoxArea.Checked = ((level & 1) == 1);
                    level >>= 1;
                    checkBoxEvent.Checked = ((level & 1) == 1);
                    level >>= 1;
                    checkBoxPhone.Checked = ((level & 1) == 1);
                    level >>= 1;
                    checkBoxLock.Checked = ((level & 1) == 1);
                    level >>= 1;
                    checkBoxAutho1.Checked = ((level & 1) == 1);
                }
            }
        }
    }
}

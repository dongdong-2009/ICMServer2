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
    public partial class DialogAddResident : Form
    {
        public DialogAddResident()
        {
            InitializeComponent();
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            using (var db = new ICMServer.Models.ICMDBContext())
            {
                holderinfo ResidentIndfo = new holderinfo();
                ResidentIndfo.C_name = textBoxName.Text;
                ResidentIndfo.C_roomid = textBoxRoomID.Text;
                ResidentIndfo.C_sex = ComboBoxSex.SelectedIndex;
                ResidentIndfo.C_isholder = ComboBoxIsResident.SelectedIndex;
                ResidentIndfo.C_phoneno = textBoxPhone.Text;
                //ResidentIndfo.birth = dateTimePickerBirth.Value;
                //ResidentIndfo.PID = textBoxPID.Text;
                db.Holderinfoes.Add(ResidentIndfo);
                db.SaveChanges();
            }
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

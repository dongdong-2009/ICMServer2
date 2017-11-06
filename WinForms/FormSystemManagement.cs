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
    public partial class FormSystemManagement : Form
    {
        public FormSystemManagement()
        {
            InitializeComponent();
        }

        private void BtnUserMan_Click(object sender, EventArgs e)
        {
            DialogAdminSet adminSet = new DialogAdminSet();
            adminSet.ShowDialog();
        }

        private void BtnSysSet_Click(object sender, EventArgs e)
        {
            DialogSystemSet sysSet = new DialogSystemSet();
            sysSet.ShowDialog();
        }

        private void BtnBackup_Click(object sender, EventArgs e)
        {
            DialogSysBackup backup = new DialogSysBackup();
            backup.ShowDialog();
        }
    }
}

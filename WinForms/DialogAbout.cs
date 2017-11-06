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
    public partial class DialogAbout : Form
    {
        public DialogAbout()
        {
            InitializeComponent();
        }

        private void DialogAbout_Load(object sender, EventArgs e)
        {
            labelAbout.BackColor = Color.Transparent;
            //labelAbout.Text += Config.Instance.AppVersion;// m_ini.IniReadValue("AddrBook", "INDEX_SYS_VERSION", m_filename);
            //labelAbout.Text += "\nCpoyright(C) 2016";
            //pictureBox.Image = Image.FromFile(".\\res\\control_server.ico");
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

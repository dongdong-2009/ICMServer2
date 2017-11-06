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
    public partial class DialogUploadAdFile : Form
    {
        ICMDBContext db = new ICMDBContext();
        advertisement ads = new advertisement();
        int Order;

        public DialogUploadAdFile(int pass) // pass: 順序值
        {
            InitializeComponent();
            Order = pass;
        }

        private void BtnSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == DialogResult.OK)
            {
                textBoxPath.Text += op.FileName;
            }
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if ((textBoxPath.Text == "") || (textBoxTopic.Text == ""))
                MessageBox.Show(strings.AdsTitleAndFilePathCannotBeNull);//"广告标题和文件位置不得为空");
            else
            {
                ads.C_title= textBoxTopic.Text;
                ads.C_path = textBoxPath.Text;
                ads.C_time = DateTime.Now;
                ads.C_no = Order;
                db.Advertisements.Add(ads);
                db.SaveChanges();
                //BtnOk.DialogResult = DialogResult.OK;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

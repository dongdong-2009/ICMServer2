using MySql.Data.MySqlClient;
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
    public partial class DialogSysBackup : Form
    {
        public DialogSysBackup()
        {
            InitializeComponent();
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            string constring = string.Format("server=localhost;user={0};pwd={1};database={2};", Config.Instance.DatabaseUser, Config.Instance.DatabasePassword, Config.Instance.DatabaseName);
            if (radioBtnBackup.Checked)
            {
                // TODO 密码不能寫死，也不能寫在程式码內
                using (MySqlConnection conn = new MySqlConnection(constring))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        using (MySqlBackup mb = new MySqlBackup(cmd))
                        {
                            cmd.Connection = conn;
                            conn.Open();
                            mb.ExportToFile(txtBoxPath.Text);
                            conn.Close();
                        }
                    }
                }
                MessageBox.Show(strings.DBBackupFinish);//"数据库备份完成!");
                //StrCmd = "mysqldump -uroot -p123456 icmdb.net > ";
            }
            else
            {
                using (MySqlConnection conn = new MySqlConnection(constring))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        using (MySqlBackup mb = new MySqlBackup(cmd))
                        {
                            cmd.Connection = conn;
                            conn.Open();
                            mb.ImportFromFile(txtBoxPath.Text);
                            conn.Close();
                        }
                    }
                }
                MessageBox.Show(strings.DBRecoverFinish);//"数据库還原完成!");
            }
            //if(GetMysqlPath(MysqlPath) == false)
            //{
            //    MessageBox.Show("Error~ Mysql not exist!");
            //}
            //else
            //{
                
            //}
        }

        //private bool GetMysqlPath(string MysqlPath)
        //{
        //    var RegLocation = @"Software\MySQL AB\MySQL Server 5.0"
        //    string regFilePath = null;

        //    object objRegisteredValue = key.GetValue("");

        //    regFilePath = objRegisteredValue.ToString();

        //    return true;
        //}

        private void BtnSelectFile_Click(object sender, EventArgs e)
        {
            if (radioBtnBackup.Checked)
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "Mysql File(*.sql)|*.sql";
                if(save.ShowDialog() == DialogResult.OK)
                {
                    txtBoxPath.Text += save.FileName;
                }
            }
            else
            {
                OpenFileDialog open = new OpenFileDialog();
                open.Filter = "Mysql File(*.sql)|*.sql";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    txtBoxPath.Text += open.FileName;
                }
            }
        }

        private void BtnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

using ICMServer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICMServer
{
    public partial class DialogSelectUpgrade : Form
    {
        public DialogSelectUpgrade()
        {
            InitializeComponent();
        }

        private void BtnSelectFile_Click(object sender, EventArgs e)
        {
            // 选择档案，开启档案對话框
            OpenFileDialog dlgFile = new OpenFileDialog();
            dlgFile.Filter = "PKG files (*.pkg)|*.pkg|All files (*.*)|*.*";
            if (dlgFile.ShowDialog() == DialogResult.OK)
            {
                textBoxPath.Text += dlgFile.FileName;
                // 选完档案后读出档案的第 8~16 bytes，该 8 bytes 就是 version number
                using (FileStream input = new FileStream(dlgFile.FileName, FileMode.Open))
                {
                    BinaryReader reader = new BinaryReader(input);
                    reader.ReadBytes(8);    // skip 8 bytes;
                    string version = Encoding.UTF8.GetString(reader.ReadBytes(8));
                    this.textBoxVersion.Text = version;
                }
            }
        }

        private readonly string[] m_DeviceFileType =
        {
            "doorcamera",// "别墅门口机"
            "lobyyu",    // "单元门口机"
            "lobyyb",    // "楼栋门口机"
            "lobyya",    // "小区门口机"
            "indoor",    // "室内机"
            "indoor",    // "室内机(SD)"
            "manager",   // "管理机"
            "public"     // "公共门鈴机"
        };

         enum DeviceType {
            DEVTYPE_PC_MANAGER                    = 0,
            DEVTYPE_OUTDOOT_DOORCAMERA,
            DEVTYPE_OUTDOOT_LOBBY_U,
            DEVTYPE_OUTDOOT_LOBBY_D,
            DEVTYPE_OUTDOOT_LOBBY_Q,
            DEVTYPE_INDOOR,
            DEVTYPE_MANAGER,
            DEVTYPE_INDOOR_SD,
            DEVTYPE_PHONE,
            DEVTYPE_PUBLIC_CAMERA,
            DEVTYPE_IPCAMERA
        };

        private readonly int[] m_DeviceType =
        {
            (int)DeviceType.DEVTYPE_OUTDOOT_DOORCAMERA, 
            (int)DeviceType.DEVTYPE_OUTDOOT_LOBBY_U,
            (int)DeviceType.DEVTYPE_OUTDOOT_LOBBY_D,
            (int)DeviceType.DEVTYPE_OUTDOOT_LOBBY_Q,
            (int)DeviceType.DEVTYPE_INDOOR,
            (int)DeviceType.DEVTYPE_INDOOR_SD,
            (int)DeviceType.DEVTYPE_MANAGER,
            (int)DeviceType.DEVTYPE_PUBLIC_CAMERA
        };

        private readonly string[] m_ResourceFileType =
        {
            "",
            "address",
            "screen",
            "card"
        };

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            if (this.textBoxPath.Text.Length == 0)
            {
                MessageBox.Show("请添加升级文件", "提示", MessageBoxButtons.OK);
                return;
            }

            // TODO:
            // 先读取目前时间
            string timeStamp = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            string typename = "";
            switch (this.ComboBoxUpgradeType.SelectedIndex) 
            {
            case 0: // software upgrade
                switch (this.ComboBoxDevType.SelectedIndex)
                {
                case 0: // "别墅门口机"
                case 1: // "单元门口机"
                case 2: // "楼栋门口机"
                case 3: // "小区门口机"
                case 4: // "室内机"
                case 5: // "室内机(SD)"
                case 6: // "管理机"
                case 7: // "公共门鈴机"
                    typename = m_DeviceFileType[this.ComboBoxDevType.SelectedIndex];
                    break;
                }
                break;

            case 1: // addressbook
            case 2: // screen saver
            case 3: // card list
                typename = m_ResourceFileType[this.ComboBoxUpgradeType.SelectedIndex];
                break;
            }
            // 根據 file type | Device type 決定 file name

            string filename = string.Format(@"{0}\data\firmware\{1}_{2}_{3}\{4}",
                Config.Instance.FTPServerRootDir, timeStamp, this.textBoxVersion.Text, typename,
                this.textBoxFileName.Text);

            if (!Directory.Exists(System.IO.Path.GetDirectoryName(filename)))
                Directory.CreateDirectory(System.IO.Path.GetDirectoryName(filename));
            File.Copy(this.textBoxPath.Text, filename);

            using (var db = new ICMDBContext())
            {
                int DeviceType;
                if (this.ComboBoxUpgradeType.SelectedIndex != 0)
                    DeviceType = -1;
                else
                    DeviceType = m_DeviceType[this.ComboBoxDevType.SelectedIndex];
                int fileType = this.ComboBoxUpgradeType.SelectedIndex + 1;
                var file = (from f in db.Upgrades
                            where f.filetype == fileType
                               && f.Device_type == DeviceType
                            select f).FirstOrDefault();
                upgrade upgradeInfo = new upgrade();
                upgradeInfo.Device_type = DeviceType;
                upgradeInfo.filetype = fileType;
                upgradeInfo.filepath = filename.Substring(Config.Instance.FTPServerRootDir.Length + 1, filename.Length - Config.Instance.FTPServerRootDir.Length - 1);
                upgradeInfo.is_default = (file == null) ? 1 : 0;
                upgradeInfo.version = this.textBoxVersion.Text;
                upgradeInfo.time = DateTime.Now;
                db.Upgrades.Add(upgradeInfo);
                db.SaveChanges();
                this.DialogResult = DialogResult.OK;
            }
        }

        private void DialogSelectUpgrade_Load(object sender, EventArgs e)
        {
            this.ComboBoxUpgradeType.SelectedIndex = 0;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private readonly string[] m_DeviceTypes = 
        {
            "别墅门口机",
            "单元门口机",
            "楼栋门口机",
            "小区门口机",
            "室内机",
            "室内机(SD)",
            "管理机",
            "公共门鈴机",
            "全部"
        };

        private readonly string[] DevicePkgFileNames = 
        {
            "OUTDOOR.PKG", 
            "LOBBY.PKG", 
            "LOBBY.PKG", 
            "LOBBY.PKG", 
            "INDOOR.PKG", 
            "INDOOR.PKG", 
            "ADMIN.PKG", 
            "OUTDOOR.PKG", 
        };

        private readonly string[] ResourcePkgFileNames = 
        {
            "", 
            "ADDRESS.PKG", 
            "SCREENSAVER.PKG", 
            "CARD.PKG"
        };

        private void ComboBoxUpgradeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.ComboBoxUpgradeType.SelectedIndex)
            {
            case 0:
                this.ComboBoxDevType.SelectedIndex = 0;
                this.textBoxFileName.Text = DevicePkgFileNames[this.ComboBoxDevType.SelectedIndex];
                this.ComboBoxDevType.Visible = true;
                break;

            default:
                this.textBoxFileName.Text = ResourcePkgFileNames[this.ComboBoxUpgradeType.SelectedIndex];
                this.ComboBoxDevType.Visible = false;
                //this.ComboBoxDevType.SelectedIndex = 8;
                break;
            }
        }

        private void ComboBoxDevType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (this.ComboBoxDevType.SelectedIndex == 8)
            //    return;
            this.textBoxFileName.Text = DevicePkgFileNames[this.ComboBoxDevType.SelectedIndex];
        }
    }
}

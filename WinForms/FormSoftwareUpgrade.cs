using ICMServer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICMServer
{
    public partial class FormSoftwareUpgrade : Form
    {
        ICMDBContext db = new ICMDBContext();

        private readonly string[] m_DeviceTypes = 
        { 
            "全部",
            "别墅门口机",
            "单元门口机",
            "楼栋门口机",
            "小区门口机",
            "室内机",
            "管理机",
            "室内机 (SD)",
            "手机",
            "公共门鈴机",
            "IP摄像头"
        };

        private readonly string[] m_FileTypes = 
        {
            "",
            "软件升级",
            "地址薄升级",
            "屏保升级",
            "门禁卡升级"
        };

        public FormSoftwareUpgrade()
        {
            InitializeComponent();
        }

        private void BtnUploadFile_Click(object sender, EventArgs e)
        {
            DialogSelectUpgrade SelectUpgradeDlg = new DialogSelectUpgrade();
            if (SelectUpgradeDlg.ShowDialog() == DialogResult.OK)
                RefreshUgradeFileList();
        }

        private async void RefreshUgradeFileList()
        {
            //using (var db = new ICMDBContext())
            {
                var files = from f in db.Upgrades
                            select f;
                this.upgradeBindingSource.DataSource = await files.ToListAsync();
            }
        }

        private void FormSoftwareUpgrade_Load(object sender, EventArgs e)
        {
            txtBoxFtpRootDir.Text = Config.Instance.FTPServerRootDir;
            RefreshUgradeFileList();
        }

        private void UpgradeDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dgvTxtBoxColDeviceType.Index)
            {
                int DeviceType = (int)e.Value;
                if (0 <= DeviceType && DeviceType < m_DeviceTypes.Count())
                {
                    e.Value = m_DeviceTypes[DeviceType];
                }
                else
                    e.Value = "全部";
            }
            else if (e.ColumnIndex == dgvTxtBoxColFileType.Index)
            {
                int fileType = (int)e.Value;
                if (fileType < m_FileTypes.Count())
                {
                    e.Value = m_FileTypes[fileType];
                }
            }
        }

        private void BtnUpgrade_Click(object sender, EventArgs e)
        {
            if (upgradeDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择一個項目");
                return;
            }
            if (upgradeDataGridView.SelectedRows.Count > 1)
            {
                MessageBox.Show("一次只能选一項");
                return;
            }

            //if (upgradeBindingSource.Current == null)
            //    return;

            var file = upgradeBindingSource.Current as upgrade;
            if (file == null)
            {
                MessageBox.Show("Please select the item!");
                return;
            }

            DialogSelectIP dlg = new DialogSelectIP(file.filepath, (int)file.Device_type);
            if (dlg.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            List<Device> Devices = dlg.Devices;
            if (Devices.Count > 0)
            {
                // open another dialog
                DialogUpdateSoftwareBox dlgUpdateSoftwareBox = new DialogUpdateSoftwareBox(file.filepath, Devices);
                dlgUpdateSoftwareBox.ShowDialog();
            }
        }

        private async void BtnDeleteFile_Click(object sender, EventArgs e)
        {
            //using (var db = new ICMDBContext())
            { 
                foreach (DataGridViewRow row in upgradeDataGridView.SelectedRows)
                {
                    upgrade upgradeInfo = (upgrade)row.DataBoundItem;

                    // delete file and directory
                    string filepath = Config.Instance.FTPServerRootDir
                                    + @"\" + upgradeInfo.filepath;
                    try
                    {
                        System.IO.File.Delete(filepath);
                        string dirpath = System.IO.Path.GetDirectoryName(filepath);
                        System.IO.Directory.Delete(dirpath);
                    }
                    catch (Exception) { }
                    db.Upgrades.Remove(upgradeInfo);
                }
                await db.SaveChangesAsync();
                RefreshUgradeFileList();
            }
        }

        private async void BtnResetToDefault_Click(object sender, EventArgs e)
        {
            if (upgradeDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择一個項目");
                return;
            }
            if (upgradeDataGridView.SelectedRows.Count > 1)
            { 
                MessageBox.Show("一次只能选一項");
                return;
            }

            upgrade selectedUpgradeInfo = (upgrade)upgradeDataGridView.SelectedRows[0].DataBoundItem;
            var upgradeInfos = from file in db.Upgrades
                               where file.filetype == selectedUpgradeInfo.filetype
                                 && file.Device_type == selectedUpgradeInfo.Device_type
                               select file;
            foreach (upgrade upgradeInfo in upgradeInfos)
            {
                upgradeInfo.is_default = 0;
            }
            selectedUpgradeInfo.is_default = 1;
            await db.SaveChangesAsync();
            RefreshUgradeFileList();
        }
    }
}

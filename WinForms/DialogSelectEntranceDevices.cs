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
    public partial class DialogSelectEntranceDevices : Form
    {
        private List<Device> m_Devices = new List<Device>();

        public DialogSelectEntranceDevices()
        {
            InitializeComponent();
        }

        private void DialogSelectEntranceDevices_Load(object sender, EventArgs e)
        {
            using (var db = new ICMDBContext())
            {
                var entranceDevices = (from d in db.Devices
                                       where 1 <= d.type && d.type <= 4
                                       select d).ToList();
                this.DeviceBindingSource.DataSource = entranceDevices;
            }
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (DeviceDataGridView.SelectedRows.Count > 0)
            {
                m_Devices = (from DataGridViewRow row in DeviceDataGridView.SelectedRows
                             select (row.DataBoundItem as Device)).ToList();
                DialogResult = DialogResult.OK;
                this.Close();
            }
            else
                MessageBox.Show("请选择要同步的设备");
        }

        public List<Device> GetReturnResult()
        {
            return m_Devices;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private readonly string[] m_DeviceTypes = 
        { 
            "伺服器",
            "别墅门口机",
            "单元门口机",
            "楼栋门口机",
            "小区门口机",
            "室内机",
            "管理机",
            "室内机 (SD)",
            "手机",
            "公共门铃机",
            "IP摄像头"
        };

        private void DeviceDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dgvTextBoxDeviceId.Index)
            {
                string DeviceId = e.Value as string;
                e.Value = DevicesAddressConverter.RoToChStr(DeviceId);
            }
            else if (e.ColumnIndex == dgvTextBoxDeviceType.Index)
            {
                int DeviceType = (int)e.Value;
                if (DeviceType < m_DeviceTypes.Count())
                {
                    e.Value = m_DeviceTypes[DeviceType];
                }
            }
        }
    }
}

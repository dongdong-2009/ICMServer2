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
    public partial class DialogDevSelectCall : Form
    {
        ICMDBContext m_db = new ICMDBContext();
        FormVideoTalk m_pass;
        public DialogDevSelectCall(FormVideoTalk data)
        {
            InitializeComponent();
            m_pass = data;
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            try
            {
                m_pass.textBox.Text = listBoxCallDev.SelectedItem.ToString();
            }
            catch
            {
            }
            finally
            {
                this.Close();
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SelectCallDev_Load(object sender, EventArgs e)
        {
            string room;
            int DevType = m_pass.ComboBoxDev.SelectedIndex + 2;
            if (DevType == (int)DeviceType.Indoor_Phone)
            {
                var query = from target in m_db.Devices
                            where (target.type == (int)DeviceType.Indoor_Phone || target.type == (int)DeviceType.Indoor_Phone_SD)
                               && (target.online == 1)
                            select target;
                foreach (var dev in query)
                {
                    room = DevicesAddressConverter.RoToChStr(dev.roomid);
                    listBoxCallDev.Items.Add(room);
                }
            }
            else
            {
                var query = from target in m_db.Devices
                            where target.type == DevType
                               && (target.online == 1)
                            select target;
                foreach (var dev in query)
                {
                    room = DevicesAddressConverter.RoToChStr(dev.roomid);
                    listBoxCallDev.Items.Add(room);
                }
            }
        }
    }
}

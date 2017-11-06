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
    public partial class DialogHandleAlarm : Form
    {
        eventwarn m_Alarm;

        public DialogHandleAlarm(eventwarn alarm)
        {
            InitializeComponent();
            m_Alarm = alarm;
        }

        private void DialogHandleAlarm_Load(object sender, EventArgs e)
        {
            srcaddrTextBox.Text = DevicesAddressConverter.RoToChStr(m_Alarm.srcaddr);
            channelTextBox.Text = m_Alarm.channel.ToString();
            timeDateTimePicker.Value = m_Alarm.time;
            typeTextBox.Text = m_Alarm.type;
            actionTextBox.Text = ActionToString(m_Alarm.Action);
            handlerTextBox.Text = m_Alarm.handler;
            int handleStatus = m_Alarm.handlestatus ?? 0;
            handlestatusListBox.SelectedIndex = handleStatus;
            if (m_Alarm.handletime != null)
                handletimeDateTimePicker.Value = (DateTime)m_Alarm.handletime;
            else
                handletimeDateTimePicker.Value = DateTime.Now;
        }

        private string ActionToString(string action)
        {
            if (action != null)
            {
                if (action == "trig")
                    return "事件觸发";
                else if (action == "unalarm")
                    return "事件解除";
                else if (action == "enable")
                    return "佈防";
                else if (action == "disable")
                    return "撤防";    
            }
            
            return "";
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            using (var db = new ICMDBContext())
            {
                db.Eventwarns.Attach(m_Alarm);
                m_Alarm.handler = handlerTextBox.Text;
                m_Alarm.handlestatus = handlestatusListBox.SelectedIndex;
                if (m_Alarm.handlestatus == 2)
                    m_Alarm.handletime = DateTime.Now;
                else
                    m_Alarm.handletime = null;
                db.SaveChanges();
                DialogResult = DialogResult.OK;
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void handlestatusListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (handlestatusListBox.SelectedIndex == 2)
                handletimeDateTimePicker.Value = DateTime.Now;
        }
    }
}

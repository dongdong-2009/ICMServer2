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
using System.IO;

namespace ICMServer
{
    public partial class FormLeaveMessage : Form
    {
        ICMDBContext m_db = new ICMDBContext();
        string m_filename = null;
        public FormLeaveMessage()
        {
            InitializeComponent();
        }

        private void FormLeaveMessage_Load(object sender, EventArgs e)
        {
            ComboBoxReadStatus.SelectedIndex = 0;
            RefreshLeaveMsgsGridView();
            ICMServer.FormVideoTalk.Instance.ReceivedNewVideoMessageEvent += _ReceivedNewVideoMessageEvent;
        }

        void _ReceivedNewVideoMessageEvent(object sender, EventArgs e)
        {
            RefreshLeaveMsgsGridView();
        }

        private async void RefreshLeaveMsgsGridView()
        {
            if (this.InvokeRequired)
            {
                this.Invoke((System.Action)(() => { RefreshLeaveMsgsGridView(); }));
            }
            else
            {
                using (var db = new ICMDBContext())
                {
                    var leaveMsgs = from a in db.Leavewords
                                   orderby a.dst_addr descending
                                   select a;
                    LeaveMsgsbindingSource.DataSource = await Task.Run(() => { return leaveMsgs.ToList(); });
                }
            }
        }

        private void BtnDel_Click(object sender, EventArgs e)
        {
            if (dataGridViewLeaveMsgs.SelectedRows.Count > 0)
            {
                using (var db = new ICMDBContext())
                {
                    var LeaveMsgs = (from DataGridViewRow row in dataGridViewLeaveMsgs.SelectedRows
                                  select (db.Leavewords.Attach(row.DataBoundItem as leaveword))).ToList();

                    db.Leavewords.RemoveRange(LeaveMsgs);
                    db.SaveChanges();
                }
                System.IO.FileInfo fiSdp = new System.IO.FileInfo(m_filename + ".sdp");
                System.IO.FileInfo fiRtpaD = new System.IO.FileInfo(m_filename + ".rtpa.data");
                System.IO.FileInfo fiRtpaI = new System.IO.FileInfo(m_filename + ".rtpa.index");
                System.IO.FileInfo fiRtpvD = new System.IO.FileInfo(m_filename + ".rtpv.data");
                System.IO.FileInfo fiRtpvI = new System.IO.FileInfo(m_filename + ".rtpv.index");
                try
                {
                    fiSdp.Delete();
                    fiRtpaD.Delete();
                    fiRtpaI.Delete();
                    fiRtpvD.Delete();
                    fiRtpvI.Delete();
                }
                catch { }
                RefreshLeaveMsgsGridView();
            }
        }

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            if (dataGridViewLeaveMsgs.SelectedRows.Count > 0)
            {
                using (var db = new ICMDBContext())
                {
                    bool NeedRefresh = false;
                    var LeaveMsgs = (from DataGridViewRow row in dataGridViewLeaveMsgs.SelectedRows
                                     select (db.Leavewords.Attach(row.DataBoundItem as leaveword))).ToList();
                    foreach(var filepath in LeaveMsgs)
                    {
                        if(File.Exists(filepath.filenames + ".sdp"))
                            (new DialogLeaveMsgPlayer(filepath.filenames)).ShowDialog();
                        else
                        {
                            MessageBox.Show(strings.FilePathNotExist);
                            db.Leavewords.Remove(filepath);
                            NeedRefresh = true;
                        }
                    }
                    if (NeedRefresh)
                    {
                        db.SaveChanges();
                        RefreshLeaveMsgsGridView();
                    }
                }
            }
        }

        private async void BtnSearch_Click(object sender, EventArgs e)
        {
            using (var db = new ICMDBContext())
            {
                var videoMsgses = (from a in db.Leavewords
                                   select a).ToList();
                if (ComboBoxReadStatus.SelectedIndex == 0)
                {
                    videoMsgses = (from a in videoMsgses
                                   where (dateTimePickerStart.Value <= Convert.ToDateTime(a.time) && Convert.ToDateTime(a.time) <= dateTimePickerEnd.Value)
                                   select a).ToList();
                    LeaveMsgsbindingSource.DataSource = await Task.Run(() => { return videoMsgses; });
                }
                else
                {
                    int read = ComboBoxReadStatus.SelectedIndex - 1;

                    videoMsgses = (from a in videoMsgses
                                   where (dateTimePickerStart.Value <= Convert.ToDateTime(a.time) && Convert.ToDateTime(a.time) <= dateTimePickerEnd.Value) && (a.readflag == ((read == 0) ? 0 : 1))
                                   select a).ToList();
                    LeaveMsgsbindingSource.DataSource = await Task.Run(() => { return videoMsgses; });
                }
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            RefreshLeaveMsgsGridView();
        }

        private void DataGridViewLeaveMsgs_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == srcaddrDataGridViewTextBoxColumn.Index)
            {
                string DoorID = (string)e.Value;
                e.Value = DevicesAddressConverter.RoToChStr(DoorID);
            }
            else if (e.ColumnIndex == dstaddrDataGridViewTextBoxColumn.Index)
            {
                string IndoorID = (string)e.Value;
                e.Value = DevicesAddressConverter.RoToChStr(IndoorID);
            }
            else if (e.ColumnIndex == readflagDataGridViewTextBoxColumn.Index)
            {
                bool Read = Convert.ToBoolean(e.Value);
                e.Value = (Read) ? strings.hasRead : strings.notRead;
            }
        }
    }
}

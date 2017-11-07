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
    public partial class FormAnnouncementManagement : Form
    {
        ICMDBContext db = new ICMDBContext();
        advertisement ads = new advertisement();
        static int sequence = 0;

        public FormAnnouncementManagement()
        {
            InitializeComponent();
        }

        // 點擊"上传广告文件"按鈕
        private void BtnUploadAd_Click(object sender, EventArgs e)
        {
            DialogUploadAdFile upup = new DialogUploadAdFile(sequence + 1); // 上传广告影片對话框
            upup.ShowDialog();

            CountSeqNum();      // 重算序号
            RefreshPlaySeq();   // 更新列表
        }

        private void RefreshPlaySeq()
        {
            listViewPlaySeq.Items.Clear();
            ICMDBContext dbRefresh = new ICMDBContext();
            foreach (var ads in dbRefresh.Advertisements.OrderBy(s => s.C_no))  // 按播放順序排序
            {
                List<string> sublist = new List<string>
                {
                    ads.C_no.ToString(),
                    ads.C_title,
                    ads.C_time.ToString(),
                    ads.C_path
                };  // 重填列表
                listViewPlaySeq.Items.Add(new ListViewItem(sublist.ToArray()));
            }
        }

        // 取得广告文件总數
        private void CountSeqNum()
        {
            ICMDBContext dbrefresh = new ICMDBContext();
            sequence = dbrefresh.Advertisements.Count();    // 广告文件总數
        }

        private void FormAnnouncementManagement_Load(object sender, EventArgs e)
        {
            ICMServer.Net.HttpServer.Instance.ReceivedReadAnnouncementEvent += HttpServer_ReceivedReadAnnouncementEvent;

            CountSeqNum();
            listViewPlaySeq.View = View.Details;
            listViewPlaySeq.GridLines = false;
            listViewPlaySeq.FullRowSelect = true;
            listViewPlaySeq.Scrollable = true;
            listViewPlaySeq.CheckBoxes = true;
            listViewPlaySeq.Columns.Add(strings.PlaySequence, 110);
            listViewPlaySeq.Columns.Add(strings.AdsTitle, 150);
            listViewPlaySeq.Columns.Add(strings.UploadTime, 150);
            listViewPlaySeq.Columns.Add(strings.Path, 400);
            RefreshPlaySeq();

            RefreshPublishInfoGridView();
        }

        private void HttpServer_ReceivedReadAnnouncementEvent(object sender, EventArgs e)
        {
            RefreshPublishInfoGridView();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            RefreshPublishInfoGridView();
        }

        private async void RefreshPublishInfoGridView()
        {
            if (this.InvokeRequired)
            {
                this.Invoke((System.Action)(() => { RefreshPublishInfoGridView(); }));
            }
            else
            {
                using (var db = new ICMDBContext())
                {
                    var publishInfo = from a in db.Publishinfoes
                                    orderby a.time
                                    select a;
                    dataGridViewPublishInfo.DataSource = await Task.Run(() => { return publishInfo.ToList(); });
                }
            }
        }

        private string HasRead(int? has_read)
        {
            if (has_read == 0)
                return strings.notRead; // 未读
            else
                return strings.hasRead; // 已读
        }

        private void BtnSetTime_Click(object sender, EventArgs e)
        {
            // 设置播放順序
            DialogAdsSeqSet adjestSeq = new DialogAdsSeqSet(sequence);
            adjestSeq.ShowDialog();
            RefreshPlaySeq();
        }

        // 刪除广告文件
        private void BtnDelData_Click(object sender, EventArgs e)
        {
            int nCount = listViewPlaySeq.Items.Count;
            DateTime queryID;

            for (int i = 0; i < nCount; i++)
            {
                if (listViewPlaySeq.Items[i].Checked)
                {
                    // 上传时间
                    queryID = Convert.ToDateTime(listViewPlaySeq.Items[i].SubItems[2].Text);
                    if (db != null)
                    {
                        var QueryDel = (from aa in db.Advertisements
                                       where aa.C_time == queryID
                                       select aa).FirstOrDefault();
                        db.Advertisements.Remove(QueryDel); // 拿时间來反查资料库取得特定资料並刪除

                        //decrese idseq
                        var QueryOrder = from zz in db.Advertisements
                                         where zz.C_no > QueryDel.C_no
                                         select zz;
                        foreach (var decrese in QueryOrder)
                        {
                            decrese.C_no -= 1;
                        }
                        //decrese idseq
                        db.SaveChanges();
                    }
                }
            }

            CountSeqNum();
            RefreshPlaySeq();
        }

        // 新增公告信息
        private void BtnNewMsg_Click(object sender, EventArgs e)
        {
            DialogMsgCreate CreateMsgDlg = new DialogMsgCreate();
            if (CreateMsgDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                publishinfo info = CreateMsgDlg.GetReturnValue();
                DialogHandlePublishInfoTask dlg = new DialogHandlePublishInfoTask(this, info);
                dlg.ShowDialog();
            }
            BtnRefresh_Click(this, e);
        }

        // 查看公告信息
        private void BtnCheck_Click(object sender, EventArgs e)
        {
            if (dataGridViewPublishInfo.SelectedRows.Count > 0)
            {
                using (var db = new ICMDBContext())
                {
                    bool NeedRefresh = false;
                    var PublishInfo = (from DataGridViewRow row in dataGridViewPublishInfo.SelectedRows
                                     select (db.Publishinfoes.Attach(row.DataBoundItem as publishinfo))).ToList();
                    foreach (var filepath in PublishInfo)
                    {
                        string curPth = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
                        if (File.Exists(curPth + "\\" + filepath.filepath))
                        {
                            DialogMsgView CheckForm = new DialogMsgView(filepath.id);
                            CheckForm.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show(strings.FilePathNotExist);
                            db.Publishinfoes.Remove(filepath);
                            NeedRefresh = true;
                        }
                    }
                    if (NeedRefresh)
                    {
                        db.SaveChanges();
                        RefreshPublishInfoGridView();
                    }
                }
            }
        }

        private void BtnDel_Click(object sender, EventArgs e)
        {
            if (dataGridViewPublishInfo.SelectedRows.Count > 0)
            {
                using (var db = new ICMDBContext())
                {
                    var PublishInfo = (from DataGridViewRow row in dataGridViewPublishInfo.SelectedRows
                                       select (db.Publishinfoes.Attach(row.DataBoundItem as publishinfo))).ToList();
                    foreach(var delPublish in PublishInfo)
                    {
                        System.IO.FileInfo fiPublish = new System.IO.FileInfo(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\" + delPublish.filepath);
                        try
                        {
                            fiPublish.Delete();
                        }
                        catch { }
                    }
                    db.Publishinfoes.RemoveRange(PublishInfo);
                    db.SaveChanges();
                }
                RefreshPublishInfoGridView();
            }
        }

        // 查询(過濾信息)
        private async void BtnSearch_Click(object sender, EventArgs e)
        {
            using (var db = new ICMDBContext())
            {
                var PublishInfo = (from a in db.Publishinfoes
                                   select a).ToList();
                if (textBoxIndoor.Text == "")
                {
                    PublishInfo = (from a in PublishInfo
                                   where (dateTimePickerStart.Value <= a.time && a.time <= dateTimePickerEnd.Value)
                                   select a).ToList();
                    dataGridViewPublishInfo.DataSource = await Task.Run(() => { return PublishInfo; });
                }
                else
                {
                    PublishInfo = (from a in PublishInfo
                                   where (dateTimePickerStart.Value <= a.time && a.time <= dateTimePickerEnd.Value) && a.dstaddr.StartsWith(textBoxIndoor.Text)
                                   select a).ToList();
                    dataGridViewPublishInfo.DataSource = await Task.Run(() => { return PublishInfo; });
                }
            }
        }

        private void BtnChooseDev_Click(object sender, EventArgs e)
        {
            DialogAreaAddress dlg = new DialogAreaAddress();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                textBoxIndoor.Text = dlg.ReturnValue;
            }
        }

        private void dataGridViewPublishInfo_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dstaddrDataGridViewTextBoxColumn.Index)
            {
                string IndoorID = (string)e.Value;
                e.Value = DevicesAddressConverter.RoToChStr(IndoorID);
            }
            else if (e.ColumnIndex == typeDataGridViewTextBoxColumn.Index)
            {
                bool type = Convert.ToBoolean(e.Value);
                e.Value = (type) ? "Image" : "Text";
            }
            else if (e.ColumnIndex == isreadDataGridViewTextBoxColumn.Index)
            {
                bool Read = Convert.ToBoolean(e.Value);
                e.Value = (Read) ? strings.hasRead : strings.notRead;
            }
        }
    }
}

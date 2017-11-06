using ICMServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ICMServer
{
    // TODO: 信息公告应该拆成兩段
    // Device 與 信息应该要採用多對多开连
    class DialogHandlePublishInfoTask : DialogProgress
    {
        FormAnnouncementManagement m_ParentForm;
        publishinfo m_Info;

        public DialogHandlePublishInfoTask(
            FormAnnouncementManagement parentForm,
            publishinfo info)
        {
            InitializeComponent();

            m_ParentForm = parentForm;
            m_Info = info;
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogHandlePublishInfoTask));
            this.SuspendLayout();
            // 
            // BtnClose
            // 
            resources.ApplyResources(this.BtnClose, "BtnClose");
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // listBoxLog
            // 
            resources.ApplyResources(this.listBoxLog, "listBoxLog");
            // 
            // DialogHandlePublishInfoTask
            // 
            resources.ApplyResources(this, "$this");
            this.Name = "DialogHandlePublishInfoTask";
            this.Load += new System.EventHandler(this.DialogHandlePublishInfoTask_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        enum DeviceType
        {
            DEVTYPE_PC_MANAGER = 0,
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

        private void ReportProgress(int value)
        {
            progressBar.Value = value;
            labelPercentage.Text = value.ToString() + "%";
            if (value == 100)
            {
                this.BtnClose.Enabled = true;
                this.Close();
            }
        }

        private Task DoPublishInfoTaskAsync(IProgress<int> progress = null)
        {
            return Task.Factory.StartNew(() => { DoPublishInfoTask(progress); }, TaskCreationOptions.LongRunning);
        }

        private async void DoPublishInfoTask(IProgress<int> progress = null)
        {
            int processCount = 0;
            int reportedProgressPercentage = -1;

            using (var db = new ICMDBContext())
            {
                // get the matched Device list
                var Devices = (from d in db.Devices
                               where (d.type == (int)DeviceType.DEVTYPE_INDOOR || d.type == (int)DeviceType.DEVTYPE_INDOOR_SD)
                                   && d.roomid.StartsWith(m_Info.dstaddr)
                               select d).ToList();
                //bool firstTime = true;
                foreach (var Device in Devices)
                {
                    // Add all Devices starts with ro to m_Info.dstaddr
                    //if (firstTime)
                    {
                        m_Info.dstaddr = Device.roomid;
                        db.Publishinfoes.Add(m_Info);
                        db.SaveChanges();
                        //firstTime = false;
                    }

                    AddLog(string.Format("sending publish@{0}", Device.ip));

                    // Query same room id
                    string roomID = Device.roomid.Remove(Device.roomid.Length - 3);

                    int newMsgCount = (from info in db.Publishinfoes
                                       where info.dstaddr.StartsWith(roomID)// == Device.roomid
                                          && info.isread == 0
                                       select info).Count();
                    // send message to Device
                    
                    //string uri = string.Format("http://{0}/message_new?count={1}&type=text", Device.ip, newMsgCount);
                    //Task.Run(async () => 
                    {
                        bool result = await ICMServer.Net.HttpClient.SendNewTextMsgCount(Device.ip, newMsgCount);
                        if (result)
                            AddLog(string.Format("send message success@{0}", Device.ip));
                        else
                            AddLog(string.Format("send message fail@{0}", Device.ip));
                    }//);
                    if (progress != null)
                    {
                        int currentProgressPercentage = (++processCount * 99 / Devices.Count);
                        if (reportedProgressPercentage != currentProgressPercentage)
                        {
                            reportedProgressPercentage = currentProgressPercentage;
                            progress.Report(reportedProgressPercentage);
                        }
                    }
                }
                if (progress != null)
                    progress.Report(100);
            }
        }

        //async Task SendMsgToClient(string uri, string ip)
        //{
        //    HttpClient client = new HttpClient();
        //    client.Timeout = new TimeSpan(0, 0, 1);

        //    try
        //    {
        //        var response = await client.GetAsync(uri);
        //        response.EnsureSuccessStatusCode();
        //        AddLog(string.Format("send message success@{0}", ip));
        //    }
        //    catch (Exception)
        //    {
        //        AddLog(string.Format("send message fail@{0}", ip));
        //    }
        //}

        private async void DialogHandlePublishInfoTask_Load(object sender, EventArgs e)
        {
            this.BtnClose.Enabled = false;
            var progressIndicator = new Progress<int>(ReportProgress);
            await DoPublishInfoTaskAsync(progressIndicator);
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

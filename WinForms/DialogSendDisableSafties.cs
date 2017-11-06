using ICMServer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICMServer
{
    public partial class DialogSendDisableSafties : DialogProgress
    {
        List<Device> m_Devices;

        public DialogSendDisableSafties(List<Device> Devices)
        {
            InitializeComponent();
            this.m_Devices = Devices;
        }

        private void ReportProgress(int value)
        {
            progressBar.Value = value;
            labelPercentage.Text = value.ToString() + "%";
            if (value == 100)
            {
                this.BtnClose.Enabled = true;
                //this.Close();
            }
        }

        private void DialogSendDisableSafties_Load(object sender, EventArgs e)
        {
            this.BtnClose.Enabled = false;
            var progressIndicator = new Progress<int>(ReportProgress);
            Task.Factory.StartNew(() => { DoWork(progressIndicator); }, TaskCreationOptions.LongRunning);
        }

        private async void DoWork(IProgress<int> progress = null)
        {
            int processCount = 0;
            int reportedProgressPercentage = -1;

            foreach (var Device in m_Devices)
            {
                AddLog("{0} / {1}: 正在传送撤防信息給设备", Device.ip, Device.roomid);
                bool result = await ICMServer.Net.HttpClient.SendDisableSafties(Device.ip, Device.roomid);
                if (result)
                    AddLog("{0} / {1}: 传送成功", Device.ip, Device.roomid);
                else
                    AddLog("{0} / {1}: 传送失敗", Device.ip, Device.roomid);

                if (progress != null)
                {
                    int currentProgressPercentage = (++processCount * 100 / m_Devices.Count);
                    if (reportedProgressPercentage != currentProgressPercentage)
                    {
                        reportedProgressPercentage = currentProgressPercentage;
                        progress.Report(reportedProgressPercentage);
                    }
                }
            }

            Thread.Sleep(500);
            if (progress != null)
                progress.Report(100);
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

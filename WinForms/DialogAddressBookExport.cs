using ICMServer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace ICMServer
{
    public partial class DialogAddressBookExport : DialogProgress
    {
        public DialogAddressBookExport()
        {
            InitializeComponent();
        }

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

        private void DialogAddressBookExport_Load(object sender, EventArgs e)
        {
            this.BtnClose.Enabled = false;
            var progressIndicator = new Progress<int>(ReportProgress);
            Task.Factory.StartNew(() => { ExportAddressBook(progressIndicator); }, TaskCreationOptions.LongRunning);
        }

        private bool ExportXml(AddrList addrList, string xmlFilePath)
        {
            bool result = false;
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true
            };
            using (MemoryStream stream = new MemoryStream())
            {
                using (XmlWriter writer = XmlWriter.Create(stream, settings))
                {
                    try
                    {
                        addrList.dev.WriteXml(writer);
                        // Reset stream to origin
                        stream.Seek(0, SeekOrigin.Begin);
                        // Load stream as XDocument
                        XDocument xdoc = XDocument.Load(stream);
                        // TODO 版本号应该要可以修改
                        xdoc.Element("AddrList").SetAttributeValue("ver", "1.0");
                        // Save to file as XML
                        xdoc.Save(xmlFilePath);
                        result = true;
                    }
                    catch (Exception)
                    {
                        //MessageBox.Show(ex.Message, "操作错误",
                        //                MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        writer.Close();
                    }
                }
            }
            return result;
        }

        private void ExportAddressBook(IProgress<int> progress = null)
        {
            int processCount = 0;
            int reportedProgressPercentage = -1;

            using (var db = new ICMDBContext())
            {
                AddrList addrList = new AddrList();
                var Devices = db.Devices.ToList();
                AddLog("总共有 {0} 笔资料需要输出", Devices.Count);
                foreach (var d in Devices)
                {
                    AddrList.devRow dev = addrList.dev.NewdevRow();
                    dev.ip = d.ip;
                    dev.ro = d.roomid;
                    dev.alias = d.Alias;
                    dev.group = d.group;
                    dev.mc = d.mac;
                    dev.ty = (byte)d.type;
                    dev.sm = d.sm;
                    dev.gw = d.gw;
                    dev.id = d.cameraid;
                    dev.pw = d.camerapw;
                    addrList.dev.AdddevRow(dev);

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
                addrList.AcceptChanges();
                string filePath = Path.GetAddressBookTempXmlFilePath();
                ExportXml(addrList, filePath);
                AddLog("已输出至 {0}", Path.GetAddressBookTempXmlFilePath());
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

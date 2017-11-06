using ICMServer.Models;
using ICMServer.Net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO;

namespace ICMServer
{
    public partial class DialogUpdateCardListBox : Form
    {
        List<Device> m_Devices;
        //string m_FilePath;
        Thread m_UpdateThread;

        List<FtpTransferStatus> m_FtpTransferStatus = new List<FtpTransferStatus>();

        public DialogUpdateCardListBox(List<Device> Devices)
        {
            InitializeComponent();
            //m_FilePath = filePath;
            m_Devices = new List<Device>();
            m_Devices.AddRange(Devices);
        }

        private void DialogUpdateCardListBox_Load(object sender, EventArgs e)
        {
            //m_Devices.Select(Device => { Device.FirmwareUpdateStatus = "ready"; return Device; }).ToList();
            this.DeviceBindingSource.DataSource = m_Devices;
            Task.Factory.StartNew(() => { DoUpdate(); }, TaskCreationOptions.LongRunning);
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            if (m_UpdateThread != null)
            {
                m_UpdateThread.Abort();
                FtpServer.Instance.NewDataConnection -= FtpServer_NewDataConnection;
                FtpServer.Instance.ClosedDataConnection -= FtpServer_ClosedDataConnection;
                FtpServer.Instance.SentData -= FtpServer_SentData;
            }
            this.Close();
        }

        private static void GenerateXmlFile(Device Device, string filePath)
        {
            XDocument doc = new XDocument(
                    new XDeclaration("1.0", "UTF-8", "yes"),
                    new XElement("CardList", new XAttribute("ver", "1.0")));
            using (var db = new ICMDBContext())
            {
                var maps = (from m in db.Icmaps
                            join d in db.Devices on m.C_entrancedoor equals d.roomid
                            where m.C_entrancedoor == Device.roomid
                            select new { DeviceId = d.roomid, DeviceType = d.type, CardNumber = m.C_icno }).ToArray();
                XElement xElement = new XElement("dev", new XAttribute("ty", Device.type), new XAttribute("ro", Device.roomid));
                doc.Element("CardList").Add(xElement);
                foreach (var map in maps)
                {
                    xElement.Add(new XElement("card", map.CardNumber));
                }
                doc.Save(filePath);
            }
        }

        private async void DoUpdate()
        {
            AddLog("Enter Update Thread");
            m_UpdateThread = Thread.CurrentThread;
            FtpServer.Instance.NewDataConnection += FtpServer_NewDataConnection;
            FtpServer.Instance.ClosedDataConnection += FtpServer_ClosedDataConnection;
            FtpServer.Instance.SentData += FtpServer_SentData;

            for (int i = 0; i < m_Devices.Count(); )
            {
                lock (m_FtpTransferStatus)
                {
                    if (m_FtpTransferStatus.Count() >= 5)
                    {
                        List<FtpTransferStatus> toBeRemoved = new List<FtpTransferStatus>();
                        foreach (var status in m_FtpTransferStatus)
                        {
                            if (status.Status != FtpTransferStatus.STATUS.LOAD_ING)
                                toBeRemoved.Add(status);
                        }

                        foreach (var status in toBeRemoved)
                        {
                            m_FtpTransferStatus.Remove(status);
                        }
                    }

                    if (m_FtpTransferStatus.Count() >= 5)
                        continue;
                }

                Device Device = m_Devices[i];
                AddLog(string.Format("Generate XML File for ({0}:{1})", Device.roomid, Device.ip));
                string TempFolderPath = Path.GetCardListTempFolderPath(Device.roomid);
                GenerateXmlFile(Device, TempFolderPath + @"\cardlist.xml");
                string pkgFilePath = Path.GetCardListFolderRelativePath(Device.roomid) + @"\CARD.PKG";
                //传入参数 目录为\\导致CMD命令执行失败。
                PkgTool.ToPkgFile(TempFolderPath, (Path.GetCardListFolderPath(Device.roomid) + @"\CARD.PKG"));
                AddLog(string.Format("HTTP ({0}): Sending Upgrade Command", Device.ip));

                FtpTransferStatus transferStatus = new FtpTransferStatus(Device, i, pkgFilePath);
                var result = await ICMServer.Net.HttpClient.SendCardListUpgradeNotification(Device.ip, Config.Instance.LocalIP, pkgFilePath);
                //string uri = string.Format("http://{0}/dev/info.cgi?action=upgrade_cardlist&url=ftp://{1}/{2}",
                //    Device.ip, Config.Instance.LocalIP, pkgFilePath.Replace(@"\", "/"));
                //System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
                //client.Timeout = new TimeSpan(0, 0, 1);
                try
                {
                    if (result)
                    {
                        //var response = await client.GetStringAsync(uri);
                        AddLog(string.Format("HTTP ({0}): Send Upgrade success!", Device.ip));
                        SetStatusColumnText(i, "0%", Color.Black);  // TODO: Progress bar
                    }
                    else
                    {
                        transferStatus.Status = FtpTransferStatus.STATUS.LOAD_ERROR;
                        AddLog(string.Format("HTTP ({0}): Send Upgrade fail!", Device.ip));
                        SetStatusColumnText(i, "send upgrade command fail!", Color.Red);
                    }
                }
                catch (Exception) { }

                lock (m_FtpTransferStatus)
                {
                    m_FtpTransferStatus.Add(transferStatus);
                }
                ++i;
            }
            AddLog("Leave Update Thread");
        }

        void FtpServer_SentData(object sender, FtpSentDataEventArgs e)
        {
            using (var db = new ICMDBContext())
            {
                lock (m_FtpTransferStatus)
                {
                    var status = (from s in m_FtpTransferStatus
                                  where s.Device.ip == e.Remote.Address.ToString()
                                  select s).FirstOrDefault();
                    if (status != null && status.Status == FtpTransferStatus.STATUS.LOAD_ING)
                    {
                        status.FileSize = e.FileSize;
                        status.SentBytes = e.SentBytes;
                        if (status.FileSize != 0)
                        {
                            int percentage = (int)(status.SentBytes * 100 / status.FileSize);
                            if (status.SentPercentage != percentage)
                            {
                                status.SentPercentage = percentage;
                                SetStatusColumnText(status.DeviceIndex, status.SentPercentage + "%", Color.Black);  // TODO: Progress bar
                            }
                        }
                        AddLog(string.Format("FTP ({0}): Data Transfer Send ({1} / {2}) bytes", e.Remote.Address, e.SentBytes, e.FileSize));
                    }
                }
            }
        }

        void FtpServer_ClosedDataConnection(object sender, FtpDataConnectionEventArgs e)
        {
            using (var db = new ICMDBContext())
            {
                lock (m_FtpTransferStatus)
                {
                    var status = (from s in m_FtpTransferStatus
                                  where s.Device.ip == e.Remote.Address.ToString()
                                  select s).FirstOrDefault();
                    if (status != null && status.Status == FtpTransferStatus.STATUS.LOAD_ING)
                    {
                        status.Status = (status.FileSize != 0 && status.FileSize == status.SentBytes)
                                      ? FtpTransferStatus.STATUS.LOAD_SUCESS
                                      : FtpTransferStatus.STATUS.LOAD_ERROR;

                        AddLog(string.Format("FTP ({0}): Data Transfer End", e.Remote.Address));
                    }
                }
            }
        }

        void FtpServer_NewDataConnection(object sender, FtpDataConnectionEventArgs e)
        {
            using (var db = new ICMDBContext())
            {
                lock (m_FtpTransferStatus)
                {
                    var status = (from s in m_FtpTransferStatus
                                  where s.Device.ip == e.Remote.Address.ToString()
                                  select s).FirstOrDefault();
                    if (status != null && status.Status == FtpTransferStatus.STATUS.LOAD_ING)
                    {
                        AddLog(string.Format("FTP ({0}): Data Transfer Begin", e.Remote.Address));
                    }
                }
            }
        }

        private readonly string[] m_DeviceTypes = 
        {
            "服务器",
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
            if (e.ColumnIndex == dgvTxtBoxColDeviceId.Index)
            {
                string DeviceId = (string)e.Value;
                e.Value = DevicesAddressConverter.RoToChStr(DeviceId);
            }
            else if (e.ColumnIndex == dgvTxtBoxColDeviceType.Index)
            {
                int DeviceType = (int)e.Value;
                if (DeviceType < m_DeviceTypes.Count())
                {
                    e.Value = m_DeviceTypes[DeviceType];
                }
            }
            else if (e.ColumnIndex == dgvTxtBoxColUpdateStatus.Index)
            {
                string status = e.Value as string;
                if (status == null || status == "")
                {
                    e.Value = "ready";
                }
            }
        }

        private void AddLog(string format, params object[] args)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((Action)(() => { AddLog(format, args); }));
            }
            else
            {
                listBoxLog.Items.Add(string.Format("[{0}]: {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), string.Format(format, args)));
                listBoxLog.SelectedIndex = listBoxLog.Items.Count - 1;
            }
        }

        private void SetStatusColumnText(int rowIndex, string message, Color foreColor)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((Action)(() => { SetStatusColumnText(rowIndex, message, foreColor); }));
            }
            else
            {
                var cell = DeviceDataGridView.Rows[rowIndex].Cells[dgvTxtBoxColUpdateStatus.Index];
                cell.Value = message;
                cell.Style.ForeColor = foreColor;
            }
        }

        class FtpTransferStatus
        {
            public enum STATUS
            {
                LOAD_ING,
                LOAD_ERROR,
                LOAD_SUCESS
            }
            public Device Device { get; private set; }
            public int DeviceIndex { get; private set; }
            public string FilePath { get; private set; }
            public long FileSize { get; set; }
            public long SentBytes { get; set; }
            public int SentPercentage { get; set; }
            public STATUS Status { get; set; }

            public FtpTransferStatus(Device Device, int DeviceIndex, string filePath)
            {
                this.Device = Device;
                this.DeviceIndex = DeviceIndex;
                this.FilePath = filePath;
                this.FileSize = 0;
                this.SentBytes = 0;
                this.SentPercentage = 0;
                this.Status = STATUS.LOAD_ING;
            }
        }
    }

    //public class DataGridViewProgressColumn : DataGridViewImageColumn
    //{
    //    public DataGridViewProgressColumn()
    //    {
    //        CellTemplate = new DataGridViewProgressCell();
    //    }
    //}

    //class DataGridViewProgressCell : DataGridViewImageCell
    //{
    //    // Used to make custom cell consistent with a DataGridViewImageCell
    //    static Image emptyImage;

    //    static DataGridViewProgressCell()
    //    {
    //        emptyImage = new Bitmap(1, 1, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
    //    }

    //    public DataGridViewProgressCell()
    //    {
    //        this.ValueType = typeof(int);
    //    }

    //    // Method required to make the Progress Cell consistent with the default Image Cell. 
    //    // The default Image Cell assumes an Image as a value, although the value of the Progress Cell is an int.
    //    protected override object GetFormattedValue(
    //        object value,
    //        int rowIndex, ref DataGridViewCellStyle cellStyle,
    //        TypeConverter valueTypeConverter,
    //        TypeConverter formattedValueTypeConverter,
    //        DataGridViewDataErrorContexts context)
    //    {
    //        return emptyImage;
    //    }

    //    protected override void Paint(System.Drawing.Graphics g, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
    //    {
    //        try
    //        {
    //            int progressVal = (int)value;
    //            float percentage = ((float)progressVal / 100.0f); // Need to convert to float before division; otherwise C# returns int which is 0 for anything but 100%.
    //            Brush backColorBrush = new SolidBrush(cellStyle.BackColor);
    //            Brush foreColorBrush = new SolidBrush(cellStyle.ForeColor);
    //            // Draws the cell grid
    //            base.Paint(g, clipBounds, cellBounds,
    //                rowIndex, cellState, value, formattedValue, errorText,
    //                cellStyle, advancedBorderStyle, (paintParts & ~DataGridViewPaintParts.ContentForeground));
    //            if (percentage > 0.0)
    //            {
    //                // Draw the progress bar and the text
    //                g.FillRectangle(new SolidBrush(Color.FromArgb(203, 235, 108)), cellBounds.X + 2, cellBounds.Y + 2, Convert.ToInt32((percentage * cellBounds.Width - 4)), cellBounds.Height - 4);
    //                g.DrawString(progressVal.ToString() + "%", cellStyle.Font, foreColorBrush, cellBounds.X + (cellBounds.Width / 2) - 5, cellBounds.Y + 2);

    //            }
    //            else
    //            {
    //                // draw the text
    //                if (this.DataGridView.CurrentRow.Index == rowIndex)
    //                    g.DrawString(progressVal.ToString() + "%", cellStyle.Font, new SolidBrush(cellStyle.SelectionForeColor), cellBounds.X + 6, cellBounds.Y + 2);
    //                else
    //                    g.DrawString(progressVal.ToString() + "%", cellStyle.Font, foreColorBrush, cellBounds.X + 6, cellBounds.Y + 2);
    //            }
    //        }
    //        catch (Exception) { }
    //    }
    //}
}
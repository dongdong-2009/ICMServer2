using ICMServer.Models;
using ICMServer.Net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace ICMServer
{
    public partial class DialogUpdateAddressBookBox : Form
    {
        List<Device> m_Devices;
        Thread m_UpdateThread;

        List<FtpTransferStatus> m_FtpTransferStatus = new List<FtpTransferStatus>();

        public DialogUpdateAddressBookBox(List<Device> Devices)
        {
            InitializeComponent();
            m_Devices = new List<Device>();
            m_Devices.AddRange(Devices);
        }

        private void DialogUpdateCardListBox_Load(object sender, EventArgs e)
        {
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

        private bool ExportXml(string xmlFilePath, AddrList.DevDataTable Devices)
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
                        Devices.WriteXml(writer);
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

        /// <summary>
        /// 输出 XML file
        /// </summary>
        /// <param name="xmlFilePath">要输出的 XML file 路径</param>
        /// <returns>true if success, else return false</returns>
        private bool Export4BytesAlignXml(string xmlInputFilePath, string xmlOutputFilePath)
        {
            bool result = false;
            try
            {
                byte[] data = File.ReadAllBytes(xmlInputFilePath);
                if (data.Count() % 4 != 0)
                {
                    Array.Resize(ref data, (data.Count() / 4 + 1) * 4);
                }
                File.WriteAllBytes(xmlOutputFilePath, data);
                result = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "操作错误",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return result;
        }

        /// <summary>
        /// 输出多個 XML files，每個 XML file 最多只包含 2 萬笔资料
        /// </summary>
        /// <param name="xmlFilePath">要输出的 XML file 路径</param>
        /// <returns>成功输出的 XML file path list</returns>
        private List<string> ExportMultiple4BytesAlignXmls(
            string xmlOutputFolderPath, 
            AddrList.DevDataTable Devices)
        {
            const int SEGMENT_SIZE = 20000;
            List<string> files = new List<string>();
            if (Devices.Count() > 0)
            {
                int fileCount = (Devices.Count() - 1) / SEGMENT_SIZE + 1;
                int devIndex = 0;
                for (int i = 0; i < fileCount; ++i)
                {
                    string xmlFilePath = (i == 0)
                        ? (xmlOutputFolderPath + @"\addressbook.xml")
                        : (xmlOutputFolderPath + @"\addressbook" + i + ".xml");

                    AddrList ds = new AddrList();
                    int devCount = Math.Min(SEGMENT_SIZE, Devices.Count() - i * SEGMENT_SIZE);
                    for (int j = 0; j < devCount; ++j)
                    {
                        ds.dev.ImportRow(Devices[devIndex++]);
                    }
                    if (ExportXml(xmlFilePath, ds.dev) && Export4BytesAlignXml(xmlFilePath, xmlFilePath))
                    {
                        files.Add(xmlFilePath);
                    }
                }
            }
            return files;
        }

        private void ExportAddressBookXmlPkg(string pkgFilePath, AddrList.DevDataTable Devices)
        {
            string xmlFilePath = Path.GetAddressBookTempXmlFilePath();
            if (ExportXml(xmlFilePath, Devices))
            {
                try
                {
                    PkgTool.ToPkgFile(System.IO.Path.GetDirectoryName(xmlFilePath), pkgFilePath);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    File.Delete(xmlFilePath);
                }
            }
        }

        private void ExportAddressBookUclPkg(string pkgFilePath, AddrList.DevDataTable Devices)
        {
            List<string> xmlFilesPath = ExportMultiple4BytesAlignXmls(
                Path.GetAddressBookTempFolderPath(),
                Devices);
            List<string> uclFilesPath = new List<string>();
            string uclTempFolderPath = Path.GetAddressBookUclTempFolderPath();
            int count = xmlFilesPath.Count;
            for (int i = 0; i < count; ++i)
            {
                string xmlFilePath = xmlFilesPath[i];
                string uclFilePath = (i == 0)
                ? (uclTempFolderPath + @"\addressbook.ucl")
                : (uclTempFolderPath + @"\addressbook" + i + ".ucl");

                try
                {
                    if (false == FileConverter.Xml2Ucl(xmlFilePath, uclFilePath))
                        throw new Exception("UCL无法導出");
                    uclFilesPath.Add(uclFilePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "操作错误",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (uclFilesPath.Count > 0)
            {
                PkgTool.ToPkgFile(uclTempFolderPath, pkgFilePath);
            }

            foreach (string filePath in xmlFilesPath)
                File.Delete(filePath);
            foreach (string filePath in uclFilesPath)
                File.Delete(filePath);
        }

        private void ExportXmlAddressBook()
        {
            using (var db = new ICMDBContext())
            {
                AddrList addrList = DeviceToAddrList();

                string filePath = Path.GetAddressBookTempXmlFilePath();
                ExportXml(filePath, addrList.dev);
                AddLog("已输出至 {0}", Path.GetAddressBookTempXmlFilePath());
            }
        }

        private void DeleteFolder(string folderPath)
        {
            try
            {
                System.IO.DirectoryInfo di = new DirectoryInfo(folderPath);

                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }
            }
            catch (Exception) { }
        }

        private string ExportAddressBookPkg()
        {
            // 旧版作法，生成 xml 后转成 pkg
            string xmlFilePath = Path.GetAddressBookTempXmlFilePath();
            string TempFolderPath = Path.GetAddressBookTempFolderPath();
            //if (!File.Exists(xmlFilePath))
            {
                AddLog("XML File 不存在, 生成 XML File 中...");
                ExportXmlAddressBook();
            }

            string pkgFilePath = Path.GetAddressBookPkgFilePath();
            PkgTool.ToPkgFile(TempFolderPath, pkgFilePath);

            return pkgFilePath;
        }

        private AddrList DeviceToAddrList()
        {
            AddrList addrList = new AddrList();
            using (var db = new ICMDBContext())
            {
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
                }
                addrList.AcceptChanges();
            }
            return addrList;
        }

        private string ExportAddressBookUclPkg()
        {
            AddrList addrList = DeviceToAddrList();

            string pkgFilePath = Path.GetAddressBookPkgFilePath();
            ExportAddressBookUclPkg(pkgFilePath, addrList.dev);

            return pkgFilePath;
        }
        

        private async void DoUpdate()
        {
            AddLog("Enter Update Thread");
            m_UpdateThread = Thread.CurrentThread;
            FtpServer.Instance.NewDataConnection += FtpServer_NewDataConnection;
            FtpServer.Instance.ClosedDataConnection += FtpServer_ClosedDataConnection;
            FtpServer.Instance.SentData += FtpServer_SentData;

            string pkgFilePath = ExportAddressBookUclPkg();//ExportAddressBookPkg();

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
                //AddLog(string.Format("Generate XML File for ({0}:{1})", Device.roomid, Device.ip));
                //string TempFolderPath = Path.GetCardListTempFolderPath(Device.roomid);
                //GenerateXmlFile(Device, TempFolderPath + @"\cardlist.xml");
                //string pkgFilePath = Path.GetCardListFolderRelativePath(Device.roomid) + @"\CARD.PKG";
                //PkgTool.ToPkgFile(TempFolderPath, Path.GetCardListFolderPath(Device.roomid) + @"\CARD.PKG");
                AddLog(string.Format("HTTP ({0}): Sending Upgrade Command", Device.ip));

                FtpTransferStatus transferStatus = new FtpTransferStatus(Device, i, pkgFilePath);
                //string uri = string.Format("http://{0}/dev/info.cgi?action=upgrade_addressbook&url=ftp://{1}/{2}",
                //    Device.ip, Config.Instance.LocalIP, pkgFilePath.Replace(@"\", "/"));
                //System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
                //client.Timeout = new TimeSpan(0, 0, 1);
                var result = await ICMServer.Net.HttpClient.SendAddressBookUpgradeNotification(Device.ip, Config.Instance.LocalIP, pkgFilePath);
                try
                {
                    if (result)
                    {
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
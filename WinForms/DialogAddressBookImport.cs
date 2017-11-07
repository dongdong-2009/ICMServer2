using ICMServer.Models;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace ICMServer
{
    public partial class DialogAddressBookImport : Form
    {
        AddrList addrList = new AddrList();

        public DialogAddressBookImport()
        {
            InitializeComponent();
            SetControls();
        }

        private void BtnSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgFile = new OpenFileDialog
            {
                Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*",
                FileName = "addressbook.xml"
            };
            if (dlgFile.ShowDialog() == DialogResult.OK)
            {
                textBoxPath.Text += dlgFile.FileName;
            }
            SetControls();
        }

        private void SetControls()
        {
            BtnStart.Enabled = textBoxPath.Text.Length > 0;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ReportProgress(int value)
        {
            progressBar.Value = value;
            labelPercent.Text = value.ToString() + "%";
        }

        private async void BtnStart_Click(object sender, EventArgs e)
        {
            Button thisButton = sender as Button;
            thisButton.Enabled = false;
            BtnClose.Enabled = false;
            BtnSelecFile.Enabled = false;
            try
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                addrList.Clear();
                addrList.dev.BeginLoadData();
                addrList.dev.ReadXml(textBoxPath.Text);
                addrList.dev.EndLoadData();
                stopwatch.Stop();
                //DebugLog.TraceMessage(string.Format("done: {0}", stopwatch.Elapsed));
                var progressIndicator = new Progress<int>(ReportProgress);
                await ICMDBContext.AddDevicesAsync(addrList, progressIndicator);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "操作错误",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            thisButton.Enabled = true;
            BtnClose.Enabled = true;
            BtnSelecFile.Enabled = true;
            this.Close();
        }
    }
}

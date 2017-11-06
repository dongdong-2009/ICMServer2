namespace ICMServer
{
    partial class DialogUpdateAddressBookBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogUpdateAddressBookBox));
            this.DeviceDataGridView = new System.Windows.Forms.DataGridView();
            this.dgvTxtBoxColIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTxtBoxColDeviceType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTxtBoxColDeviceId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTxtBoxColUpdateStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTxtBoxColSoftwareVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeviceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBoxLog = new System.Windows.Forms.GroupBox();
            this.BtnClose = new System.Windows.Forms.Button();
            this.listBoxLog = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.DeviceDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeviceBindingSource)).BeginInit();
            this.groupBoxLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // DeviceDataGridView
            // 
            resources.ApplyResources(this.DeviceDataGridView, "DeviceDataGridView");
            this.DeviceDataGridView.AllowUserToAddRows = false;
            this.DeviceDataGridView.AllowUserToDeleteRows = false;
            this.DeviceDataGridView.AutoGenerateColumns = false;
            this.DeviceDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DeviceDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvTxtBoxColIP,
            this.dgvTxtBoxColDeviceType,
            this.dgvTxtBoxColDeviceId,
            this.dgvTxtBoxColUpdateStatus,
            this.dgvTxtBoxColSoftwareVersion});
            this.DeviceDataGridView.DataSource = this.DeviceBindingSource;
            this.DeviceDataGridView.Name = "DeviceDataGridView";
            this.DeviceDataGridView.ReadOnly = true;
            this.DeviceDataGridView.RowTemplate.Height = 24;
            this.DeviceDataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DeviceDataGridView_CellFormatting);
            // 
            // dgvTxtBoxColIP
            // 
            this.dgvTxtBoxColIP.DataPropertyName = "ip";
            resources.ApplyResources(this.dgvTxtBoxColIP, "dgvTxtBoxColIP");
            this.dgvTxtBoxColIP.Name = "dgvTxtBoxColIP";
            this.dgvTxtBoxColIP.ReadOnly = true;
            // 
            // dgvTxtBoxColDeviceType
            // 
            this.dgvTxtBoxColDeviceType.DataPropertyName = "type";
            resources.ApplyResources(this.dgvTxtBoxColDeviceType, "dgvTxtBoxColDeviceType");
            this.dgvTxtBoxColDeviceType.Name = "dgvTxtBoxColDeviceType";
            this.dgvTxtBoxColDeviceType.ReadOnly = true;
            // 
            // dgvTxtBoxColDeviceId
            // 
            this.dgvTxtBoxColDeviceId.DataPropertyName = "roomid";
            resources.ApplyResources(this.dgvTxtBoxColDeviceId, "dgvTxtBoxColDeviceId");
            this.dgvTxtBoxColDeviceId.Name = "dgvTxtBoxColDeviceId";
            this.dgvTxtBoxColDeviceId.ReadOnly = true;
            // 
            // dgvTxtBoxColUpdateStatus
            // 
            resources.ApplyResources(this.dgvTxtBoxColUpdateStatus, "dgvTxtBoxColUpdateStatus");
            this.dgvTxtBoxColUpdateStatus.Name = "dgvTxtBoxColUpdateStatus";
            this.dgvTxtBoxColUpdateStatus.ReadOnly = true;
            // 
            // dgvTxtBoxColSoftwareVersion
            // 
            this.dgvTxtBoxColSoftwareVersion.DataPropertyName = "cVer";
            resources.ApplyResources(this.dgvTxtBoxColSoftwareVersion, "dgvTxtBoxColSoftwareVersion");
            this.dgvTxtBoxColSoftwareVersion.Name = "dgvTxtBoxColSoftwareVersion";
            this.dgvTxtBoxColSoftwareVersion.ReadOnly = true;
            // 
            // DeviceBindingSource
            // 
            this.DeviceBindingSource.DataSource = typeof(ICMServer.Models.Device);
            // 
            // groupBoxLog
            // 
            resources.ApplyResources(this.groupBoxLog, "groupBoxLog");
            this.groupBoxLog.Controls.Add(this.BtnClose);
            this.groupBoxLog.Controls.Add(this.listBoxLog);
            this.groupBoxLog.Name = "groupBoxLog";
            this.groupBoxLog.TabStop = false;
            // 
            // BtnClose
            // 
            resources.ApplyResources(this.BtnClose, "BtnClose");
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // listBoxLog
            // 
            resources.ApplyResources(this.listBoxLog, "listBoxLog");
            this.listBoxLog.FormattingEnabled = true;
            this.listBoxLog.Name = "listBoxLog";
            // 
            // DialogUpdateAddressBookBox
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.groupBoxLog);
            this.Controls.Add(this.DeviceDataGridView);
            this.Name = "DialogUpdateAddressBookBox";
            this.Load += new System.EventHandler(this.DialogUpdateCardListBox_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DeviceDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeviceBindingSource)).EndInit();
            this.groupBoxLog.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource DeviceBindingSource;
        private System.Windows.Forms.DataGridView DeviceDataGridView;
        private System.Windows.Forms.GroupBox groupBoxLog;
        private System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.ListBox listBoxLog;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTxtBoxColIP;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTxtBoxColDeviceType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTxtBoxColDeviceId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTxtBoxColUpdateStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTxtBoxColSoftwareVersion;
    }
}
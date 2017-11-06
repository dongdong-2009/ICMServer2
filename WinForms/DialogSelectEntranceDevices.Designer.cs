namespace ICMServer
{
    partial class DialogSelectEntranceDevices
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogSelectEntranceDevices));
            this.DeviceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DeviceDataGridView = new System.Windows.Forms.DataGridView();
            this.BtnStart = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.dgvTextBoxDeviceIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTextBoxDeviceType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTextBoxDeviceId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTextBoxCardlistVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DeviceBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeviceDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // DeviceBindingSource
            // 
            this.DeviceBindingSource.DataSource = typeof(ICMServer.Models.Device);
            // 
            // DeviceDataGridView
            // 
            resources.ApplyResources(this.DeviceDataGridView, "DeviceDataGridView");
            this.DeviceDataGridView.AllowUserToAddRows = false;
            this.DeviceDataGridView.AllowUserToDeleteRows = false;
            this.DeviceDataGridView.AutoGenerateColumns = false;
            this.DeviceDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DeviceDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvTextBoxDeviceIP,
            this.dgvTextBoxDeviceType,
            this.dgvTextBoxDeviceId,
            this.dgvTextBoxCardlistVersion});
            this.DeviceDataGridView.DataSource = this.DeviceBindingSource;
            this.DeviceDataGridView.Name = "DeviceDataGridView";
            this.DeviceDataGridView.ReadOnly = true;
            this.DeviceDataGridView.RowTemplate.Height = 24;
            this.DeviceDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DeviceDataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DeviceDataGridView_CellFormatting);
            // 
            // BtnStart
            // 
            resources.ApplyResources(this.BtnStart, "BtnStart");
            this.BtnStart.Name = "BtnStart";
            this.BtnStart.UseVisualStyleBackColor = true;
            this.BtnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // BtnCancel
            // 
            resources.ApplyResources(this.BtnCancel, "BtnCancel");
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // dgvTextBoxDeviceIP
            // 
            this.dgvTextBoxDeviceIP.DataPropertyName = "ip";
            resources.ApplyResources(this.dgvTextBoxDeviceIP, "dgvTextBoxDeviceIP");
            this.dgvTextBoxDeviceIP.Name = "dgvTextBoxDeviceIP";
            this.dgvTextBoxDeviceIP.ReadOnly = true;
            // 
            // dgvTextBoxDeviceType
            // 
            this.dgvTextBoxDeviceType.DataPropertyName = "type";
            resources.ApplyResources(this.dgvTextBoxDeviceType, "dgvTextBoxDeviceType");
            this.dgvTextBoxDeviceType.Name = "dgvTextBoxDeviceType";
            this.dgvTextBoxDeviceType.ReadOnly = true;
            // 
            // dgvTextBoxDeviceId
            // 
            this.dgvTextBoxDeviceId.DataPropertyName = "roomid";
            resources.ApplyResources(this.dgvTextBoxDeviceId, "dgvTextBoxDeviceId");
            this.dgvTextBoxDeviceId.Name = "dgvTextBoxDeviceId";
            this.dgvTextBoxDeviceId.ReadOnly = true;
            // 
            // dgvTextBoxCardlistVersion
            // 
            this.dgvTextBoxCardlistVersion.DataPropertyName = "cVer";
            resources.ApplyResources(this.dgvTextBoxCardlistVersion, "dgvTextBoxCardlistVersion");
            this.dgvTextBoxCardlistVersion.Name = "dgvTextBoxCardlistVersion";
            this.dgvTextBoxCardlistVersion.ReadOnly = true;
            // 
            // DialogSelectEntranceDevices
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnStart);
            this.Controls.Add(this.DeviceDataGridView);
            this.Name = "DialogSelectEntranceDevices";
            this.Load += new System.EventHandler(this.DialogSelectEntranceDevices_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DeviceBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeviceDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource DeviceBindingSource;
        private System.Windows.Forms.DataGridView DeviceDataGridView;
        private System.Windows.Forms.Button BtnStart;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTextBoxDeviceIP;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTextBoxDeviceType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTextBoxDeviceId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTextBoxCardlistVersion;
    }
}
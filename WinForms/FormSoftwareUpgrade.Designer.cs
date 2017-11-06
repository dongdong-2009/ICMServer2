namespace ICMServer
{
    partial class FormSoftwareUpgrade
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSoftwareUpgrade));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BtnResetToDefault = new System.Windows.Forms.Button();
            this.BtnDeleteFile = new System.Windows.Forms.Button();
            this.BtnUpgrade = new System.Windows.Forms.Button();
            this.BtnUploadFile = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBoxFtpRootDir = new System.Windows.Forms.TextBox();
            this.upgradeDataGridView = new System.Windows.Forms.DataGridView();
            this.upgradeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dgvTxtBoxColVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTxtBoxColFileType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTxtBoxColDeviceType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTxtBoxColUploadTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTxtBoxColFilePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvChkBoxColIsDefault = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.upgradeDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.upgradeBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.BtnResetToDefault);
            this.groupBox1.Controls.Add(this.BtnDeleteFile);
            this.groupBox1.Controls.Add(this.BtnUpgrade);
            this.groupBox1.Controls.Add(this.BtnUploadFile);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // BtnResetToDefault
            // 
            resources.ApplyResources(this.BtnResetToDefault, "BtnResetToDefault");
            this.BtnResetToDefault.Name = "BtnResetToDefault";
            this.BtnResetToDefault.UseVisualStyleBackColor = true;
            this.BtnResetToDefault.Click += new System.EventHandler(this.BtnResetToDefault_Click);
            // 
            // BtnDeleteFile
            // 
            resources.ApplyResources(this.BtnDeleteFile, "BtnDeleteFile");
            this.BtnDeleteFile.Name = "BtnDeleteFile";
            this.BtnDeleteFile.UseVisualStyleBackColor = true;
            this.BtnDeleteFile.Click += new System.EventHandler(this.BtnDeleteFile_Click);
            // 
            // BtnUpgrade
            // 
            resources.ApplyResources(this.BtnUpgrade, "BtnUpgrade");
            this.BtnUpgrade.Name = "BtnUpgrade";
            this.BtnUpgrade.UseVisualStyleBackColor = true;
            this.BtnUpgrade.Click += new System.EventHandler(this.BtnUpgrade_Click);
            // 
            // BtnUploadFile
            // 
            resources.ApplyResources(this.BtnUploadFile, "BtnUploadFile");
            this.BtnUploadFile.Name = "BtnUploadFile";
            this.BtnUploadFile.UseVisualStyleBackColor = true;
            this.BtnUploadFile.Click += new System.EventHandler(this.BtnUploadFile_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // txtBoxFtpRootDir
            // 
            resources.ApplyResources(this.txtBoxFtpRootDir, "txtBoxFtpRootDir");
            this.txtBoxFtpRootDir.Name = "txtBoxFtpRootDir";
            // 
            // upgradeDataGridView
            // 
            resources.ApplyResources(this.upgradeDataGridView, "upgradeDataGridView");
            this.upgradeDataGridView.AllowUserToAddRows = false;
            this.upgradeDataGridView.AutoGenerateColumns = false;
            this.upgradeDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.upgradeDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvTxtBoxColVersion,
            this.dgvTxtBoxColFileType,
            this.dgvTxtBoxColDeviceType,
            this.dgvTxtBoxColUploadTime,
            this.dgvTxtBoxColFilePath,
            this.dgvChkBoxColIsDefault});
            this.upgradeDataGridView.DataSource = this.upgradeBindingSource;
            this.upgradeDataGridView.Name = "upgradeDataGridView";
            this.upgradeDataGridView.ReadOnly = true;
            this.upgradeDataGridView.RowTemplate.Height = 24;
            this.upgradeDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.upgradeDataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.UpgradeDataGridView_CellFormatting);
            // 
            // upgradeBindingSource
            // 
            this.upgradeBindingSource.DataSource = typeof(ICMServer.Models.upgrade);
            // 
            // dgvTxtBoxColVersion
            // 
            this.dgvTxtBoxColVersion.DataPropertyName = "version";
            resources.ApplyResources(this.dgvTxtBoxColVersion, "dgvTxtBoxColVersion");
            this.dgvTxtBoxColVersion.Name = "dgvTxtBoxColVersion";
            this.dgvTxtBoxColVersion.ReadOnly = true;
            // 
            // dgvTxtBoxColFileType
            // 
            this.dgvTxtBoxColFileType.DataPropertyName = "filetype";
            resources.ApplyResources(this.dgvTxtBoxColFileType, "dgvTxtBoxColFileType");
            this.dgvTxtBoxColFileType.Name = "dgvTxtBoxColFileType";
            this.dgvTxtBoxColFileType.ReadOnly = true;
            // 
            // dgvTxtBoxColDeviceType
            // 
            this.dgvTxtBoxColDeviceType.DataPropertyName = "Device_type";
            resources.ApplyResources(this.dgvTxtBoxColDeviceType, "dgvTxtBoxColDeviceType");
            this.dgvTxtBoxColDeviceType.Name = "dgvTxtBoxColDeviceType";
            this.dgvTxtBoxColDeviceType.ReadOnly = true;
            // 
            // dgvTxtBoxColUploadTime
            // 
            this.dgvTxtBoxColUploadTime.DataPropertyName = "time";
            resources.ApplyResources(this.dgvTxtBoxColUploadTime, "dgvTxtBoxColUploadTime");
            this.dgvTxtBoxColUploadTime.Name = "dgvTxtBoxColUploadTime";
            this.dgvTxtBoxColUploadTime.ReadOnly = true;
            // 
            // dgvTxtBoxColFilePath
            // 
            this.dgvTxtBoxColFilePath.DataPropertyName = "filepath";
            resources.ApplyResources(this.dgvTxtBoxColFilePath, "dgvTxtBoxColFilePath");
            this.dgvTxtBoxColFilePath.Name = "dgvTxtBoxColFilePath";
            this.dgvTxtBoxColFilePath.ReadOnly = true;
            // 
            // dgvChkBoxColIsDefault
            // 
            this.dgvChkBoxColIsDefault.DataPropertyName = "is_default";
            this.dgvChkBoxColIsDefault.FalseValue = "0";
            resources.ApplyResources(this.dgvChkBoxColIsDefault, "dgvChkBoxColIsDefault");
            this.dgvChkBoxColIsDefault.Name = "dgvChkBoxColIsDefault";
            this.dgvChkBoxColIsDefault.ReadOnly = true;
            this.dgvChkBoxColIsDefault.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvChkBoxColIsDefault.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dgvChkBoxColIsDefault.TrueValue = "1";
            // 
            // FormSoftwareUpgrade
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.upgradeDataGridView);
            this.Controls.Add(this.txtBoxFtpRootDir);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormSoftwareUpgrade";
            this.Load += new System.EventHandler(this.FormSoftwareUpgrade_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.upgradeDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.upgradeBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button BtnResetToDefault;
        private System.Windows.Forms.Button BtnDeleteFile;
        private System.Windows.Forms.Button BtnUpgrade;
        private System.Windows.Forms.Button BtnUploadFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBoxFtpRootDir;
        private System.Windows.Forms.BindingSource upgradeBindingSource;
        private System.Windows.Forms.DataGridView upgradeDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTxtBoxColVersion;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTxtBoxColFileType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTxtBoxColDeviceType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTxtBoxColUploadTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTxtBoxColFilePath;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvChkBoxColIsDefault;
    }
}
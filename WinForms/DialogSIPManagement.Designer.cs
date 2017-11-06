namespace ICMServer
{
    partial class DialogSIPManagement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogSIPManagement));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BtnSync = new System.Windows.Forms.Button();
            this.BtnQuery = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.BtnMod = new System.Windows.Forms.Button();
            this.BtnCheck = new System.Windows.Forms.Button();
            this.BtnDel = new System.Windows.Forms.Button();
            this.BtnClose = new System.Windows.Forms.Button();
            this.sipaccountBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sipaccountDataGridView = new System.Windows.Forms.DataGridView();
            this.dgvTxtBoxColName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTxtBoxColDeviceId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTxtBoxColGroup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBoxDeviceId = new DeviceAddressControlLib.DeviceAddressControl();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sipaccountBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sipaccountDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxDeviceId);
            this.groupBox1.Controls.Add(this.BtnSync);
            this.groupBox1.Controls.Add(this.BtnQuery);
            this.groupBox1.Controls.Add(this.label1);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // BtnSync
            // 
            resources.ApplyResources(this.BtnSync, "BtnSync");
            this.BtnSync.Name = "BtnSync";
            this.BtnSync.UseVisualStyleBackColor = true;
            // 
            // BtnQuery
            // 
            resources.ApplyResources(this.BtnQuery, "BtnQuery");
            this.BtnQuery.Name = "BtnQuery";
            this.BtnQuery.UseVisualStyleBackColor = true;
            this.BtnQuery.Click += new System.EventHandler(this.BtnQuery_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // BtnAdd
            // 
            resources.ApplyResources(this.BtnAdd, "BtnAdd");
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.UseVisualStyleBackColor = true;
            // 
            // BtnMod
            // 
            resources.ApplyResources(this.BtnMod, "BtnMod");
            this.BtnMod.Name = "BtnMod";
            this.BtnMod.UseVisualStyleBackColor = true;
            // 
            // BtnCheck
            // 
            resources.ApplyResources(this.BtnCheck, "BtnCheck");
            this.BtnCheck.Name = "BtnCheck";
            this.BtnCheck.UseVisualStyleBackColor = true;
            // 
            // BtnDel
            // 
            resources.ApplyResources(this.BtnDel, "BtnDel");
            this.BtnDel.Name = "BtnDel";
            this.BtnDel.UseVisualStyleBackColor = true;
            // 
            // BtnClose
            // 
            resources.ApplyResources(this.BtnClose, "BtnClose");
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // sipaccountBindingSource
            // 
            this.sipaccountBindingSource.DataSource = typeof(ICMServer.Models.sipaccount);
            // 
            // sipaccountDataGridView
            // 
            this.sipaccountDataGridView.AllowUserToAddRows = false;
            resources.ApplyResources(this.sipaccountDataGridView, "sipaccountDataGridView");
            this.sipaccountDataGridView.AutoGenerateColumns = false;
            this.sipaccountDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sipaccountDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvTxtBoxColName,
            this.dgvTxtBoxColDeviceId,
            this.dgvTxtBoxColGroup});
            this.sipaccountDataGridView.DataSource = this.sipaccountBindingSource;
            this.sipaccountDataGridView.Name = "sipaccountDataGridView";
            this.sipaccountDataGridView.ReadOnly = true;
            this.sipaccountDataGridView.RowTemplate.Height = 24;
            this.sipaccountDataGridView.VirtualMode = true;
            // 
            // dgvTxtBoxColName
            // 
            this.dgvTxtBoxColName.DataPropertyName = "C_user";
            resources.ApplyResources(this.dgvTxtBoxColName, "dgvTxtBoxColName");
            this.dgvTxtBoxColName.Name = "dgvTxtBoxColName";
            this.dgvTxtBoxColName.ReadOnly = true;
            // 
            // dgvTxtBoxColDeviceId
            // 
            this.dgvTxtBoxColDeviceId.DataPropertyName = "C_room";
            resources.ApplyResources(this.dgvTxtBoxColDeviceId, "dgvTxtBoxColDeviceId");
            this.dgvTxtBoxColDeviceId.Name = "dgvTxtBoxColDeviceId";
            this.dgvTxtBoxColDeviceId.ReadOnly = true;
            // 
            // dgvTxtBoxColGroup
            // 
            this.dgvTxtBoxColGroup.DataPropertyName = "C_usergroup";
            resources.ApplyResources(this.dgvTxtBoxColGroup, "dgvTxtBoxColGroup");
            this.dgvTxtBoxColGroup.Name = "dgvTxtBoxColGroup";
            this.dgvTxtBoxColGroup.ReadOnly = true;
            // 
            // textBoxDeviceId
            // 
            this.textBoxDeviceId.AllowInternalTab = false;
            this.textBoxDeviceId.AutoHeight = true;
            this.textBoxDeviceId.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxDeviceId.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.textBoxDeviceId.Cursor = System.Windows.Forms.Cursors.IBeam;
            resources.ApplyResources(this.textBoxDeviceId, "textBoxDeviceId");
            this.textBoxDeviceId.Name = "textBoxDeviceId";
            this.textBoxDeviceId.ReadOnly = false;
            // 
            // DialogSIPManagement
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.sipaccountDataGridView);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.BtnCheck);
            this.Controls.Add(this.BtnDel);
            this.Controls.Add(this.BtnMod);
            this.Controls.Add(this.BtnAdd);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "DialogSIPManagement";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sipaccountBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sipaccountDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button BtnSync;
        private System.Windows.Forms.Button BtnQuery;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.Button BtnMod;
        private System.Windows.Forms.Button BtnCheck;
        private System.Windows.Forms.Button BtnDel;
        private System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.BindingSource sipaccountBindingSource;
        private System.Windows.Forms.DataGridView sipaccountDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTxtBoxColName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTxtBoxColDeviceId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTxtBoxColGroup;
        private DeviceAddressControlLib.DeviceAddressControl textBoxDeviceId;
    }
}
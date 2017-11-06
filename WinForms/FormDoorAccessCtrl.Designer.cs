namespace ICMServer
{
    partial class FormDoorAccessCtrl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDoorAccessCtrl));
            this.BtnSearch = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BtnRefresh = new System.Windows.Forms.Button();
            this.BtnCardMod = new System.Windows.Forms.Button();
            this.BtnCardDel = new System.Windows.Forms.Button();
            this.BtnExport = new System.Windows.Forms.Button();
            this.BtnImport = new System.Windows.Forms.Button();
            this.BtnSyncMsg = new System.Windows.Forms.Button();
            this.BtnCardAdd = new System.Windows.Forms.Button();
            this.textBoxQueryName = new System.Windows.Forms.TextBox();
            this.textBoxQueryCard = new System.Windows.Forms.TextBox();
            this.textBoxQueryRo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.iccardDataGridView = new System.Windows.Forms.DataGridView();
            this.dgvTextBoxCardNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTextBoxUserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTextBoxRoomId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTextBoxCardType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTextBoxValidityStart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTextBoxValidityEnd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTextBoxEnabled = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iccardBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.icmapBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.icmapDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iccardDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iccardBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmapBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmapDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnSearch
            // 
            resources.ApplyResources(this.BtnSearch, "BtnSearch");
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.UseVisualStyleBackColor = true;
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.BtnRefresh);
            this.groupBox1.Controls.Add(this.BtnCardMod);
            this.groupBox1.Controls.Add(this.BtnCardDel);
            this.groupBox1.Controls.Add(this.BtnExport);
            this.groupBox1.Controls.Add(this.BtnImport);
            this.groupBox1.Controls.Add(this.BtnSyncMsg);
            this.groupBox1.Controls.Add(this.BtnCardAdd);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // BtnRefresh
            // 
            resources.ApplyResources(this.BtnRefresh, "BtnRefresh");
            this.BtnRefresh.Name = "BtnRefresh";
            this.BtnRefresh.UseVisualStyleBackColor = true;
            this.BtnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // BtnCardMod
            // 
            resources.ApplyResources(this.BtnCardMod, "BtnCardMod");
            this.BtnCardMod.Name = "BtnCardMod";
            this.BtnCardMod.UseVisualStyleBackColor = true;
            this.BtnCardMod.Click += new System.EventHandler(this.BtnCardMod_Click);
            // 
            // BtnCardDel
            // 
            resources.ApplyResources(this.BtnCardDel, "BtnCardDel");
            this.BtnCardDel.Name = "BtnCardDel";
            this.BtnCardDel.UseVisualStyleBackColor = true;
            this.BtnCardDel.Click += new System.EventHandler(this.BtnCardDel_Click);
            // 
            // BtnExport
            // 
            resources.ApplyResources(this.BtnExport, "BtnExport");
            this.BtnExport.Name = "BtnExport";
            this.BtnExport.UseVisualStyleBackColor = true;
            this.BtnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // BtnImport
            // 
            resources.ApplyResources(this.BtnImport, "BtnImport");
            this.BtnImport.Name = "BtnImport";
            this.BtnImport.UseVisualStyleBackColor = true;
            this.BtnImport.Click += new System.EventHandler(this.BtnImport_Click);
            // 
            // BtnSyncMsg
            // 
            resources.ApplyResources(this.BtnSyncMsg, "BtnSyncMsg");
            this.BtnSyncMsg.Name = "BtnSyncMsg";
            this.BtnSyncMsg.UseVisualStyleBackColor = true;
            this.BtnSyncMsg.Click += new System.EventHandler(this.BtnSyncMsg_Click);
            // 
            // BtnCardAdd
            // 
            resources.ApplyResources(this.BtnCardAdd, "BtnCardAdd");
            this.BtnCardAdd.Name = "BtnCardAdd";
            this.BtnCardAdd.UseVisualStyleBackColor = true;
            this.BtnCardAdd.Click += new System.EventHandler(this.BtnCardAdd_Click);
            // 
            // textBoxQueryName
            // 
            resources.ApplyResources(this.textBoxQueryName, "textBoxQueryName");
            this.textBoxQueryName.Name = "textBoxQueryName";
            // 
            // textBoxQueryCard
            // 
            resources.ApplyResources(this.textBoxQueryCard, "textBoxQueryCard");
            this.textBoxQueryCard.Name = "textBoxQueryCard";
            // 
            // textBoxQueryRo
            // 
            resources.ApplyResources(this.textBoxQueryRo, "textBoxQueryRo");
            this.textBoxQueryRo.Name = "textBoxQueryRo";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // iccardDataGridView
            // 
            this.iccardDataGridView.AllowUserToAddRows = false;
            this.iccardDataGridView.AllowUserToDeleteRows = false;
            resources.ApplyResources(this.iccardDataGridView, "iccardDataGridView");
            this.iccardDataGridView.AutoGenerateColumns = false;
            this.iccardDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.iccardDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvTextBoxCardNumber,
            this.dgvTextBoxUserName,
            this.dgvTextBoxRoomId,
            this.dgvTextBoxCardType,
            this.dgvTextBoxValidityStart,
            this.dgvTextBoxValidityEnd,
            this.dgvTextBoxEnabled});
            this.iccardDataGridView.DataSource = this.iccardBindingSource;
            this.iccardDataGridView.Name = "iccardDataGridView";
            this.iccardDataGridView.ReadOnly = true;
            this.iccardDataGridView.RowTemplate.Height = 24;
            this.iccardDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.iccardDataGridView.VirtualMode = true;
            this.iccardDataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.IccardDataGridView_CellFormatting);
            // 
            // dgvTextBoxCardNumber
            // 
            this.dgvTextBoxCardNumber.DataPropertyName = "C_icno";
            resources.ApplyResources(this.dgvTextBoxCardNumber, "dgvTextBoxCardNumber");
            this.dgvTextBoxCardNumber.Name = "dgvTextBoxCardNumber";
            this.dgvTextBoxCardNumber.ReadOnly = true;
            // 
            // dgvTextBoxUserName
            // 
            this.dgvTextBoxUserName.DataPropertyName = "C_username";
            resources.ApplyResources(this.dgvTextBoxUserName, "dgvTextBoxUserName");
            this.dgvTextBoxUserName.Name = "dgvTextBoxUserName";
            this.dgvTextBoxUserName.ReadOnly = true;
            // 
            // dgvTextBoxRoomId
            // 
            this.dgvTextBoxRoomId.DataPropertyName = "C_roomid";
            resources.ApplyResources(this.dgvTextBoxRoomId, "dgvTextBoxRoomId");
            this.dgvTextBoxRoomId.Name = "dgvTextBoxRoomId";
            this.dgvTextBoxRoomId.ReadOnly = true;
            // 
            // dgvTextBoxCardType
            // 
            this.dgvTextBoxCardType.DataPropertyName = "C_ictype";
            resources.ApplyResources(this.dgvTextBoxCardType, "dgvTextBoxCardType");
            this.dgvTextBoxCardType.Name = "dgvTextBoxCardType";
            this.dgvTextBoxCardType.ReadOnly = true;
            // 
            // dgvTextBoxValidityStart
            // 
            this.dgvTextBoxValidityStart.DataPropertyName = "C_uptime";
            resources.ApplyResources(this.dgvTextBoxValidityStart, "dgvTextBoxValidityStart");
            this.dgvTextBoxValidityStart.Name = "dgvTextBoxValidityStart";
            this.dgvTextBoxValidityStart.ReadOnly = true;
            // 
            // dgvTextBoxValidityEnd
            // 
            this.dgvTextBoxValidityEnd.DataPropertyName = "C_downtime";
            resources.ApplyResources(this.dgvTextBoxValidityEnd, "dgvTextBoxValidityEnd");
            this.dgvTextBoxValidityEnd.Name = "dgvTextBoxValidityEnd";
            this.dgvTextBoxValidityEnd.ReadOnly = true;
            // 
            // dgvTextBoxEnabled
            // 
            this.dgvTextBoxEnabled.DataPropertyName = "C_available";
            resources.ApplyResources(this.dgvTextBoxEnabled, "dgvTextBoxEnabled");
            this.dgvTextBoxEnabled.Name = "dgvTextBoxEnabled";
            this.dgvTextBoxEnabled.ReadOnly = true;
            // 
            // iccardBindingSource
            // 
            this.iccardBindingSource.DataSource = typeof(ICMServer.Models.Iccard);
            this.iccardBindingSource.CurrentChanged += new System.EventHandler(this.IccardBindingSource_CurrentChanged);
            // 
            // icmapBindingSource
            // 
            this.icmapBindingSource.DataSource = typeof(ICMServer.Models.icmap);
            // 
            // icmapDataGridView
            // 
            this.icmapDataGridView.AllowUserToAddRows = false;
            this.icmapDataGridView.AllowUserToDeleteRows = false;
            this.icmapDataGridView.AllowUserToResizeColumns = false;
            this.icmapDataGridView.AllowUserToResizeRows = false;
            resources.ApplyResources(this.icmapDataGridView, "icmapDataGridView");
            this.icmapDataGridView.AutoGenerateColumns = false;
            this.icmapDataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.icmapDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.icmapDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn3});
            this.icmapDataGridView.DataSource = this.icmapBindingSource;
            this.icmapDataGridView.GridColor = System.Drawing.SystemColors.Window;
            this.icmapDataGridView.MultiSelect = false;
            this.icmapDataGridView.Name = "icmapDataGridView";
            this.icmapDataGridView.ReadOnly = true;
            this.icmapDataGridView.RowHeadersVisible = false;
            this.icmapDataGridView.RowTemplate.Height = 24;
            this.icmapDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.icmapDataGridView.VirtualMode = true;
            this.icmapDataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.IcmapDataGridView_CellFormatting);
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "C_entrancedoor";
            resources.ApplyResources(this.dataGridViewTextBoxColumn3, "dataGridViewTextBoxColumn3");
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // FormDoorAccessCtrl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.icmapDataGridView);
            this.Controls.Add(this.iccardDataGridView);
            this.Controls.Add(this.textBoxQueryRo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxQueryCard);
            this.Controls.Add(this.textBoxQueryName);
            this.Controls.Add(this.BtnSearch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormDoorAccessCtrl";
            this.Load += new System.EventHandler(this.FormDoorAccessCtrl_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.iccardDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iccardBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmapBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icmapDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button BtnExport;
        private System.Windows.Forms.Button BtnImport;
        private System.Windows.Forms.Button BtnSyncMsg;
        private System.Windows.Forms.Button BtnCardAdd;
        private System.Windows.Forms.TextBox textBoxQueryName;
        private System.Windows.Forms.TextBox textBoxQueryCard;
        private System.Windows.Forms.TextBox textBoxQueryRo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button BtnCardMod;
        private System.Windows.Forms.Button BtnCardDel;
        private System.Windows.Forms.Button BtnRefresh;
        private System.Windows.Forms.BindingSource iccardBindingSource;
        private System.Windows.Forms.DataGridView iccardDataGridView;
        private System.Windows.Forms.BindingSource icmapBindingSource;
        private System.Windows.Forms.DataGridView icmapDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTextBoxCardNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTextBoxUserName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTextBoxRoomId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTextBoxCardType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTextBoxValidityStart;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTextBoxValidityEnd;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTextBoxEnabled;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    }
}
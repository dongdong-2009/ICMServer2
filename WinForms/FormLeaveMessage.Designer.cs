namespace ICMServer
{
    partial class FormLeaveMessage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLeaveMessage));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BtnRefresh = new System.Windows.Forms.Button();
            this.BtnPlay = new System.Windows.Forms.Button();
            this.BtnDel = new System.Windows.Forms.Button();
            this.BtnSearch = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePickerStart = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerEnd = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ComboBoxReadStatus = new System.Windows.Forms.ComboBox();
            this.dataGridViewLeaveMsgs = new System.Windows.Forms.DataGridView();
            this.srcaddrDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dstaddrDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.readflagDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LeaveMsgsbindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLeaveMsgs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LeaveMsgsbindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.BtnRefresh);
            this.groupBox1.Controls.Add(this.BtnPlay);
            this.groupBox1.Controls.Add(this.BtnDel);
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
            // BtnPlay
            // 
            resources.ApplyResources(this.BtnPlay, "BtnPlay");
            this.BtnPlay.Name = "BtnPlay";
            this.BtnPlay.UseVisualStyleBackColor = true;
            this.BtnPlay.Click += new System.EventHandler(this.BtnPlay_Click);
            // 
            // BtnDel
            // 
            resources.ApplyResources(this.BtnDel, "BtnDel");
            this.BtnDel.Name = "BtnDel";
            this.BtnDel.UseVisualStyleBackColor = true;
            this.BtnDel.Click += new System.EventHandler(this.BtnDel_Click);
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
            // dateTimePickerStart
            // 
            resources.ApplyResources(this.dateTimePickerStart, "dateTimePickerStart");
            this.dateTimePickerStart.Name = "dateTimePickerStart";
            // 
            // dateTimePickerEnd
            // 
            resources.ApplyResources(this.dateTimePickerEnd, "dateTimePickerEnd");
            this.dateTimePickerEnd.Name = "dateTimePickerEnd";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // ComboBoxReadStatus
            // 
            this.ComboBoxReadStatus.FormattingEnabled = true;
            this.ComboBoxReadStatus.Items.AddRange(new object[] {
            resources.GetString("ComboBoxReadStatus.Items"),
            resources.GetString("ComboBoxReadStatus.Items1"),
            resources.GetString("ComboBoxReadStatus.Items2")});
            resources.ApplyResources(this.ComboBoxReadStatus, "ComboBoxReadStatus");
            this.ComboBoxReadStatus.Name = "ComboBoxReadStatus";
            // 
            // dataGridViewLeaveMsgs
            // 
            this.dataGridViewLeaveMsgs.AllowUserToAddRows = false;
            this.dataGridViewLeaveMsgs.AllowUserToDeleteRows = false;
            resources.ApplyResources(this.dataGridViewLeaveMsgs, "dataGridViewLeaveMsgs");
            this.dataGridViewLeaveMsgs.AutoGenerateColumns = false;
            this.dataGridViewLeaveMsgs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewLeaveMsgs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.srcaddrDataGridViewTextBoxColumn,
            this.dstaddrDataGridViewTextBoxColumn,
            this.timeDataGridViewTextBoxColumn,
            this.readflagDataGridViewTextBoxColumn});
            this.dataGridViewLeaveMsgs.DataSource = this.LeaveMsgsbindingSource;
            this.dataGridViewLeaveMsgs.Name = "dataGridViewLeaveMsgs";
            this.dataGridViewLeaveMsgs.ReadOnly = true;
            this.dataGridViewLeaveMsgs.RowTemplate.Height = 24;
            this.dataGridViewLeaveMsgs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewLeaveMsgs.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DataGridViewLeaveMsgs_CellFormatting);
            // 
            // srcaddrDataGridViewTextBoxColumn
            // 
            this.srcaddrDataGridViewTextBoxColumn.DataPropertyName = "src_addr";
            resources.ApplyResources(this.srcaddrDataGridViewTextBoxColumn, "srcaddrDataGridViewTextBoxColumn");
            this.srcaddrDataGridViewTextBoxColumn.Name = "srcaddrDataGridViewTextBoxColumn";
            this.srcaddrDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dstaddrDataGridViewTextBoxColumn
            // 
            this.dstaddrDataGridViewTextBoxColumn.DataPropertyName = "dst_addr";
            resources.ApplyResources(this.dstaddrDataGridViewTextBoxColumn, "dstaddrDataGridViewTextBoxColumn");
            this.dstaddrDataGridViewTextBoxColumn.Name = "dstaddrDataGridViewTextBoxColumn";
            this.dstaddrDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // timeDataGridViewTextBoxColumn
            // 
            this.timeDataGridViewTextBoxColumn.DataPropertyName = "time";
            resources.ApplyResources(this.timeDataGridViewTextBoxColumn, "timeDataGridViewTextBoxColumn");
            this.timeDataGridViewTextBoxColumn.Name = "timeDataGridViewTextBoxColumn";
            this.timeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // readflagDataGridViewTextBoxColumn
            // 
            this.readflagDataGridViewTextBoxColumn.DataPropertyName = "readflag";
            resources.ApplyResources(this.readflagDataGridViewTextBoxColumn, "readflagDataGridViewTextBoxColumn");
            this.readflagDataGridViewTextBoxColumn.Name = "readflagDataGridViewTextBoxColumn";
            this.readflagDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // LeaveMsgsbindingSource
            // 
            this.LeaveMsgsbindingSource.DataSource = typeof(ICMServer.Models.leaveword);
            // 
            // FormLeaveMessage
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridViewLeaveMsgs);
            this.Controls.Add(this.ComboBoxReadStatus);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dateTimePickerEnd);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePickerStart);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.BtnSearch);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormLeaveMessage";
            this.Load += new System.EventHandler(this.FormLeaveMessage_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLeaveMsgs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LeaveMsgsbindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button BtnPlay;
        private System.Windows.Forms.Button BtnDel;
        private System.Windows.Forms.Button BtnSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePickerStart;
        private System.Windows.Forms.DateTimePicker dateTimePickerEnd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox ComboBoxReadStatus;
        private System.Windows.Forms.Button BtnRefresh;
        private System.Windows.Forms.DataGridView dataGridViewLeaveMsgs;
        private System.Windows.Forms.BindingSource LeaveMsgsbindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn srcaddrDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dstaddrDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn timeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn readflagDataGridViewTextBoxColumn;
    }
}
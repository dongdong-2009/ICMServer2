namespace ICMServer
{
    partial class FormAnnouncementManagement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAnnouncementManagement));
            this.tabControlMsgSpread = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.BtnDelData = new System.Windows.Forms.Button();
            this.BtnSetTime = new System.Windows.Forms.Button();
            this.BtnUploadAd = new System.Windows.Forms.Button();
            this.listViewPlaySeq = new System.Windows.Forms.ListView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridViewPublishInfo = new System.Windows.Forms.DataGridView();
            this.titleDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dstaddrDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.typeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.filepathDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isreadDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bindingSourcePublishInfo = new System.Windows.Forms.BindingSource(this.components);
            this.BtnChooseDev = new System.Windows.Forms.Button();
            this.textBoxIndoor = new System.Windows.Forms.TextBox();
            this.dateTimePickerEnd = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerStart = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BtnRefresh = new System.Windows.Forms.Button();
            this.BtnDel = new System.Windows.Forms.Button();
            this.BtnCheck = new System.Windows.Forms.Button();
            this.BtnNewMsg = new System.Windows.Forms.Button();
            this.BtnSearch = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControlMsgSpread.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPublishInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourcePublishInfo)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlMsgSpread
            // 
            this.tabControlMsgSpread.Controls.Add(this.tabPage1);
            this.tabControlMsgSpread.Controls.Add(this.tabPage2);
            resources.ApplyResources(this.tabControlMsgSpread, "tabControlMsgSpread");
            this.tabControlMsgSpread.Name = "tabControlMsgSpread";
            this.tabControlMsgSpread.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.BtnDelData);
            this.tabPage1.Controls.Add(this.BtnSetTime);
            this.tabPage1.Controls.Add(this.BtnUploadAd);
            this.tabPage1.Controls.Add(this.listViewPlaySeq);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // BtnDelData
            // 
            resources.ApplyResources(this.BtnDelData, "BtnDelData");
            this.BtnDelData.Name = "BtnDelData";
            this.BtnDelData.UseVisualStyleBackColor = true;
            this.BtnDelData.Click += new System.EventHandler(this.BtnDelData_Click);
            // 
            // BtnSetTime
            // 
            resources.ApplyResources(this.BtnSetTime, "BtnSetTime");
            this.BtnSetTime.Name = "BtnSetTime";
            this.BtnSetTime.UseVisualStyleBackColor = true;
            this.BtnSetTime.Click += new System.EventHandler(this.BtnSetTime_Click);
            // 
            // BtnUploadAd
            // 
            resources.ApplyResources(this.BtnUploadAd, "BtnUploadAd");
            this.BtnUploadAd.Name = "BtnUploadAd";
            this.BtnUploadAd.UseVisualStyleBackColor = true;
            this.BtnUploadAd.Click += new System.EventHandler(this.BtnUploadAd_Click);
            // 
            // listViewPlaySeq
            // 
            resources.ApplyResources(this.listViewPlaySeq, "listViewPlaySeq");
            this.listViewPlaySeq.CheckBoxes = true;
            this.listViewPlaySeq.Name = "listViewPlaySeq";
            this.listViewPlaySeq.UseCompatibleStateImageBehavior = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridViewPublishInfo);
            this.tabPage2.Controls.Add(this.BtnChooseDev);
            this.tabPage2.Controls.Add(this.textBoxIndoor);
            this.tabPage2.Controls.Add(this.dateTimePickerEnd);
            this.tabPage2.Controls.Add(this.dateTimePickerStart);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.BtnSearch);
            this.tabPage2.Controls.Add(this.label2);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridViewPublishInfo
            // 
            this.dataGridViewPublishInfo.AllowUserToAddRows = false;
            this.dataGridViewPublishInfo.AllowUserToDeleteRows = false;
            resources.ApplyResources(this.dataGridViewPublishInfo, "dataGridViewPublishInfo");
            this.dataGridViewPublishInfo.AutoGenerateColumns = false;
            this.dataGridViewPublishInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPublishInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.titleDataGridViewTextBoxColumn,
            this.dstaddrDataGridViewTextBoxColumn,
            this.timeDataGridViewTextBoxColumn,
            this.typeDataGridViewTextBoxColumn,
            this.filepathDataGridViewTextBoxColumn,
            this.isreadDataGridViewTextBoxColumn});
            this.dataGridViewPublishInfo.DataSource = this.bindingSourcePublishInfo;
            this.dataGridViewPublishInfo.Name = "dataGridViewPublishInfo";
            this.dataGridViewPublishInfo.RowTemplate.Height = 24;
            this.dataGridViewPublishInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewPublishInfo.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridViewPublishInfo_CellFormatting);
            // 
            // titleDataGridViewTextBoxColumn
            // 
            this.titleDataGridViewTextBoxColumn.DataPropertyName = "title";
            resources.ApplyResources(this.titleDataGridViewTextBoxColumn, "titleDataGridViewTextBoxColumn");
            this.titleDataGridViewTextBoxColumn.Name = "titleDataGridViewTextBoxColumn";
            // 
            // dstaddrDataGridViewTextBoxColumn
            // 
            this.dstaddrDataGridViewTextBoxColumn.DataPropertyName = "dstaddr";
            resources.ApplyResources(this.dstaddrDataGridViewTextBoxColumn, "dstaddrDataGridViewTextBoxColumn");
            this.dstaddrDataGridViewTextBoxColumn.Name = "dstaddrDataGridViewTextBoxColumn";
            // 
            // timeDataGridViewTextBoxColumn
            // 
            this.timeDataGridViewTextBoxColumn.DataPropertyName = "time";
            resources.ApplyResources(this.timeDataGridViewTextBoxColumn, "timeDataGridViewTextBoxColumn");
            this.timeDataGridViewTextBoxColumn.Name = "timeDataGridViewTextBoxColumn";
            // 
            // typeDataGridViewTextBoxColumn
            // 
            this.typeDataGridViewTextBoxColumn.DataPropertyName = "type";
            resources.ApplyResources(this.typeDataGridViewTextBoxColumn, "typeDataGridViewTextBoxColumn");
            this.typeDataGridViewTextBoxColumn.Name = "typeDataGridViewTextBoxColumn";
            // 
            // filepathDataGridViewTextBoxColumn
            // 
            this.filepathDataGridViewTextBoxColumn.DataPropertyName = "filepath";
            resources.ApplyResources(this.filepathDataGridViewTextBoxColumn, "filepathDataGridViewTextBoxColumn");
            this.filepathDataGridViewTextBoxColumn.Name = "filepathDataGridViewTextBoxColumn";
            // 
            // isreadDataGridViewTextBoxColumn
            // 
            this.isreadDataGridViewTextBoxColumn.DataPropertyName = "isread";
            resources.ApplyResources(this.isreadDataGridViewTextBoxColumn, "isreadDataGridViewTextBoxColumn");
            this.isreadDataGridViewTextBoxColumn.Name = "isreadDataGridViewTextBoxColumn";
            // 
            // bindingSourcePublishInfo
            // 
            this.bindingSourcePublishInfo.DataSource = typeof(ICMServer.Models.publishinfo);
            // 
            // BtnChooseDev
            // 
            resources.ApplyResources(this.BtnChooseDev, "BtnChooseDev");
            this.BtnChooseDev.Name = "BtnChooseDev";
            this.BtnChooseDev.UseVisualStyleBackColor = true;
            this.BtnChooseDev.Click += new System.EventHandler(this.BtnChooseDev_Click);
            // 
            // textBoxIndoor
            // 
            resources.ApplyResources(this.textBoxIndoor, "textBoxIndoor");
            this.textBoxIndoor.Name = "textBoxIndoor";
            // 
            // dateTimePickerEnd
            // 
            resources.ApplyResources(this.dateTimePickerEnd, "dateTimePickerEnd");
            this.dateTimePickerEnd.Name = "dateTimePickerEnd";
            // 
            // dateTimePickerStart
            // 
            resources.ApplyResources(this.dateTimePickerStart, "dateTimePickerStart");
            this.dateTimePickerStart.Name = "dateTimePickerStart";
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
            this.groupBox1.Controls.Add(this.BtnDel);
            this.groupBox1.Controls.Add(this.BtnCheck);
            this.groupBox1.Controls.Add(this.BtnNewMsg);
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
            // BtnDel
            // 
            resources.ApplyResources(this.BtnDel, "BtnDel");
            this.BtnDel.Name = "BtnDel";
            this.BtnDel.UseVisualStyleBackColor = true;
            this.BtnDel.Click += new System.EventHandler(this.BtnDel_Click);
            // 
            // BtnCheck
            // 
            resources.ApplyResources(this.BtnCheck, "BtnCheck");
            this.BtnCheck.Name = "BtnCheck";
            this.BtnCheck.UseVisualStyleBackColor = true;
            this.BtnCheck.Click += new System.EventHandler(this.BtnCheck_Click);
            // 
            // BtnNewMsg
            // 
            resources.ApplyResources(this.BtnNewMsg, "BtnNewMsg");
            this.BtnNewMsg.Name = "BtnNewMsg";
            this.BtnNewMsg.UseVisualStyleBackColor = true;
            this.BtnNewMsg.Click += new System.EventHandler(this.BtnNewMsg_Click);
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
            // FormAnnouncementManagement
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControlMsgSpread);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormAnnouncementManagement";
            this.Load += new System.EventHandler(this.FormAnnouncementManagement_Load);
            this.tabControlMsgSpread.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPublishInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourcePublishInfo)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlMsgSpread;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DateTimePicker dateTimePickerEnd;
        private System.Windows.Forms.DateTimePicker dateTimePickerStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button BtnRefresh;
        private System.Windows.Forms.Button BtnDel;
        private System.Windows.Forms.Button BtnCheck;
        private System.Windows.Forms.Button BtnNewMsg;
        private System.Windows.Forms.Button BtnSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView listViewPlaySeq;
        private System.Windows.Forms.Button BtnDelData;
        private System.Windows.Forms.Button BtnSetTime;
        private System.Windows.Forms.Button BtnUploadAd;
        private System.Windows.Forms.Button BtnChooseDev;
        private System.Windows.Forms.TextBox textBoxIndoor;
        private System.Windows.Forms.BindingSource bindingSourcePublishInfo;
        private System.Windows.Forms.DataGridView dataGridViewPublishInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn titleDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dstaddrDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn timeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn typeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn filepathDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn isreadDataGridViewTextBoxColumn;
    }
}
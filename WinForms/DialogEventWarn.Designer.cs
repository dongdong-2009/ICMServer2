namespace ICMServer
{
    partial class DialogEventWarn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogEventWarn));
            this.buttonRevocation = new System.Windows.Forms.Button();
            this.BtnClose = new System.Windows.Forms.Button();
            this.eventwarnBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.eventwarnDataGridView = new System.Windows.Forms.DataGridView();
            this.dgvTextBoxAlarmSrc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTextBoxAlarmTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTextBoxAlarmType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTextBoxAlarmAction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTextBoxAlarmChannel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.eventwarnBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventwarnDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonRevocation
            // 
            resources.ApplyResources(this.buttonRevocation, "buttonRevocation");
            this.buttonRevocation.Name = "buttonRevocation";
            this.buttonRevocation.UseVisualStyleBackColor = true;
            // 
            // BtnClose
            // 
            resources.ApplyResources(this.BtnClose, "BtnClose");
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // eventwarnBindingSource
            // 
            this.eventwarnBindingSource.DataSource = typeof(ICMServer.Models.eventwarn);
            // 
            // eventwarnDataGridView
            // 
            resources.ApplyResources(this.eventwarnDataGridView, "eventwarnDataGridView");
            this.eventwarnDataGridView.AllowUserToAddRows = false;
            this.eventwarnDataGridView.AllowUserToDeleteRows = false;
            this.eventwarnDataGridView.AutoGenerateColumns = false;
            this.eventwarnDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.eventwarnDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvTextBoxAlarmSrc,
            this.dgvTextBoxAlarmTime,
            this.dgvTextBoxAlarmType,
            this.dgvTextBoxAlarmAction,
            this.dgvTextBoxAlarmChannel});
            this.eventwarnDataGridView.DataSource = this.eventwarnBindingSource;
            this.eventwarnDataGridView.Name = "eventwarnDataGridView";
            this.eventwarnDataGridView.ReadOnly = true;
            this.eventwarnDataGridView.RowTemplate.Height = 24;
            this.eventwarnDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.eventwarnDataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.eventwarnDataGridView_CellFormatting);
            // 
            // dgvTextBoxAlarmSrc
            // 
            this.dgvTextBoxAlarmSrc.DataPropertyName = "srcaddr";
            resources.ApplyResources(this.dgvTextBoxAlarmSrc, "dgvTextBoxAlarmSrc");
            this.dgvTextBoxAlarmSrc.Name = "dgvTextBoxAlarmSrc";
            this.dgvTextBoxAlarmSrc.ReadOnly = true;
            // 
            // dgvTextBoxAlarmTime
            // 
            this.dgvTextBoxAlarmTime.DataPropertyName = "time";
            resources.ApplyResources(this.dgvTextBoxAlarmTime, "dgvTextBoxAlarmTime");
            this.dgvTextBoxAlarmTime.Name = "dgvTextBoxAlarmTime";
            this.dgvTextBoxAlarmTime.ReadOnly = true;
            // 
            // dgvTextBoxAlarmType
            // 
            this.dgvTextBoxAlarmType.DataPropertyName = "type";
            resources.ApplyResources(this.dgvTextBoxAlarmType, "dgvTextBoxAlarmType");
            this.dgvTextBoxAlarmType.Name = "dgvTextBoxAlarmType";
            this.dgvTextBoxAlarmType.ReadOnly = true;
            // 
            // dgvTextBoxAlarmAction
            // 
            this.dgvTextBoxAlarmAction.DataPropertyName = "action";
            resources.ApplyResources(this.dgvTextBoxAlarmAction, "dgvTextBoxAlarmAction");
            this.dgvTextBoxAlarmAction.Name = "dgvTextBoxAlarmAction";
            this.dgvTextBoxAlarmAction.ReadOnly = true;
            // 
            // dgvTextBoxAlarmChannel
            // 
            this.dgvTextBoxAlarmChannel.DataPropertyName = "channel";
            resources.ApplyResources(this.dgvTextBoxAlarmChannel, "dgvTextBoxAlarmChannel");
            this.dgvTextBoxAlarmChannel.Name = "dgvTextBoxAlarmChannel";
            this.dgvTextBoxAlarmChannel.ReadOnly = true;
            // 
            // DialogEventWarn
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.eventwarnDataGridView);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.buttonRevocation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "DialogEventWarn";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.DialogEventWarn_Load);
            ((System.ComponentModel.ISupportInitialize)(this.eventwarnBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventwarnDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonRevocation;
        private System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.BindingSource eventwarnBindingSource;
        private System.Windows.Forms.DataGridView eventwarnDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTextBoxAlarmSrc;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTextBoxAlarmTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTextBoxAlarmType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTextBoxAlarmAction;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTextBoxAlarmChannel;

    }
}
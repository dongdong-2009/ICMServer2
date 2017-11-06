namespace ICMServer
{
    partial class DialogHandleAlarm
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
            System.Windows.Forms.Label actionLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogHandleAlarm));
            System.Windows.Forms.Label channelLabel;
            System.Windows.Forms.Label handlerLabel;
            System.Windows.Forms.Label handlestatusLabel;
            System.Windows.Forms.Label handletimeLabel;
            System.Windows.Forms.Label srcaddrLabel;
            System.Windows.Forms.Label timeLabel;
            System.Windows.Forms.Label typeLabel;
            this.actionTextBox = new System.Windows.Forms.TextBox();
            this.channelTextBox = new System.Windows.Forms.TextBox();
            this.handlerTextBox = new System.Windows.Forms.TextBox();
            this.handlestatusListBox = new System.Windows.Forms.ListBox();
            this.handletimeDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.srcaddrTextBox = new System.Windows.Forms.TextBox();
            this.timeDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.typeTextBox = new System.Windows.Forms.TextBox();
            this.BtnOK = new System.Windows.Forms.Button();
            this.BtnClose = new System.Windows.Forms.Button();
            actionLabel = new System.Windows.Forms.Label();
            channelLabel = new System.Windows.Forms.Label();
            handlerLabel = new System.Windows.Forms.Label();
            handlestatusLabel = new System.Windows.Forms.Label();
            handletimeLabel = new System.Windows.Forms.Label();
            srcaddrLabel = new System.Windows.Forms.Label();
            timeLabel = new System.Windows.Forms.Label();
            typeLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // actionLabel
            // 
            resources.ApplyResources(actionLabel, "actionLabel");
            actionLabel.Name = "actionLabel";
            // 
            // channelLabel
            // 
            resources.ApplyResources(channelLabel, "channelLabel");
            channelLabel.Name = "channelLabel";
            // 
            // handlerLabel
            // 
            resources.ApplyResources(handlerLabel, "handlerLabel");
            handlerLabel.Name = "handlerLabel";
            // 
            // handlestatusLabel
            // 
            resources.ApplyResources(handlestatusLabel, "handlestatusLabel");
            handlestatusLabel.Name = "handlestatusLabel";
            // 
            // handletimeLabel
            // 
            resources.ApplyResources(handletimeLabel, "handletimeLabel");
            handletimeLabel.Name = "handletimeLabel";
            // 
            // srcaddrLabel
            // 
            resources.ApplyResources(srcaddrLabel, "srcaddrLabel");
            srcaddrLabel.Name = "srcaddrLabel";
            // 
            // timeLabel
            // 
            resources.ApplyResources(timeLabel, "timeLabel");
            timeLabel.Name = "timeLabel";
            // 
            // typeLabel
            // 
            resources.ApplyResources(typeLabel, "typeLabel");
            typeLabel.Name = "typeLabel";
            // 
            // actionTextBox
            // 
            resources.ApplyResources(this.actionTextBox, "actionTextBox");
            this.actionTextBox.Name = "actionTextBox";
            this.actionTextBox.ReadOnly = true;
            // 
            // channelTextBox
            // 
            resources.ApplyResources(this.channelTextBox, "channelTextBox");
            this.channelTextBox.Name = "channelTextBox";
            this.channelTextBox.ReadOnly = true;
            // 
            // handlerTextBox
            // 
            resources.ApplyResources(this.handlerTextBox, "handlerTextBox");
            this.handlerTextBox.Name = "handlerTextBox";
            // 
            // handlestatusListBox
            // 
            resources.ApplyResources(this.handlestatusListBox, "handlestatusListBox");
            this.handlestatusListBox.FormattingEnabled = true;
            this.handlestatusListBox.Items.AddRange(new object[] {
            resources.GetString("handlestatusListBox.Items"),
            resources.GetString("handlestatusListBox.Items1"),
            resources.GetString("handlestatusListBox.Items2")});
            this.handlestatusListBox.Name = "handlestatusListBox";
            this.handlestatusListBox.SelectedIndexChanged += new System.EventHandler(this.handlestatusListBox_SelectedIndexChanged);
            // 
            // handletimeDateTimePicker
            // 
            resources.ApplyResources(this.handletimeDateTimePicker, "handletimeDateTimePicker");
            this.handletimeDateTimePicker.Name = "handletimeDateTimePicker";
            // 
            // srcaddrTextBox
            // 
            resources.ApplyResources(this.srcaddrTextBox, "srcaddrTextBox");
            this.srcaddrTextBox.Name = "srcaddrTextBox";
            this.srcaddrTextBox.ReadOnly = true;
            // 
            // timeDateTimePicker
            // 
            resources.ApplyResources(this.timeDateTimePicker, "timeDateTimePicker");
            this.timeDateTimePicker.Name = "timeDateTimePicker";
            // 
            // typeTextBox
            // 
            resources.ApplyResources(this.typeTextBox, "typeTextBox");
            this.typeTextBox.Name = "typeTextBox";
            this.typeTextBox.ReadOnly = true;
            // 
            // BtnOK
            // 
            resources.ApplyResources(this.BtnOK, "BtnOK");
            this.BtnOK.Name = "BtnOK";
            this.BtnOK.UseVisualStyleBackColor = true;
            this.BtnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // BtnClose
            // 
            resources.ApplyResources(this.BtnClose, "BtnClose");
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // DialogHandleAlarm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.BtnOK);
            this.Controls.Add(actionLabel);
            this.Controls.Add(this.actionTextBox);
            this.Controls.Add(channelLabel);
            this.Controls.Add(this.channelTextBox);
            this.Controls.Add(handlerLabel);
            this.Controls.Add(this.handlerTextBox);
            this.Controls.Add(handlestatusLabel);
            this.Controls.Add(this.handlestatusListBox);
            this.Controls.Add(handletimeLabel);
            this.Controls.Add(this.handletimeDateTimePicker);
            this.Controls.Add(srcaddrLabel);
            this.Controls.Add(this.srcaddrTextBox);
            this.Controls.Add(timeLabel);
            this.Controls.Add(this.timeDateTimePicker);
            this.Controls.Add(typeLabel);
            this.Controls.Add(this.typeTextBox);
            this.Name = "DialogHandleAlarm";
            this.Load += new System.EventHandler(this.DialogHandleAlarm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox actionTextBox;
        private System.Windows.Forms.TextBox channelTextBox;
        private System.Windows.Forms.TextBox handlerTextBox;
        private System.Windows.Forms.ListBox handlestatusListBox;
        private System.Windows.Forms.DateTimePicker handletimeDateTimePicker;
        private System.Windows.Forms.TextBox srcaddrTextBox;
        private System.Windows.Forms.DateTimePicker timeDateTimePicker;
        private System.Windows.Forms.TextBox typeTextBox;
        private System.Windows.Forms.Button BtnOK;
        private System.Windows.Forms.Button BtnClose;
    }
}
namespace ICMServer
{
    partial class DialogAdminMan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogAdminMan));
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxAuthoName = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxSelectAll = new System.Windows.Forms.CheckBox();
            this.checkBoxAutho1 = new System.Windows.Forms.CheckBox();
            this.checkBoxLock = new System.Windows.Forms.CheckBox();
            this.checkBoxPhone = new System.Windows.Forms.CheckBox();
            this.checkBoxEvent = new System.Windows.Forms.CheckBox();
            this.checkBoxArea = new System.Windows.Forms.CheckBox();
            this.checkBoxDev = new System.Windows.Forms.CheckBox();
            this.BtnOk = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // textBoxAuthoName
            // 
            resources.ApplyResources(this.textBoxAuthoName, "textBoxAuthoName");
            this.textBoxAuthoName.Name = "textBoxAuthoName";
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.checkBoxSelectAll);
            this.groupBox1.Controls.Add(this.checkBoxAutho1);
            this.groupBox1.Controls.Add(this.checkBoxLock);
            this.groupBox1.Controls.Add(this.checkBoxPhone);
            this.groupBox1.Controls.Add(this.checkBoxEvent);
            this.groupBox1.Controls.Add(this.checkBoxArea);
            this.groupBox1.Controls.Add(this.checkBoxDev);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // checkBoxSelectAll
            // 
            resources.ApplyResources(this.checkBoxSelectAll, "checkBoxSelectAll");
            this.checkBoxSelectAll.Name = "checkBoxSelectAll";
            this.checkBoxSelectAll.UseVisualStyleBackColor = true;
            this.checkBoxSelectAll.CheckedChanged += new System.EventHandler(this.checkBoxSelectAll_CheckedChanged);
            // 
            // checkBoxAutho1
            // 
            resources.ApplyResources(this.checkBoxAutho1, "checkBoxAutho1");
            this.checkBoxAutho1.Name = "checkBoxAutho1";
            this.checkBoxAutho1.Tag = "32";
            this.checkBoxAutho1.UseVisualStyleBackColor = true;
            // 
            // checkBoxLock
            // 
            resources.ApplyResources(this.checkBoxLock, "checkBoxLock");
            this.checkBoxLock.Name = "checkBoxLock";
            this.checkBoxLock.Tag = "16";
            this.checkBoxLock.UseVisualStyleBackColor = true;
            // 
            // checkBoxPhone
            // 
            resources.ApplyResources(this.checkBoxPhone, "checkBoxPhone");
            this.checkBoxPhone.Name = "checkBoxPhone";
            this.checkBoxPhone.Tag = "8";
            this.checkBoxPhone.UseVisualStyleBackColor = true;
            // 
            // checkBoxEvent
            // 
            resources.ApplyResources(this.checkBoxEvent, "checkBoxEvent");
            this.checkBoxEvent.Name = "checkBoxEvent";
            this.checkBoxEvent.Tag = "4";
            this.checkBoxEvent.UseVisualStyleBackColor = true;
            // 
            // checkBoxArea
            // 
            resources.ApplyResources(this.checkBoxArea, "checkBoxArea");
            this.checkBoxArea.Name = "checkBoxArea";
            this.checkBoxArea.Tag = "2";
            this.checkBoxArea.UseVisualStyleBackColor = true;
            // 
            // checkBoxDev
            // 
            resources.ApplyResources(this.checkBoxDev, "checkBoxDev");
            this.checkBoxDev.Name = "checkBoxDev";
            this.checkBoxDev.Tag = "1";
            this.checkBoxDev.UseVisualStyleBackColor = true;
            // 
            // BtnOk
            // 
            resources.ApplyResources(this.BtnOk, "BtnOk");
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.UseVisualStyleBackColor = true;
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // BtnCancel
            // 
            resources.ApplyResources(this.BtnCancel, "BtnCancel");
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // DialogAdminMan
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnOk);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBoxAuthoName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "DialogAdminMan";
            this.Load += new System.EventHandler(this.AdminMan_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxAuthoName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button BtnOk;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.CheckBox checkBoxSelectAll;
        private System.Windows.Forms.CheckBox checkBoxAutho1;
        private System.Windows.Forms.CheckBox checkBoxLock;
        private System.Windows.Forms.CheckBox checkBoxPhone;
        private System.Windows.Forms.CheckBox checkBoxEvent;
        private System.Windows.Forms.CheckBox checkBoxArea;
        private System.Windows.Forms.CheckBox checkBoxDev;
    }
}
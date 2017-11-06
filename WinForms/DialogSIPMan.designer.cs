namespace ICMServer
{
    partial class DialogSIPMan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogSIPMan));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.maskedTextBoxRoom = new System.Windows.Forms.MaskedTextBox();
            this.BtnSync = new System.Windows.Forms.Button();
            this.BtnQuery = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.listViewSIPAccount = new System.Windows.Forms.ListView();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.BtnCheck = new System.Windows.Forms.Button();
            this.BtnDel = new System.Windows.Forms.Button();
            this.BtnClose = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.maskedTextBoxRoom);
            this.groupBox1.Controls.Add(this.BtnSync);
            this.groupBox1.Controls.Add(this.BtnQuery);
            this.groupBox1.Controls.Add(this.label1);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // maskedTextBoxRoom
            // 
            this.maskedTextBoxRoom.AsciiOnly = true;
            this.maskedTextBoxRoom.Culture = new System.Globalization.CultureInfo("");
            this.maskedTextBoxRoom.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals;
            resources.ApplyResources(this.maskedTextBoxRoom, "maskedTextBoxRoom");
            this.maskedTextBoxRoom.Name = "maskedTextBoxRoom";
            this.maskedTextBoxRoom.ResetOnPrompt = false;
            this.maskedTextBoxRoom.ResetOnSpace = false;
            this.maskedTextBoxRoom.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals;
            // 
            // BtnSync
            // 
            resources.ApplyResources(this.BtnSync, "BtnSync");
            this.BtnSync.Name = "BtnSync";
            this.BtnSync.UseVisualStyleBackColor = true;
            this.BtnSync.Click += new System.EventHandler(this.BtnSync_Click);
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
            // listViewSIPAccount
            // 
            resources.ApplyResources(this.listViewSIPAccount, "listViewSIPAccount");
            this.listViewSIPAccount.Name = "listViewSIPAccount";
            this.listViewSIPAccount.UseCompatibleStateImageBehavior = false;
            // 
            // BtnAdd
            // 
            resources.ApplyResources(this.BtnAdd, "BtnAdd");
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.UseVisualStyleBackColor = true;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // BtnCheck
            // 
            resources.ApplyResources(this.BtnCheck, "BtnCheck");
            this.BtnCheck.Name = "BtnCheck";
            this.BtnCheck.UseVisualStyleBackColor = true;
            this.BtnCheck.Click += new System.EventHandler(this.BtnCheck_Click);
            // 
            // BtnDel
            // 
            resources.ApplyResources(this.BtnDel, "BtnDel");
            this.BtnDel.Name = "BtnDel";
            this.BtnDel.UseVisualStyleBackColor = true;
            this.BtnDel.Click += new System.EventHandler(this.BtnDel_Click);
            // 
            // BtnClose
            // 
            resources.ApplyResources(this.BtnClose, "BtnClose");
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // DialogSIPMan
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.BtnCheck);
            this.Controls.Add(this.BtnDel);
            this.Controls.Add(this.BtnAdd);
            this.Controls.Add(this.listViewSIPAccount);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "DialogSIPMan";
            this.Load += new System.EventHandler(this.SIPMan_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button BtnSync;
        private System.Windows.Forms.Button BtnQuery;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listViewSIPAccount;
        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.Button BtnCheck;
        private System.Windows.Forms.Button BtnDel;
        private System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxRoom;
    }
}
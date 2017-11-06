namespace ICMServer
{
    partial class DialogLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogLogin));
            this.ComboBoxUserName = new System.Windows.Forms.ComboBox();
            this.BtnLogin = new System.Windows.Forms.Button();
            this.BtnClose = new System.Windows.Forms.Button();
            this.textBoxUserPassword = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ComboBoxUserName
            // 
            this.ComboBoxUserName.BackColor = System.Drawing.Color.White;
            this.ComboBoxUserName.DropDownWidth = 138;
            this.ComboBoxUserName.FormattingEnabled = true;
            resources.ApplyResources(this.ComboBoxUserName, "ComboBoxUserName");
            this.ComboBoxUserName.Name = "ComboBoxUserName";
            // 
            // BtnLogin
            // 
            this.BtnLogin.BackgroundImage = global::ICMServer.DialogLoginResource.BtnLoginBackgroundImage;
            resources.ApplyResources(this.BtnLogin, "BtnLogin");
            this.BtnLogin.ForeColor = System.Drawing.Color.Teal;
            this.BtnLogin.Name = "BtnLogin";
            this.BtnLogin.UseVisualStyleBackColor = true;
            this.BtnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
            this.BtnLogin.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnLogin_MouseDown);
            this.BtnLogin.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BtnLogin_MouseUp);
            // 
            // BtnClose
            // 
            this.BtnClose.BackgroundImage = global::ICMServer.DialogLoginResource.BtnCloseBackgroundImage;
            resources.ApplyResources(this.BtnClose, "BtnClose");
            this.BtnClose.ForeColor = System.Drawing.Color.Teal;
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            this.BtnClose.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnClose_MouseDown);
            this.BtnClose.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BtnClose_MouseUp);
            // 
            // textBoxUserPassword
            // 
            this.textBoxUserPassword.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.textBoxUserPassword, "textBoxUserPassword");
            this.textBoxUserPassword.Name = "textBoxUserPassword";
            // 
            // DialogLogin
            // 
            this.AcceptButton = this.BtnLogin;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackgroundImage = global::ICMServer.DialogLoginResource.dlgLoginBackgroundImage;
            this.ControlBox = false;
            this.Controls.Add(this.textBoxUserPassword);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.BtnLogin);
            this.Controls.Add(this.ComboBoxUserName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DialogLogin";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.DialogLogin_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DialogLogin_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DialogLogin_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DialogLogin_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ComboBoxUserName;
        private System.Windows.Forms.Button BtnLogin;
        private System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.TextBox textBoxUserPassword;
    }
}
namespace ICMServer
{
    partial class DialogAddressBookImport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogAddressBookImport));
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.BtnSelecFile = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.BtnStart = new System.Windows.Forms.Button();
            this.BtnClose = new System.Windows.Forms.Button();
            this.labelPercent = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // textBoxPath
            // 
            resources.ApplyResources(this.textBoxPath, "textBoxPath");
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.ReadOnly = true;
            // 
            // BtnSelecFile
            // 
            resources.ApplyResources(this.BtnSelecFile, "BtnSelecFile");
            this.BtnSelecFile.Name = "BtnSelecFile";
            this.BtnSelecFile.UseVisualStyleBackColor = true;
            this.BtnSelecFile.Click += new System.EventHandler(this.BtnSelectFile_Click);
            // 
            // progressBar
            // 
            resources.ApplyResources(this.progressBar, "progressBar");
            this.progressBar.Name = "progressBar";
            // 
            // BtnStart
            // 
            resources.ApplyResources(this.BtnStart, "BtnStart");
            this.BtnStart.Name = "BtnStart";
            this.BtnStart.UseVisualStyleBackColor = true;
            this.BtnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // BtnClose
            // 
            resources.ApplyResources(this.BtnClose, "BtnClose");
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // labelPercent
            // 
            resources.ApplyResources(this.labelPercent, "labelPercent");
            this.labelPercent.Name = "labelPercent";
            // 
            // DialogAddressBookImport
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.labelPercent);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.BtnStart);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.BtnSelecFile);
            this.Controls.Add(this.textBoxPath);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "DialogAddressBookImport";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxPath;
        private System.Windows.Forms.Button BtnSelecFile;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button BtnStart;
        private System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.Label labelPercent;
    }
}
namespace ICMServer
{
    partial class DialogSysBackup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogSysBackup));
            this.radioBtnBackup = new System.Windows.Forms.RadioButton();
            this.radioBtnRecover = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.BtnConfirm = new System.Windows.Forms.Button();
            this.BtnQuit = new System.Windows.Forms.Button();
            this.txtBoxPath = new System.Windows.Forms.TextBox();
            this.BtnSelectFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // radioBtnBackup
            // 
            resources.ApplyResources(this.radioBtnBackup, "radioBtnBackup");
            this.radioBtnBackup.Name = "radioBtnBackup";
            this.radioBtnBackup.TabStop = true;
            this.radioBtnBackup.UseVisualStyleBackColor = true;
            // 
            // radioBtnRecover
            // 
            resources.ApplyResources(this.radioBtnRecover, "radioBtnRecover");
            this.radioBtnRecover.Name = "radioBtnRecover";
            this.radioBtnRecover.TabStop = true;
            this.radioBtnRecover.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // BtnConfirm
            // 
            resources.ApplyResources(this.BtnConfirm, "BtnConfirm");
            this.BtnConfirm.Name = "BtnConfirm";
            this.BtnConfirm.UseVisualStyleBackColor = true;
            this.BtnConfirm.Click += new System.EventHandler(this.BtnConfirm_Click);
            // 
            // BtnQuit
            // 
            resources.ApplyResources(this.BtnQuit, "BtnQuit");
            this.BtnQuit.Name = "BtnQuit";
            this.BtnQuit.UseVisualStyleBackColor = true;
            this.BtnQuit.Click += new System.EventHandler(this.BtnQuit_Click);
            // 
            // txtBoxPath
            // 
            resources.ApplyResources(this.txtBoxPath, "txtBoxPath");
            this.txtBoxPath.Name = "txtBoxPath";
            this.txtBoxPath.ReadOnly = true;
            // 
            // BtnSelectFile
            // 
            resources.ApplyResources(this.BtnSelectFile, "BtnSelectFile");
            this.BtnSelectFile.Name = "BtnSelectFile";
            this.BtnSelectFile.UseVisualStyleBackColor = true;
            this.BtnSelectFile.Click += new System.EventHandler(this.BtnSelectFile_Click);
            // 
            // DialogSysBackup
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.BtnSelectFile);
            this.Controls.Add(this.txtBoxPath);
            this.Controls.Add(this.BtnQuit);
            this.Controls.Add(this.BtnConfirm);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.radioBtnRecover);
            this.Controls.Add(this.radioBtnBackup);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "DialogSysBackup";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioBtnBackup;
        private System.Windows.Forms.RadioButton radioBtnRecover;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BtnConfirm;
        private System.Windows.Forms.Button BtnQuit;
        private System.Windows.Forms.TextBox txtBoxPath;
        private System.Windows.Forms.Button BtnSelectFile;
    }
}
namespace ICMServer
{
    partial class FormSystemManagement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSystemManagement));
            this.BtnUserMan = new System.Windows.Forms.Button();
            this.BtnSysSet = new System.Windows.Forms.Button();
            this.BtnBackup = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BtnUserMan
            // 
            resources.ApplyResources(this.BtnUserMan, "BtnUserMan");
            this.BtnUserMan.Name = "BtnUserMan";
            this.BtnUserMan.UseVisualStyleBackColor = true;
            this.BtnUserMan.Click += new System.EventHandler(this.BtnUserMan_Click);
            // 
            // BtnSysSet
            // 
            resources.ApplyResources(this.BtnSysSet, "BtnSysSet");
            this.BtnSysSet.Name = "BtnSysSet";
            this.BtnSysSet.UseVisualStyleBackColor = true;
            this.BtnSysSet.Click += new System.EventHandler(this.BtnSysSet_Click);
            // 
            // BtnBackup
            // 
            resources.ApplyResources(this.BtnBackup, "BtnBackup");
            this.BtnBackup.Name = "BtnBackup";
            this.BtnBackup.UseVisualStyleBackColor = true;
            this.BtnBackup.Click += new System.EventHandler(this.BtnBackup_Click);
            // 
            // FormSystemManagement
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.BtnBackup);
            this.Controls.Add(this.BtnSysSet);
            this.Controls.Add(this.BtnUserMan);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormSystemManagement";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnUserMan;
        private System.Windows.Forms.Button BtnSysSet;
        private System.Windows.Forms.Button BtnBackup;
    }
}
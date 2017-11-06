namespace ICMServer
{
    partial class DialogSyncSIP
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogSyncSIP));
            this.BtnStartSync = new System.Windows.Forms.Button();
            this.listViewSync = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // BtnStartSync
            // 
            resources.ApplyResources(this.BtnStartSync, "BtnStartSync");
            this.BtnStartSync.Name = "BtnStartSync";
            this.BtnStartSync.UseVisualStyleBackColor = true;
            this.BtnStartSync.Click += new System.EventHandler(this.BtnStartSync_Click);
            // 
            // listViewSync
            // 
            resources.ApplyResources(this.listViewSync, "listViewSync");
            this.listViewSync.Name = "listViewSync";
            this.listViewSync.UseCompatibleStateImageBehavior = false;
            // 
            // DialogSyncSIP
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listViewSync);
            this.Controls.Add(this.BtnStartSync);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "DialogSyncSIP";
            this.Load += new System.EventHandler(this.DialogSyncSIP_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnStartSync;
        private System.Windows.Forms.ListView listViewSync;
    }
}
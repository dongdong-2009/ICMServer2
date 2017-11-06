namespace ICMServer
{
    partial class DialogAddressBookExport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogAddressBookExport));
            this.SuspendLayout();
            // 
            // progressBar
            // 
            resources.ApplyResources(this.progressBar, "progressBar");
            // 
            // labelPercentage
            // 
            resources.ApplyResources(this.labelPercentage, "labelPercentage");
            // 
            // BtnClose
            // 
            resources.ApplyResources(this.BtnClose, "BtnClose");
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // listBoxLog
            // 
            resources.ApplyResources(this.listBoxLog, "listBoxLog");
            // 
            // DialogAddressBookExport
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "DialogAddressBookExport";
            this.Load += new System.EventHandler(this.DialogAddressBookExport_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
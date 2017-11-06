namespace ICMServer
{
    partial class DialogProgress
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogProgress));
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.listBoxLog = new System.Windows.Forms.ListBox();
            this.labelPercentage = new System.Windows.Forms.Label();
            this.BtnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            resources.ApplyResources(this.progressBar, "progressBar");
            this.progressBar.Name = "progressBar";
            // 
            // listBoxLog
            // 
            resources.ApplyResources(this.listBoxLog, "listBoxLog");
            this.listBoxLog.FormattingEnabled = true;
            this.listBoxLog.Name = "listBoxLog";
            // 
            // labelPercentage
            // 
            resources.ApplyResources(this.labelPercentage, "labelPercentage");
            this.labelPercentage.Name = "labelPercentage";
            // 
            // BtnClose
            // 
            resources.ApplyResources(this.BtnClose, "BtnClose");
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.UseVisualStyleBackColor = true;
            // 
            // DialogProgress
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.labelPercentage);
            this.Controls.Add(this.listBoxLog);
            this.Controls.Add(this.progressBar);
            this.Name = "DialogProgress";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.ProgressBar progressBar;
        protected System.Windows.Forms.Label labelPercentage;
        protected System.Windows.Forms.Button BtnClose;
        protected System.Windows.Forms.ListBox listBoxLog;
    }
}
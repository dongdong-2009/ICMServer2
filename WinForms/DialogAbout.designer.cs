namespace ICMServer
{
    partial class DialogAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogAbout));
            this.BtnOK = new System.Windows.Forms.Button();
            this.labelAbout = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BtnOK
            // 
            resources.ApplyResources(this.BtnOK, "BtnOK");
            this.BtnOK.Name = "BtnOK";
            this.BtnOK.UseVisualStyleBackColor = true;
            this.BtnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // labelAbout
            // 
            resources.ApplyResources(this.labelAbout, "labelAbout");
            this.labelAbout.BackColor = System.Drawing.Color.White;
            this.labelAbout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelAbout.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.labelAbout.Name = "labelAbout";
            // 
            // DialogAbout
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::ICMServer.Alarm.GUSON_logo;
            this.Controls.Add(this.labelAbout);
            this.Controls.Add(this.BtnOK);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "DialogAbout";
            this.Load += new System.EventHandler(this.DialogAbout_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button BtnOK;
        private System.Windows.Forms.Label labelAbout;
    }
}
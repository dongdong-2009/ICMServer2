namespace ICMServer
{
    partial class DialogLeaveMsgPlayer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogLeaveMsgPlayer));
            this.pictureBoxPlayer = new System.Windows.Forms.PictureBox();
            this.trackBarSpeaker = new System.Windows.Forms.TrackBar();
            this.BtnStop = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlayer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSpeaker)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxPlayer
            // 
            resources.ApplyResources(this.pictureBoxPlayer, "pictureBoxPlayer");
            this.pictureBoxPlayer.Name = "pictureBoxPlayer";
            this.pictureBoxPlayer.TabStop = false;
            // 
            // trackBarSpeaker
            // 
            resources.ApplyResources(this.trackBarSpeaker, "trackBarSpeaker");
            this.trackBarSpeaker.Name = "trackBarSpeaker";
            this.trackBarSpeaker.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TrackBarSpeaker_MouseUp);
            // 
            // BtnStop
            // 
            resources.ApplyResources(this.BtnStop, "BtnStop");
            this.BtnStop.Name = "BtnStop";
            this.BtnStop.UseVisualStyleBackColor = true;
            this.BtnStop.Click += new System.EventHandler(this.BtnStop_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // DialogLeaveMsgPlayer
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnStop);
            this.Controls.Add(this.trackBarSpeaker);
            this.Controls.Add(this.pictureBoxPlayer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "DialogLeaveMsgPlayer";
            this.Load += new System.EventHandler(this.LeaveMsgPlayer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlayer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSpeaker)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxPlayer;
        private System.Windows.Forms.TrackBar trackBarSpeaker;
        private System.Windows.Forms.Button BtnStop;
        private System.Windows.Forms.Label label1;
    }
}
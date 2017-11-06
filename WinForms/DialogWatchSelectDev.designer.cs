namespace ICMServer
{
    partial class DialogWatchSelectDev
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogWatchSelectDev));
            this.ComboBoxSelectDev = new System.Windows.Forms.ComboBox();
            this.BtnOk = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.ComboBoxIPMap = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // ComboBoxSelectDev
            // 
            resources.ApplyResources(this.ComboBoxSelectDev, "ComboBoxSelectDev");
            this.ComboBoxSelectDev.FormattingEnabled = true;
            this.ComboBoxSelectDev.Name = "ComboBoxSelectDev";
            // 
            // BtnOk
            // 
            resources.ApplyResources(this.BtnOk, "BtnOk");
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.UseVisualStyleBackColor = true;
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // BtnCancel
            // 
            resources.ApplyResources(this.BtnCancel, "BtnCancel");
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // ComboBoxIPMap
            // 
            resources.ApplyResources(this.ComboBoxIPMap, "ComboBoxIPMap");
            this.ComboBoxIPMap.FormattingEnabled = true;
            this.ComboBoxIPMap.Name = "ComboBoxIPMap";
            // 
            // DialogWatchSelectDev
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.Controls.Add(this.ComboBoxIPMap);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnOk);
            this.Controls.Add(this.ComboBoxSelectDev);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "DialogWatchSelectDev";
            this.Load += new System.EventHandler(this.WatchSelectDev_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox ComboBoxSelectDev;
        private System.Windows.Forms.Button BtnOk;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.ComboBox ComboBoxIPMap;
    }
}
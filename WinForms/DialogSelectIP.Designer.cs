namespace ICMServer
{
    partial class DialogSelectIP
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogSelectIP));
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.BtnOK = new System.Windows.Forms.Button();
            this.BtnClose = new System.Windows.Forms.Button();
            this.ComboBoxUpgradeType = new System.Windows.Forms.ComboBox();
            this.textBoxDeviceAddress = new System.Windows.Forms.TextBox();
            this.BtnSelectDeviceAddress = new System.Windows.Forms.Button();
            this.ComboBoxUpgradeIPs = new System.Windows.Forms.ComboBox();
            this.DeviceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.DeviceBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // BtnOK
            // 
            resources.ApplyResources(this.BtnOK, "BtnOK");
            this.BtnOK.Name = "BtnOK";
            this.BtnOK.UseVisualStyleBackColor = true;
            this.BtnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // BtnClose
            // 
            resources.ApplyResources(this.BtnClose, "BtnClose");
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // ComboBoxUpgradeType
            // 
            resources.ApplyResources(this.ComboBoxUpgradeType, "ComboBoxUpgradeType");
            this.ComboBoxUpgradeType.FormattingEnabled = true;
            this.ComboBoxUpgradeType.Items.AddRange(new object[] {
            resources.GetString("ComboBoxUpgradeType.Items"),
            resources.GetString("ComboBoxUpgradeType.Items1")});
            this.ComboBoxUpgradeType.Name = "ComboBoxUpgradeType";
            this.ComboBoxUpgradeType.SelectedIndexChanged += new System.EventHandler(this.ComboBoxUpgradeType_SelectedIndexChanged);
            // 
            // textBoxDeviceAddress
            // 
            resources.ApplyResources(this.textBoxDeviceAddress, "textBoxDeviceAddress");
            this.textBoxDeviceAddress.Name = "textBoxDeviceAddress";
            // 
            // BtnSelectDeviceAddress
            // 
            resources.ApplyResources(this.BtnSelectDeviceAddress, "BtnSelectDeviceAddress");
            this.BtnSelectDeviceAddress.Name = "BtnSelectDeviceAddress";
            this.BtnSelectDeviceAddress.UseVisualStyleBackColor = true;
            this.BtnSelectDeviceAddress.Click += new System.EventHandler(this.BtnSelectDeviceAddress_Click);
            // 
            // ComboBoxUpgradeIPs
            // 
            resources.ApplyResources(this.ComboBoxUpgradeIPs, "ComboBoxUpgradeIPs");
            this.ComboBoxUpgradeIPs.DataSource = this.DeviceBindingSource;
            this.ComboBoxUpgradeIPs.DisplayMember = "ip";
            this.ComboBoxUpgradeIPs.FormattingEnabled = true;
            this.ComboBoxUpgradeIPs.Name = "ComboBoxUpgradeIPs";
            // 
            // DeviceBindingSource
            // 
            this.DeviceBindingSource.DataSource = typeof(ICMServer.Models.Device);
            // 
            // DialogSelectIP
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ComboBoxUpgradeIPs);
            this.Controls.Add(this.BtnSelectDeviceAddress);
            this.Controls.Add(this.textBoxDeviceAddress);
            this.Controls.Add(this.ComboBoxUpgradeType);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.BtnOK);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Name = "DialogSelectIP";
            this.Load += new System.EventHandler(this.DialogSelectIP_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DeviceBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button BtnOK;
        private System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.ComboBox ComboBoxUpgradeType;
        private System.Windows.Forms.TextBox textBoxDeviceAddress;
        private System.Windows.Forms.Button BtnSelectDeviceAddress;
        private System.Windows.Forms.ComboBox ComboBoxUpgradeIPs;
        private System.Windows.Forms.BindingSource DeviceBindingSource;
    }
}
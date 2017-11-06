namespace ICMServer
{
    partial class DialogDevAlter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogDevAlter));
            this.ComboBoxDeviceType = new System.Windows.Forms.ComboBox();
            this.labelDeviceAddr = new System.Windows.Forms.Label();
            this.labelDeviceType = new System.Windows.Forms.Label();
            this.labelDeviceIp = new System.Windows.Forms.Label();
            this.labelDeviceGateway = new System.Windows.Forms.Label();
            this.labelDeviceSubnetMask = new System.Windows.Forms.Label();
            this.ComboBoxGroupIp = new System.Windows.Forms.ComboBox();
            this.labelGroupIp = new System.Windows.Forms.Label();
            this.labelDeviceAlias = new System.Windows.Forms.Label();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.BtnOK = new System.Windows.Forms.Button();
            this.ipAddressControlDevIP = new IPAddressControlLib.IPAddressControl();
            this.ipAddressControlGW = new IPAddressControlLib.IPAddressControl();
            this.ipAddressControlSM = new IPAddressControlLib.IPAddressControl();
            this.labelCamID = new System.Windows.Forms.Label();
            this.labelCamPW = new System.Windows.Forms.Label();
            this.textBoxCamID = new System.Windows.Forms.TextBox();
            this.textBoxCamPW = new System.Windows.Forms.TextBox();
            this.checkBoxSD = new System.Windows.Forms.CheckBox();
            this.textBoxAlias = new System.Windows.Forms.TextBox();
            this.labelDeviceMac = new System.Windows.Forms.Label();
            this.textBoxDeviceMac = new System.Windows.Forms.MaskedTextBox();
            this.DeviceAddressControl = new DeviceAddressControlLib.DeviceAddressControl();
            this.SuspendLayout();
            // 
            // ComboBoxDeviceType
            // 
            this.ComboBoxDeviceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxDeviceType.FormattingEnabled = true;
            this.ComboBoxDeviceType.Items.AddRange(new object[] {
            resources.GetString("ComboBoxDeviceType.Items"),
            resources.GetString("ComboBoxDeviceType.Items1"),
            resources.GetString("ComboBoxDeviceType.Items2"),
            resources.GetString("ComboBoxDeviceType.Items3"),
            resources.GetString("ComboBoxDeviceType.Items4"),
            resources.GetString("ComboBoxDeviceType.Items5"),
            resources.GetString("ComboBoxDeviceType.Items6"),
            resources.GetString("ComboBoxDeviceType.Items7"),
            resources.GetString("ComboBoxDeviceType.Items8"),
            resources.GetString("ComboBoxDeviceType.Items9"),
            resources.GetString("ComboBoxDeviceType.Items10")});
            resources.ApplyResources(this.ComboBoxDeviceType, "ComboBoxDeviceType");
            this.ComboBoxDeviceType.Name = "ComboBoxDeviceType";
            this.ComboBoxDeviceType.SelectedIndexChanged += new System.EventHandler(this.ComboBoxDeviceType_SelectedIndexChanged);
            // 
            // labelDeviceAddr
            // 
            resources.ApplyResources(this.labelDeviceAddr, "labelDeviceAddr");
            this.labelDeviceAddr.Name = "labelDeviceAddr";
            // 
            // labelDeviceType
            // 
            resources.ApplyResources(this.labelDeviceType, "labelDeviceType");
            this.labelDeviceType.Name = "labelDeviceType";
            // 
            // labelDeviceIp
            // 
            resources.ApplyResources(this.labelDeviceIp, "labelDeviceIp");
            this.labelDeviceIp.Name = "labelDeviceIp";
            // 
            // labelDeviceGateway
            // 
            resources.ApplyResources(this.labelDeviceGateway, "labelDeviceGateway");
            this.labelDeviceGateway.Name = "labelDeviceGateway";
            // 
            // labelDeviceSubnetMask
            // 
            resources.ApplyResources(this.labelDeviceSubnetMask, "labelDeviceSubnetMask");
            this.labelDeviceSubnetMask.Name = "labelDeviceSubnetMask";
            // 
            // ComboBoxGroupIp
            // 
            this.ComboBoxGroupIp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxGroupIp.FormattingEnabled = true;
            this.ComboBoxGroupIp.Items.AddRange(new object[] {
            resources.GetString("ComboBoxGroupIp.Items"),
            resources.GetString("ComboBoxGroupIp.Items1"),
            resources.GetString("ComboBoxGroupIp.Items2"),
            resources.GetString("ComboBoxGroupIp.Items3"),
            resources.GetString("ComboBoxGroupIp.Items4"),
            resources.GetString("ComboBoxGroupIp.Items5"),
            resources.GetString("ComboBoxGroupIp.Items6"),
            resources.GetString("ComboBoxGroupIp.Items7"),
            resources.GetString("ComboBoxGroupIp.Items8"),
            resources.GetString("ComboBoxGroupIp.Items9"),
            resources.GetString("ComboBoxGroupIp.Items10"),
            resources.GetString("ComboBoxGroupIp.Items11"),
            resources.GetString("ComboBoxGroupIp.Items12"),
            resources.GetString("ComboBoxGroupIp.Items13"),
            resources.GetString("ComboBoxGroupIp.Items14"),
            resources.GetString("ComboBoxGroupIp.Items15"),
            resources.GetString("ComboBoxGroupIp.Items16"),
            resources.GetString("ComboBoxGroupIp.Items17"),
            resources.GetString("ComboBoxGroupIp.Items18"),
            resources.GetString("ComboBoxGroupIp.Items19"),
            resources.GetString("ComboBoxGroupIp.Items20"),
            resources.GetString("ComboBoxGroupIp.Items21"),
            resources.GetString("ComboBoxGroupIp.Items22"),
            resources.GetString("ComboBoxGroupIp.Items23"),
            resources.GetString("ComboBoxGroupIp.Items24"),
            resources.GetString("ComboBoxGroupIp.Items25"),
            resources.GetString("ComboBoxGroupIp.Items26"),
            resources.GetString("ComboBoxGroupIp.Items27"),
            resources.GetString("ComboBoxGroupIp.Items28"),
            resources.GetString("ComboBoxGroupIp.Items29"),
            resources.GetString("ComboBoxGroupIp.Items30"),
            resources.GetString("ComboBoxGroupIp.Items31"),
            resources.GetString("ComboBoxGroupIp.Items32"),
            resources.GetString("ComboBoxGroupIp.Items33"),
            resources.GetString("ComboBoxGroupIp.Items34"),
            resources.GetString("ComboBoxGroupIp.Items35"),
            resources.GetString("ComboBoxGroupIp.Items36"),
            resources.GetString("ComboBoxGroupIp.Items37"),
            resources.GetString("ComboBoxGroupIp.Items38"),
            resources.GetString("ComboBoxGroupIp.Items39"),
            resources.GetString("ComboBoxGroupIp.Items40"),
            resources.GetString("ComboBoxGroupIp.Items41"),
            resources.GetString("ComboBoxGroupIp.Items42"),
            resources.GetString("ComboBoxGroupIp.Items43"),
            resources.GetString("ComboBoxGroupIp.Items44"),
            resources.GetString("ComboBoxGroupIp.Items45"),
            resources.GetString("ComboBoxGroupIp.Items46"),
            resources.GetString("ComboBoxGroupIp.Items47"),
            resources.GetString("ComboBoxGroupIp.Items48"),
            resources.GetString("ComboBoxGroupIp.Items49"),
            resources.GetString("ComboBoxGroupIp.Items50"),
            resources.GetString("ComboBoxGroupIp.Items51"),
            resources.GetString("ComboBoxGroupIp.Items52"),
            resources.GetString("ComboBoxGroupIp.Items53"),
            resources.GetString("ComboBoxGroupIp.Items54"),
            resources.GetString("ComboBoxGroupIp.Items55"),
            resources.GetString("ComboBoxGroupIp.Items56"),
            resources.GetString("ComboBoxGroupIp.Items57"),
            resources.GetString("ComboBoxGroupIp.Items58"),
            resources.GetString("ComboBoxGroupIp.Items59"),
            resources.GetString("ComboBoxGroupIp.Items60"),
            resources.GetString("ComboBoxGroupIp.Items61"),
            resources.GetString("ComboBoxGroupIp.Items62"),
            resources.GetString("ComboBoxGroupIp.Items63")});
            resources.ApplyResources(this.ComboBoxGroupIp, "ComboBoxGroupIp");
            this.ComboBoxGroupIp.Name = "ComboBoxGroupIp";
            // 
            // labelGroupIp
            // 
            resources.ApplyResources(this.labelGroupIp, "labelGroupIp");
            this.labelGroupIp.Name = "labelGroupIp";
            // 
            // labelDeviceAlias
            // 
            resources.ApplyResources(this.labelDeviceAlias, "labelDeviceAlias");
            this.labelDeviceAlias.Name = "labelDeviceAlias";
            // 
            // BtnCancel
            // 
            resources.ApplyResources(this.BtnCancel, "BtnCancel");
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnOK
            // 
            resources.ApplyResources(this.BtnOK, "BtnOK");
            this.BtnOK.Name = "BtnOK";
            this.BtnOK.UseVisualStyleBackColor = true;
            this.BtnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // ipAddressControlDevIP
            // 
            this.ipAddressControlDevIP.AllowInternalTab = false;
            this.ipAddressControlDevIP.AutoHeight = true;
            this.ipAddressControlDevIP.BackColor = System.Drawing.SystemColors.Window;
            this.ipAddressControlDevIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ipAddressControlDevIP.Cursor = System.Windows.Forms.Cursors.IBeam;
            resources.ApplyResources(this.ipAddressControlDevIP, "ipAddressControlDevIP");
            this.ipAddressControlDevIP.Name = "ipAddressControlDevIP";
            this.ipAddressControlDevIP.ReadOnly = false;
            // 
            // ipAddressControlGW
            // 
            this.ipAddressControlGW.AllowInternalTab = false;
            this.ipAddressControlGW.AutoHeight = true;
            this.ipAddressControlGW.BackColor = System.Drawing.SystemColors.Window;
            this.ipAddressControlGW.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ipAddressControlGW.Cursor = System.Windows.Forms.Cursors.IBeam;
            resources.ApplyResources(this.ipAddressControlGW, "ipAddressControlGW");
            this.ipAddressControlGW.Name = "ipAddressControlGW";
            this.ipAddressControlGW.ReadOnly = false;
            // 
            // ipAddressControlSM
            // 
            this.ipAddressControlSM.AllowInternalTab = false;
            this.ipAddressControlSM.AutoHeight = true;
            this.ipAddressControlSM.BackColor = System.Drawing.SystemColors.Window;
            this.ipAddressControlSM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ipAddressControlSM.Cursor = System.Windows.Forms.Cursors.IBeam;
            resources.ApplyResources(this.ipAddressControlSM, "ipAddressControlSM");
            this.ipAddressControlSM.Name = "ipAddressControlSM";
            this.ipAddressControlSM.ReadOnly = false;
            // 
            // labelCamID
            // 
            resources.ApplyResources(this.labelCamID, "labelCamID");
            this.labelCamID.Name = "labelCamID";
            // 
            // labelCamPW
            // 
            resources.ApplyResources(this.labelCamPW, "labelCamPW");
            this.labelCamPW.Name = "labelCamPW";
            // 
            // textBoxCamID
            // 
            resources.ApplyResources(this.textBoxCamID, "textBoxCamID");
            this.textBoxCamID.Name = "textBoxCamID";
            // 
            // textBoxCamPW
            // 
            resources.ApplyResources(this.textBoxCamPW, "textBoxCamPW");
            this.textBoxCamPW.Name = "textBoxCamPW";
            // 
            // checkBoxSD
            // 
            resources.ApplyResources(this.checkBoxSD, "checkBoxSD");
            this.checkBoxSD.Name = "checkBoxSD";
            this.checkBoxSD.UseVisualStyleBackColor = true;
            // 
            // textBoxAlias
            // 
            resources.ApplyResources(this.textBoxAlias, "textBoxAlias");
            this.textBoxAlias.Name = "textBoxAlias";
            // 
            // labelDeviceMac
            // 
            resources.ApplyResources(this.labelDeviceMac, "labelDeviceMac");
            this.labelDeviceMac.Name = "labelDeviceMac";
            // 
            // textBoxDeviceMac
            // 
            resources.ApplyResources(this.textBoxDeviceMac, "textBoxDeviceMac");
            this.textBoxDeviceMac.Name = "textBoxDeviceMac";
            // 
            // DeviceAddressControl
            // 
            this.DeviceAddressControl.AllowInternalTab = false;
            this.DeviceAddressControl.AutoHeight = true;
            this.DeviceAddressControl.BackColor = System.Drawing.SystemColors.Window;
            this.DeviceAddressControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DeviceAddressControl.Cursor = System.Windows.Forms.Cursors.IBeam;
            resources.ApplyResources(this.DeviceAddressControl, "DeviceAddressControl");
            this.DeviceAddressControl.Name = "DeviceAddressControl";
            this.DeviceAddressControl.ReadOnly = false;
            // 
            // DialogDevAlter
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DeviceAddressControl);
            this.Controls.Add(this.labelDeviceMac);
            this.Controls.Add(this.textBoxDeviceMac);
            this.Controls.Add(this.textBoxAlias);
            this.Controls.Add(this.checkBoxSD);
            this.Controls.Add(this.textBoxCamPW);
            this.Controls.Add(this.textBoxCamID);
            this.Controls.Add(this.labelCamID);
            this.Controls.Add(this.labelCamPW);
            this.Controls.Add(this.ipAddressControlSM);
            this.Controls.Add(this.ipAddressControlGW);
            this.Controls.Add(this.ipAddressControlDevIP);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnOK);
            this.Controls.Add(this.ComboBoxDeviceType);
            this.Controls.Add(this.labelDeviceAddr);
            this.Controls.Add(this.labelDeviceType);
            this.Controls.Add(this.labelDeviceIp);
            this.Controls.Add(this.labelDeviceGateway);
            this.Controls.Add(this.labelDeviceSubnetMask);
            this.Controls.Add(this.ComboBoxGroupIp);
            this.Controls.Add(this.labelGroupIp);
            this.Controls.Add(this.labelDeviceAlias);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "DialogDevAlter";
            this.Load += new System.EventHandler(this.DialogDevAlter_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ComboBoxDeviceType;
        private System.Windows.Forms.Label labelDeviceAddr;
        private System.Windows.Forms.Label labelDeviceType;
        private System.Windows.Forms.Label labelDeviceIp;
        private System.Windows.Forms.Label labelDeviceGateway;
        private System.Windows.Forms.Label labelDeviceSubnetMask;
        private System.Windows.Forms.ComboBox ComboBoxGroupIp;
        private System.Windows.Forms.Label labelGroupIp;
        private System.Windows.Forms.Label labelDeviceAlias;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.Button BtnOK;
        private IPAddressControlLib.IPAddressControl ipAddressControlDevIP;
        private IPAddressControlLib.IPAddressControl ipAddressControlGW;
        private IPAddressControlLib.IPAddressControl ipAddressControlSM;
        private System.Windows.Forms.Label labelCamID;
        private System.Windows.Forms.Label labelCamPW;
        private System.Windows.Forms.TextBox textBoxCamID;
        private System.Windows.Forms.TextBox textBoxCamPW;
        private System.Windows.Forms.CheckBox checkBoxSD;
        private System.Windows.Forms.TextBox textBoxAlias;
        private System.Windows.Forms.Label labelDeviceMac;
        private System.Windows.Forms.MaskedTextBox textBoxDeviceMac;
        private DeviceAddressControlLib.DeviceAddressControl DeviceAddressControl;
    }
}
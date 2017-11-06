namespace ICMServer
{
    partial class DialogSelectUpgrade
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogSelectUpgrade));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BtnSelectFile = new System.Windows.Forms.Button();
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxFileName = new System.Windows.Forms.TextBox();
            this.textBoxVersion = new System.Windows.Forms.TextBox();
            this.ComboBoxDevType = new System.Windows.Forms.ComboBox();
            this.ComboBoxUpgradeType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnConfirm = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BtnSelectFile);
            this.groupBox1.Controls.Add(this.textBoxPath);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // BtnSelectFile
            // 
            resources.ApplyResources(this.BtnSelectFile, "BtnSelectFile");
            this.BtnSelectFile.Name = "BtnSelectFile";
            this.BtnSelectFile.UseVisualStyleBackColor = true;
            this.BtnSelectFile.Click += new System.EventHandler(this.BtnSelectFile_Click);
            // 
            // textBoxPath
            // 
            resources.ApplyResources(this.textBoxPath, "textBoxPath");
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.ReadOnly = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxFileName);
            this.groupBox2.Controls.Add(this.textBoxVersion);
            this.groupBox2.Controls.Add(this.ComboBoxDevType);
            this.groupBox2.Controls.Add(this.ComboBoxUpgradeType);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // textBoxFileName
            // 
            resources.ApplyResources(this.textBoxFileName, "textBoxFileName");
            this.textBoxFileName.Name = "textBoxFileName";
            // 
            // textBoxVersion
            // 
            resources.ApplyResources(this.textBoxVersion, "textBoxVersion");
            this.textBoxVersion.Name = "textBoxVersion";
            // 
            // ComboBoxDevType
            // 
            this.ComboBoxDevType.FormattingEnabled = true;
            this.ComboBoxDevType.Items.AddRange(new object[] {
            resources.GetString("ComboBoxDevType.Items"),
            resources.GetString("ComboBoxDevType.Items1"),
            resources.GetString("ComboBoxDevType.Items2"),
            resources.GetString("ComboBoxDevType.Items3"),
            resources.GetString("ComboBoxDevType.Items4"),
            resources.GetString("ComboBoxDevType.Items5"),
            resources.GetString("ComboBoxDevType.Items6"),
            resources.GetString("ComboBoxDevType.Items7")});
            resources.ApplyResources(this.ComboBoxDevType, "ComboBoxDevType");
            this.ComboBoxDevType.Name = "ComboBoxDevType";
            this.ComboBoxDevType.SelectedIndexChanged += new System.EventHandler(this.ComboBoxDevType_SelectedIndexChanged);
            // 
            // ComboBoxUpgradeType
            // 
            this.ComboBoxUpgradeType.FormattingEnabled = true;
            this.ComboBoxUpgradeType.Items.AddRange(new object[] {
            resources.GetString("ComboBoxUpgradeType.Items"),
            resources.GetString("ComboBoxUpgradeType.Items1"),
            resources.GetString("ComboBoxUpgradeType.Items2"),
            resources.GetString("ComboBoxUpgradeType.Items3")});
            resources.ApplyResources(this.ComboBoxUpgradeType, "ComboBoxUpgradeType");
            this.ComboBoxUpgradeType.Name = "ComboBoxUpgradeType";
            this.ComboBoxUpgradeType.SelectedIndexChanged += new System.EventHandler(this.ComboBoxUpgradeType_SelectedIndexChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // BtnConfirm
            // 
            resources.ApplyResources(this.BtnConfirm, "BtnConfirm");
            this.BtnConfirm.Name = "BtnConfirm";
            this.BtnConfirm.UseVisualStyleBackColor = true;
            this.BtnConfirm.Click += new System.EventHandler(this.BtnConfirm_Click);
            // 
            // BtnCancel
            // 
            resources.ApplyResources(this.BtnCancel, "BtnCancel");
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // DialogSelectUpgrade
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnConfirm);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "DialogSelectUpgrade";
            this.Load += new System.EventHandler(this.DialogSelectUpgrade_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button BtnSelectFile;
        private System.Windows.Forms.TextBox textBoxPath;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBoxFileName;
        private System.Windows.Forms.TextBox textBoxVersion;
        private System.Windows.Forms.ComboBox ComboBoxDevType;
        private System.Windows.Forms.ComboBox ComboBoxUpgradeType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnConfirm;
        private System.Windows.Forms.Button BtnCancel;
    }
}
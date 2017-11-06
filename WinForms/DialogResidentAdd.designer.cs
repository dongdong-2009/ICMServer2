namespace ICMServer
{
    partial class DialogAddResident
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogAddResident));
            this.BtnCancel = new System.Windows.Forms.Button();
            this.BtnOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.textBoxRoomID = new System.Windows.Forms.TextBox();
            this.textBoxPhone = new System.Windows.Forms.TextBox();
            this.textBoxPID = new System.Windows.Forms.TextBox();
            this.ComboBoxSex = new System.Windows.Forms.ComboBox();
            this.dateTimePickerBirth = new System.Windows.Forms.DateTimePicker();
            this.ComboBoxIsResident = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
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
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
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
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // textBoxName
            // 
            resources.ApplyResources(this.textBoxName, "textBoxName");
            this.textBoxName.Name = "textBoxName";
            // 
            // textBoxRoomID
            // 
            resources.ApplyResources(this.textBoxRoomID, "textBoxRoomID");
            this.textBoxRoomID.Name = "textBoxRoomID";
            // 
            // textBoxPhone
            // 
            resources.ApplyResources(this.textBoxPhone, "textBoxPhone");
            this.textBoxPhone.Name = "textBoxPhone";
            // 
            // textBoxPID
            // 
            resources.ApplyResources(this.textBoxPID, "textBoxPID");
            this.textBoxPID.Name = "textBoxPID";
            // 
            // ComboBoxSex
            // 
            this.ComboBoxSex.FormattingEnabled = true;
            this.ComboBoxSex.Items.AddRange(new object[] {
            resources.GetString("ComboBoxSex.Items"),
            resources.GetString("ComboBoxSex.Items1")});
            resources.ApplyResources(this.ComboBoxSex, "ComboBoxSex");
            this.ComboBoxSex.Name = "ComboBoxSex";
            // 
            // dateTimePickerBirth
            // 
            resources.ApplyResources(this.dateTimePickerBirth, "dateTimePickerBirth");
            this.dateTimePickerBirth.Name = "dateTimePickerBirth";
            // 
            // ComboBoxIsResident
            // 
            this.ComboBoxIsResident.FormattingEnabled = true;
            this.ComboBoxIsResident.Items.AddRange(new object[] {
            resources.GetString("ComboBoxIsResident.Items"),
            resources.GetString("ComboBoxIsResident.Items1")});
            resources.ApplyResources(this.ComboBoxIsResident, "ComboBoxIsResident");
            this.ComboBoxIsResident.Name = "ComboBoxIsResident";
            // 
            // DialogAddResident
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ComboBoxIsResident);
            this.Controls.Add(this.dateTimePickerBirth);
            this.Controls.Add(this.ComboBoxSex);
            this.Controls.Add(this.textBoxPID);
            this.Controls.Add(this.textBoxPhone);
            this.Controls.Add(this.textBoxRoomID);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "DialogAddResident";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.Button BtnOK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.TextBox textBoxRoomID;
        private System.Windows.Forms.TextBox textBoxPhone;
        private System.Windows.Forms.TextBox textBoxPID;
        private System.Windows.Forms.ComboBox ComboBoxSex;
        private System.Windows.Forms.DateTimePicker dateTimePickerBirth;
        private System.Windows.Forms.ComboBox ComboBoxIsResident;
    }
}
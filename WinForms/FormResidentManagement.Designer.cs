namespace ICMServer
{
    partial class FormResidentManagement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormResidentManagement));
            this.listViewResidentInfo = new System.Windows.Forms.ListView();
            this.BtnSearch = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.ComboBoxIsResident = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BtnDel = new System.Windows.Forms.Button();
            this.BtnAddNewHolder = new System.Windows.Forms.Button();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.checkBoxAllSelect = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewResidentInfo
            // 
            resources.ApplyResources(this.listViewResidentInfo, "listViewResidentInfo");
            this.listViewResidentInfo.Name = "listViewResidentInfo";
            this.listViewResidentInfo.UseCompatibleStateImageBehavior = false;
            // 
            // BtnSearch
            // 
            resources.ApplyResources(this.BtnSearch, "BtnSearch");
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.UseVisualStyleBackColor = true;
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // ComboBoxIsResident
            // 
            resources.ApplyResources(this.ComboBoxIsResident, "ComboBoxIsResident");
            this.ComboBoxIsResident.FormattingEnabled = true;
            this.ComboBoxIsResident.Items.AddRange(new object[] {
            resources.GetString("ComboBoxIsResident.Items"),
            resources.GetString("ComboBoxIsResident.Items1")});
            this.ComboBoxIsResident.Name = "ComboBoxIsResident";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.BtnDel);
            this.groupBox1.Controls.Add(this.BtnAddNewHolder);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // BtnDel
            // 
            resources.ApplyResources(this.BtnDel, "BtnDel");
            this.BtnDel.Name = "BtnDel";
            this.BtnDel.UseVisualStyleBackColor = true;
            this.BtnDel.Click += new System.EventHandler(this.BtnDel_Click);
            // 
            // BtnAddNewHolder
            // 
            resources.ApplyResources(this.BtnAddNewHolder, "BtnAddNewHolder");
            this.BtnAddNewHolder.Name = "BtnAddNewHolder";
            this.BtnAddNewHolder.UseVisualStyleBackColor = true;
            this.BtnAddNewHolder.Click += new System.EventHandler(this.BtnAddNewHolder_Click);
            // 
            // textBoxName
            // 
            resources.ApplyResources(this.textBoxName, "textBoxName");
            this.textBoxName.Name = "textBoxName";
            // 
            // checkBoxAllSelect
            // 
            resources.ApplyResources(this.checkBoxAllSelect, "checkBoxAllSelect");
            this.checkBoxAllSelect.Name = "checkBoxAllSelect";
            this.checkBoxAllSelect.UseVisualStyleBackColor = true;
            this.checkBoxAllSelect.CheckedChanged += new System.EventHandler(this.CheckBoxAllSelect_CheckedChanged);
            // 
            // FormResidentManagement
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBoxAllSelect);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.listViewResidentInfo);
            this.Controls.Add(this.BtnSearch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ComboBoxIsResident);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormResidentManagement";
            this.Load += new System.EventHandler(this.FormResidentManagement_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listViewResidentInfo;
        private System.Windows.Forms.Button BtnSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ComboBoxIsResident;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button BtnDel;
        private System.Windows.Forms.Button BtnAddNewHolder;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.CheckBox checkBoxAllSelect;
    }
}
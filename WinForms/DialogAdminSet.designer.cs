namespace ICMServer
{
    partial class DialogAdminSet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogAdminSet));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BtnDelAutho = new System.Windows.Forms.Button();
            this.BtnModAutho = new System.Windows.Forms.Button();
            this.BtnAddAutho = new System.Windows.Forms.Button();
            this.listViewAutho = new System.Windows.Forms.ListView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.BtnDelAdmin = new System.Windows.Forms.Button();
            this.BtnModAdmin = new System.Windows.Forms.Button();
            this.BtnAddAdmin = new System.Windows.Forms.Button();
            this.listViewAdmin = new System.Windows.Forms.ListView();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BtnDelAutho);
            this.groupBox1.Controls.Add(this.BtnModAutho);
            this.groupBox1.Controls.Add(this.BtnAddAutho);
            this.groupBox1.Controls.Add(this.listViewAutho);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // BtnDelAutho
            // 
            resources.ApplyResources(this.BtnDelAutho, "BtnDelAutho");
            this.BtnDelAutho.Name = "BtnDelAutho";
            this.BtnDelAutho.UseVisualStyleBackColor = true;
            this.BtnDelAutho.Click += new System.EventHandler(this.BtnDelAutho_Click);
            // 
            // BtnModAutho
            // 
            resources.ApplyResources(this.BtnModAutho, "BtnModAutho");
            this.BtnModAutho.Name = "BtnModAutho";
            this.BtnModAutho.UseVisualStyleBackColor = true;
            this.BtnModAutho.Click += new System.EventHandler(this.BtnModAutho_Click);
            // 
            // BtnAddAutho
            // 
            resources.ApplyResources(this.BtnAddAutho, "BtnAddAutho");
            this.BtnAddAutho.Name = "BtnAddAutho";
            this.BtnAddAutho.UseVisualStyleBackColor = true;
            this.BtnAddAutho.Click += new System.EventHandler(this.BtnAddAutho_Click);
            // 
            // listViewAutho
            // 
            this.listViewAutho.CheckBoxes = true;
            resources.ApplyResources(this.listViewAutho, "listViewAutho");
            this.listViewAutho.Name = "listViewAutho";
            this.listViewAutho.UseCompatibleStateImageBehavior = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.BtnDelAdmin);
            this.groupBox2.Controls.Add(this.BtnModAdmin);
            this.groupBox2.Controls.Add(this.BtnAddAdmin);
            this.groupBox2.Controls.Add(this.listViewAdmin);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // BtnDelAdmin
            // 
            resources.ApplyResources(this.BtnDelAdmin, "BtnDelAdmin");
            this.BtnDelAdmin.Name = "BtnDelAdmin";
            this.BtnDelAdmin.UseVisualStyleBackColor = true;
            this.BtnDelAdmin.Click += new System.EventHandler(this.BtnDelAdmin_Click);
            // 
            // BtnModAdmin
            // 
            resources.ApplyResources(this.BtnModAdmin, "BtnModAdmin");
            this.BtnModAdmin.Name = "BtnModAdmin";
            this.BtnModAdmin.UseVisualStyleBackColor = true;
            this.BtnModAdmin.Click += new System.EventHandler(this.BtnModAdmin_Click);
            // 
            // BtnAddAdmin
            // 
            resources.ApplyResources(this.BtnAddAdmin, "BtnAddAdmin");
            this.BtnAddAdmin.Name = "BtnAddAdmin";
            this.BtnAddAdmin.UseVisualStyleBackColor = true;
            this.BtnAddAdmin.Click += new System.EventHandler(this.BtnAddAdmin_Click);
            // 
            // listViewAdmin
            // 
            this.listViewAdmin.CheckBoxes = true;
            resources.ApplyResources(this.listViewAdmin, "listViewAdmin");
            this.listViewAdmin.Name = "listViewAdmin";
            this.listViewAdmin.UseCompatibleStateImageBehavior = false;
            // 
            // DialogAdminSet
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "DialogAdminSet";
            this.Load += new System.EventHandler(this.AdminSet_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button BtnDelAutho;
        private System.Windows.Forms.Button BtnModAutho;
        private System.Windows.Forms.Button BtnAddAutho;
        private System.Windows.Forms.ListView listViewAutho;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button BtnDelAdmin;
        private System.Windows.Forms.Button BtnModAdmin;
        private System.Windows.Forms.Button BtnAddAdmin;
        private System.Windows.Forms.ListView listViewAdmin;
    }
}
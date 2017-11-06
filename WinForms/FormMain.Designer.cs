namespace ICMServer
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.menuItemSystem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSystemLanguage = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSimplifiedChinese = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemExitSystem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemBasicFunction = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemVideoTalk = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDeviceManagement = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemResidentManagement = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDoorAccessCtrl = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAnnouncementManagement = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSoftwareUpgrade = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSystemManagement = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemLogManagement = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemLeaveMessage = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.BtnBasicFunctionsBlock = new System.Windows.Forms.ToolStripButton();
            this.BtnVideoTalk = new System.Windows.Forms.ToolStripButton();
            this.BtnDeviceManagement = new System.Windows.Forms.ToolStripButton();
            this.BtnResidentManagement = new System.Windows.Forms.ToolStripButton();
            this.BtnDoorAccessCtrl = new System.Windows.Forms.ToolStripButton();
            this.BtnAnnouncementManagement = new System.Windows.Forms.ToolStripButton();
            this.BtnSoftwareUpgrade = new System.Windows.Forms.ToolStripButton();
            this.BtnSystemManagement = new System.Windows.Forms.ToolStripButton();
            this.BtnLogManagement = new System.Windows.Forms.ToolStripButton();
            this.BtnLeaveMessage = new System.Windows.Forms.ToolStripButton();
            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemSystem,
            this.menuItemBasicFunction,
            this.menuItemHelp});
            resources.ApplyResources(this.menuStrip, "menuStrip");
            this.menuStrip.Name = "menuStrip";
            // 
            // menuItemSystem
            // 
            this.menuItemSystem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemSystemLanguage,
            this.menuItemExitSystem});
            this.menuItemSystem.Name = "menuItemSystem";
            resources.ApplyResources(this.menuItemSystem, "menuItemSystem");
            // 
            // menuItemSystemLanguage
            // 
            this.menuItemSystemLanguage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemSimplifiedChinese});
            this.menuItemSystemLanguage.Name = "menuItemSystemLanguage";
            resources.ApplyResources(this.menuItemSystemLanguage, "menuItemSystemLanguage");
            // 
            // menuItemSimplifiedChinese
            // 
            this.menuItemSimplifiedChinese.Name = "menuItemSimplifiedChinese";
            resources.ApplyResources(this.menuItemSimplifiedChinese, "menuItemSimplifiedChinese");
            this.menuItemSimplifiedChinese.Click += new System.EventHandler(this.MenuItemSimplifiedChinese_Click);
            // 
            // menuItemExitSystem
            // 
            this.menuItemExitSystem.Name = "menuItemExitSystem";
            resources.ApplyResources(this.menuItemExitSystem, "menuItemExitSystem");
            this.menuItemExitSystem.Click += new System.EventHandler(this.MenuItemExitSystem_Click);
            // 
            // menuItemBasicFunction
            // 
            this.menuItemBasicFunction.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemVideoTalk,
            this.menuItemDeviceManagement,
            this.menuItemResidentManagement,
            this.menuItemDoorAccessCtrl,
            this.menuItemAnnouncementManagement,
            this.menuItemSoftwareUpgrade,
            this.menuItemSystemManagement,
            this.menuItemLogManagement,
            this.menuItemLeaveMessage});
            this.menuItemBasicFunction.Name = "menuItemBasicFunction";
            resources.ApplyResources(this.menuItemBasicFunction, "menuItemBasicFunction");
            // 
            // menuItemVideoTalk
            // 
            this.menuItemVideoTalk.Name = "menuItemVideoTalk";
            resources.ApplyResources(this.menuItemVideoTalk, "menuItemVideoTalk");
            this.menuItemVideoTalk.Click += new System.EventHandler(this.ToolStripMenuItem1_Click);
            // 
            // menuItemDeviceManagement
            // 
            this.menuItemDeviceManagement.Name = "menuItemDeviceManagement";
            resources.ApplyResources(this.menuItemDeviceManagement, "menuItemDeviceManagement");
            this.menuItemDeviceManagement.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // menuItemResidentManagement
            // 
            this.menuItemResidentManagement.Name = "menuItemResidentManagement";
            resources.ApplyResources(this.menuItemResidentManagement, "menuItemResidentManagement");
            this.menuItemResidentManagement.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // menuItemDoorAccessCtrl
            // 
            this.menuItemDoorAccessCtrl.Name = "menuItemDoorAccessCtrl";
            resources.ApplyResources(this.menuItemDoorAccessCtrl, "menuItemDoorAccessCtrl");
            this.menuItemDoorAccessCtrl.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // menuItemAnnouncementManagement
            // 
            this.menuItemAnnouncementManagement.Name = "menuItemAnnouncementManagement";
            resources.ApplyResources(this.menuItemAnnouncementManagement, "menuItemAnnouncementManagement");
            this.menuItemAnnouncementManagement.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // menuItemSoftwareUpgrade
            // 
            this.menuItemSoftwareUpgrade.Name = "menuItemSoftwareUpgrade";
            resources.ApplyResources(this.menuItemSoftwareUpgrade, "menuItemSoftwareUpgrade");
            this.menuItemSoftwareUpgrade.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // menuItemSystemManagement
            // 
            this.menuItemSystemManagement.Name = "menuItemSystemManagement";
            resources.ApplyResources(this.menuItemSystemManagement, "menuItemSystemManagement");
            this.menuItemSystemManagement.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // menuItemLogManagement
            // 
            this.menuItemLogManagement.Name = "menuItemLogManagement";
            resources.ApplyResources(this.menuItemLogManagement, "menuItemLogManagement");
            this.menuItemLogManagement.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // menuItemLeaveMessage
            // 
            this.menuItemLeaveMessage.Name = "menuItemLeaveMessage";
            resources.ApplyResources(this.menuItemLeaveMessage, "menuItemLeaveMessage");
            this.menuItemLeaveMessage.Click += new System.EventHandler(this.MenuItem_Click);
            // 
            // menuItemHelp
            // 
            this.menuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemAbout});
            this.menuItemHelp.Name = "menuItemHelp";
            resources.ApplyResources(this.menuItemHelp, "menuItemHelp");
            // 
            // menuItemAbout
            // 
            this.menuItemAbout.Name = "menuItemAbout";
            resources.ApplyResources(this.menuItemAbout, "menuItemAbout");
            this.menuItemAbout.Click += new System.EventHandler(this.MenuItemAbout_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            resources.ApplyResources(this.statusStrip, "statusStrip");
            this.statusStrip.Name = "statusStrip";
            // 
            // toolStrip
            // 
            resources.ApplyResources(this.toolStrip, "toolStrip");
            this.toolStrip.BackColor = System.Drawing.Color.Black;
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BtnBasicFunctionsBlock,
            this.BtnVideoTalk,
            this.BtnDeviceManagement,
            this.BtnResidentManagement,
            this.BtnDoorAccessCtrl,
            this.BtnAnnouncementManagement,
            this.BtnSoftwareUpgrade,
            this.BtnSystemManagement,
            this.BtnLogManagement,
            this.BtnLeaveMessage});
            this.toolStrip.Name = "toolStrip";
            // 
            // BtnBasicFunctionsBlock
            // 
            this.BtnBasicFunctionsBlock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BtnBasicFunctionsBlock.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.BtnBasicFunctionsBlock.ForeColor = System.Drawing.Color.Red;
            resources.ApplyResources(this.BtnBasicFunctionsBlock, "BtnBasicFunctionsBlock");
            this.BtnBasicFunctionsBlock.Name = "BtnBasicFunctionsBlock";
            this.BtnBasicFunctionsBlock.Click += new System.EventHandler(this.FunctionsBlockButton_Click);
            // 
            // BtnVideoTalk
            // 
            this.BtnVideoTalk.ForeColor = System.Drawing.Color.White;
            this.BtnVideoTalk.Image = global::ICMServer.FormMainResource.BtnVideoTalk;
            resources.ApplyResources(this.BtnVideoTalk, "BtnVideoTalk");
            this.BtnVideoTalk.Name = "BtnVideoTalk";
            this.BtnVideoTalk.Click += new System.EventHandler(this.Button_Click);
            // 
            // BtnDeviceManagement
            // 
            this.BtnDeviceManagement.ForeColor = System.Drawing.Color.White;
            this.BtnDeviceManagement.Image = global::ICMServer.FormMainResource.BtnDeviceManagement;
            resources.ApplyResources(this.BtnDeviceManagement, "BtnDeviceManagement");
            this.BtnDeviceManagement.Name = "BtnDeviceManagement";
            this.BtnDeviceManagement.Click += new System.EventHandler(this.Button_Click);
            // 
            // BtnResidentManagement
            // 
            this.BtnResidentManagement.ForeColor = System.Drawing.Color.White;
            this.BtnResidentManagement.Image = global::ICMServer.FormMainResource.BtnResidentManagement;
            resources.ApplyResources(this.BtnResidentManagement, "BtnResidentManagement");
            this.BtnResidentManagement.Name = "BtnResidentManagement";
            this.BtnResidentManagement.Click += new System.EventHandler(this.Button_Click);
            // 
            // BtnDoorAccessCtrl
            // 
            this.BtnDoorAccessCtrl.ForeColor = System.Drawing.Color.White;
            this.BtnDoorAccessCtrl.Image = global::ICMServer.FormMainResource.BtnDoorAccessCtrl;
            resources.ApplyResources(this.BtnDoorAccessCtrl, "BtnDoorAccessCtrl");
            this.BtnDoorAccessCtrl.Name = "BtnDoorAccessCtrl";
            this.BtnDoorAccessCtrl.Click += new System.EventHandler(this.Button_Click);
            // 
            // BtnAnnouncementManagement
            // 
            this.BtnAnnouncementManagement.ForeColor = System.Drawing.Color.White;
            this.BtnAnnouncementManagement.Image = global::ICMServer.FormMainResource.BtnAnnouncementManagement;
            resources.ApplyResources(this.BtnAnnouncementManagement, "BtnAnnouncementManagement");
            this.BtnAnnouncementManagement.Name = "BtnAnnouncementManagement";
            this.BtnAnnouncementManagement.Click += new System.EventHandler(this.Button_Click);
            // 
            // BtnSoftwareUpgrade
            // 
            this.BtnSoftwareUpgrade.ForeColor = System.Drawing.Color.White;
            this.BtnSoftwareUpgrade.Image = global::ICMServer.FormMainResource.BtnSoftwareUpgrade;
            resources.ApplyResources(this.BtnSoftwareUpgrade, "BtnSoftwareUpgrade");
            this.BtnSoftwareUpgrade.Name = "BtnSoftwareUpgrade";
            this.BtnSoftwareUpgrade.Click += new System.EventHandler(this.Button_Click);
            // 
            // BtnSystemManagement
            // 
            this.BtnSystemManagement.ForeColor = System.Drawing.Color.White;
            this.BtnSystemManagement.Image = global::ICMServer.FormMainResource.BtnSystemManagement;
            resources.ApplyResources(this.BtnSystemManagement, "BtnSystemManagement");
            this.BtnSystemManagement.Name = "BtnSystemManagement";
            this.BtnSystemManagement.Click += new System.EventHandler(this.Button_Click);
            // 
            // BtnLogManagement
            // 
            this.BtnLogManagement.ForeColor = System.Drawing.Color.White;
            this.BtnLogManagement.Image = global::ICMServer.FormMainResource.BtnLogManagement;
            resources.ApplyResources(this.BtnLogManagement, "BtnLogManagement");
            this.BtnLogManagement.Name = "BtnLogManagement";
            this.BtnLogManagement.Click += new System.EventHandler(this.Button_Click);
            // 
            // BtnLeaveMessage
            // 
            this.BtnLeaveMessage.ForeColor = System.Drawing.Color.White;
            this.BtnLeaveMessage.Image = global::ICMServer.FormMainResource.BtnLeaveMessage;
            resources.ApplyResources(this.BtnLeaveMessage, "BtnLeaveMessage");
            this.BtnLeaveMessage.Name = "BtnLeaveMessage";
            this.BtnLeaveMessage.Click += new System.EventHandler(this.Button_Click);
            // 
            // FormMain
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "FormMain";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuItemSystem;
        private System.Windows.Forms.ToolStripMenuItem menuItemSystemLanguage;
        private System.Windows.Forms.ToolStripMenuItem menuItemSimplifiedChinese;
        private System.Windows.Forms.ToolStripMenuItem menuItemExitSystem;
        private System.Windows.Forms.ToolStripMenuItem menuItemBasicFunction;
        private System.Windows.Forms.ToolStripMenuItem menuItemDeviceManagement;
        private System.Windows.Forms.ToolStripMenuItem menuItemResidentManagement;
        private System.Windows.Forms.ToolStripMenuItem menuItemDoorAccessCtrl;
        private System.Windows.Forms.ToolStripMenuItem menuItemAnnouncementManagement;
        private System.Windows.Forms.ToolStripMenuItem menuItemSoftwareUpgrade;
        private System.Windows.Forms.ToolStripMenuItem menuItemSystemManagement;
        private System.Windows.Forms.ToolStripMenuItem menuItemLogManagement;
        private System.Windows.Forms.ToolStripMenuItem menuItemLeaveMessage;
        private System.Windows.Forms.ToolStripMenuItem menuItemHelp;
        private System.Windows.Forms.ToolStripMenuItem menuItemAbout;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton BtnBasicFunctionsBlock;
        private System.Windows.Forms.ToolStripButton BtnDeviceManagement;
        private System.Windows.Forms.ToolStripButton BtnResidentManagement;
        private System.Windows.Forms.ToolStripButton BtnDoorAccessCtrl;
        private System.Windows.Forms.ToolStripButton BtnAnnouncementManagement;
        private System.Windows.Forms.ToolStripButton BtnSoftwareUpgrade;
        private System.Windows.Forms.ToolStripButton BtnSystemManagement;
        private System.Windows.Forms.ToolStripButton BtnLogManagement;
        private System.Windows.Forms.ToolStripButton BtnLeaveMessage;
        private System.Windows.Forms.ToolStripButton BtnVideoTalk;
        private System.Windows.Forms.ToolStripMenuItem menuItemVideoTalk;
    }
}
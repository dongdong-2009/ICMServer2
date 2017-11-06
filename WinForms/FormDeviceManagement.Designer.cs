namespace ICMServer
{
    partial class FormDeviceManagement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDeviceManagement));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.PageDeviceInfo = new System.Windows.Forms.TabPage();
            this.dataGridViewDevDataTable = new System.Windows.Forms.DataGridView();
            this.dgvTxtBoxColOnlineStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTxtBoxColDeviceAlias = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvComboBoxColDeviceType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTxtBoxColDeviceId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTxtBoxColIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTxtBoxColSubnetMask = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTxtBoxColGateway = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTxtBoxColMulticastIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTxtBoxColMacAddr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTxtBoxColSoftwareVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bindingSourceDevices = new System.Windows.Forms.BindingSource(this.components);
            this.ComboBoxDevStatus = new System.Windows.Forms.ComboBox();
            this.labelDeviceStatus = new System.Windows.Forms.Label();
            this.ComboBoxDevTypeForPageDevInfo = new System.Windows.Forms.ComboBox();
            this.labelDeviceTypeForPageDevInfo = new System.Windows.Forms.Label();
            this.DeviceInfoToolbox = new System.Windows.Forms.GroupBox();
            this.BtnRefresh = new System.Windows.Forms.Button();
            this.BtnShowDeviceInfo = new System.Windows.Forms.Button();
            this.PageDeviceManagement = new System.Windows.Forms.TabPage();
            this.dataGridViewDevManager = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bindingSourceDevicesForDeviceManagement = new System.Windows.Forms.BindingSource(this.components);
            this.checkBoxAllSelect = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.BtnUpdateAddressBook = new System.Windows.Forms.Button();
            this.BtnExportAddressBook = new System.Windows.Forms.Button();
            this.BtnImportAddressBook = new System.Windows.Forms.Button();
            this.BtnRefreshDevMan = new System.Windows.Forms.Button();
            this.BtnDelDev = new System.Windows.Forms.Button();
            this.BtnModifyDev = new System.Windows.Forms.Button();
            this.BtnAddDev = new System.Windows.Forms.Button();
            this.listViewDevManager = new System.Windows.Forms.ListView();
            this.buttonQuery = new System.Windows.Forms.Button();
            this.ComboBoxQueryTypeForPageDevMgmt = new System.Windows.Forms.ComboBox();
            this.labelDeviceTypeForPageDevMgmt = new System.Windows.Forms.Label();
            this.PageSipAccountManagement = new System.Windows.Forms.TabPage();
            this.listViewSIPAccount = new System.Windows.Forms.ListView();
            this.BtnPushTest = new System.Windows.Forms.Button();
            this.BtnRefreshOnline = new System.Windows.Forms.Button();
            this.BtnSIPAccountRefresh = new System.Windows.Forms.Button();
            this.BtnAccountManager = new System.Windows.Forms.Button();
            this.dataGridViewSIPAccounts = new System.Windows.Forms.DataGridView();
            this.dgvTxtBoxColSIPAccountStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTxtBoxColSIPAccountName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTxtBoxColSIPAccountDeviceId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvTxtBoxColSIPAccountGroup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bindingSourceSIPAccounts = new System.Windows.Forms.BindingSource(this.components);
            this.imageListTreeIcon = new System.Windows.Forms.ImageList(this.components);
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.DeviceTypesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tabControl.SuspendLayout();
            this.PageDeviceInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDevDataTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceDevices)).BeginInit();
            this.DeviceInfoToolbox.SuspendLayout();
            this.PageDeviceManagement.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDevManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceDevicesForDeviceManagement)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.PageSipAccountManagement.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSIPAccounts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceSIPAccounts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeviceTypesBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.Controls.Add(this.PageDeviceInfo);
            this.tabControl.Controls.Add(this.PageDeviceManagement);
            this.tabControl.Controls.Add(this.PageSipAccountManagement);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            // 
            // PageDeviceInfo
            // 
            resources.ApplyResources(this.PageDeviceInfo, "PageDeviceInfo");
            this.PageDeviceInfo.Controls.Add(this.dataGridViewDevDataTable);
            this.PageDeviceInfo.Controls.Add(this.ComboBoxDevStatus);
            this.PageDeviceInfo.Controls.Add(this.labelDeviceStatus);
            this.PageDeviceInfo.Controls.Add(this.ComboBoxDevTypeForPageDevInfo);
            this.PageDeviceInfo.Controls.Add(this.labelDeviceTypeForPageDevInfo);
            this.PageDeviceInfo.Controls.Add(this.DeviceInfoToolbox);
            this.PageDeviceInfo.Name = "PageDeviceInfo";
            this.PageDeviceInfo.UseVisualStyleBackColor = true;
            // 
            // dataGridViewDevDataTable
            // 
            resources.ApplyResources(this.dataGridViewDevDataTable, "dataGridViewDevDataTable");
            this.dataGridViewDevDataTable.AllowUserToAddRows = false;
            this.dataGridViewDevDataTable.AllowUserToDeleteRows = false;
            this.dataGridViewDevDataTable.AutoGenerateColumns = false;
            this.dataGridViewDevDataTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDevDataTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvTxtBoxColOnlineStatus,
            this.dgvTxtBoxColDeviceAlias,
            this.dgvComboBoxColDeviceType,
            this.dgvTxtBoxColDeviceId,
            this.dgvTxtBoxColIP,
            this.dgvTxtBoxColSubnetMask,
            this.dgvTxtBoxColGateway,
            this.dgvTxtBoxColMulticastIP,
            this.dgvTxtBoxColMacAddr,
            this.dataGridViewTextBoxColumn11,
            this.dataGridViewTextBoxColumn12,
            this.dgvTxtBoxColSoftwareVersion});
            this.dataGridViewDevDataTable.DataSource = this.bindingSourceDevices;
            this.dataGridViewDevDataTable.Name = "dataGridViewDevDataTable";
            this.dataGridViewDevDataTable.ReadOnly = true;
            this.dataGridViewDevDataTable.RowTemplate.Height = 24;
            this.dataGridViewDevDataTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewDevDataTable.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DataGridViewDevDataTable_CellFormatting);
            // 
            // dgvTxtBoxColOnlineStatus
            // 
            this.dgvTxtBoxColOnlineStatus.DataPropertyName = "online";
            resources.ApplyResources(this.dgvTxtBoxColOnlineStatus, "dgvTxtBoxColOnlineStatus");
            this.dgvTxtBoxColOnlineStatus.Name = "dgvTxtBoxColOnlineStatus";
            this.dgvTxtBoxColOnlineStatus.ReadOnly = true;
            // 
            // dgvTxtBoxColDeviceAlias
            // 
            this.dgvTxtBoxColDeviceAlias.DataPropertyName = "alias";
            resources.ApplyResources(this.dgvTxtBoxColDeviceAlias, "dgvTxtBoxColDeviceAlias");
            this.dgvTxtBoxColDeviceAlias.Name = "dgvTxtBoxColDeviceAlias";
            this.dgvTxtBoxColDeviceAlias.ReadOnly = true;
            // 
            // dgvComboBoxColDeviceType
            // 
            this.dgvComboBoxColDeviceType.DataPropertyName = "type";
            resources.ApplyResources(this.dgvComboBoxColDeviceType, "dgvComboBoxColDeviceType");
            this.dgvComboBoxColDeviceType.Name = "dgvComboBoxColDeviceType";
            this.dgvComboBoxColDeviceType.ReadOnly = true;
            // 
            // dgvTxtBoxColDeviceId
            // 
            this.dgvTxtBoxColDeviceId.DataPropertyName = "roomid";
            resources.ApplyResources(this.dgvTxtBoxColDeviceId, "dgvTxtBoxColDeviceId");
            this.dgvTxtBoxColDeviceId.Name = "dgvTxtBoxColDeviceId";
            this.dgvTxtBoxColDeviceId.ReadOnly = true;
            // 
            // dgvTxtBoxColIP
            // 
            this.dgvTxtBoxColIP.DataPropertyName = "ip";
            resources.ApplyResources(this.dgvTxtBoxColIP, "dgvTxtBoxColIP");
            this.dgvTxtBoxColIP.Name = "dgvTxtBoxColIP";
            this.dgvTxtBoxColIP.ReadOnly = true;
            // 
            // dgvTxtBoxColSubnetMask
            // 
            this.dgvTxtBoxColSubnetMask.DataPropertyName = "sm";
            resources.ApplyResources(this.dgvTxtBoxColSubnetMask, "dgvTxtBoxColSubnetMask");
            this.dgvTxtBoxColSubnetMask.Name = "dgvTxtBoxColSubnetMask";
            this.dgvTxtBoxColSubnetMask.ReadOnly = true;
            // 
            // dgvTxtBoxColGateway
            // 
            this.dgvTxtBoxColGateway.DataPropertyName = "gw";
            resources.ApplyResources(this.dgvTxtBoxColGateway, "dgvTxtBoxColGateway");
            this.dgvTxtBoxColGateway.Name = "dgvTxtBoxColGateway";
            this.dgvTxtBoxColGateway.ReadOnly = true;
            // 
            // dgvTxtBoxColMulticastIP
            // 
            this.dgvTxtBoxColMulticastIP.DataPropertyName = "group";
            resources.ApplyResources(this.dgvTxtBoxColMulticastIP, "dgvTxtBoxColMulticastIP");
            this.dgvTxtBoxColMulticastIP.Name = "dgvTxtBoxColMulticastIP";
            this.dgvTxtBoxColMulticastIP.ReadOnly = true;
            // 
            // dgvTxtBoxColMacAddr
            // 
            this.dgvTxtBoxColMacAddr.DataPropertyName = "mac";
            resources.ApplyResources(this.dgvTxtBoxColMacAddr, "dgvTxtBoxColMacAddr");
            this.dgvTxtBoxColMacAddr.Name = "dgvTxtBoxColMacAddr";
            this.dgvTxtBoxColMacAddr.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.DataPropertyName = "cameraid";
            resources.ApplyResources(this.dataGridViewTextBoxColumn11, "dataGridViewTextBoxColumn11");
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.DataPropertyName = "camerapw";
            resources.ApplyResources(this.dataGridViewTextBoxColumn12, "dataGridViewTextBoxColumn12");
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.ReadOnly = true;
            // 
            // dgvTxtBoxColSoftwareVersion
            // 
            this.dgvTxtBoxColSoftwareVersion.DataPropertyName = "fVer";
            resources.ApplyResources(this.dgvTxtBoxColSoftwareVersion, "dgvTxtBoxColSoftwareVersion");
            this.dgvTxtBoxColSoftwareVersion.Name = "dgvTxtBoxColSoftwareVersion";
            this.dgvTxtBoxColSoftwareVersion.ReadOnly = true;
            // 
            // bindingSourceDevices
            // 
            this.bindingSourceDevices.DataSource = typeof(ICMServer.Models.Device);
            // 
            // ComboBoxDevStatus
            // 
            resources.ApplyResources(this.ComboBoxDevStatus, "ComboBoxDevStatus");
            this.ComboBoxDevStatus.FormattingEnabled = true;
            this.ComboBoxDevStatus.Items.AddRange(new object[] {
            resources.GetString("ComboBoxDevStatus.Items"),
            resources.GetString("ComboBoxDevStatus.Items1"),
            resources.GetString("ComboBoxDevStatus.Items2")});
            this.ComboBoxDevStatus.Name = "ComboBoxDevStatus";
            this.ComboBoxDevStatus.SelectedIndexChanged += new System.EventHandler(this.ComboBoxDevStatus_SelectedIndexChanged);
            // 
            // labelDeviceStatus
            // 
            resources.ApplyResources(this.labelDeviceStatus, "labelDeviceStatus");
            this.labelDeviceStatus.Name = "labelDeviceStatus";
            // 
            // ComboBoxDevTypeForPageDevInfo
            // 
            resources.ApplyResources(this.ComboBoxDevTypeForPageDevInfo, "ComboBoxDevTypeForPageDevInfo");
            this.ComboBoxDevTypeForPageDevInfo.FormattingEnabled = true;
            this.ComboBoxDevTypeForPageDevInfo.Name = "ComboBoxDevTypeForPageDevInfo";
            this.ComboBoxDevTypeForPageDevInfo.SelectedIndexChanged += new System.EventHandler(this.ComboBoxDevTypeForPageDevInfo_SelectedIndexChanged);
            // 
            // labelDeviceTypeForPageDevInfo
            // 
            resources.ApplyResources(this.labelDeviceTypeForPageDevInfo, "labelDeviceTypeForPageDevInfo");
            this.labelDeviceTypeForPageDevInfo.Name = "labelDeviceTypeForPageDevInfo";
            // 
            // DeviceInfoToolbox
            // 
            resources.ApplyResources(this.DeviceInfoToolbox, "DeviceInfoToolbox");
            this.DeviceInfoToolbox.Controls.Add(this.BtnRefresh);
            this.DeviceInfoToolbox.Controls.Add(this.BtnShowDeviceInfo);
            this.DeviceInfoToolbox.Name = "DeviceInfoToolbox";
            this.DeviceInfoToolbox.TabStop = false;
            // 
            // BtnRefresh
            // 
            resources.ApplyResources(this.BtnRefresh, "BtnRefresh");
            this.BtnRefresh.Name = "BtnRefresh";
            this.BtnRefresh.UseVisualStyleBackColor = true;
            this.BtnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // BtnShowDeviceInfo
            // 
            resources.ApplyResources(this.BtnShowDeviceInfo, "BtnShowDeviceInfo");
            this.BtnShowDeviceInfo.Name = "BtnShowDeviceInfo";
            this.BtnShowDeviceInfo.UseVisualStyleBackColor = true;
            this.BtnShowDeviceInfo.Click += new System.EventHandler(this.BtnShowDeviceInfo_Click);
            // 
            // PageDeviceManagement
            // 
            resources.ApplyResources(this.PageDeviceManagement, "PageDeviceManagement");
            this.PageDeviceManagement.Controls.Add(this.dataGridViewDevManager);
            this.PageDeviceManagement.Controls.Add(this.checkBoxAllSelect);
            this.PageDeviceManagement.Controls.Add(this.groupBox2);
            this.PageDeviceManagement.Controls.Add(this.listViewDevManager);
            this.PageDeviceManagement.Controls.Add(this.buttonQuery);
            this.PageDeviceManagement.Controls.Add(this.ComboBoxQueryTypeForPageDevMgmt);
            this.PageDeviceManagement.Controls.Add(this.labelDeviceTypeForPageDevMgmt);
            this.PageDeviceManagement.Name = "PageDeviceManagement";
            this.PageDeviceManagement.UseVisualStyleBackColor = true;
            // 
            // dataGridViewDevManager
            // 
            resources.ApplyResources(this.dataGridViewDevManager, "dataGridViewDevManager");
            this.dataGridViewDevManager.AllowUserToAddRows = false;
            this.dataGridViewDevManager.AllowUserToDeleteRows = false;
            this.dataGridViewDevManager.AutoGenerateColumns = false;
            this.dataGridViewDevManager.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDevManager.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10,
            this.dataGridViewTextBoxColumn13,
            this.dataGridViewTextBoxColumn14});
            this.dataGridViewDevManager.DataSource = this.bindingSourceDevicesForDeviceManagement;
            this.dataGridViewDevManager.Name = "dataGridViewDevManager";
            this.dataGridViewDevManager.ReadOnly = true;
            this.dataGridViewDevManager.RowTemplate.Height = 24;
            this.dataGridViewDevManager.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewDevManager.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DataGridViewDevManager_CellFormatting);
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "alias";
            resources.ApplyResources(this.dataGridViewTextBoxColumn2, "dataGridViewTextBoxColumn2");
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "type";
            resources.ApplyResources(this.dataGridViewTextBoxColumn3, "dataGridViewTextBoxColumn3");
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "roomid";
            resources.ApplyResources(this.dataGridViewTextBoxColumn4, "dataGridViewTextBoxColumn4");
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "ip";
            resources.ApplyResources(this.dataGridViewTextBoxColumn5, "dataGridViewTextBoxColumn5");
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "sm";
            resources.ApplyResources(this.dataGridViewTextBoxColumn6, "dataGridViewTextBoxColumn6");
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "gw";
            resources.ApplyResources(this.dataGridViewTextBoxColumn7, "dataGridViewTextBoxColumn7");
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "group";
            resources.ApplyResources(this.dataGridViewTextBoxColumn8, "dataGridViewTextBoxColumn8");
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "mac";
            resources.ApplyResources(this.dataGridViewTextBoxColumn9, "dataGridViewTextBoxColumn9");
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.DataPropertyName = "cameraid";
            resources.ApplyResources(this.dataGridViewTextBoxColumn10, "dataGridViewTextBoxColumn10");
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.DataPropertyName = "camerapw";
            resources.ApplyResources(this.dataGridViewTextBoxColumn13, "dataGridViewTextBoxColumn13");
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn14
            // 
            this.dataGridViewTextBoxColumn14.DataPropertyName = "fVer";
            resources.ApplyResources(this.dataGridViewTextBoxColumn14, "dataGridViewTextBoxColumn14");
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            this.dataGridViewTextBoxColumn14.ReadOnly = true;
            // 
            // bindingSourceDevicesForDeviceManagement
            // 
            this.bindingSourceDevicesForDeviceManagement.DataSource = typeof(ICMServer.Models.Device);
            // 
            // checkBoxAllSelect
            // 
            resources.ApplyResources(this.checkBoxAllSelect, "checkBoxAllSelect");
            this.checkBoxAllSelect.Name = "checkBoxAllSelect";
            this.checkBoxAllSelect.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.BtnUpdateAddressBook);
            this.groupBox2.Controls.Add(this.BtnExportAddressBook);
            this.groupBox2.Controls.Add(this.BtnImportAddressBook);
            this.groupBox2.Controls.Add(this.BtnRefreshDevMan);
            this.groupBox2.Controls.Add(this.BtnDelDev);
            this.groupBox2.Controls.Add(this.BtnModifyDev);
            this.groupBox2.Controls.Add(this.BtnAddDev);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // BtnUpdateAddressBook
            // 
            resources.ApplyResources(this.BtnUpdateAddressBook, "BtnUpdateAddressBook");
            this.BtnUpdateAddressBook.Name = "BtnUpdateAddressBook";
            this.BtnUpdateAddressBook.UseVisualStyleBackColor = true;
            this.BtnUpdateAddressBook.Click += new System.EventHandler(this.BtnUpdateAddressBook_Click);
            // 
            // BtnExportAddressBook
            // 
            resources.ApplyResources(this.BtnExportAddressBook, "BtnExportAddressBook");
            this.BtnExportAddressBook.Name = "BtnExportAddressBook";
            this.BtnExportAddressBook.UseVisualStyleBackColor = true;
            this.BtnExportAddressBook.Click += new System.EventHandler(this.BtnExportAddressBook_Click);
            // 
            // BtnImportAddressBook
            // 
            resources.ApplyResources(this.BtnImportAddressBook, "BtnImportAddressBook");
            this.BtnImportAddressBook.Name = "BtnImportAddressBook";
            this.BtnImportAddressBook.UseVisualStyleBackColor = true;
            this.BtnImportAddressBook.Click += new System.EventHandler(this.BtnImportAddressBook_Click);
            // 
            // BtnRefreshDevMan
            // 
            resources.ApplyResources(this.BtnRefreshDevMan, "BtnRefreshDevMan");
            this.BtnRefreshDevMan.Name = "BtnRefreshDevMan";
            this.BtnRefreshDevMan.UseVisualStyleBackColor = true;
            this.BtnRefreshDevMan.Click += new System.EventHandler(this.BtnRefreshDevMan_Click);
            // 
            // BtnDelDev
            // 
            resources.ApplyResources(this.BtnDelDev, "BtnDelDev");
            this.BtnDelDev.Name = "BtnDelDev";
            this.BtnDelDev.UseVisualStyleBackColor = true;
            this.BtnDelDev.Click += new System.EventHandler(this.BtnDelDev_Click);
            // 
            // BtnModifyDev
            // 
            resources.ApplyResources(this.BtnModifyDev, "BtnModifyDev");
            this.BtnModifyDev.Name = "BtnModifyDev";
            this.BtnModifyDev.UseVisualStyleBackColor = true;
            this.BtnModifyDev.Click += new System.EventHandler(this.BtnModifyDev_Click);
            // 
            // BtnAddDev
            // 
            resources.ApplyResources(this.BtnAddDev, "BtnAddDev");
            this.BtnAddDev.Name = "BtnAddDev";
            this.BtnAddDev.UseVisualStyleBackColor = true;
            this.BtnAddDev.Click += new System.EventHandler(this.BtnAddDev_Click);
            // 
            // listViewDevManager
            // 
            resources.ApplyResources(this.listViewDevManager, "listViewDevManager");
            this.listViewDevManager.Name = "listViewDevManager";
            this.listViewDevManager.UseCompatibleStateImageBehavior = false;
            // 
            // buttonQuery
            // 
            resources.ApplyResources(this.buttonQuery, "buttonQuery");
            this.buttonQuery.Name = "buttonQuery";
            this.buttonQuery.UseVisualStyleBackColor = true;
            this.buttonQuery.Click += new System.EventHandler(this.ButtonQuery_Click);
            // 
            // ComboBoxQueryTypeForPageDevMgmt
            // 
            resources.ApplyResources(this.ComboBoxQueryTypeForPageDevMgmt, "ComboBoxQueryTypeForPageDevMgmt");
            this.ComboBoxQueryTypeForPageDevMgmt.FormattingEnabled = true;
            this.ComboBoxQueryTypeForPageDevMgmt.Name = "ComboBoxQueryTypeForPageDevMgmt";
            // 
            // labelDeviceTypeForPageDevMgmt
            // 
            resources.ApplyResources(this.labelDeviceTypeForPageDevMgmt, "labelDeviceTypeForPageDevMgmt");
            this.labelDeviceTypeForPageDevMgmt.Name = "labelDeviceTypeForPageDevMgmt";
            // 
            // PageSipAccountManagement
            // 
            resources.ApplyResources(this.PageSipAccountManagement, "PageSipAccountManagement");
            this.PageSipAccountManagement.Controls.Add(this.listViewSIPAccount);
            this.PageSipAccountManagement.Controls.Add(this.BtnPushTest);
            this.PageSipAccountManagement.Controls.Add(this.BtnRefreshOnline);
            this.PageSipAccountManagement.Controls.Add(this.BtnSIPAccountRefresh);
            this.PageSipAccountManagement.Controls.Add(this.BtnAccountManager);
            this.PageSipAccountManagement.Controls.Add(this.dataGridViewSIPAccounts);
            this.PageSipAccountManagement.Name = "PageSipAccountManagement";
            this.PageSipAccountManagement.UseVisualStyleBackColor = true;
            // 
            // listViewSIPAccount
            // 
            resources.ApplyResources(this.listViewSIPAccount, "listViewSIPAccount");
            this.listViewSIPAccount.Name = "listViewSIPAccount";
            this.listViewSIPAccount.UseCompatibleStateImageBehavior = false;
            // 
            // BtnPushTest
            // 
            resources.ApplyResources(this.BtnPushTest, "BtnPushTest");
            this.BtnPushTest.Name = "BtnPushTest";
            this.BtnPushTest.UseVisualStyleBackColor = true;
            this.BtnPushTest.Click += new System.EventHandler(this.BtnPushTest_Click);
            // 
            // BtnRefreshOnline
            // 
            resources.ApplyResources(this.BtnRefreshOnline, "BtnRefreshOnline");
            this.BtnRefreshOnline.Name = "BtnRefreshOnline";
            this.BtnRefreshOnline.UseVisualStyleBackColor = true;
            this.BtnRefreshOnline.Click += new System.EventHandler(this.BtnRefreshOnline_Click);
            // 
            // BtnSIPAccountRefresh
            // 
            resources.ApplyResources(this.BtnSIPAccountRefresh, "BtnSIPAccountRefresh");
            this.BtnSIPAccountRefresh.Name = "BtnSIPAccountRefresh";
            this.BtnSIPAccountRefresh.UseVisualStyleBackColor = true;
            this.BtnSIPAccountRefresh.Click += new System.EventHandler(this.BtnSIPAccountRefresh_Click);
            // 
            // BtnAccountManager
            // 
            resources.ApplyResources(this.BtnAccountManager, "BtnAccountManager");
            this.BtnAccountManager.Name = "BtnAccountManager";
            this.BtnAccountManager.UseVisualStyleBackColor = true;
            this.BtnAccountManager.Click += new System.EventHandler(this.BtnAccountManager_Click);
            // 
            // dataGridViewSIPAccounts
            // 
            resources.ApplyResources(this.dataGridViewSIPAccounts, "dataGridViewSIPAccounts");
            this.dataGridViewSIPAccounts.AllowUserToAddRows = false;
            this.dataGridViewSIPAccounts.AutoGenerateColumns = false;
            this.dataGridViewSIPAccounts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSIPAccounts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvTxtBoxColSIPAccountStatus,
            this.dgvTxtBoxColSIPAccountName,
            this.dgvTxtBoxColSIPAccountDeviceId,
            this.dgvTxtBoxColSIPAccountGroup});
            this.dataGridViewSIPAccounts.DataSource = this.bindingSourceSIPAccounts;
            this.dataGridViewSIPAccounts.Name = "dataGridViewSIPAccounts";
            this.dataGridViewSIPAccounts.ReadOnly = true;
            this.dataGridViewSIPAccounts.RowTemplate.Height = 24;
            this.dataGridViewSIPAccounts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewSIPAccounts.VirtualMode = true;
            this.dataGridViewSIPAccounts.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DataGridViewSIPAccounts_CellFormatting);
            // 
            // dgvTxtBoxColSIPAccountStatus
            // 
            resources.ApplyResources(this.dgvTxtBoxColSIPAccountStatus, "dgvTxtBoxColSIPAccountStatus");
            this.dgvTxtBoxColSIPAccountStatus.Name = "dgvTxtBoxColSIPAccountStatus";
            this.dgvTxtBoxColSIPAccountStatus.ReadOnly = true;
            // 
            // dgvTxtBoxColSIPAccountName
            // 
            this.dgvTxtBoxColSIPAccountName.DataPropertyName = "C_user";
            resources.ApplyResources(this.dgvTxtBoxColSIPAccountName, "dgvTxtBoxColSIPAccountName");
            this.dgvTxtBoxColSIPAccountName.Name = "dgvTxtBoxColSIPAccountName";
            this.dgvTxtBoxColSIPAccountName.ReadOnly = true;
            // 
            // dgvTxtBoxColSIPAccountDeviceId
            // 
            this.dgvTxtBoxColSIPAccountDeviceId.DataPropertyName = "C_room";
            resources.ApplyResources(this.dgvTxtBoxColSIPAccountDeviceId, "dgvTxtBoxColSIPAccountDeviceId");
            this.dgvTxtBoxColSIPAccountDeviceId.Name = "dgvTxtBoxColSIPAccountDeviceId";
            this.dgvTxtBoxColSIPAccountDeviceId.ReadOnly = true;
            // 
            // dgvTxtBoxColSIPAccountGroup
            // 
            this.dgvTxtBoxColSIPAccountGroup.DataPropertyName = "C_usergroup";
            resources.ApplyResources(this.dgvTxtBoxColSIPAccountGroup, "dgvTxtBoxColSIPAccountGroup");
            this.dgvTxtBoxColSIPAccountGroup.Name = "dgvTxtBoxColSIPAccountGroup";
            this.dgvTxtBoxColSIPAccountGroup.ReadOnly = true;
            // 
            // bindingSourceSIPAccounts
            // 
            this.bindingSourceSIPAccounts.DataSource = typeof(ICMServer.Models.sipaccount);
            // 
            // imageListTreeIcon
            // 
            this.imageListTreeIcon.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            resources.ApplyResources(this.imageListTreeIcon, "imageListTreeIcon");
            this.imageListTreeIcon.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // timer
            // 
            this.timer.Interval = 10000;
            this.timer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // FormDeviceManagement
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormDeviceManagement";
            this.Load += new System.EventHandler(this.FormDeviceManagement_Load);
            this.tabControl.ResumeLayout(false);
            this.PageDeviceInfo.ResumeLayout(false);
            this.PageDeviceInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDevDataTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceDevices)).EndInit();
            this.DeviceInfoToolbox.ResumeLayout(false);
            this.PageDeviceManagement.ResumeLayout(false);
            this.PageDeviceManagement.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDevManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceDevicesForDeviceManagement)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.PageSipAccountManagement.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSIPAccounts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceSIPAccounts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeviceTypesBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage PageDeviceInfo;
        private System.Windows.Forms.TabPage PageDeviceManagement;
        private System.Windows.Forms.GroupBox DeviceInfoToolbox;
        private System.Windows.Forms.Button BtnRefresh;
        private System.Windows.Forms.Button BtnShowDeviceInfo;
        private System.Windows.Forms.ComboBox ComboBoxDevStatus;
        private System.Windows.Forms.Label labelDeviceStatus;
        private System.Windows.Forms.ComboBox ComboBoxDevTypeForPageDevInfo;
        private System.Windows.Forms.Label labelDeviceTypeForPageDevInfo;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button BtnModifyDev;
        private System.Windows.Forms.Button BtnAddDev;
        private System.Windows.Forms.ListView listViewDevManager;
        private System.Windows.Forms.Button buttonQuery;
        private System.Windows.Forms.ComboBox ComboBoxQueryTypeForPageDevMgmt;
        private System.Windows.Forms.Label labelDeviceTypeForPageDevMgmt;
        private System.Windows.Forms.Button BtnImportAddressBook;
        private System.Windows.Forms.Button BtnRefreshDevMan;
        private System.Windows.Forms.Button BtnDelDev;
        private System.Windows.Forms.CheckBox checkBoxAllSelect;
        private System.Windows.Forms.ImageList imageListTreeIcon;
        private System.Windows.Forms.TabPage PageSipAccountManagement;
        private System.Windows.Forms.Button BtnPushTest;
        private System.Windows.Forms.Button BtnRefreshOnline;
        private System.Windows.Forms.Button BtnSIPAccountRefresh;
        private System.Windows.Forms.Button BtnAccountManager;
        private System.Windows.Forms.BindingSource DeviceTypesBindingSource;
        private System.Windows.Forms.DataGridView dataGridViewDevDataTable;
        private System.Windows.Forms.BindingSource bindingSourceDevices;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.DataGridView dataGridViewSIPAccounts;
        private System.Windows.Forms.BindingSource bindingSourceSIPAccounts;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTxtBoxColSIPAccountStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTxtBoxColSIPAccountName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTxtBoxColSIPAccountDeviceId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTxtBoxColSIPAccountGroup;
        private System.Windows.Forms.ListView listViewSIPAccount;
        private System.Windows.Forms.DataGridView dataGridViewDevManager;
        private System.Windows.Forms.BindingSource bindingSourceDevicesForDeviceManagement;
        private System.Windows.Forms.Button BtnUpdateAddressBook;
        private System.Windows.Forms.Button BtnExportAddressBook;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTxtBoxColOnlineStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTxtBoxColDeviceAlias;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvComboBoxColDeviceType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTxtBoxColDeviceId;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTxtBoxColIP;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTxtBoxColSubnetMask;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTxtBoxColGateway;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTxtBoxColMulticastIP;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTxtBoxColMacAddr;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvTxtBoxColSoftwareVersion;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
    }
}
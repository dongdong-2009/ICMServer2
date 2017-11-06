#pragma once

namespace ICMServer {

	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;

	/// <summary>
	/// FormMain 的摘要
	/// </summary>
	public ref class FormMain : public System::Windows::Forms::Form
	{
	public:
		FormMain(void)
		{
			InitializeComponent();
			//
			//TODO:  在此加入建構函式程式碼
			//
		}

	protected:
		/// <summary>
		/// 清除任何使用中的資源。
		/// </summary>
		~FormMain()
		{
			if (components)
			{
				delete components;
			}
		}
    private: System::Windows::Forms::MenuStrip^  menuStrip;
    private: System::Windows::Forms::ToolStripMenuItem^  menuItemSystem;
    protected:

    private: System::Windows::Forms::ToolStripMenuItem^  menuItemSystemLog;
    private: System::Windows::Forms::ToolStripMenuItem^  menuItemSystemLanguage;
    private: System::Windows::Forms::ToolStripMenuItem^  menuItemSimplifiedChinese;
    private: System::Windows::Forms::ToolStripMenuItem^  menuItemTraditionalChinese;
    private: System::Windows::Forms::ToolStripMenuItem^  menuItemExitSystem;
    private: System::Windows::Forms::ToolStripMenuItem^  menuItemBasicFunction;
    private: System::Windows::Forms::ToolStripMenuItem^  menuItemDeviceManagement;
    private: System::Windows::Forms::ToolStripMenuItem^  menuItemResidentManagement;
    private: System::Windows::Forms::ToolStripMenuItem^  menuItemDoorAccessCtrl;
    private: System::Windows::Forms::ToolStripMenuItem^  menuItemAnnouncementManagement;
    private: System::Windows::Forms::ToolStripMenuItem^  menuItemSoftwareUpgrade;
    private: System::Windows::Forms::ToolStripMenuItem^  menuItemSystemManagement;
    private: System::Windows::Forms::ToolStripMenuItem^  menuItemLogManagement;
    private: System::Windows::Forms::ToolStripMenuItem^  menuItemLeaveMessage;
    private: System::Windows::Forms::ToolStripMenuItem^  menuItemTalk;
    private: System::Windows::Forms::ToolStripMenuItem^  menuItemVideoTalk;
    private: System::Windows::Forms::ToolStripMenuItem^  menuItemToolkit;
    private: System::Windows::Forms::ToolStripMenuItem^  menuItemSoftwarePackageToolkit;
    private: System::Windows::Forms::ToolStripMenuItem^  menuItemHelp;
    private: System::Windows::Forms::ToolStripMenuItem^  menuItemAbout;
    private: System::Windows::Forms::ToolStripMenuItem^  menuItemHowTo;
    private: System::Windows::Forms::StatusStrip^  statusStrip;
    private: System::Windows::Forms::ToolStrip^  toolStrip;
    private: System::Windows::Forms::ToolStripButton^  btnBasicFunctionsBlock;
    private: System::Windows::Forms::ToolStripButton^  btnDeviceManagement;
    private: System::Windows::Forms::ToolStripButton^  btnResidentManagement;
    private: System::Windows::Forms::ToolStripButton^  btnDoorAccessCtrl;
    private: System::Windows::Forms::ToolStripButton^  btnAnnouncementManagement;
    private: System::Windows::Forms::ToolStripButton^  btnSoftwareUpgrade;
    private: System::Windows::Forms::ToolStripButton^  btnSystemManagement;
    private: System::Windows::Forms::ToolStripButton^  btnLogManagement;
    private: System::Windows::Forms::ToolStripButton^  btnLeaveMessage;
    private: System::Windows::Forms::ToolStripButton^  btnTalkFunctionBlock;
    private: System::Windows::Forms::ToolStripButton^  btnVideoTalk;








	private:
		/// <summary>
		/// 設計工具所需的變數。
		/// </summary>
		System::ComponentModel::Container ^components;

#pragma region Windows Form Designer generated code
		/// <summary>
		/// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
		/// 修改這個方法的內容。
		/// </summary>
		void InitializeComponent(void)
		{
            System::ComponentModel::ComponentResourceManager^  resources = (gcnew System::ComponentModel::ComponentResourceManager(FormMain::typeid));
            this->menuStrip = (gcnew System::Windows::Forms::MenuStrip());
            this->menuItemSystem = (gcnew System::Windows::Forms::ToolStripMenuItem());
            this->menuItemSystemLog = (gcnew System::Windows::Forms::ToolStripMenuItem());
            this->menuItemSystemLanguage = (gcnew System::Windows::Forms::ToolStripMenuItem());
            this->menuItemSimplifiedChinese = (gcnew System::Windows::Forms::ToolStripMenuItem());
            this->menuItemTraditionalChinese = (gcnew System::Windows::Forms::ToolStripMenuItem());
            this->menuItemExitSystem = (gcnew System::Windows::Forms::ToolStripMenuItem());
            this->menuItemBasicFunction = (gcnew System::Windows::Forms::ToolStripMenuItem());
            this->menuItemDeviceManagement = (gcnew System::Windows::Forms::ToolStripMenuItem());
            this->menuItemResidentManagement = (gcnew System::Windows::Forms::ToolStripMenuItem());
            this->menuItemDoorAccessCtrl = (gcnew System::Windows::Forms::ToolStripMenuItem());
            this->menuItemAnnouncementManagement = (gcnew System::Windows::Forms::ToolStripMenuItem());
            this->menuItemSoftwareUpgrade = (gcnew System::Windows::Forms::ToolStripMenuItem());
            this->menuItemSystemManagement = (gcnew System::Windows::Forms::ToolStripMenuItem());
            this->menuItemLogManagement = (gcnew System::Windows::Forms::ToolStripMenuItem());
            this->menuItemLeaveMessage = (gcnew System::Windows::Forms::ToolStripMenuItem());
            this->menuItemTalk = (gcnew System::Windows::Forms::ToolStripMenuItem());
            this->menuItemVideoTalk = (gcnew System::Windows::Forms::ToolStripMenuItem());
            this->menuItemToolkit = (gcnew System::Windows::Forms::ToolStripMenuItem());
            this->menuItemSoftwarePackageToolkit = (gcnew System::Windows::Forms::ToolStripMenuItem());
            this->menuItemHelp = (gcnew System::Windows::Forms::ToolStripMenuItem());
            this->menuItemAbout = (gcnew System::Windows::Forms::ToolStripMenuItem());
            this->menuItemHowTo = (gcnew System::Windows::Forms::ToolStripMenuItem());
            this->statusStrip = (gcnew System::Windows::Forms::StatusStrip());
            this->toolStrip = (gcnew System::Windows::Forms::ToolStrip());
            this->btnBasicFunctionsBlock = (gcnew System::Windows::Forms::ToolStripButton());
            this->btnDeviceManagement = (gcnew System::Windows::Forms::ToolStripButton());
            this->btnResidentManagement = (gcnew System::Windows::Forms::ToolStripButton());
            this->btnDoorAccessCtrl = (gcnew System::Windows::Forms::ToolStripButton());
            this->btnAnnouncementManagement = (gcnew System::Windows::Forms::ToolStripButton());
            this->btnSoftwareUpgrade = (gcnew System::Windows::Forms::ToolStripButton());
            this->btnSystemManagement = (gcnew System::Windows::Forms::ToolStripButton());
            this->btnLogManagement = (gcnew System::Windows::Forms::ToolStripButton());
            this->btnLeaveMessage = (gcnew System::Windows::Forms::ToolStripButton());
            this->btnTalkFunctionBlock = (gcnew System::Windows::Forms::ToolStripButton());
            this->btnVideoTalk = (gcnew System::Windows::Forms::ToolStripButton());
            this->menuStrip->SuspendLayout();
            this->toolStrip->SuspendLayout();
            this->SuspendLayout();
            //
            // menuStrip
            //
            this->menuStrip->Items->AddRange(gcnew cli::array< System::Windows::Forms::ToolStripItem^  >(5)
            {
                this->menuItemSystem, this->menuItemBasicFunction,
                    this->menuItemTalk, this->menuItemToolkit, this->menuItemHelp
            });
            resources->ApplyResources(this->menuStrip, L"menuStrip");
            this->menuStrip->Name = L"menuStrip";
            //
            // menuItemSystem
            //
            this->menuItemSystem->DropDownItems->AddRange(gcnew cli::array< System::Windows::Forms::ToolStripItem^  >(3)
            {
                this->menuItemSystemLog,
                    this->menuItemSystemLanguage, this->menuItemExitSystem
            });
            this->menuItemSystem->Name = L"menuItemSystem";
            resources->ApplyResources(this->menuItemSystem, L"menuItemSystem");
            //
            // menuItemSystemLog
            //
            this->menuItemSystemLog->Name = L"menuItemSystemLog";
            resources->ApplyResources(this->menuItemSystemLog, L"menuItemSystemLog");
            //
            // menuItemSystemLanguage
            //
            this->menuItemSystemLanguage->DropDownItems->AddRange(gcnew cli::array< System::Windows::Forms::ToolStripItem^  >(2)
            {
                this->menuItemSimplifiedChinese,
                    this->menuItemTraditionalChinese
            });
            this->menuItemSystemLanguage->Name = L"menuItemSystemLanguage";
            resources->ApplyResources(this->menuItemSystemLanguage, L"menuItemSystemLanguage");
            //
            // menuItemSimplifiedChinese
            //
            this->menuItemSimplifiedChinese->Name = L"menuItemSimplifiedChinese";
            resources->ApplyResources(this->menuItemSimplifiedChinese, L"menuItemSimplifiedChinese");
            //
            // menuItemTraditionalChinese
            //
            this->menuItemTraditionalChinese->Name = L"menuItemTraditionalChinese";
            resources->ApplyResources(this->menuItemTraditionalChinese, L"menuItemTraditionalChinese");
            //
            // menuItemExitSystem
            //
            this->menuItemExitSystem->Name = L"menuItemExitSystem";
            resources->ApplyResources(this->menuItemExitSystem, L"menuItemExitSystem");
            //
            // menuItemBasicFunction
            //
            this->menuItemBasicFunction->DropDownItems->AddRange(gcnew cli::array< System::Windows::Forms::ToolStripItem^  >(8)
            {
                this->menuItemDeviceManagement,
                    this->menuItemResidentManagement, this->menuItemDoorAccessCtrl, this->menuItemAnnouncementManagement, this->menuItemSoftwareUpgrade,
                    this->menuItemSystemManagement, this->menuItemLogManagement, this->menuItemLeaveMessage
            });
            this->menuItemBasicFunction->Name = L"menuItemBasicFunction";
            resources->ApplyResources(this->menuItemBasicFunction, L"menuItemBasicFunction");
            //
            // menuItemDeviceManagement
            //
            this->menuItemDeviceManagement->Name = L"menuItemDeviceManagement";
            resources->ApplyResources(this->menuItemDeviceManagement, L"menuItemDeviceManagement");
            //
            // menuItemResidentManagement
            //
            this->menuItemResidentManagement->Name = L"menuItemResidentManagement";
            resources->ApplyResources(this->menuItemResidentManagement, L"menuItemResidentManagement");
            //
            // menuItemDoorAccessCtrl
            //
            this->menuItemDoorAccessCtrl->Name = L"menuItemDoorAccessCtrl";
            resources->ApplyResources(this->menuItemDoorAccessCtrl, L"menuItemDoorAccessCtrl");
            //
            // menuItemAnnouncementManagement
            //
            this->menuItemAnnouncementManagement->Name = L"menuItemAnnouncementManagement";
            resources->ApplyResources(this->menuItemAnnouncementManagement, L"menuItemAnnouncementManagement");
            //
            // menuItemSoftwareUpgrade
            //
            this->menuItemSoftwareUpgrade->Name = L"menuItemSoftwareUpgrade";
            resources->ApplyResources(this->menuItemSoftwareUpgrade, L"menuItemSoftwareUpgrade");
            //
            // menuItemSystemManagement
            //
            this->menuItemSystemManagement->Name = L"menuItemSystemManagement";
            resources->ApplyResources(this->menuItemSystemManagement, L"menuItemSystemManagement");
            //
            // menuItemLogManagement
            //
            this->menuItemLogManagement->Name = L"menuItemLogManagement";
            resources->ApplyResources(this->menuItemLogManagement, L"menuItemLogManagement");
            //
            // menuItemLeaveMessage
            //
            this->menuItemLeaveMessage->Name = L"menuItemLeaveMessage";
            resources->ApplyResources(this->menuItemLeaveMessage, L"menuItemLeaveMessage");
            //
            // menuItemTalk
            //
            this->menuItemTalk->DropDownItems->AddRange(gcnew cli::array< System::Windows::Forms::ToolStripItem^  >(1) { this->menuItemVideoTalk });
            this->menuItemTalk->Name = L"menuItemTalk";
            resources->ApplyResources(this->menuItemTalk, L"menuItemTalk");
            //
            // menuItemVideoTalk
            //
            this->menuItemVideoTalk->Name = L"menuItemVideoTalk";
            resources->ApplyResources(this->menuItemVideoTalk, L"menuItemVideoTalk");
            //
            // menuItemToolkit
            //
            this->menuItemToolkit->DropDownItems->AddRange(gcnew cli::array< System::Windows::Forms::ToolStripItem^  >(1) { this->menuItemSoftwarePackageToolkit });
            this->menuItemToolkit->Name = L"menuItemToolkit";
            resources->ApplyResources(this->menuItemToolkit, L"menuItemToolkit");
            //
            // menuItemSoftwarePackageToolkit
            //
            this->menuItemSoftwarePackageToolkit->Name = L"menuItemSoftwarePackageToolkit";
            resources->ApplyResources(this->menuItemSoftwarePackageToolkit, L"menuItemSoftwarePackageToolkit");
            //
            // menuItemHelp
            //
            this->menuItemHelp->DropDownItems->AddRange(gcnew cli::array< System::Windows::Forms::ToolStripItem^  >(2)
            {
                this->menuItemAbout,
                    this->menuItemHowTo
            });
            this->menuItemHelp->Name = L"menuItemHelp";
            resources->ApplyResources(this->menuItemHelp, L"menuItemHelp");
            //
            // menuItemAbout
            //
            this->menuItemAbout->Name = L"menuItemAbout";
            resources->ApplyResources(this->menuItemAbout, L"menuItemAbout");
            //
            // menuItemHowTo
            //
            this->menuItemHowTo->Name = L"menuItemHowTo";
            resources->ApplyResources(this->menuItemHowTo, L"menuItemHowTo");
            //
            // statusStrip
            //
            resources->ApplyResources(this->statusStrip, L"statusStrip");
            this->statusStrip->Name = L"statusStrip";
            //
            // toolStrip
            //
            resources->ApplyResources(this->toolStrip, L"toolStrip");
            this->toolStrip->BackColor = System::Drawing::Color::Black;
            this->toolStrip->GripStyle = System::Windows::Forms::ToolStripGripStyle::Hidden;
            this->toolStrip->Items->AddRange(gcnew cli::array< System::Windows::Forms::ToolStripItem^  >(11)
            {
                this->btnBasicFunctionsBlock,
                    this->btnDeviceManagement, this->btnResidentManagement, this->btnDoorAccessCtrl, this->btnAnnouncementManagement, this->btnSoftwareUpgrade,
                    this->btnSystemManagement, this->btnLogManagement, this->btnLeaveMessage, this->btnTalkFunctionBlock, this->btnVideoTalk
            });
            this->toolStrip->Name = L"toolStrip";
            //
            // btnBasicFunctionsBlock
            //
            this->btnBasicFunctionsBlock->BackColor = System::Drawing::Color::FromArgb(static_cast<System::Int32>(static_cast<System::Byte>(64)),
                static_cast<System::Int32>(static_cast<System::Byte>(64)), static_cast<System::Int32>(static_cast<System::Byte>(64)));
            this->btnBasicFunctionsBlock->DisplayStyle = System::Windows::Forms::ToolStripItemDisplayStyle::Text;
            this->btnBasicFunctionsBlock->ForeColor = System::Drawing::Color::Red;
            resources->ApplyResources(this->btnBasicFunctionsBlock, L"btnBasicFunctionsBlock");
            this->btnBasicFunctionsBlock->Name = L"btnBasicFunctionsBlock";
            //
            // btnDeviceManagement
            //
            this->btnDeviceManagement->ForeColor = System::Drawing::Color::White;
            resources->ApplyResources(this->btnDeviceManagement, L"btnDeviceManagement");
            this->btnDeviceManagement->Name = L"btnDeviceManagement";
            //
            // btnResidentManagement
            //
            this->btnResidentManagement->ForeColor = System::Drawing::Color::White;
            resources->ApplyResources(this->btnResidentManagement, L"btnResidentManagement");
            this->btnResidentManagement->Name = L"btnResidentManagement";
            //
            // btnDoorAccessCtrl
            //
            this->btnDoorAccessCtrl->ForeColor = System::Drawing::Color::White;
            resources->ApplyResources(this->btnDoorAccessCtrl, L"btnDoorAccessCtrl");
            this->btnDoorAccessCtrl->Name = L"btnDoorAccessCtrl";
            //
            // btnAnnouncementManagement
            //
            this->btnAnnouncementManagement->ForeColor = System::Drawing::Color::White;
            resources->ApplyResources(this->btnAnnouncementManagement, L"btnAnnouncementManagement");
            this->btnAnnouncementManagement->Name = L"btnAnnouncementManagement";
            //
            // btnSoftwareUpgrade
            //
            this->btnSoftwareUpgrade->ForeColor = System::Drawing::Color::White;
            resources->ApplyResources(this->btnSoftwareUpgrade, L"btnSoftwareUpgrade");
            this->btnSoftwareUpgrade->Name = L"btnSoftwareUpgrade";
            //
            // btnSystemManagement
            //
            this->btnSystemManagement->ForeColor = System::Drawing::Color::White;
            resources->ApplyResources(this->btnSystemManagement, L"btnSystemManagement");
            this->btnSystemManagement->Name = L"btnSystemManagement";
            //
            // btnLogManagement
            //
            this->btnLogManagement->ForeColor = System::Drawing::Color::White;
            resources->ApplyResources(this->btnLogManagement, L"btnLogManagement");
            this->btnLogManagement->Name = L"btnLogManagement";
            //
            // btnLeaveMessage
            //
            this->btnLeaveMessage->ForeColor = System::Drawing::Color::White;
            resources->ApplyResources(this->btnLeaveMessage, L"btnLeaveMessage");
            this->btnLeaveMessage->Name = L"btnLeaveMessage";
            //
            // btnTalkFunctionBlock
            //
            this->btnTalkFunctionBlock->BackColor = System::Drawing::Color::FromArgb(static_cast<System::Int32>(static_cast<System::Byte>(64)),
                static_cast<System::Int32>(static_cast<System::Byte>(64)), static_cast<System::Int32>(static_cast<System::Byte>(64)));
            this->btnTalkFunctionBlock->DisplayStyle = System::Windows::Forms::ToolStripItemDisplayStyle::Text;
            this->btnTalkFunctionBlock->ForeColor = System::Drawing::Color::Red;
            resources->ApplyResources(this->btnTalkFunctionBlock, L"btnTalkFunctionBlock");
            this->btnTalkFunctionBlock->Name = L"btnTalkFunctionBlock";
            //
            // btnVideoTalk
            //
            this->btnVideoTalk->ForeColor = System::Drawing::Color::White;
            resources->ApplyResources(this->btnVideoTalk, L"btnVideoTalk");
            this->btnVideoTalk->Name = L"btnVideoTalk";
            //
            // FormMain
            //
            resources->ApplyResources(this, L"$this");
            this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
            this->Controls->Add(this->toolStrip);
            this->Controls->Add(this->statusStrip);
            this->Controls->Add(this->menuStrip);
            this->IsMdiContainer = true;
            this->MainMenuStrip = this->menuStrip;
            this->Name = L"FormMain";
            this->menuStrip->ResumeLayout(false);
            this->menuStrip->PerformLayout();
            this->toolStrip->ResumeLayout(false);
            this->toolStrip->PerformLayout();
            this->ResumeLayout(false);
            this->PerformLayout();

        }
#pragma endregion
};
}

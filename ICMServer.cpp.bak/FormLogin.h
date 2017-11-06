#pragma once

namespace ICMServer {

    using namespace System;
    using namespace System::ComponentModel;
    using namespace System::Collections;
    using namespace System::Windows::Forms;
    using namespace System::Data;
    using namespace System::Drawing;
    using namespace ICMServer::Models;

    /// <summary>
    /// FormLogin 的摘要
    /// </summary>
    public ref class FormLogin : public System::Windows::Forms::Form
    {
    public:
        FormLogin(void)
        {
            InitializeComponent();
            //
            //TODO:  在此加入建構函式程式碼
            //
            this->btnLogin->BackgroundImage = ICMServer::FormLoginResource::btnLoginBackgroundImage;
            this->btnClose->BackgroundImage = ICMServer::FormLoginResource::btnCloseBackgroundImage;
        }

    protected:
        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        ~FormLogin()
        {
            if (components)
            {
                delete components;
            }
        }

    private:
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        System::ComponentModel::Container ^components;

        /// <summary>
        /// 用於視窗拖曳移動
        /// </summary>
        int m_PX;
        int m_PY;
        bool m_IsDragging;
    private: System::Windows::Forms::Button^  btnClose;
    private: System::Windows::Forms::ComboBox^  comboBoxUserName;
    private: System::Windows::Forms::TextBox^  textBoxUserPassword;
    private: System::Windows::Forms::Button^  btnLogin;

#pragma region Windows Form Designer generated code
             /// <summary>
             /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
             /// 修改這個方法的內容。
             /// </summary>
             void InitializeComponent(void)
             {
                 System::ComponentModel::ComponentResourceManager^  resources = (gcnew System::ComponentModel::ComponentResourceManager(FormLogin::typeid));
                 this->btnLogin = (gcnew System::Windows::Forms::Button());
                 this->btnClose = (gcnew System::Windows::Forms::Button());
                 this->comboBoxUserName = (gcnew System::Windows::Forms::ComboBox());
                 this->textBoxUserPassword = (gcnew System::Windows::Forms::TextBox());
                 this->SuspendLayout();
                 // 
                 // btnLogin
                 // 
                 resources->ApplyResources(this->btnLogin, L"btnLogin");
                 this->btnLogin->ForeColor = System::Drawing::Color::Teal;
                 this->btnLogin->Name = L"btnLogin";
                 this->btnLogin->UseVisualStyleBackColor = true;
                 this->btnLogin->Click += gcnew System::EventHandler(this, &FormLogin::btnLogin_Click);
                 this->btnLogin->MouseDown += gcnew System::Windows::Forms::MouseEventHandler(this, &FormLogin::btnLogin_MouseDown);
                 this->btnLogin->MouseUp += gcnew System::Windows::Forms::MouseEventHandler(this, &FormLogin::btnLogin_MouseUp);
                 // 
                 // btnClose
                 // 
                 resources->ApplyResources(this->btnClose, L"btnClose");
                 this->btnClose->ForeColor = System::Drawing::Color::Teal;
                 this->btnClose->Name = L"btnClose";
                 this->btnClose->UseVisualStyleBackColor = true;
                 this->btnClose->Click += gcnew System::EventHandler(this, &FormLogin::btnClose_Click);
                 this->btnClose->MouseDown += gcnew System::Windows::Forms::MouseEventHandler(this, &FormLogin::btnClose_MouseDown);
                 this->btnClose->MouseUp += gcnew System::Windows::Forms::MouseEventHandler(this, &FormLogin::btnClose_MouseUp);
                 // 
                 // comboBoxUserName
                 // 
                 this->comboBoxUserName->BackColor = System::Drawing::Color::White;
                 this->comboBoxUserName->FormattingEnabled = true;
                 resources->ApplyResources(this->comboBoxUserName, L"comboBoxUserName");
                 this->comboBoxUserName->Name = L"comboBoxUserName";
                 // 
                 // textBoxUserPassword
                 // 
                 this->textBoxUserPassword->BackColor = System::Drawing::Color::White;
                 resources->ApplyResources(this->textBoxUserPassword, L"textBoxUserPassword");
                 this->textBoxUserPassword->Name = L"textBoxUserPassword";
                 // 
                 // FormLogin
                 // 
                 resources->ApplyResources(this, L"$this");
                 this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
                 this->AutoValidate = System::Windows::Forms::AutoValidate::EnableAllowFocusChange;
                 this->ControlBox = false;
                 this->Controls->Add(this->textBoxUserPassword);
                 this->Controls->Add(this->comboBoxUserName);
                 this->Controls->Add(this->btnClose);
                 this->Controls->Add(this->btnLogin);
                 this->FormBorderStyle = System::Windows::Forms::FormBorderStyle::None;
                 this->Name = L"FormLogin";
                 this->SizeGripStyle = System::Windows::Forms::SizeGripStyle::Show;
                 this->Load += gcnew System::EventHandler(this, &FormLogin::FormLogin_Load);
                 this->MouseDown += gcnew System::Windows::Forms::MouseEventHandler(this, &FormLogin::FormLogin_MouseDown);
                 this->MouseMove += gcnew System::Windows::Forms::MouseEventHandler(this, &FormLogin::FormLogin_MouseMove);
                 this->MouseUp += gcnew System::Windows::Forms::MouseEventHandler(this, &FormLogin::FormLogin_MouseUp);
                 this->ResumeLayout(false);
                 this->PerformLayout();

             }
#pragma endregion
    private: System::Void FormLogin_MouseDown(System::Object^  sender, System::Windows::Forms::MouseEventArgs^  e)
    {
        if (e->Button == System::Windows::Forms::MouseButtons::Left)
        {
            m_PX = e->X;
            m_PY = e->Y;
            m_IsDragging = true;
        }
    }

    private: System::Void FormLogin_MouseMove(System::Object^  sender, System::Windows::Forms::MouseEventArgs^  e)
    {
        if (m_IsDragging)
        {
            this->Location = Point(this->Left + e->X - m_PX, this->Top + e->Y - m_PY);
        }
    }

    private: System::Void FormLogin_MouseUp(System::Object^  sender, System::Windows::Forms::MouseEventArgs^  e)
    {
        m_IsDragging = false;
    }

    private: System::Void btnLogin_Click(System::Object^  sender, System::EventArgs^  e)
    {
        String^ username = comboBoxUserName->Text;
        String^ password = textBoxUserPassword->Text;
        auto user = ICMDBContext::GetUserByName(username);
        if (user != nullptr)
        {
            if (ICMServer::Security::MD5Encode(password) == user->password)
            {
                this->DialogResult = System::Windows::Forms::DialogResult::OK;  // open mdiparent main form
                this->Close();
            }
            else
                MessageBox::Show("密碼錯誤");
        }
        else
            MessageBox::Show(String::Format("用戶名 {0} 不存在", username));
    }

    private: System::Void btnLogin_MouseDown(System::Object^  sender, System::Windows::Forms::MouseEventArgs^  e)
    {
        this->btnLogin->BackgroundImage = ICMServer::FormLoginResource::btnLoginClickedBackgroundImage;
    }

    private: System::Void btnLogin_MouseUp(System::Object^  sender, System::Windows::Forms::MouseEventArgs^  e)
    {
        this->btnLogin->BackgroundImage = ICMServer::FormLoginResource::btnLoginBackgroundImage;
    }

    private: System::Void btnClose_Click(System::Object^  sender, System::EventArgs^  e)
    {
        this->Close();
    }

    private: System::Void btnClose_MouseDown(System::Object^  sender, System::Windows::Forms::MouseEventArgs^  e)
    {
        this->btnClose->BackgroundImage = ICMServer::FormLoginResource::btnCloseClickedBackgroundImage;
    }

    private: System::Void btnClose_MouseUp(System::Object^  sender, System::Windows::Forms::MouseEventArgs^  e)
    {
        this->btnClose->BackgroundImage = ICMServer::FormLoginResource::btnCloseBackgroundImage;
    }

    private: System::Void FormLogin_Load(System::Object^  sender, System::EventArgs^  e)
    {
        comboBoxUserName->Items->Add(L"admin");
        comboBoxUserName->SelectedIndex = 0;
    }
    };
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ICMServer
{
    public partial class FormMain : Form
    {
        class Button
        {
            internal ToolStripButton button = null;
            internal ToolStripButton parent = null;
            internal ToolStripMenuItem menuItem = null;
            internal Bitmap image = null;
            internal Bitmap focusedImage = null;
            internal string formName = null;
            internal Form m_Form = null;

            internal void ResetToDefaultImage()
            {
                if (button != null)
                    button.Image = image;
            }

            internal void SetToFocusedImage()
            {
                if (button != null)
                    button.Image = focusedImage;
            }

            /// <summary>
            /// 判断要开启的視窗是否已經开启
            /// 若視窗已开启，将该視窗带到最上层
            /// </summary>
            /// <param name="childFormName"></param>
            /// <returns>form if form is opened, else return null</returns>
            private Form BringFormToFront(Form parentForm, string childFormName)
            {
                if (m_Form != null)
                {
                    // 将視窗带到到最上层
                    m_Form.BringToFront();
                }
                return m_Form;
            }

            internal void ShowForm(Form mdiParent)
            {
                Form form = BringFormToFront(mdiParent, formName);
                if (form == null)
                {
                    if (formName == null)
                        return;

                    //m_Form = System.Reflection.Assembly.GetExecutingAssembly().CreateInstance("ICMServer.Wpf" + formName) as Form;
                    //if (m_Form == null)
                    m_Form = System.Reflection.Assembly.GetExecutingAssembly().CreateInstance("ICMServer." + formName) as Form;
                    if (m_Form != null)
                    {
                        Form f = m_Form as Form;
                        f.MdiParent = mdiParent;
                        f.Dock = DockStyle.Fill;
                        f.Show();
                    }
                }
            }
        }

        List<Button> m_buttons;
        Button m_currButton = null;

        public FormMain()
        {
            UILanguageSet();
            InitializeComponent();
            m_buttons = new List<Button>
            {
                new Button
                {
                    button = this.BtnDeviceManagement,
                    parent = this.BtnBasicFunctionsBlock,
                    menuItem = this.menuItemDeviceManagement,
                    image = global::ICMServer.FormMainResource.BtnDeviceManagement,
                    focusedImage = global::ICMServer.FormMainResource.BtnDeviceManagementFocused,
                    formName = "FormDeviceManagement"
                },
                new Button
                {
                    button = this.BtnResidentManagement,
                    parent = this.BtnBasicFunctionsBlock,
                    menuItem = this.menuItemResidentManagement,
                    image = global::ICMServer.FormMainResource.BtnResidentManagement,
                    focusedImage = global::ICMServer.FormMainResource.BtnResidentManagementFocused,
                    formName = "FormResidentManagement"
                },
                new Button
                {
                    button = this.BtnDoorAccessCtrl,
                    parent = this.BtnBasicFunctionsBlock,
                    menuItem = this.menuItemDoorAccessCtrl,
                    image = global::ICMServer.FormMainResource.BtnDoorAccessCtrl,
                    focusedImage = global::ICMServer.FormMainResource.BtnDoorAccessCtrlFocused,
                    formName = "FormDoorAccessCtrl"
                },
                new Button
                {
                    button = this.BtnAnnouncementManagement,
                    parent = this.BtnBasicFunctionsBlock,
                    menuItem = this.menuItemAnnouncementManagement,
                    image = global::ICMServer.FormMainResource.BtnAnnouncementManagement,
                    focusedImage = global::ICMServer.FormMainResource.BtnAnnouncementManagementFocused,
                    formName = "FormAnnouncementManagement"
                },
                new Button
                {
                    button = this.BtnSoftwareUpgrade,
                    parent = this.BtnBasicFunctionsBlock,
                    menuItem = this.menuItemSoftwareUpgrade,
                    image = global::ICMServer.FormMainResource.BtnSoftwareUpgrade,
                    focusedImage = global::ICMServer.FormMainResource.BtnSoftwareUpgradeFocused,
                    formName = "FormSoftwareUpgrade"
                },
                new Button
                {
                    button = this.BtnSystemManagement,
                    parent = this.BtnBasicFunctionsBlock,
                    menuItem = this.menuItemSystemManagement,
                    image = global::ICMServer.FormMainResource.BtnSystemManagement,
                    focusedImage = global::ICMServer.FormMainResource.BtnSystemManagementFocused,
                    formName = "FormSystemManagement"
                },
                new Button
                {
                    button = this.BtnLogManagement,
                    parent = this.BtnBasicFunctionsBlock,
                    menuItem = this.menuItemLogManagement,
                    image = global::ICMServer.FormMainResource.BtnLogManagement,
                    focusedImage = global::ICMServer.FormMainResource.BtnLogManagementFocused,
                    formName = "FormLogManagement"
                },
                new Button
                {
                    button = this.BtnLeaveMessage,
                    parent = this.BtnBasicFunctionsBlock,
                    menuItem = this.menuItemLeaveMessage,
                    image = global::ICMServer.FormMainResource.BtnLeaveMessage,
                    focusedImage = global::ICMServer.FormMainResource.BtnLeaveMessageFocused,
                    formName = "FormLeaveMessage"
                },
                new Button
                {
                    button = this.BtnVideoTalk,
                    //parent = this.BtnTalkFunctionsBlock,
                    parent = this.BtnBasicFunctionsBlock,
                    menuItem = this.menuItemVideoTalk,
                    image = global::ICMServer.FormMainResource.BtnVideoTalk,
                    focusedImage = global::ICMServer.FormMainResource.BtnVideoTalkFocused,
                    formName = "FormVideoTalk"
                }
            };
            InitVideoIntercom();
            //Button_Click(this.BtnLogManagement, null);
            Button_Click(this.BtnVideoTalk, null);
            //Button_Click(this.BtnDeviceManagement, null);
        }


        pCallBackPro DllCallBack = new pCallBackPro(FormVideoTalk.DllCallBackFunc);

        private void UILanguageSet()
        {
            if(Config.Instance.AppLanaguage == "2")
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN");
        }

        /// <summary>
        /// 改變一组 buttons 的 Visible 属性
        /// </summary>
        /// <param name="buttons"></param>
        /// <param name="visible"></param>
        private void SetButtonsVisible(ToolStripButton parent)
        {
            foreach (var button in m_buttons)
            {
                button.button.Visible = (button.parent == parent);
            }
        }

        private void FunctionsBlockButton_Click(object sender, EventArgs e)
        {
            ToolStripButton button = sender as ToolStripButton;
            SetButtonsVisible(button);
        }

        private Button ToolStripButtonToButton(ToolStripButton button)
        {
            foreach (var b in m_buttons)
            {
                if (b.button == button)
                    return b;
            }

            return null;
        }

        private Button ToolStripMenuItemToButton(ToolStripMenuItem menuItem)
        {
            foreach (var b in m_buttons)
            {
                if (b.menuItem == menuItem)
                    return b;
            }

            return null;
        }

        private void DoButtonClick(Button button)
        {
            if (button == null)
                return;

            if (m_currButton != null)
            {
                m_currButton.ResetToDefaultImage();
                if (m_currButton.parent != button.parent)
                {
                    SetButtonsVisible(button.parent);
                }
            }
            button.SetToFocusedImage();
            button.ShowForm(this);
            m_currButton = button;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            ToolStripButton button = sender as ToolStripButton;
            if (button == null)
                return;

            //if (m_currButton == null || m_currButton.button != button)
            if(true)
            {
                Button newButton = ToolStripButtonToButton(button);
                DoButtonClick(newButton);
            }
        }


        private void MenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            if (menuItem == null)
                return;

            if (m_currButton == null || m_currButton.menuItem != menuItem)
            {
                Button newButton = ToolStripMenuItemToButton(menuItem);
                DoButtonClick(newButton);
            }
        }

        private void MenuItemSimplifiedChinese_Click(object sender, EventArgs e)
        {
            Config.Instance.AppLanaguage = "2";
            MessageBox.Show("将在重启后切换语系");
        }

        private void MenuItemTraditionalChinese_Click(object sender, EventArgs e)
        {
            Config.Instance.AppLanaguage = "3";
            MessageBox.Show("将在重啟后切換语言");
        }

        /// <summary>
        /// 如果该目錄不存在的话，試著建立目錄
        /// </summary>
        /// <param name="folderPath">目錄路径</param>
        private static void CreateFolderIfNotExist(string folderPath)
        {
            bool exists = System.IO.Directory.Exists(folderPath);
            if (!exists)
                System.IO.Directory.CreateDirectory(folderPath);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            try
            {
                //this.Text += Config.Instance.AppVersion;
            }
            catch { }

            CreateFolderIfNotExist(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + @"\data");
            CreateFolderIfNotExist(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + @"\data\leaveword");
            CreateFolderIfNotExist(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + @"\data\CardXML");
            CreateFolderIfNotExist(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + @"\data\snapshot");
            CreateFolderIfNotExist(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + @"\data\weather");
            CreateFolderIfNotExist(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + @"\data\message");

            RtspService rtsp = new RtspService();
            rtsp.Run();

            //WeatherService weather = new WeatherService();
            //weather.Run();

            //***create thread***
            //InitVideoIntercom();
        }

        // 此 function 应该放在 video talk form
        private void InitVideoIntercom()
        {
            SetLocalIPAddress();
            Init();
            InitCamera();
            int nDevice = NativeMethods.GetCaptureDeviceCount();
            if (nDevice > 0)
                NativeMethods.Dll_SetCameraDeviceId(0);
        }

        private void InitCamera()
        {
            try
            {
                NativeMethods.Dll_initCamera();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Init()
        {
            try
            {
                NativeMethods.Dll_init(DllCallBack); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetLocalIPAddress()
        {
            StringBuilder ip = new StringBuilder(Config.Instance.LocalIP);
            StringBuilder AddrName = new StringBuilder("000000000000");
            try
            {
                NativeMethods.Dll_SetLocalIpAddress(ip, AddrName, (uint)Config.Instance.SIPCommunicationPort);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MenuItemAbout_Click(object sender, EventArgs e)
        {
            DialogAbout about = new DialogAbout();
            about.Show();
        }

        private void MenuItemExitSystem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            if (menuItem == null)
                return;

            if (m_currButton == null || m_currButton.menuItem != menuItem)
            {
                Button newButton = ToolStripMenuItemToButton(menuItem);
                DoButtonClick(newButton);
            }
        }
    }
}

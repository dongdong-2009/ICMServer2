using GalaSoft.MvvmLight.Messaging;
using ICMServer.Services;
using ICMServer.Services.Net;
using ICMServer.WPF.Messages;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ICMServer.WPF.Views
{
    /// <summary>
    /// FormVideoTalk.xaml 的互動邏輯
    /// </summary>
    public partial class FormVideoTalk : UserControl
    {
        System.Windows.Forms.Form _parentForm;

        public FormVideoTalk(System.Windows.Forms.Form parentForm)
        {
            InitializeComponent();
            VideoWindow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            VideoWindow.Image = System.Drawing.Image.FromFile(@"Images\video_window_default_background.bmp");
            VideoTalkService.Instance.SetVideoWindow(VideoWindow);
            this._parentForm = parentForm;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Register<ReceivedIncomingCallEvent>(this, (msg) =>
            {
                VideoWindow.Image = System.Drawing.Image.FromFile(@"Images\video_window_recevied_incoming_call.bmp");
                if (this._parentForm != null)
                    this._parentForm.BringToFront();
            });
            Messenger.Default.Register<AcceptedIncomingCallEvent>(this, (msg) =>
            {
                VideoWindow.Image = System.Drawing.Image.FromFile(@"Images\video_window_on_phone_call.bmp");
            });
            Messenger.Default.Register<ReceivedHangUpEvent>(this, async (msg) =>
            {
                VideoWindow.Image = System.Drawing.Image.FromFile(@"Images\video_window_hang_up.bmp");
                await Task.Delay(1000);
                VideoWindow.Image = System.Drawing.Image.FromFile(@"Images\video_window_default_background.bmp");
            });
            Messenger.Default.Register<ReceivedOutgoingCallTimeoutEvent>(this, (msg) =>
            {
                MessageBox.Show(CulturesHelper.GetTextValue("OutgoingCallTimeout"));
            });
            Messenger.Default.Register<CallingEvent>(this, (msg) =>
            {
                VideoWindow.Image = System.Drawing.Image.FromFile(@"Images\video_window_calling.bmp");
            });
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Unregister(this);
        }
    }
}

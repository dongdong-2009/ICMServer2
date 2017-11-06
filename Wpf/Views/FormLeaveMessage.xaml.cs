using System.Windows;
using System.Windows.Controls;

namespace ICMServer.WPF.Views
{
    /// <summary>
    /// FormLeaveMessage.xaml 的互動邏輯
    /// </summary>
    public partial class FormLeaveMessage : UserControl
    {
        public FormLeaveMessage()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //ICMServer.FormVideoTalk.Instance.ReceivedReadLeaveMsgs +=
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }
    }
}

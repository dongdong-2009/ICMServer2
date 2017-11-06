using ICMServer.Models;
using System.Runtime.InteropServices;
using System.Windows;

namespace ICMServer.WPF.Views
{
    /// <summary>
    /// DialogPlayVideoMessage.xaml 的互動邏輯
    /// </summary>
    public partial class DialogPlayVideoMessage : Window
    {
        leaveword _videoMsg;

        public DialogPlayVideoMessage(leaveword videoMsg)
        {
            InitializeComponent();
            this._videoMsg = videoMsg;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (this._videoMsg != null)
            {
                HandleRef handleref = new HandleRef(Player, Player.Handle);
                NativeMethods.Dll_Player(this._videoMsg.filenames, handleref.Handle);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            NativeMethods.Dll_ClosePlayer();
        }
    }
}

using System.Windows;
using System.Windows.Input;

namespace ICMServer.WPF.Views
{
    /// <summary>
    /// MainWindow.xaml 的互动邏輯
    /// </summary>
    public partial class DialogLogin : Window
    {
        public DialogLogin()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

using System.Windows;

namespace ICMServer.WPF.Views
{
    /// <summary>
    /// DialogAddUser.xaml 的互動邏輯
    /// </summary>
    public partial class DialogAddUser : Window
    {
        public DialogAddUser()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //((UserViewModel)this.DataContext).ValidateCommand.Execute(null);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

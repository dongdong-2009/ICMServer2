using System.Windows;

namespace ICMServer.WPF.Views
{
    /// <summary>
    /// DialogImportAddressBook.xaml 的互動邏輯
    /// </summary>
    public partial class DialogImportAddressBook : Window
    {
        public DialogImportAddressBook()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

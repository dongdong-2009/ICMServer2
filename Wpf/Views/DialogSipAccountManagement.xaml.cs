using System.Windows;

namespace ICMServer.WPF.Views
{
    /// <summary>
    /// DialogSipAccountManagement.xaml 的互動邏輯
    /// </summary>
    public partial class DialogSipAccountManagement : Window
    {
        public DialogSipAccountManagement()
        {
            InitializeComponent();
            //this.DataContext = new DialogSipAccountManagementViewModel(
            //    ServiceLocator.Current.GetInstance<ICollectionModel<sipaccount>>(this.GetType().Name),
            //    ServiceLocator.Current.GetInstance<IDialogService>());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

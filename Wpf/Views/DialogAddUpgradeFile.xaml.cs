using FluentValidation;
using ICMServer.Models;
using ICMServer.Services;
using ICMServer.WPF.Models;
using ICMServer.WPF.ViewModels;
using Microsoft.Practices.ServiceLocation;
using System.Windows;

namespace ICMServer.WPF.Views
{
    /// <summary>
    /// DialogAddUpgradeFile.xaml 的互動邏輯
    /// </summary>
    public partial class DialogAddUpgradeFile : Window
    {
        public DialogAddUpgradeFile()
        {
            InitializeComponent();
            this.DataContext = new UpgradeFileViewModel(
               ServiceLocator.Current.GetInstance<IValidator<UpgradeFileViewModel>>(),
               ServiceLocator.Current.GetInstance<ICollectionModel<upgrade>>(),
               ServiceLocator.Current.GetInstance<IDialogService>());
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((UpgradeFileViewModel)this.DataContext).ValidateCommand.Execute(null);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
    }
}

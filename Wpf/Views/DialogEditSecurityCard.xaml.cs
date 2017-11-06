using FluentValidation;
using ICMServer.Models;
using ICMServer.Services;
using ICMServer.Services.Data;
using ICMServer.WPF.Models;
using ICMServer.WPF.ViewModels;
using Microsoft.Practices.ServiceLocation;
using System.Windows;

namespace ICMServer.WPF.Views
{
    /// <summary>
    /// DialogEditSecurityCard.xaml 的互動邏輯
    /// </summary>
    public partial class DialogEditSecurityCard : Window
    {
        public DialogEditSecurityCard(iccard card)
        {
            InitializeComponent();
            this.DataContext = new SecurityCardViewModel(
                ServiceLocator.Current.GetInstance<IValidator<SecurityCardViewModel>>(),
                ServiceLocator.Current.GetInstance<ICollectionModel<iccard>>(),
                ServiceLocator.Current.GetInstance<IDataService<icmap>>(),
                ServiceLocator.Current.GetInstance<IDeviceDataService>(),
                ServiceLocator.Current.GetInstance<IDialogService>(),
                card);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((SecurityCardViewModel)this.DataContext).ValidateCommand.Execute(null);
        }
    }
}

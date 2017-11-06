using FluentValidation;
using ICMServer.Services;
using ICMServer.WPF.Models;
using ICMServer.WPF.ViewModels;
using Microsoft.Practices.ServiceLocation;
using System.Windows;

namespace ICMServer.WPF.Views
{
    /// <summary>
    /// DialogAddAdvertisement.xaml 的互動邏輯
    /// </summary>
    public partial class DialogAddAdvertisement : Window
    {
        public DialogAddAdvertisement()
        {
            InitializeComponent();
            this.DataContext = new AdvertisementViewModel(
                ServiceLocator.Current.GetInstance<IValidator<AdvertisementViewModel>>(),
                ServiceLocator.Current.GetInstance<IAdvertisementsModel>(),
                ServiceLocator.Current.GetInstance<IDialogService>());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((AdvertisementViewModel)this.DataContext).ValidateCommand.Execute(null);
        }
    }
}

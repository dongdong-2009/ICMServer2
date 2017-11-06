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
    /// DialogAddSipAccount.xaml 的互動邏輯
    /// </summary>
    public partial class DialogAddSipAccount : Window
    {
        public DialogAddSipAccount(string roomAddress)
        {
            InitializeComponent();
            var viewModel = new SipAccountViewModel(
               ServiceLocator.Current.GetInstance<IValidator<SipAccountViewModel>>(),
               ServiceLocator.Current.GetInstance<ICollectionModel<sipaccount>>(),
               ServiceLocator.Current.GetInstance<IDialogService>());
            viewModel.RoomAddress = roomAddress;
            this.DataContext = viewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

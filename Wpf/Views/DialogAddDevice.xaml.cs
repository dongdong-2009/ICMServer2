using FluentValidation;
using ICMServer.Models;
using ICMServer.WPF.Models;
using ICMServer.WPF.ViewModels;
using Microsoft.Practices.ServiceLocation;
using System.Windows;

namespace ICMServer.WPF.Views
{
    /// <summary>
    /// DialogAddDevice.xaml 的互動邏輯
    /// </summary>
    public partial class DialogAddDevice : Window
    {
        public DialogAddDevice()
        {
            InitializeComponent();
            //var validator = ServiceLocator.Current.GetInstance<IValidator<DeviceViewModel>>();
            //var validatorAdapter = ServiceLocator.Current.GetInstance
            this.DataContext = new DeviceViewModel(
                ServiceLocator.Current.GetInstance<IValidator<DeviceViewModel>>(),
                ServiceLocator.Current.GetInstance<ICollectionModel<Device>>(),
                ServiceLocator.Current.GetInstance<IRoomsModel>());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((DeviceViewModel)this.DataContext).ValidateCommand.Execute(null);
        }
    }
}

using ICMServer.WPF.ViewModels;
using System;
using System.Windows;

namespace ICMServer.WPF.Views
{
    /// <summary>
    /// DialogInputIP.xaml 的互動邏輯
    /// </summary>
    public partial class DialogInputIPAddress : Window
    {
        private DialogInputIPAddressViewModel _viewModel;
        private string _IPAddress = "";

        public DialogInputIPAddress()
        {
            InitializeComponent();
            this._viewModel = ((DialogInputIPAddressViewModel)this.DataContext);
            this._viewModel.OkClicked += _viewModel_OkClicked;
        }

        private void _viewModel_OkClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this._viewModel.IPAddress))
            {
                this._IPAddress = this._viewModel.IPAddress;
            }
        }

        public string IPAddress
        {
            get { return this._IPAddress; }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this._viewModel.ValidateCommand.Execute(null);
        }
    }
}

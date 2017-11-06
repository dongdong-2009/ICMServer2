using ICMServer.Models;
using ICMServer.WPF.Models;
using ICMServer.WPF.ViewModels;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Windows;

namespace ICMServer.WPF.Views
{
    /// <summary>
    /// DialogSelectOnlineDeviceWithCamera.xaml 的互動邏輯
    /// </summary>
    public partial class DialogSelectOnlineDeviceWithCamera : Window
    {
        private Device _device;
        private DialogSelectOnlineDeviceWithCameraViewModel _viewModel;

        public DialogSelectOnlineDeviceWithCamera()
        {
            InitializeComponent();
            this._viewModel = new DialogSelectOnlineDeviceWithCameraViewModel(
                ServiceLocator.Current.GetInstance<ICollectionModel<Device>>());
            this._viewModel.OkClicked += _viewModel_OkClicked;
            this.DataContext = this._viewModel;
        }

        private void _viewModel_OkClicked(object sender, EventArgs e)
        {
            if (this._viewModel.Devices.CurrentItem != null)
            {
                this._device = this._viewModel.Devices.CurrentItem as Device;
            }
        }

        public Device Device
        {
            get 
            {
                return this._device;
            }
        }
    }
}

using ICMServer.Models;
using ICMServer.WPF.Models;
using ICMServer.WPF.ViewModels;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Windows;

namespace ICMServer.WPF.Views
{
    /// <summary>
    /// DialogSelectOnlineDevice.xaml 的互動邏輯
    /// </summary>
    public partial class DialogSelectOnlineDevice : Window
    {
        private Device _device;
        private DialogSelectOnlineDeviceViewModel _viewModel;

        public DialogSelectOnlineDevice(DeviceType deviceType)
        {
            InitializeComponent();
            this._viewModel = new DialogSelectOnlineDeviceViewModel(
                //ServiceLocator.Current.GetInstance<IDeviceDataService>(),
                ServiceLocator.Current.GetInstance<ICollectionModel<Device>>(),
                deviceType);
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

        public string DeviceAddress
        {
            get 
            {
                return (this._device != null) ? _device.roomid : "";
            }
        }
    }
}

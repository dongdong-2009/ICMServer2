using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using ICMServer.Models;
using ICMServer.WPF.Models;
using System;
using System.Collections;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace ICMServer.WPF.ViewModels
{
    public class DialogSelectOnlineDeviceViewModel : ViewModelBase
    {
        private readonly ICollectionModel<Device> _dataModel;
        private readonly DeviceType _deviceType;

#if DEBUG
        public DialogSelectOnlineDeviceViewModel()
        {

        }
#endif

        public DialogSelectOnlineDeviceViewModel(
            ICollectionModel<Device> dataModel,
            DeviceType deviceType)
        {
            this._dataModel = dataModel;
            this._deviceType = deviceType;

            Devices = (ListCollectionView)new ListCollectionView((IList)_dataModel.Data);
            using (Devices.DeferRefresh())
            {
                Devices.Filter = delegate (object obj)
                {
                    Device device = obj as Device;
                    if (device != null
                     && device.online == 1
                     && device.type.HasValue && device.type == (int)_deviceType)
                    {
                        return true;
                    }
                    return false;
                };
                Devices.SortDescriptions.Add(new SortDescription("roomid", ListSortDirection.Ascending));
            }
        }

        #region Devices
        private ListCollectionView _Devices;
        public ListCollectionView Devices
        {
            get { return _Devices; }
            private set { this.Set(ref _Devices, value); }
        }
        #endregion
        
        public event EventHandler OkClicked = delegate { };
        public event EventHandler CancelClicked = delegate { };

        #region OkCommand
        private ICommand _OkCommand;
        public ICommand OkCommand
        {
            get
            {
                return _OkCommand ?? (_OkCommand = new RelayCommand<object>(
                    (window) => 
                    {
                        this.OkClicked(this, EventArgs.Empty);
                        this.CloseDialog(window as Window, true);
                    },
                    (window) =>
                    {
                        return (this.Devices.CurrentItem as Device) != null;
                    }));
            }
        }
        #endregion

        #region CancelCommand
        private ICommand _CancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                return _CancelCommand ?? (_CancelCommand = new RelayCommand<object>(
                    (window) =>
                    {
                        this.CancelClicked(this, EventArgs.Empty);
                        this.CloseDialog(window as Window, false);
                    }));
            }
        }
        #endregion

        public void CloseDialog(Window view, bool dialogResult)
        {
            if (view != null)
                view.DialogResult = dialogResult;
        }
    }
}

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
    public class DialogSelectOnlineDeviceWithCameraViewModel : ViewModelBase
    {
        private readonly ICollectionModel<Device> _dataModel;

        //protected readonly IDeviceDataService _dataService;
        //private ObservableRangeCollection<Device> _DevicesData;
        //private object _lockForDevicesData = new object();

        public DialogSelectOnlineDeviceWithCameraViewModel(
            ICollectionModel<Device> dataModel)
        {
            this._dataModel = dataModel;

            //_DevicesData = new ObservableRangeCollection<Device>();
            //BindingOperations.EnableCollectionSynchronization(_DevicesData, _lockForDevicesData);
            Devices = (ListCollectionView)new ListCollectionView((IList)_dataModel.Data);
            using (Devices.DeferRefresh())
            {
                Devices.Filter = delegate (object obj)
                {
                    Device device = obj as Device;
                    if (device != null
                     && device.online == 1
                     && device.type.HasValue && (int)DeviceType.Door_Camera <= device.type && device.type <= (int)DeviceType.Lobby_Phone_Area)
                    {
                        return true;
                    }
                    return false;
                };
                Devices.SortDescriptions.Add(new SortDescription("roomid", ListSortDirection.Ascending));
            }
            //RefreshCommand.Execute(null);
        }

        #region Devices
        private ListCollectionView _Devices;
        public ListCollectionView Devices
        {
            get { return _Devices; }
            private set { this.Set(ref _Devices, value); }
        }
        #endregion

        //#region RefreshCommand
        //private ICommand _refreshCommand;
        //public ICommand RefreshCommand
        //{
        //    get
        //    {
        //        return _refreshCommand ?? (_refreshCommand = AsyncCommand.Create(
        //            () => {
        //                return LoadData();
        //            }));
        //    }
        //}
        //#endregion

        //protected Task LoadData()
        //{
        //    return Task.Run(() =>
        //    {
        //        var devices = _dataService.Select(device => 
        //            (int)DeviceType.Door_Camera <=  device.type && device.type <= (int)DeviceType.Lobby_Phone_Area
        //            && device.online == 1)
        //            .OrderBy(device => device.roomid)
        //            .ToList();
        //        lock (this._DevicesData)
        //        {
        //            _DevicesData.ReplaceRange(devices);
        //        }
        //    });
        //}

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
                    (window) => { return (this.Devices.CurrentItem as Device) != null; }));
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

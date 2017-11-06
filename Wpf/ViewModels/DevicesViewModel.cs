using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Threading;
using ICMServer.Models;
using ICMServer.Services;
using ICMServer.WPF.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace ICMServer.WPF.ViewModels
{
    public class DevicesViewModel : ViewModelBase
    {
        // TODO: 同步地址簿
        private readonly ICollectionModel<Device> _dataModel;
        private readonly IRoomsModel _roomDataModel;
        private readonly IDialogService _dialogService;

        // 請使用 ObservableRangeCollection 來取代 ObservableCollection，
        // 主要是在實作refresh data 這功能時，使用此類別的ReplaceRange method可以避免DataGrid 跳動
        //private ObservableRangeCollection<Device> _devicesCollection;
        //private static object _lock = new object();

        public DevicesViewModel(
            ICollectionModel<Device> dataModel,
            IRoomsModel roomDataModel,
            IDialogService dialogService)
        {
            this._dialogService = dialogService;
            this._roomDataModel = roomDataModel;
            this._dataModel = dataModel;
            this._dataModel.Data.CollectionChanged += _dataCollection_CollectionChanged;
            Devices = (ListCollectionView)CollectionViewSource.GetDefaultView((IList)_dataModel.Data);

            RefreshCommand.Execute(null);
        }

        private void _dataCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                case NotifyCollectionChangedAction.Remove:
                case NotifyCollectionChangedAction.Reset:
                    // 必須透過DispatcherHelper.CheckBeginInvokeOnUI
                    // 將 RaiseCanExecuteChanged() 丟到 UI thread 執行
                    // 否則會出現 InvalidOperationException
                    DispatcherHelper.CheckBeginInvokeOnUI(() =>
                    {
                        ExportAddressBookCommnad.RaiseCanExecuteChanged();
                        //SyncAddressBookWithDevicesCommand.RaiseCanExecuteChanged();
                    });
                    break;
            }
        }

        #region Devices
        private ListCollectionView _devices;
        public ListCollectionView Devices
        {
            get { return _devices; }
            private set { this.Set(ref _devices, value); }
        }
        #endregion

        #region RefreshCommand
        private ICommand _refreshCommand;
        public ICommand RefreshCommand
        {
            get
            {
                return _refreshCommand ?? (_refreshCommand = AsyncCommand.Create(
                    async () => 
                    {
                        await _dataModel.RefillDataAsync();
                        await _roomDataModel.RefillDataAsync();
                    }));
            }
        }
        #endregion

        #region DisplayDeviceDetailCommand
        private RelayCommand _displayDeviceDetailCommand;
        /// <summary>
        /// Gets the DisplayDeviceDetailCommand.
        /// </summary>
        public RelayCommand DisplayDeviceDetailCommand
        {
            get
            {
                return _displayDeviceDetailCommand
                    ?? (_displayDeviceDetailCommand = new RelayCommand(
                    () => { this._dialogService.ShowViewDeviceDialog(Devices.CurrentItem as Device); },
                    () => { return (Devices.CurrentItem as Device) != null; }));
            }
        }
        #endregion

        #region AddDeviceCommand
        private RelayCommand _addDeviceCommand;
        public RelayCommand AddDeviceCommand
        {
            get
            {
                return _addDeviceCommand ?? (_addDeviceCommand = new RelayCommand(
                        () => { this._dialogService.ShowAddDeviceDialog(); },
                        () => { return true; }));
            }
        }
        #endregion

        #region EditDeviceCommand
        private RelayCommand _editDeviceCommand;
        public RelayCommand EditDeviceCommand
        {
            get
            {
                return _editDeviceCommand ?? (_editDeviceCommand = new RelayCommand(
                        () => { this._dialogService.ShowEditDeviceDialog(Devices.CurrentItem as Device); },
                        () => { return (Devices.CurrentItem as Device) != null; }));
            }
        }
        #endregion

        #region DeleteDevicesCommand
        private ICommand _deleteDevicesCommand;
        public ICommand DeleteDevicesCommand
        {
            get
            {
                return _deleteDevicesCommand ?? (_deleteDevicesCommand = new AsyncCommand<IList, object>(
                    async (devices, _) => 
                    {
                        await _dataModel.DeleteAsync(devices as IList);
                        await _roomDataModel.DeleteRoomsWhichHaveNoDevicesAsync();
                        return null;
                    },
                    (devices) => { return (devices != null) && (devices.Count > 0); }));
            }
        }
        #endregion

        #region ImportAddressBookCommand
        private RelayCommand _importAddressBookCommand;
        public RelayCommand ImportAddressBookCommand
        {
            get
            {
                return _importAddressBookCommand ?? (_importAddressBookCommand = new RelayCommand(
                    () => { _dialogService.ShowImportAddressBookDialog(); }));
            }
        }
        #endregion

        #region ExportAddressBookCommnad
        private RelayCommand _exportAddressBookCommnad;
        public RelayCommand ExportAddressBookCommnad
        {
            get
            {
                return _exportAddressBookCommnad ?? (_exportAddressBookCommnad = new RelayCommand(
                        () => { _dialogService.ShowExportAddressBookDialog(); },
                        () => { return _dataModel.Data.Count > 0; }));
            }
        }
        #endregion

        //#region SyncAddressBookWithDevicesCommand
        //private RelayCommand _syncAddressBookWithDevicesCommand;
        //public RelayCommand SyncAddressBookWithDevicesCommand
        //{
        //    get
        //    {
        //        return _syncAddressBookWithDevicesCommand ?? (_syncAddressBookWithDevicesCommand = new RelayCommand(
        //                () => { this._dialogService.ShowSelectDevicesDialog(); },
        //                () => { return _dataModel.Data.Count > 0; }));
        //    }
        //}
        //#endregion

        #region OnlineStatusFilter
        #region RemoveOnlineStatusFilterCommand
        private ICommand _removeOnlineStatusFilterCommand;
        public ICommand RemoveOnlineStatusFilterCommand
        {
            get
            {
                return _removeOnlineStatusFilterCommand ?? (_removeOnlineStatusFilterCommand = new AsyncCommand<object>(
                    async (param, _) => { await RemoveOnlineStatusFilterAsync(); return null; },
                    () => CanRemoveOnlineStatusFilter));
            }
        }

        private bool _canRemoveOnlineStatusFilter;
        public bool CanRemoveOnlineStatusFilter
        {
            get { return _canRemoveOnlineStatusFilter; }
            set { Set(ref _canRemoveOnlineStatusFilter, value); }
        }
        #endregion

        private Nullable<int> _onlineStatusFilterValue;
        public Nullable<int> OnlineStatusFilterValue
        {
            get { return _onlineStatusFilterValue; }
            set
            {
                if (Set(ref _onlineStatusFilterValue, value))
                {
                    ApplyFilter(_onlineStatusFilterValue.HasValue ? FilterField.OnlineStatus : FilterField.None);
                }
            }
        }

        Predicate<object> _onlineStatusFilter;
        Predicate<object> OnlineStatusFilter
        {
            get { return _onlineStatusFilter ?? (_onlineStatusFilter = new Predicate<object>(FilterByOnlineStatus)); }
        }

        private bool FilterByOnlineStatus(object obj)
        {
            Device device = obj as Device;
            if (device != null && device.online == OnlineStatusFilterValue)
            {
                return true;
            }
            return false;
        }

        public async Task RemoveOnlineStatusFilterAsync()
        {
            await Task.Run(() =>
            {
                filters.RemoveFilter(OnlineStatusFilter);
                OnlineStatusFilterValue = null;
                CanRemoveOnlineStatusFilter = false;
            });
            Devices.Filter = filters.Filter;
        }

        public void AddOnlineStatusFilter()
        {
            // see Notes on Adding Filters:
            if (CanRemoveOnlineStatusFilter)
            {
                filters.RemoveFilter(OnlineStatusFilter);
                filters.AddFilter(OnlineStatusFilter);
                Devices.Filter = filters.Filter;
            }
            else
            {
                filters.AddFilter(OnlineStatusFilter);
                Devices.Filter = filters.Filter;
                CanRemoveOnlineStatusFilter = true;
            }
        }
        #endregion

        #region DeviceTypeFilter
        #region RemoveDeviceTypeFilterCommand
        private ICommand _removeDeviceTypeFilterCommand;
        public ICommand RemoveDeviceTypeFilterCommand
        {
            get
            {
                return _removeDeviceTypeFilterCommand ?? (_removeDeviceTypeFilterCommand = new AsyncCommand<object>(
                    async (param, _) => { await RemoveDeviceTypeFilterAsync(); return null; },
                    () => CanRemoveDeviceTypeFilter));
            }
        }

        private bool _canRemoveDeviceTypeFilter;
        public bool CanRemoveDeviceTypeFilter
        {
            get { return _canRemoveDeviceTypeFilter; }
            set { Set(ref _canRemoveDeviceTypeFilter, value); }
        }
        #endregion

        private Nullable<int> _deviceTypeFilterValue;
        public Nullable<int> DeviceTypeFilterValue
        {
            get { return _deviceTypeFilterValue; }
            set
            {
                if (Set(ref _deviceTypeFilterValue, value))
                {
                    ApplyFilter(_deviceTypeFilterValue.HasValue ? FilterField.DeviceType : FilterField.None);
                }
            }
        }

        private bool FilterByDeviceType(object obj)
        {
            Device device = obj as Device;
            if (device != null && device.type == DeviceTypeFilterValue)
            {
                return true;
            }
            return false;
        }

        Predicate<object> _deviceTypeFilter;
        Predicate<object> DeviceTypeFilter
        {
            get { return _deviceTypeFilter ?? (_deviceTypeFilter = new Predicate<object>(FilterByDeviceType)); }
        }

        public async Task RemoveDeviceTypeFilterAsync()
        {
            await Task.Run(() =>
            {
                filters.RemoveFilter(DeviceTypeFilter);
                DeviceTypeFilterValue = null;
                CanRemoveDeviceTypeFilter = false;
            });
            Devices.Filter = filters.Filter;
        }

        public void AddDeviceTypeFilter()
        {
            // see Notes on Adding Filters:
            if (CanRemoveDeviceTypeFilter)
            {
                filters.RemoveFilter(DeviceTypeFilter);
                filters.AddFilter(DeviceTypeFilter);
                Devices.Filter = filters.Filter;
            }
            else
            {
                filters.AddFilter(DeviceTypeFilter);
                Devices.Filter = filters.Filter;
                CanRemoveDeviceTypeFilter = true;
            }
        }
        #endregion

        #region FilterBaseFunctions

        GroupFilter filters = new GroupFilter();

        private enum FilterField
        {
            DeviceType,
            OnlineStatus,
            None
        }

        private void ApplyFilter(FilterField field)
        {
            switch (field)
            {
                case FilterField.DeviceType:
                    AddDeviceTypeFilter();
                    break;

                case FilterField.OnlineStatus:
                    AddOnlineStatusFilter();
                    break;

                default:
                    break;
            }
        }

        public async Task ResetFiltersAsync()
        {
            // clear filters 
            await RemoveDeviceTypeFilterAsync();
            await RemoveOnlineStatusFilterAsync();
        }

        #region ResetFiltersCommand
        private ICommand _resetFiltersCommand;
        public ICommand ResetFiltersCommand
        {
            get
            {
                return _resetFiltersCommand ?? (_resetFiltersCommand = new AsyncCommand<object>(
                    async (param, _) => { await ResetFiltersAsync(); return null; },
                    () => { return true; }));
            }
        }
        #endregion
        #endregion
    }

    public class GroupFilter
    {
        private List<Predicate<object>> _filters;

        public Predicate<object> Filter { get; private set; }

        public GroupFilter()
        {
            _filters = new List<Predicate<object>>();
            Filter = InternalFilter;
        }

        private bool InternalFilter(object o)
        {
            foreach (var filter in _filters)
            {
                if (!filter(o))
                {
                    return false;
                }
            }

            return true;
        }

        public void AddFilter(Predicate<object> filter)
        {
            _filters.Add(filter);
        }

        public void RemoveFilter(Predicate<object> filter)
        {
            if (_filters.Contains(filter))
            {
                _filters.Remove(filter);
            }
        }
    }
}

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using ICMServer.Models;
using ICMServer.Services;
using ICMServer.Services.Data;
using ICMServer.WPF.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace ICMServer.WPF.ViewModels
{
    public class DialogSelectDevicesToBeUpgradedViewModel : ViewModelBase
    {
        //private readonly ObservableCollection<Device> _selectedDevices = new ObservableCollection<Device>();
        private readonly ICollectionModel<Device> _deviceDataModel;
        private readonly IUpgradeTasksModel _upgradeTaskDataModel;
        private readonly IDialogService _dialogService;
        private readonly DeviceType? _deviceType = null;
        private readonly upgrade _upgradeFile;

        public DialogSelectDevicesToBeUpgradedViewModel(
            ICollectionModel<Device> deviceDataModel,
            IUpgradeTasksModel upgradeTaskDataModel,
            IDialogService dialogService,
            upgrade upgradeFile)
        {
            this._deviceDataModel = deviceDataModel;
            this._upgradeTaskDataModel = upgradeTaskDataModel;
            this._dialogService = dialogService;
            this._upgradeFile = upgradeFile;
            if (upgradeFile.filetype.HasValue)
            {
                if (upgradeFile.filetype == (int)UpgradeFileType.SoftwareUpgrade)
                {
                    _deviceType = (DeviceType?)upgradeFile.device_type;
                }
            }

            Devices = (ListCollectionView)new ListCollectionView((IList)_deviceDataModel.Data);
            using (Devices.DeferRefresh())
            {
                Devices.Filter = delegate (object obj)
                {
                    Device device = obj as Device;
                    if (device != null && device.type.HasValue)
                    {
                        if (_deviceType.HasValue)
                        {
                            if (device.type == (int)_deviceType)
                                return true;
                        }
                        else
                        {
                            switch (device.type)
                            {
                                // case (int)DeviceType.Control_Server:
                                case (int)DeviceType.Door_Camera:
                                case (int)DeviceType.Lobby_Phone_Unit:
                                case (int)DeviceType.Lobby_Phone_Building:
                                case (int)DeviceType.Lobby_Phone_Area:
                                case (int)DeviceType.Indoor_Phone:
                                case (int)DeviceType.Administrator_Unit:
                                case (int)DeviceType.Indoor_Phone_SD:
                                // case (int)DeviceType.Mobile_Phone:
                                case (int)DeviceType.Emergency_Intercom_Unit:
                                // case (int)DeviceType.IPCAM:
                                    return true;
                            }
                        }
                    }
                    return false;
                };
                Devices.SortDescriptions.Add(new SortDescription("ip", ListSortDirection.Ascending));
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

        #region SelectDeviceOption
        private SelectDeviceOption _selectDeviceOption;
        public SelectDeviceOption SelectDeviceOption
        {
            get { return _selectDeviceOption; }
            set { this.Set(ref _selectDeviceOption, value); }
        }
        #endregion

        #region DeviceAddress
        private string _deviceAddress = "";
        public string DeviceAddress
        {
            get { return _deviceAddress; }
            set { this.Set(ref _deviceAddress, value); }
        }
        #endregion

        private ICommand _PickDeviceAddressCommand;
        public ICommand PickDeviceAddressCommand
        {
            get
            {
                return _PickDeviceAddressCommand ?? (_PickDeviceAddressCommand = new RelayCommand(
                    () =>
                    {
                        _dialogService.ShowSelectDeviceAddressDialog("", (deviceAddress) =>
                        {
                            this.DeviceAddress = deviceAddress;
                        });
                    }));
            }
        }

        #region OkCommand
        private IAsyncCommand _OkCommand;
        public IAsyncCommand OkCommand
        {
            get
            {
                return _OkCommand ?? (_OkCommand = new AsyncCommand<Window, object>(
                    async (window, _) =>
                    {
                        Stopwatch sw = new Stopwatch();
                        sw.Start();

                        IsUpdating = true;

                        List<Task> insertTasks = new List<Task>();
                        switch (this.SelectDeviceOption)
                        {
                            case SelectDeviceOption.ByIP:
                                {
                                    var device = this.Devices.CurrentItem as Device;
                                    if (device != null)
                                        insertTasks.Add(InsertNewUpgradeTaskAsync(device));
                                    this.Progress = 10;
                                }
                                break;

                            case SelectDeviceOption.ByDeviceAddress:
                                var devices = (from d in this.Devices.Cast<Device>()
                                               where d.roomid.StartsWith(this.DeviceAddress)
                                               select d).ToList();

                                await Task.Run(() =>
                                {
                                    int i = 0;
                                    int devicesCount = devices.Count;
                                    if (devicesCount > 0)
                                    {
                                        foreach (var device in devices)
                                        {
                                            insertTasks.Add(InsertNewUpgradeTaskAsync(device));
                                            this.Progress = (++i) * 10 / devicesCount;
                                        }
                                    }
                                });
                                break;
                        }
                        //sw.Stop();
                        //DebugLog.TraceMessage(string.Format("Elapsed={0}", sw.Elapsed));
                        await TaskExtension.WhenAll(
                            insertTasks,
                            tasks => 
                            {
                                //DebugLog.TraceMessage(string.Format("C: {0} T: {1}", tasks.Count(task => task.IsCompleted), tasks.Count()));
                                this.Progress = (tasks.Count(task => task.IsCompleted)) * 90 / tasks.Count() + 10;
                            });

                        this.Progress = 100;
                        this._dialogService.ShowMessageBox(string.Format("已將 {0} 個更新任務置入排程", insertTasks.Count));
                        IsUpdating = false;
                        //this.CloseDialog(window as Window, true);
                        return null;
                    }));
            }
        }
        #endregion

        #region IsUpdating
        private bool _isUpdating;
        public bool IsUpdating
        {
            get { return _isUpdating; }
            private set
            {
                Set(ref _isUpdating, value);
                OkCommand.RaiseCanExecuteChanged();
            }
        }
        #endregion

        #region Progress
        private int _progress = 0;
        public int Progress
        {
            get { return _progress; }
            set { this.Set(ref _progress, value); }
        }
        #endregion

        private async Task InsertNewUpgradeTaskAsync(Device device)
        {
            var job = new UpgradeTask();
            job.DeviceID = device.id;
            job.UpgradeID = this._upgradeFile.id;
            await this._upgradeTaskDataModel.InsertAsync(job);
        }

        #region CloseCommand
        private ICommand _CloseCommand;
        public ICommand CloseCommand
        {
            get
            {
                return _CloseCommand ?? (_CloseCommand = new RelayCommand<Window>(
                    (window) =>
                    {
                        //this.CancelClicked(this, EventArgs.Empty);
                        this.CloseDialog(window as Window, false);
                    },
                    (_) => { return !IsUpdating; }));
            }
        }
        #endregion

        public void CloseDialog(Window view, bool dialogResult)
        {
            if (view != null)
                view.DialogResult = dialogResult;
        }
    }

    public enum SelectDeviceOption
    {
        ByIP = 0,
        ByDeviceAddress
    }
}

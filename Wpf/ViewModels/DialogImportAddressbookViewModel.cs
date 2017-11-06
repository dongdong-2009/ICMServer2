using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using ICMServer.Models;
using ICMServer.Services;
using ICMServer.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICMServer.WPF.ViewModels
{
    public class DialogImportAddressbookViewModel : ViewModelBase
    {
        private readonly ICollectionModel<Device> _devicesModel;
        private readonly IRoomsModel _roomsModel;
        private readonly IDialogService _dialogService;

        public DialogImportAddressbookViewModel(
            ICollectionModel<Device> devicesModel,
            IRoomsModel roomsModel,
            IDialogService dialogService)
        {
            _devicesModel = devicesModel;
            _roomsModel = roomsModel;
            _dialogService = dialogService;
            IsUpdating = false;
        }

        #region FilePath
        private string _filePath;
        public string FilePath
        {
            get { return _filePath; }
            private set
            {
                Set(ref _filePath, value);
                StartCommand.RaiseCanExecuteChanged();
            }
        }
        #endregion

        #region Progress
        private int _progress;
        public int Progress
        {
            get { return _progress; }
            private set
            {
                Set(ref _progress, value);
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
                PickFileCommand.RaiseCanExecuteChanged();
                StartCommand.RaiseCanExecuteChanged();
            }
        }
        #endregion

        #region PickFileCommand
        private RelayCommand _pickFileCommand;
        public RelayCommand PickFileCommand
        {
            get
            {
                return _pickFileCommand ?? (_pickFileCommand = new RelayCommand(() =>
                    {
                        _dialogService.ShowOpenFileDialog(
                            "XML files (*.xml)|*.xml|All files (*.*)|*.*",
                            "addressbook.xml",
                            (PickedFilePath) => { this.FilePath = PickedFilePath; });
                    },
                    () =>
                    {
                        return !IsUpdating;
                    }));
            }
        }
        #endregion

        #region StartCommand
        private IAsyncCommand _startCommand;
        public IAsyncCommand StartCommand
        {
            get
            {
                return _startCommand ?? (_startCommand = new AsyncCommand<object>
                    (async (param, cancelToken) =>
                    {
                        IsUpdating = true;
                        try
                        {
                            await Task.Run(() =>
                            {
                                AddrList addrList = new AddrList();
                                addrList.dev.BeginLoadData();
                                addrList.dev.ReadXml(FilePath);
                                addrList.dev.EndLoadData();

                                int totalProcesses = addrList.dev.Count() + 1;
                                int processesDone = 0;

                                Progress = 0;
                                _devicesModel.DeleteAll();
                                _roomsModel.DeleteAll();
                                Progress = ++processesDone * 100 / totalProcesses;
                                //lock (_devicesModel.Devices)
                                try
                                {
                                    //List<Task> insertTasks = new List<Task>();
                                    foreach (var d in addrList.dev)
                                    {
                                        if (cancelToken.IsCancellationRequested == true)
                                        {
                                            cancelToken.ThrowIfCancellationRequested();
                                        }
                                        Device dev = new Device();
                                        dev.ip = d.ip;
                                        dev.roomid = d.ro;
                                        dev.alias = (d.IsaliasNull()) ? null : d.alias;
                                        dev.group = (d.IsgroupNull()) ? null : d.group;
                                        dev.mac = (d.IsmcNull()) ? null : d.mc;
                                        dev.type = d.ty;
                                        dev.sm = d.sm;
                                        dev.gw = d.gw;
                                        dev.cameraid = (d.IsidNull()) ? null : d.id;
                                        dev.camerapw = (d.IspwNull()) ? null : d.pw;
                                        _devicesModel.Insert(dev);

                                        Room room = new Room();
                                        room.ID = d.ro.Substring(0, 14);
                                        _roomsModel.Insert(room);

                                        Progress = ++processesDone * 100 / totalProcesses;
                                    }
                                }
                                catch (Exception) { }
                                Progress = 100;
                            });
                        }
                        catch (Exception e)
                        {
                            throw e;    // rethrow exeception
                        }
                        finally
                        { 
                            IsUpdating = false;
                        }
                        return null;
                    },
                    () =>
                    {
                        return (!IsUpdating) && (!string.IsNullOrWhiteSpace(FilePath));
                    }));
            }
        }
        #endregion
    }
}

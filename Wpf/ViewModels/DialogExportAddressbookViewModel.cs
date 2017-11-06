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
    public class DialogExportAddressbookViewModel : ViewModelBase
    {
        private readonly ICollectionModel<Device> _devicesModel;
        private readonly IDialogService _dialogService;
        private object _lock = new object();

        public DialogExportAddressbookViewModel(
            ICollectionModel<Device> devicesModel,
            IDialogService dialogService)
        {
            _devicesModel = devicesModel;
            _dialogService = dialogService;
        }

        #region FilePath
        private string _filePath;
        public string FilePath
        {
            get { lock (_lock) { return _filePath; } }
            private set
            {
                lock (_lock)
                {
                    Set(ref _filePath, value);
                }
                StartCommand.RaiseCanExecuteChanged();
            }
        }
        #endregion

        #region IsUpdating
        private bool _isUpdating;
        public bool IsUpdating
        {
            get { lock (_lock) { return _isUpdating; } }
            private set
            {
                lock (_lock)
                {
                    Set(ref _isUpdating, value);
                }
                PickFileCommand.RaiseCanExecuteChanged();
                StartCommand.RaiseCanExecuteChanged();
            }
        }
        #endregion

        #region Operation
        private ExportAddressBookOperation _operation;
        public ExportAddressBookOperation Operation
        {
            get { return _operation; }
            set
            {
                lock (_lock)
                {
                    if (!IsUpdating)
                    {
                        if (Set(ref _operation, value))
                        {
                            switch (_operation)
                            {
                                default:
                                case ExportAddressBookOperation.ToXml:
                                    _operationExecutor = new ExportAddressBookToXml();
                                    break;

                                case ExportAddressBookOperation.ToXmlPkg:
                                    _operationExecutor = new ExportAddressBookToXmlPkg();
                                    break;

                                case ExportAddressBookOperation.ToUclPkg:
                                    _operationExecutor = new ExportAddressBookToUclPkg();
                                    break;
                            }

                            if (!string.IsNullOrEmpty(FilePath))
                            {
                                FilePath = System.IO.Path.GetDirectoryName(FilePath)
                                         + System.IO.Path.GetFileNameWithoutExtension(FilePath)
                                         + "."
                                         + _operationExecutor.DefaultExtFileName;
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region OperationExecutor
        private IExportAddressBookOperationExecutor _operationExecutor = new ExportAddressBookToXml();
        #endregion

        //#region Filter
        //private string Filter
        //{
        //    get
        //    {
        //        string value = "";
        //        switch (Operation)
        //        {
        //            default:
        //            case ExportAddressBookOperations.ToXml:
        //                value = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
        //                break;

        //            case ExportAddressBookOperations.ToXmlPkg:
        //            case ExportAddressBookOperations.ToUclPkg:
        //                value = "PKG files (*.pkg)|*.pkg|All files (*.*)|*.*";
        //                break;
        //        }
        //        return value;
        //    }
        //}
        //#endregion

        //#region DefaultFileName
        //private string DefaultFileName
        //{
        //    get
        //    {
        //        string value = "";
        //        switch (Operation)
        //        {
        //            default:
        //            case ExportAddressBookOperations.ToXml:
        //                value = "addressbook.xml";
        //                break;

        //            case ExportAddressBookOperations.ToXmlPkg:
        //            case ExportAddressBookOperations.ToUclPkg:
        //                value = "addressbook.pkg";
        //                break;
        //        }
        //        return value;
        //    }
        //}
        //#endregion

        #region PickFileCommand
        private RelayCommand _pickFileCommand;
        public RelayCommand PickFileCommand
        {
            get
            {
                return _pickFileCommand ?? (_pickFileCommand = new RelayCommand(() =>
                    {
                        _dialogService.ShowSaveFileDialog(
                            _operationExecutor.Filter,
                            _operationExecutor.DefaultFileName,
                            (PickedFilePath) => { this.FilePath = PickedFilePath; });
                    },
                    () => { return !IsUpdating; }));
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
                                var devices = this._devicesModel.Data.ToList();
                                _operationExecutor.Export(FilePath, devices, Time.GetSevenCharsHexTimestamp());
                                //    AddrList addrList = new AddrList();
                                //    addrList.dev.BeginLoadData();
                                //    addrList.dev.ReadXml(FilePath);
                                //    addrList.dev.EndLoadData();

                                //    int totalProcesses = addrList.dev.Count() + 1;
                                //    int processesDone = 0;

                                //    Progress = 0;
                                //    _devicesModel.DeleteAll();
                                //    _roomsModel.DeleteAll();
                                //    Progress = ++processesDone * 100 / totalProcesses;
                                //    //lock (_devicesModel.Devices)
                                //    {
                                //        foreach (var d in addrList.dev)
                                //        {
                                //            if (cancelToken.IsCancellationRequested == true)
                                //            {
                                //                cancelToken.ThrowIfCancellationRequested();
                                //            }
                                //            Device dev = new Device();
                                //            dev.ip = d.ip;
                                //            dev.roomid = d.ro;
                                //            dev.alias = (d.IsaliasNull()) ? null : d.alias;
                                //            dev.group = (d.IsgroupNull()) ? null : d.group;
                                //            dev.mac = (d.IsmcNull()) ? null : d.mc;
                                //            dev.type = d.ty;
                                //            dev.sm = d.sm;
                                //            dev.gw = d.gw;
                                //            dev.cameraid = (d.IsidNull()) ? null : d.id;
                                //            dev.camerapw = (d.IspwNull()) ? null : d.pw;
                                //            _devicesModel.Insert(dev);

                                //            Room room = new Room();
                                //            room.ID = d.ro.Substring(0, 14);
                                //            _roomsModel.Insert(room);

                                //            Progress = ++processesDone * 100 / totalProcesses;
                                //        }
                                //    }
                                //    Progress = 100;
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
                    () => { return (!IsUpdating) && (!string.IsNullOrWhiteSpace(FilePath)); }));
            }
        }
        #endregion
    }

    public enum ExportAddressBookOperation
    {
        ToXml = 0,
        ToXmlPkg,
        ToUclPkg
    }

    internal interface IExportAddressBookOperationExecutor
    {
        string Filter { get; }
        string DefaultFileName { get; }
        string DefaultExtFileName { get; }
        void Export(
            string fileName,
            List<Device> devices,
            string version = "1.0");
    }

    internal class ExportAddressBookToXml : IExportAddressBookOperationExecutor
    {
        public string DefaultExtFileName
        {
            get { return "xml"; }
        }

        public string DefaultFileName
        {
            get { return "addressbook.xml"; }
        }

        public string Filter
        {
            get { return "XML files (*.xml)|*.xml|All files (*.*)|*.*"; }
        }

        public void Export(string fileName, List<Device> devices, string version = "1.0")
        {
            AddressBook.ExportXml(fileName, devices, version);
        }
    }

    internal class ExportAddressBookToXmlPkg : IExportAddressBookOperationExecutor
    {
        public string DefaultExtFileName
        {
            get { return "pkg"; }
        }

        public string DefaultFileName
        {
            get { return "addressbook.pkg"; }
        }

        public string Filter
        {
            get { return "PKG files (*.pkg)|*.pkg|All files (*.*)|*.*"; }
        }

        public void Export(string fileName, List<Device> devices, string version = "1.0")
        {
            AddressBook.ExportXmlPkg(fileName, devices, version);
        }
    }

    internal class ExportAddressBookToUclPkg : IExportAddressBookOperationExecutor
    {
        public string DefaultExtFileName
        {
            get { return "pkg"; }
        }

        public string DefaultFileName
        {
            get { return "addressbook.pkg"; }
        }

        public string Filter
        {
            get { return "PKG files (*.pkg)|*.pkg|All files (*.*)|*.*"; }
        }

        public void Export(string fileName, List<Device> devices, string version = "1.0")
        {
            AddressBook.ExportUclPkg(fileName, devices, version);
        }
    }
}

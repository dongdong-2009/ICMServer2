using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using ICMServer.Models;
using ICMServer.Services;
using ICMServer.Services.Data;
using ICMServer.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICMServer.WPF.ViewModels
{
    public class DialogExportCardListViewModel : ViewModelBase
    {
        private readonly IDeviceDataService _deviceDataService;
        private readonly IDialogService _dialogService;
        private object _lock = new object();

        public DialogExportCardListViewModel(
            IDeviceDataService deviceDataService,
            IDialogService dialogService)
        {
            _deviceDataService = deviceDataService;
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
        private ExportCardListOperation _operation;
        public ExportCardListOperation Operation
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
                                case ExportCardListOperation.ToXml:
                                    _operationExecutor = new ExportCardListToXml();
                                    break;

                                case ExportCardListOperation.ToXmlPkg:
                                    _operationExecutor = new ExportCardListToXmlPkg();
                                    break;

                                case ExportCardListOperation.ToUclPkg:
                                    _operationExecutor = new ExportCardListToUclPkg();
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
        private IExportCardListOperationExecutor _operationExecutor = new ExportCardListToXml();
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
                                var maps = _deviceDataService.SelectCardList();
                                _operationExecutor.Export(FilePath, maps, Time.GetSevenCharsHexTimestamp());
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

    public enum ExportCardListOperation
    {
        ToXml = 0,
        ToXmlPkg,
        ToUclPkg
    }

    internal interface IExportCardListOperationExecutor
    {
        string Filter { get; }
        string DefaultFileName { get; }
        string DefaultExtFileName { get; }
        void Export(
            string fileName,
            IEnumerable<DeviceSecurityCardDto> maps,
            string version = "1.0");
    }

    internal class ExportCardListToXml : IExportCardListOperationExecutor
    {
        public string DefaultExtFileName
        {
            get { return "xml"; }
        }

        public string DefaultFileName
        {
            get { return "cardlist.xml"; }
        }

        public string Filter
        {
            get { return "XML files (*.xml)|*.xml|All files (*.*)|*.*"; }
        }

        public void Export(string fileName, IEnumerable<DeviceSecurityCardDto> maps, string version = "1.0")
        {
            CardList.ExportXml(fileName, maps, version);
        }
    }

    internal class ExportCardListToXmlPkg : IExportCardListOperationExecutor
    {
        public string DefaultExtFileName
        {
            get { return "pkg"; }
        }

        public string DefaultFileName
        {
            get { return "CARD.pkg"; }
        }

        public string Filter
        {
            get { return "PKG files (*.pkg)|*.pkg|All files (*.*)|*.*"; }
        }

        public void Export(string fileName, IEnumerable<DeviceSecurityCardDto> maps, string version = "1.0")
        {
            CardList.ExportXmlPkg(fileName, maps, version);
        }
    }

    internal class ExportCardListToUclPkg : IExportCardListOperationExecutor
    {
        public string DefaultExtFileName
        {
            get { return "pkg"; }
        }

        public string DefaultFileName
        {
            get { return "CARD.pkg"; }
        }

        public string Filter
        {
            get { return "PKG files (*.pkg)|*.pkg|All files (*.*)|*.*"; }
        }

        public void Export(string fileName, IEnumerable<DeviceSecurityCardDto> maps, string version = "1.0")
        {
            CardList.ExportUclPkg(fileName, maps, version);
        }
    }
}

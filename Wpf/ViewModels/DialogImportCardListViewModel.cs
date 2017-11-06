using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using ICMServer.Models;
using ICMServer.Services;
using ICMServer.Services.Data;
using ICMServer.WPF.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ICMServer.WPF.ViewModels
{
    public class DialogImportCardListViewModel : ViewModelBase
    {
        private readonly ICollectionModel<iccard> _cardsModel;
        private readonly IDialogService _dialogService;
        private readonly IDataService<iccard> _securityCardDataService;
        private readonly IDataService<icmap> _securityCardDeviceDataService;
        private readonly IDeviceDataService _deviceDataService;
        private readonly Action _afterImportedCallback;

        public DialogImportCardListViewModel(
            ICollectionModel<iccard> cardsModel,
            IDataService<iccard> securityCardDataService,
            IDataService<icmap> securityCardDeviceDataService,
            IDeviceDataService deviceDataService,
            IDialogService dialogService,
            Action afterImportedCallback = null)
        {
            _securityCardDataService = securityCardDataService;
            _securityCardDeviceDataService = securityCardDeviceDataService;
            _deviceDataService = deviceDataService;
            _cardsModel = cardsModel;
            _dialogService = dialogService;
            _afterImportedCallback = afterImportedCallback;
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
                    //Messenger.Default.Send(OpenFileDialog
                    _dialogService.ShowOpenFileDialog(
                        "XML files (*.xml)|*.xml|All files (*.*)|*.*",
                        "cardlist.xml",
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
                                XDocument doc = XDocument.Load(FilePath);
                                var deviceNodes = doc.Root.Elements("dev");

                                int totalProcesses = deviceNodes.Count() + 1;
                                int processesDone = 0;

                                Progress = 0;
                                foreach (var deviceNode in deviceNodes)
                                {
                                    if (cancelToken.IsCancellationRequested == true)
                                    {
                                        cancelToken.ThrowIfCancellationRequested();
                                    }
                                    string deviceId = deviceNode.Attribute("ro").Value;
                                    var deviceInDB = _deviceDataService.Select(d => d.roomid == deviceId).FirstOrDefault();
                                    //if (deviceInDB != null)
                                    {
                                        var cards = (from card in deviceNode.Elements("card")
                                                     select new iccard { C_icno = card.Value, C_roomid = "00-00-00-00-00" }).ToList();
                                        //using (var db = new ICMDBContext())
                                        {
                                            foreach (var card in cards)
                                            {
                                                if (cancelToken.IsCancellationRequested == true)
                                                {
                                                    cancelToken.ThrowIfCancellationRequested();
                                                }
                                                if (deviceInDB != null)
                                                {
                                                    var mapInDB = _securityCardDeviceDataService.Select(
                                                        m => m.C_icno == card.C_icno
                                                          && m.C_entrancedoor == deviceId).FirstOrDefault();
                                                    if (mapInDB == null)
                                                    {
                                                        _securityCardDeviceDataService.Insert(new icmap { C_icno = card.C_icno, C_entrancedoor = deviceId });
                                                    }
                                                }

                                                var cardInDB = _securityCardDataService.Select(c => c.C_icno == card.C_icno).FirstOrDefault();
                                                if (cardInDB == null)
                                                {
                                                    _cardsModel.Insert(card);
                                                }
                                                
                                            }
                                        }
                                        //db.SaveChanges();
                                    }
                                    Progress = ++processesDone * 100 / totalProcesses;
                                }
                            
                                //AddrList addrList = new AddrList();
                                //addrList.dev.BeginLoadData();
                                //addrList.dev.ReadXml(FilePath);
                                //addrList.dev.EndLoadData();

                                //int totalProcesses = addrList.dev.Count() + 1;
                                //int processesDone = 0;

                                //Progress = 0;
                                //_cardsModel.DeleteAll();
                                //Progress = ++processesDone * 100 / totalProcesses;
                                ////lock (_devicesModel.Devices)
                                //{
                                //    foreach (var d in addrList.dev)
                                //    {
                                //        if (cancelToken.IsCancellationRequested == true)
                                //        {
                                //            cancelToken.ThrowIfCancellationRequested();
                                //        }
                                //        Device dev = new Device();
                                //        dev.ip = d.ip;
                                //        dev.roomid = d.ro;
                                //        dev.alias = (d.IsaliasNull()) ? null : d.alias;
                                //        dev.group = (d.IsgroupNull()) ? null : d.group;
                                //        dev.mac = (d.IsmcNull()) ? null : d.mc;
                                //        dev.type = d.ty;
                                //        dev.sm = d.sm;
                                //        dev.gw = d.gw;
                                //        dev.cameraid = (d.IsidNull()) ? null : d.id;
                                //        dev.camerapw = (d.IspwNull()) ? null : d.pw;
                                //        _cardsModel.Insert(dev);

                                //        Progress = ++processesDone * 100 / totalProcesses;
                                //    }
                                //}
                                Progress = 100;
                            });
                        }
                        catch (Exception e)
                        {
                            throw e;    // rethrow exeception
                        }
                        finally
                        {
                            if (_afterImportedCallback != null)
                                _afterImportedCallback();
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

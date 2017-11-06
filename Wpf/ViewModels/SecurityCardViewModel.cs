using FluentValidation;
using GalaSoft.MvvmLight.CommandWpf;
using ICMServer.Models;
using ICMServer.Services;
using ICMServer.Services.Data;
using ICMServer.WPF.Collections.ObjectModel;
using ICMServer.WPF.Models;
using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Data;
using System.Windows.Input;

namespace ICMServer.WPF.ViewModels
{
    // TODO: AddCommand 和 UpdateCommand 必須等到 RelatedDevicesData 和 UnRelatedDevicesData 載入完畢後才能啟用
    public class SecurityCardViewModel : SingleDataViewModelBase<iccard, SecurityCardViewModel>
    {
        //private ISecurityCardsDevicesModel _securityCardsDevicesDataModel;
        private readonly IDataService<icmap> _securityCardDeviceDataService;
        private readonly IDeviceDataService _deviceDataService;
        private readonly IDialogService _dialogService;

        private ObservableRangeCollection<string> _relatedDevicesData;
        private ObservableRangeCollection<string> _unrelatedDevicesData;
        private object _lockForRelatedDevicesData = new object();
        private object _lockForUnrelatedDevicesData = new object();
        //private ISecurityCardUnrelatedDevicesModel _securityCardUnrelatedDevicesDataModel;

        /// <summary>
        /// Initializes a new instance of the SecurityCardViewModel class.
        /// </summary>
        public SecurityCardViewModel(
            IValidator<SecurityCardViewModel> validator,
            ICollectionModel<iccard> dataModel,
            IDataService<icmap> securityCardDeviceDataService,
            IDeviceDataService deviceDataService,
            IDialogService dialogService,
            iccard data = null) : base(validator, dataModel, data)
        {
            Init();

            this._securityCardDeviceDataService = securityCardDeviceDataService;
            this._deviceDataService = deviceDataService;
            this._dialogService = dialogService;

            RefreshCommand.Execute(null);
        }

#if DEBUG
        public SecurityCardViewModel() : base()
        {
            Init();
        }

        protected override void InitSampleData()
        {
            ID = 1;
            CardNumber = "00000123131231231";
            RoomAddress = "02-01-06-01-01";
            OwnerName = "王小明";
            CardType = SecurityCardType.Resident;
            IsActive = true;
            InsertedTime = DateTime.Now;
            StartTime = DateTime.Now - new TimeSpan(30, 0, 0, 0);
            EndTime = DateTime.Now + new TimeSpan(30, 0, 0, 0);
            //_relatedDevicesData.Add("01-02-03-04-05-06");
            //_relatedDevicesData.Add("01-02-03-04-05-07");
            //_unrelatedDevicesData.Add("02-02-03-04-05-06");
            //_unrelatedDevicesData.Add("02-02-03-04-05-07");
        }
#endif

        protected virtual void Init()
        {
            _relatedDevicesData = new ObservableRangeCollection<string>();
            _unrelatedDevicesData = new ObservableRangeCollection<string>();
            BindingOperations.EnableCollectionSynchronization(_relatedDevicesData, _lockForRelatedDevicesData);
            BindingOperations.EnableCollectionSynchronization(_unrelatedDevicesData, _lockForUnrelatedDevicesData);
            RelatedDevices = (ListCollectionView)CollectionViewSource.GetDefaultView((IList)_relatedDevicesData);
            UnrelatedDevices = (ListCollectionView)CollectionViewSource.GetDefaultView((IList)_unrelatedDevicesData);
        }

        #region RefreshCommand
        private ICommand _refreshCommand;
        public ICommand RefreshCommand
        {
            get
            {
                return _refreshCommand ?? (_refreshCommand = AsyncCommand.Create(
                    () => { return Task.Run(() => 
                    {
                        var allEntranceDevices = _deviceDataService.Select(device => (int)DeviceType.Control_Server < device.type
                                                                                  && device.type < (int)DeviceType.Indoor_Phone)
                                                 .Select(device => device.roomid)
                                                 .ToList();
                        if (this._data == null)
                        {
                            lock (this._relatedDevicesData)
                            {
                                _relatedDevicesData.Clear();
                                _unrelatedDevicesData.ReplaceRange(allEntranceDevices);
                            }
                        }
                        else
                        {
                            var securityCardRelatedDevices = _securityCardDeviceDataService.Select(mapping => mapping.C_icno == this._data.C_icno)
                                                             .Select(mapping => mapping.C_entrancedoor)
                                                             .ToList();
                            var securityCardUnrelatedDevices = (from d in allEntranceDevices
                                                                //where !(from rd in securityCardRelatedDevices
                                                                //        select rd).Equals(d)
                                                                where !securityCardRelatedDevices.Contains(d)
                                                                select d).ToList();
                            lock (this._relatedDevicesData)
                            {
                                _relatedDevicesData.ReplaceRange(securityCardRelatedDevices);
                                _unrelatedDevicesData.ReplaceRange(securityCardUnrelatedDevices);
                            }
                        }
                    }); }));
            }
        }
        #endregion

        protected override void InitDefaultValue()
        {
            CardType = SecurityCardType.Resident;
        }

        protected override void ModelToViewModel()
        {
            ID = this._data.C_icid;
            CardNumber = this._data.C_icno;
            RoomAddress = this._data.C_roomid;
            OwnerName = this._data.C_username;
            if (this._data.C_ictype.HasValue)
                CardType = (SecurityCardType)this._data.C_ictype;
            else
                CardType = null;
            IsActive = this._data.C_available.HasValue && this._data.C_available != 0;
            InsertedTime = this._data.C_time;
            StartTime = this._data.C_uptime;
            EndTime = this._data.C_downtime;
        }

        protected override void ViewModelToModel()
        {
            this._data.C_icno = CardNumber;
            this._data.C_roomid = RoomAddress;
            this._data.C_username = OwnerName;
            if (CardType != null)
                this._data.C_ictype = (int)CardType;
            else
                this._data.C_ictype = null;
            this._data.C_available = IsActive ? 1 : 0;
            this._data.C_time = InsertedTime;
            this._data.C_uptime = StartTime;
            this._data.C_downtime = EndTime;
        }

        #region Data Mapping Properties
        public int ID { get; set; }

        private string _cardNumber;
        /// <summary>
        /// 门禁卡卡號，由16個數字組成
        /// </summary>
        public string CardNumber
        {
            get { return _cardNumber; }
            set { this.Set(ref _cardNumber, value); }
        }

        private string _roomAddress;
        /// <summary>
        /// 门禁卡使用者门牌号
        /// </summary>
        public string RoomAddress
        {
            get { return _roomAddress; }
            set { this.Set(ref _roomAddress, value); }
        }

        private string _ownerName;
        /// <summary>
        /// 使用者名称
        /// </summary>
        public string OwnerName
        {
            get { return _ownerName; }
            set { this.Set(ref _ownerName, value); }
        }

        private SecurityCardType? _cardType;
        /// <summary>
        /// 门禁卡类型
        /// </summary>
        public SecurityCardType? CardType
        {
            get { return _cardType; }
            set { this.Set(ref _cardType, value); }
        }

        private bool _isActive;
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsActive
        {
            get
            {
                if (_isActive)
                {
                    if ((!StartTime.HasValue || StartTime <= DateTime.Now)
                     && (!EndTime.HasValue   || EndTime   >= DateTime.Now))
                        return true;
                }
                return false;
            }
            set { this.Set(ref _isActive, value); }
        }

        private DateTime? _insertedTime;
        /// <summary>
        /// 门禁卡添加时间
        /// </summary>
        public System.DateTime? InsertedTime
        {
            get { return _insertedTime; }
            set { this.Set(ref _insertedTime, value); }
        }

        private DateTime? _startTime;
        /// <summary>
        /// 门禁卡时效开始时间
        /// </summary>
        public DateTime? StartTime
        {
            get { return _startTime; }
            set
            {
                if (this.Set(ref _startTime, value))
                {
                    this.RaisePropertyChanged(() => IsStartTimeValid);
                }
            }
        }

        private DateTime? _endTime;
        /// <summary>
        /// 门禁卡时效结束时间
        /// </summary>
        public DateTime? EndTime
        {
            get { return _endTime; }
            set
            {
                if (this.Set(ref _endTime, value))
                {
                    this.RaisePropertyChanged(() => IsEndTimeValid);
                }
            }
        }

        #endregion

        public bool IsStartTimeValid
        {
            get { return StartTime != null; }
            set
            {
                if (value == false)
                    this.StartTime = null;
            }
        }

        public bool IsEndTimeValid
        {
            get { return EndTime != null; }
            set
            {
                if (value == false)
                    this.EndTime = null;
            }
        }

        #region RelatedDevices
        private ListCollectionView _relatedDevices;
        public ListCollectionView RelatedDevices
        {
            get { return _relatedDevices; }
            private set { this.Set(ref _relatedDevices, value); }
        }
        #endregion

        #region UnrelatedDevices
        private ListCollectionView _unrelatedDevices;
        public ListCollectionView UnrelatedDevices
        {
            get { return _unrelatedDevices; }
            private set { this.Set(ref _unrelatedDevices, value); }
        }
        #endregion

        #region AddRelatedDevicesCommand
        private IAsyncCommand _addRelatedDevicesCommand;
        public IAsyncCommand AddRelatedDevicesCommand
        {
            get
            {
                return _addRelatedDevicesCommand ?? (_addRelatedDevicesCommand = new AsyncCommand<IList, object>(
                    async (deviceAddresses, _) => { await AddRelatedDevicesAsync(deviceAddresses as IList); return null; },
                    (deviceAddresses) => { return (deviceAddresses != null) && (deviceAddresses.Count > 0); }));
            }
        }

        protected virtual Task AddRelatedDevicesAsync(IList objs)
        {
            return Task.Run(() =>
            {
                lock (this._relatedDevicesData)
                {
                    foreach (var obj in objs)
                    {
                        _relatedDevicesData.Add(obj as string);
                        _unrelatedDevicesData.Remove(obj as string);
                    }
                }
            });
        }
        #endregion

        #region DeleteRelatedDevicesCommand
        private IAsyncCommand _deleteRelatedDevicesCommand;
        public IAsyncCommand DeleteRelatedDevicesCommand
        {
            get
            {
                return _deleteRelatedDevicesCommand ?? (_deleteRelatedDevicesCommand = new AsyncCommand<IList, object>(
                    async (deviceAddresses, _) => { await DeleteRelatedDevicesAsync(deviceAddresses as IList); return null; },
                    (deviceAddresses) => { return (deviceAddresses != null) && (deviceAddresses.Count > 0); }));
            }
        }

        protected virtual Task DeleteRelatedDevicesAsync(IList objs)
        {
            return Task.Run(() =>
            {
                lock (this._relatedDevicesData)
                {
                    lock (this._relatedDevicesData)
                    {
                        foreach (var obj in objs)
                        {
                            _unrelatedDevicesData.Add(obj as string);
                            _relatedDevicesData.Remove(obj as string);
                        }
                    }
                }
            });
        }
        #endregion

        #region PickRoomAddressCommand
        private RelayCommand _PickRoomAddressCommand;
        public RelayCommand PickRoomAddressCommand
        {
            get
            {
                return _PickRoomAddressCommand ?? (_PickRoomAddressCommand = new RelayCommand(() =>
                {
                    this._dialogService.ShowSelectRoomAddressDialog(
                        this.RoomAddress, 
                        (roomAddress) => { this.RoomAddress = roomAddress; });
                }));
            }
        }
        #endregion

        protected override async Task AddAsync()
        {
            this._data = new iccard();
            if (this._dataModel != null && this._data != null)
            {
                // TODO: transcation 機制
                // 現有方式在資料有錯的時候無法復原
                ViewModelToModel();
                using (var ts = new TransactionScope())
                {
                    try
                    {
                        await this._dataModel.InsertAsync(this._data);
                        foreach (var deviceAddress in this.RelatedDevices)
                        {
                            icmap m = new icmap
                            {
                                C_icno = this._data.C_icno,
                                C_entrancedoor = deviceAddress as string
                            };
                            await this._securityCardDeviceDataService.InsertAsync(m);
                        }
                        ts.Complete();
                    }
                    catch (Exception) { }
                }
                RefreshCommand.Execute(null);
            }
        }

        protected override async Task UpdateAsync()
        {
            if (this._dataModel != null && this._data != null)
            {
                ViewModelToModel();
                using (var ts = new TransactionScope())
                {
                    try
                    {
                        await this._dataModel.UpdateAsync(this._data);
                        await this._securityCardDeviceDataService.DeleteAsync(m => m.C_icno == this._data.C_icno);
                        foreach (var deviceAddress in this.RelatedDevices)
                        {
                            icmap m = new icmap
                            {
                                C_icno = this._data.C_icno,
                                C_entrancedoor = deviceAddress as string
                            };
                            await this._securityCardDeviceDataService.InsertAsync(m);
                        }
                        ts.Complete();
                    }
                    catch (Exception) { }
                }
                RefreshCommand.Execute(null);
            }
        }
    }
}

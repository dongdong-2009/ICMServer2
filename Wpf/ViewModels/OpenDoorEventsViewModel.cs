using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using ICMServer.Models;
using ICMServer.Services;
using ICMServer.Services.Data;
using ICMServer.WPF.Models;
using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace ICMServer.WPF.ViewModels
{
    public class OpenDoorEventsViewModel : ViewModelBase
    {
        private readonly ICollectionModel<eventopendoor> _dataModel;
        private readonly IDataService<photograph> _dataServiceSnapshot;
        private readonly IDialogService _dialogService;
        private readonly IPrintService _printService;

        // 請使用 ObservableRangeCollection 來取代 ObservableCollection，
        // 主要是在實作refresh data 這功能時，使用此類別的ReplaceRange method可以避免DataGrid 跳動
        //private ObservableRangeCollection<Device> _devicesCollection;
        //private static object _lock = new object();

        // TODO: BUG...DateTime always 以 en-US 呈現
        // TODO: 開門者要轉換為比較有意義的呈現文字
        public OpenDoorEventsViewModel(
            ICollectionModel<eventopendoor> dataModel,
            IDataService<photograph> dataServiceSnapshot,
            IDialogService dialogService,
            IPrintService printService)
        {
            this._dialogService = dialogService;
            this._dataModel = dataModel;
            this._dataServiceSnapshot = dataServiceSnapshot;
            this._printService = printService;
            OpenDoorEvents = (ListCollectionView)CollectionViewSource.GetDefaultView((IList)_dataModel.Data);
            OpenDoorEvents.CurrentChanged += OpenDoorEvents_CurrentChanged;

            RefreshCommand.Execute(null);
        }

        private void OpenDoorEvents_CurrentChanged(object sender, EventArgs e)
        {
            this.DeleteOpenDoorEventsCommand.RaiseCanExecuteChanged();
        }

        protected DeferredAction _updateSnapshot;
        protected readonly TimeSpan _updateSnapshotDelay = TimeSpan.FromMilliseconds(250);
        protected DeferredAction UpdateSnapshot
        {
            get
            {
                return _updateSnapshot ?? (_updateSnapshot = DeferredAction.Create(
                    () => { UpdateSnapshotCommand.Execute(OpenDoorEvents.CurrentItem); }));
            }
        }

        #region RefreshCommand
        private ICommand _updateSnapshotCommand;
        public ICommand UpdateSnapshotCommand
        {
            get
            {
                return _updateSnapshotCommand ?? (_updateSnapshotCommand = new AsyncCommand<eventopendoor, object>(
                    async (openDoorEvent, _) => { await UpdateSnapshotAsync(openDoorEvent); return null; },
                    (openDoorEvent) => { return (openDoorEvent != null) ; }));
            }
        }
        #endregion

        async Task UpdateSnapshotAsync(eventopendoor openDoorEvent)
        {
            await Task.Run(() => 
            {
                var snapshot = this._dataServiceSnapshot.Select(photo => photo.C_srcaddr == openDoorEvent.C_from
                                                       && photo.C_time == openDoorEvent.C_time).FirstOrDefault();
                if (snapshot != null)
                    this.Snapshot = ByteToImage(snapshot.C_img);
                else
                    this.Snapshot = null;
            });
        }

        protected System.Drawing.Image ByteToImage(byte[] src)
        {
            if (src.Length > 0)
            {
                try
                {
                    MemoryStream mStream = new MemoryStream();
                    byte[] pData = src;
                    mStream.Write(pData, 0, Convert.ToInt32(pData.Length));
                    Bitmap bm = new Bitmap(mStream, false);
                    mStream.Dispose();
                    return bm;
                }
                catch (Exception) { }
            }
            return null;
        }

        #region Snapshot
        private System.Drawing.Image _snapshot;
        public System.Drawing.Image Snapshot
        {
            get { return this._snapshot; }
            protected set { this.Set(ref _snapshot, value); }
        }
        #endregion

        #region OpenDoorEvents
        private ListCollectionView _openDoorEvents;
        public ListCollectionView OpenDoorEvents
        {
            get { return _openDoorEvents; }
            private set
            {
                this.Set(ref _openDoorEvents, value);
            }
        }
        #endregion

        #region RefreshCommand
        private ICommand _refreshCommand;
        public ICommand RefreshCommand
        {
            get
            {
                return _refreshCommand ?? (_refreshCommand = AsyncCommand.Create(_dataModel.RefillDataAsync));
            }
        }
        #endregion

        #region DeleteOpenDoorEventsCommand
        private IAsyncCommand _deleteOpenDoorEventsCommand;
        public IAsyncCommand DeleteOpenDoorEventsCommand
        {
            get
            {
                return _deleteOpenDoorEventsCommand ?? (_deleteOpenDoorEventsCommand = new AsyncCommand<IList, object>(
                    async (openDoorEvents, _) => { await _dataModel.DeleteAsync(openDoorEvents as IList); return null; },
                    (openDoorEvents) => { return (openDoorEvents != null) && (openDoorEvents.Count > 0); }));
            }
        }
        #endregion

        #region PrintCommand
        private RelayCommand<object> _printCommand;
        public RelayCommand<object> PrintCommand
        {
            get
            {
                return _printCommand ?? (_printCommand = new RelayCommand<object>(
                    (grid) => { _printService.Print(grid as DataGrid); }));
            }
        }
        #endregion

        #region PickLobbyPhoneAddressFilterValueCommand
        private RelayCommand _PickLobbyPhoneAddressFilterValueCommand;
        public RelayCommand PickLobbyPhoneAddressFilterValueCommand
        {
            get
            {
                return _PickLobbyPhoneAddressFilterValueCommand ?? (_PickLobbyPhoneAddressFilterValueCommand = new RelayCommand(() =>
                {
                    this._dialogService.ShowSelectRoomAddressDialog(
                        this.LobbyPhoneAddressFilterValue,
                        (roomAddress) => { this.LobbyPhoneAddressFilterValue = roomAddress; });
                }));
            }
        }
        #endregion

        #region BeginDateFilter
        #region RemoveBeginDateFilterCommand
        private ICommand _removeBeginDateFilterCommand;
        public ICommand RemoveBeginDateFilterCommand
        {
            get
            {
                return _removeBeginDateFilterCommand ?? (_removeBeginDateFilterCommand = new AsyncCommand<object>(
                    async (param, _) => { await RemoveBeginDateFilterAsync(); return null; },
                    () => CanRemoveBeginDateFilter));
            }
        }

        private bool _canRemoveBeginDateFilter;
        public bool CanRemoveBeginDateFilter
        {
            get { return _canRemoveBeginDateFilter; }
            private set { this.Set(ref _canRemoveBeginDateFilter, value); }
        }
        #endregion

        private DateTime? _BeginDateFilterValue;
        public DateTime? BeginDateFilterValue
        {
            get { return _BeginDateFilterValue; }
            set
            {
                if (this.Set(ref _BeginDateFilterValue, value))
                {
                    ApplyFilter(_BeginDateFilterValue.HasValue ? FilterField.BeginDate : FilterField.None);
                }
            }
        }

        Predicate<object> _BeginDateFilter;
        Predicate<object> BeginDateFilter
        {
            get { return _BeginDateFilter ?? (_BeginDateFilter = new Predicate<object>(FilterByBeginDate)); }
        }

        private bool FilterByBeginDate(object obj)
        {
            eventopendoor @event = obj as eventopendoor;
            if (@event != null && (@event.C_time.Date >= BeginDateFilterValue.Value.Date))
            {
                return true;
            }
            return false;
        }

        private async Task RemoveBeginDateFilterAsync()
        {
            await Task.Run(() =>
            {
                filters.RemoveFilter(BeginDateFilter);
                BeginDateFilterValue = null;
                CanRemoveBeginDateFilter = false;
            });
            OpenDoorEvents.Filter = filters.Filter;
        }

        public void AddBeginDateFilter()
        {
            // see Notes on Adding Filters:
            if (CanRemoveBeginDateFilter)
            {
                filters.RemoveFilter(BeginDateFilter);
                filters.AddFilter(BeginDateFilter);
                OpenDoorEvents.Filter = filters.Filter;
            }
            else
            {
                filters.AddFilter(BeginDateFilter);
                OpenDoorEvents.Filter = filters.Filter;
                CanRemoveBeginDateFilter = true;
            }
        }
        #endregion

        #region EndDateFilter
        #region RemoveEndDateFilterCommand
        private ICommand _removeEndDateFilterCommand;
        public ICommand RemoveEndDateFilterCommand
        {
            get
            {
                return _removeEndDateFilterCommand ?? (_removeEndDateFilterCommand = new AsyncCommand<object>(
                    async (param, _) => { await RemoveEndDateFilterAsync(); return null; },
                    () => CanRemoveEndDateFilter));
            }
        }

        private bool _canRemoveEndDateFilter;
        public bool CanRemoveEndDateFilter
        {
            get { return _canRemoveEndDateFilter; }
            private set { this.Set(ref _canRemoveEndDateFilter, value); }
        }
        #endregion

        private DateTime? _EndDateFilterValue;
        public DateTime? EndDateFilterValue
        {
            get { return _EndDateFilterValue; }
            set
            {
                if (this.Set(ref _EndDateFilterValue, value))
                {
                    ApplyFilter(_EndDateFilterValue.HasValue ? FilterField.EndDate : FilterField.None);
                }
            }
        }

        Predicate<object> _EndDateFilter;
        Predicate<object> EndDateFilter
        {
            get { return _EndDateFilter ?? (_EndDateFilter = new Predicate<object>(FilterByEndDate)); }
        }

        private bool FilterByEndDate(object obj)
        {
            eventopendoor @event = obj as eventopendoor;
            if (@event != null && (@event.C_time.Date <= EndDateFilterValue.Value.Date))
            {
                return true;
            }
            return false;
        }

        private async Task RemoveEndDateFilterAsync()
        {
            await Task.Run(() =>
            {
                filters.RemoveFilter(EndDateFilter);
                EndDateFilterValue = null;
                CanRemoveEndDateFilter = false;
            });
            OpenDoorEvents.Filter = filters.Filter;
        }

        public void AddEndDateFilter()
        {
            // see Notes on Adding Filters:
            if (CanRemoveEndDateFilter)
            {
                filters.RemoveFilter(EndDateFilter);
                filters.AddFilter(EndDateFilter);
                OpenDoorEvents.Filter = filters.Filter;
            }
            else
            {
                filters.AddFilter(EndDateFilter);
                OpenDoorEvents.Filter = filters.Filter;
                CanRemoveEndDateFilter = true;
            }
        }
        #endregion

        #region LobbyPhoneAddressFilter
        #region RemoveLobbyPhoneAddressFilterCommand
        private ICommand _removeLobbyPhoneAddressFilterCommand;
        public ICommand RemoveLobbyPhoneAddressFilterCommand
        {
            get
            {
                return _removeLobbyPhoneAddressFilterCommand ?? (_removeLobbyPhoneAddressFilterCommand = new AsyncCommand<object>(
                    async (param, _) => { await RemoveLobbyPhoneAddressFilterAsync(); return null; },
                    () => CanRemoveLobbyPhoneAddressFilter));
            }
        }

        private bool _canRemoveLobbyPhoneAddressFilter;
        public bool CanRemoveLobbyPhoneAddressFilter
        {
            get { return _canRemoveLobbyPhoneAddressFilter; }
            private set { this.Set(ref _canRemoveLobbyPhoneAddressFilter, value); }
        }
        #endregion

        private string _LobbyPhoneAddressFilterValue;
        public string LobbyPhoneAddressFilterValue
        {
            get { return _LobbyPhoneAddressFilterValue; }
            set
            {
                if (this.Set(ref _LobbyPhoneAddressFilterValue, value))
                {
                    if (!string.IsNullOrWhiteSpace(_LobbyPhoneAddressFilterValue))
                        ApplyFilter(FilterField.LobbyPhoneAddress);
                    else
                        RemoveLobbyPhoneAddressFilterCommand.Execute(null);
                }
            }
        }

        Predicate<object> _LobbyPhoneAddressFilter;
        Predicate<object> LobbyPhoneAddressFilter
        {
            get { return _LobbyPhoneAddressFilter ?? (_LobbyPhoneAddressFilter = new Predicate<object>(FilterByLobbyPhoneAddress)); }
        }

        private bool FilterByLobbyPhoneAddress(object obj)
        {
            eventopendoor @event = obj as eventopendoor;
            if (@event != null && (@event.C_from.StartsWith(this.LobbyPhoneAddressFilterValue)))
            {
                return true;
            }
            return false;
        }

        private async Task RemoveLobbyPhoneAddressFilterAsync()
        {
            await Task.Run(() =>
            {
                filters.RemoveFilter(LobbyPhoneAddressFilter);
                LobbyPhoneAddressFilterValue = null;
                CanRemoveLobbyPhoneAddressFilter = false;
            });
            OpenDoorEvents.Filter = filters.Filter;
        }

        public void AddLobbyPhoneAddressFilter()
        {
            // see Notes on Adding Filters:
            if (CanRemoveLobbyPhoneAddressFilter)
            {
                filters.RemoveFilter(LobbyPhoneAddressFilter);
                filters.AddFilter(LobbyPhoneAddressFilter);
                OpenDoorEvents.Filter = filters.Filter;
            }
            else
            {
                filters.AddFilter(LobbyPhoneAddressFilter);
                OpenDoorEvents.Filter = filters.Filter;
                CanRemoveLobbyPhoneAddressFilter = true;
            }
        }
        #endregion
        
        #region FilterBaseFunctions

        GroupFilter filters = new GroupFilter();

        private enum FilterField
        {
            BeginDate,
            EndDate,
            LobbyPhoneAddress,
            None
        }

        private void ApplyFilter(FilterField field)
        {
            switch (field)
            {
                case FilterField.BeginDate:
                    AddBeginDateFilter();
                    break;

                case FilterField.EndDate:
                    AddEndDateFilter();
                    break;

                case FilterField.LobbyPhoneAddress:
                    AddLobbyPhoneAddressFilter();
                    break;

                default:
                    break;
            }
        }

        public async Task ResetFiltersAsync()
        {
            // clear filters 
            await RemoveBeginDateFilterAsync();
            await RemoveEndDateFilterAsync();
            await RemoveLobbyPhoneAddressFilterAsync();
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
}

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Threading;
using ICMServer.Models;
using ICMServer.Services;
using ICMServer.Services.Data;
using ICMServer.WPF.Models;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using System.Xml.Linq;

namespace ICMServer.WPF.ViewModels
{
    public class SecurityCardsViewModel : ViewModelBase
    {
        private readonly ICollectionModel<iccard> _dataModel;
        private readonly IDeviceDataService _deviceDataService;
        private readonly ISecurityCardsDevicesModel _securityCardsDevicesDataModel;
        private readonly IDialogService _dialogService;

        public SecurityCardsViewModel(
            ICollectionModel<iccard> dataModel,
            IDeviceDataService deviceDataService,
            ISecurityCardsDevicesModel securityCardsDevicesDataModel,
            IDialogService dialogService)
        {
            this._deviceDataService = deviceDataService;
            this._dialogService = dialogService;
            this._dataModel = dataModel;
            this._dataModel.Data.CollectionChanged += _dataCollection_CollectionChanged;
            this._securityCardsDevicesDataModel = securityCardsDevicesDataModel;

            SecurityCards = (ListCollectionView)CollectionViewSource.GetDefaultView((IList)_dataModel.Data);
            SecurityCards.CurrentChanged += SecurityCards_CurrentChanged;
            Devices = (ListCollectionView)CollectionViewSource.GetDefaultView((IList)_securityCardsDevicesDataModel.Data);

            RefreshCommand.Execute(null);
        }

        private void _dataCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                case NotifyCollectionChangedAction.Remove:
                case NotifyCollectionChangedAction.Reset:
                    DispatcherHelper.CheckBeginInvokeOnUI(() =>
                    {
                        ExportCardListCommand.RaiseCanExecuteChanged();
                        //SyncCardListWithDevicesCommand.RaiseCanExecuteChanged();
                    });
                    break;
            }
        }

        private void SecurityCards_CurrentChanged(object sender, EventArgs e)
        {
            this.EditSecurityCardCommand.RaiseCanExecuteChanged();
            this.DeleteSecurityCardsCommand.RaiseCanExecuteChanged();
            this._securityCardsDevicesDataModel.SetSecurityCard(this.SecurityCards.CurrentItem as iccard);
        }

        #region SecurityCards
        private ListCollectionView _securityCards;
        public ListCollectionView SecurityCards
        {
            get { return _securityCards; }
            private set { this.Set(ref _securityCards, value); }
        }
        #endregion

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
                return _refreshCommand ?? (_refreshCommand = AsyncCommand.Create(_dataModel.RefillDataAsync));
            }
        }
        #endregion

        #region AddSecurityCardCommand
        private RelayCommand _addSecurityCardCommand;
        public RelayCommand AddSecurityCardCommand
        {
            get
            {
                return _addSecurityCardCommand ?? (_addSecurityCardCommand = new RelayCommand(
                    () => { this._dialogService.ShowAddSecurityCardDialog(); },
                    () => { return true; }));
            }
        }
        #endregion

        #region EditSecurityCardCommand
        private RelayCommand _editSecurityCardCommand;
        public RelayCommand EditSecurityCardCommand
        {
            get
            {
                return _editSecurityCardCommand ?? (_editSecurityCardCommand = new RelayCommand(
                    () => { this._dialogService.ShowEditSecurityCardDialog(SecurityCards.CurrentItem as iccard); },
                    () => { return (SecurityCards.CurrentItem as iccard) != null; }));
            }
        }
        #endregion

        #region DeleteSecurityCardsCommand
        private IAsyncCommand _deleteSecurityCardsCommand;
        public IAsyncCommand DeleteSecurityCardsCommand
        {
            get
            {
                return _deleteSecurityCardsCommand ?? (_deleteSecurityCardsCommand = new AsyncCommand<IList, object>(
                    async (cards, _) => 
                    {
                        await _dataModel.DeleteAsync(cards as IList);
                        return null;
                    },
                    (cards) => { return (cards != null) && (cards.Count > 0); }));
            }
        }
        #endregion

        #region ImportCardListCommand
        private RelayCommand _importCardListCommand;
        public RelayCommand ImportCardListCommand
        {
            get
            {
                return _importCardListCommand ?? (_importCardListCommand = new RelayCommand(
                    () => 
                    {
                        this._dialogService.ShowImportCardListDialog(
                            () => this._securityCardsDevicesDataModel.SetSecurityCard(this.SecurityCards.CurrentItem as iccard));
                    }));
            }
        }
        #endregion

        #region ExportCardListCommand
        private RelayCommand _exportCardListCommand;
        public RelayCommand ExportCardListCommand
        {
            get
            {
                return _exportCardListCommand ?? (_exportCardListCommand = new RelayCommand(
                    () => { _dialogService.ShowExportCardListDialog(); },
                    () => { return _dataModel.Data.Count > 0; }));
            }
        }
        #endregion

        //#region SyncCardListWithDevicesCommand
        //private RelayCommand _syncCardListWithDevicesCommand;
        //public RelayCommand SyncCardListWithDevicesCommand
        //{
        //    get
        //    {
        //        return _syncCardListWithDevicesCommand ?? (_syncCardListWithDevicesCommand = new RelayCommand(
        //            () => { this._dialogService.ShowSelectDevicesDialog(); },
        //            () => { return _dataModel.Data.Count > 0; }));
        //    }
        //}
        //#endregion

        #region PickRoomAddressFilterValueCommand
        private RelayCommand _PickRoomAddressFilterValueCommand;
        public RelayCommand PickRoomAddressFilterValueCommand
        {
            get
            {
                return _PickRoomAddressFilterValueCommand ?? (_PickRoomAddressFilterValueCommand = new RelayCommand(() =>
                {
                    this._dialogService.ShowSelectRoomAddressDialog(
                        this.RoomAddressFilterValue,
                        (roomAddress) => { this.RoomAddressFilterValue = roomAddress; });
                }));
            }
        }
        #endregion
        
        #region CardNumberFilter
        private bool FilterByCardNumber(object obj)
        {
            iccard card = obj as iccard;
            if (card != null && card.C_icno.Contains(CardNumberFilterValue))
            {
                return true;
            }
            return false;
        }

        private string _CardNumberFilterValue;
        public string CardNumberFilterValue
        {
            get { return _CardNumberFilterValue; }
            set
            {
                if (this.Set(ref _CardNumberFilterValue, value))
                {
                    if (!string.IsNullOrWhiteSpace(_CardNumberFilterValue))
                        ApplyFilter(FilterField.CardNumber);
                    else
                        RemoveCardNumberFilterCommand.Execute(null);
                }
            }
        }
        
        #region RemoveCardNumberFilterCommand
        private ICommand _removeCardNumberFilterCommand;
        public ICommand RemoveCardNumberFilterCommand
        {
            get
            {
                return _removeCardNumberFilterCommand ?? (_removeCardNumberFilterCommand = new AsyncCommand<object>(
                    async (param, _) => { await RemoveCardNumberFilterAsync(); return null; },
                    () => CanRemoveCardNumberFilter));
            }
        }

        private bool _canRemoveCardNumberFilter;
        public bool CanRemoveCardNumberFilter
        {
            get { return _canRemoveCardNumberFilter; }
            private set { this.Set(ref _canRemoveCardNumberFilter, value); }
        }
        #endregion

        Predicate<object> _CardNumberFilter;
        Predicate<object> CardNumberFilter
        {
            get { return _CardNumberFilter ?? (_CardNumberFilter = new Predicate<object>(FilterByCardNumber)); }
        }

        private async Task RemoveCardNumberFilterAsync()
        {
            await Task.Run(() =>
            {
                filters.RemoveFilter(CardNumberFilter);
                CardNumberFilterValue = null;
                CanRemoveCardNumberFilter = false;
            });
            SecurityCards.Filter = filters.Filter;
        }

        public void AddCardNumberFilter()
        {
            // see Notes on Adding Filters:
            if (CanRemoveCardNumberFilter)
            {
                filters.RemoveFilter(CardNumberFilter);
                filters.AddFilter(CardNumberFilter);
                SecurityCards.Filter = filters.Filter;
            }
            else
            {
                filters.AddFilter(CardNumberFilter);
                SecurityCards.Filter = filters.Filter;
                CanRemoveCardNumberFilter = true;
            }
        }
        #endregion

        #region NameFilter
        private bool FilterByName(object obj)
        {
            iccard card = obj as iccard;
            if (card != null && card.C_username != null && card.C_username.Contains(NameFilterValue))
            {
                return true;
            }
            return false;
        }

        private string _NameFilterValue;
        public string NameFilterValue
        {
            get { return _NameFilterValue; }
            set
            {
                if (this.Set(ref _NameFilterValue, value))
                {
                    if (!string.IsNullOrWhiteSpace(_NameFilterValue))
                        ApplyFilter(FilterField.Name);
                    else
                        RemoveNameFilterCommand.Execute(null);
                }
            }
        }
        
        //public async void RemoveNameFilterAsync_()
        //{
        //    await RemoveNameFilterAsync();
        //}

        #region RemoveNameFilterCommand
        private ICommand _removeNameFilterCommand;
        public ICommand RemoveNameFilterCommand
        {
            get
            {
                return _removeNameFilterCommand ?? (_removeNameFilterCommand = new AsyncCommand<object>(
                    async (param, _) => { await RemoveNameFilterAsync(); return null; },
                    () => CanRemoveNameFilter));
            }
        }

        private bool _canRemoveNameFilter;
        public bool CanRemoveNameFilter
        {
            get { return _canRemoveNameFilter; }
            private set { this.Set(ref _canRemoveNameFilter, value); }
        }
        #endregion

        Predicate<object> _NameFilter;
        Predicate<object> NameFilter
        {
            get { return _NameFilter ?? (_NameFilter = new Predicate<object>(FilterByName)); }
        }

        private async Task RemoveNameFilterAsync()
        {
            await Task.Run(() =>
            {
                filters.RemoveFilter(NameFilter);
                NameFilterValue = null;
                CanRemoveNameFilter = false;
            });
            SecurityCards.Filter = filters.Filter;
        }

        public void AddNameFilter()
        {
            // see Notes on Adding Filters:
            if (CanRemoveNameFilter)
            {
                filters.RemoveFilter(NameFilter);
                filters.AddFilter(NameFilter);
                SecurityCards.Filter = filters.Filter;
            }
            else
            {
                filters.AddFilter(NameFilter);
                SecurityCards.Filter = filters.Filter;
                CanRemoveNameFilter = true;
            }
        }
        #endregion
        
        #region RoomAddressFilter
        private bool FilterByRoomAddress(object obj)
        {
            iccard card = obj as iccard;
            if (card != null && card.C_roomid.StartsWith(RoomAddressFilterValue))
            {
                return true;
            }
            return false;
        }

        private string _RoomAddressFilterValue;
        public string RoomAddressFilterValue
        {
            get { return _RoomAddressFilterValue; }
            set
            {
                if (this.Set(ref _RoomAddressFilterValue, value))
                {
                    if (!string.IsNullOrWhiteSpace(_RoomAddressFilterValue))
                        ApplyFilter(FilterField.RoomAddress);
                    else
                        RemoveRoomAddressFilterCommand.Execute(null);
                }
            }
        }

        //public async void RemoveRoomAddressFilterAsync_()
        //{
        //    await RemoveRoomAddressFilterAsync();
        //}
        
        #region RemoveRoomAddressFilterCommand
        private ICommand _removeRoomAddressFilterCommand;
        public ICommand RemoveRoomAddressFilterCommand
        {
            get
            {
                return _removeRoomAddressFilterCommand ?? (_removeRoomAddressFilterCommand = new AsyncCommand<object>(
                    async (param, _) => { await RemoveRoomAddressFilterAsync(); return null; },
                    () => CanRemoveRoomAddressFilter));
            }
        }

        private bool _canRemoveRoomAddressFilter;
        public bool CanRemoveRoomAddressFilter
        {
            get { return _canRemoveRoomAddressFilter; }
            private set { this.Set(ref _canRemoveRoomAddressFilter, value); }
        }
        #endregion

        Predicate<object> _RoomAddressFilter;
        Predicate<object> RoomAddressFilter
        {
            get { return _RoomAddressFilter ?? (_RoomAddressFilter = new Predicate<object>(FilterByRoomAddress)); }
        }

        private async Task RemoveRoomAddressFilterAsync()
        {
            await Task.Run(() =>
            {
                filters.RemoveFilter(RoomAddressFilter);
                RoomAddressFilterValue = null;
                CanRemoveRoomAddressFilter = false;
            });
            SecurityCards.Filter = filters.Filter;
        }

        public void AddRoomAddressFilter()
        {
            // see Notes on Adding Filters:
            if (CanRemoveRoomAddressFilter)
            {
                filters.RemoveFilter(RoomAddressFilter);
                filters.AddFilter(RoomAddressFilter);
                SecurityCards.Filter = filters.Filter;
            }
            else
            {
                filters.AddFilter(RoomAddressFilter);
                SecurityCards.Filter = filters.Filter;
                CanRemoveRoomAddressFilter = true;
            }
        }
        #endregion

        #region FilterBaseFunctions
        GroupFilter filters = new GroupFilter();

        private enum FilterField
        {
            CardNumber,
            Name,
            RoomAddress,
            None
        }

        private void ApplyFilter(FilterField field)
        {
            switch (field)
            {
                case FilterField.CardNumber:
                    AddCardNumberFilter();
                    break;

                case FilterField.Name:
                    AddNameFilter();
                    break;

                case FilterField.RoomAddress:
                    AddRoomAddressFilter();
                    break;

                default:
                    break;
            }
        }

        public async Task ResetFiltersAsync()
        {
            // clear filters
            await RemoveCardNumberFilterAsync();
            await RemoveNameFilterAsync();
            await RemoveRoomAddressFilterAsync();
        }

        #region ResetFiltersCommand
        private ICommand _resetFiltersCommand;
        public ICommand ResetFiltersCommand
        {
            get
            {
                return _resetFiltersCommand ?? (_resetFiltersCommand = new AsyncCommand<object>(
                    async (param, _) => { await ResetFiltersAsync(); return null; }));
            }
        }
        #endregion
        #endregion
    }
}

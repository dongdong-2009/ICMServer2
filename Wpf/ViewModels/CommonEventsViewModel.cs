using GalaSoft.MvvmLight.CommandWpf;
using ICMServer.Models;
using ICMServer.Services;
using ICMServer.WPF.Models;
using System;
using System.Collections;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace ICMServer.WPF.ViewModels
{
    public class CommonEventsViewModel : ValidatableViewModelBase
    {
        private readonly ICollectionModel<eventcommon> _dataModel;
        private readonly IDialogService _dialogService;
        private readonly IPrintService _printService;

        public CommonEventsViewModel(
            ICollectionModel<eventcommon> dataModel,
            IDialogService dialogService,
            IPrintService printService)
        {
            this._dialogService = dialogService;
            this._dataModel = dataModel;
            this._printService = printService;
            CommonEvents = (ListCollectionView)CollectionViewSource.GetDefaultView((IList)_dataModel.Data);
            CommonEvents.CurrentChanged += CommonEvents_CurrentChanged;

            RefreshCommand.Execute(null);
        }

        private void CommonEvents_CurrentChanged(object sender, EventArgs e)
        {
            this.DeleteCommonEventsCommand.RaiseCanExecuteChanged();
        }

        #region CommonEvents
        private ListCollectionView _commonEvents;
        public ListCollectionView CommonEvents
        {
            get { return _commonEvents; }
            private set
            {
                this.Set(ref _commonEvents, value);
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

        #region EditCommonEventCommand
        private RelayCommand _editCommonEventCommand;
        public RelayCommand EditCommonEventCommand
        {
            get
            {
                return _editCommonEventCommand ?? (_editCommonEventCommand = new RelayCommand(
                        () => { this._dialogService.ShowEditCommonEventDialog(CommonEvents.CurrentItem as eventcommon); },
                        () => { return (CommonEvents.CurrentItem as eventcommon) != null; }));
            }
        }
        #endregion

        #region DeleteCommonEventsCommand
        private IAsyncCommand _deleteCommonEventsCommand;
        public IAsyncCommand DeleteCommonEventsCommand
        {
            get
            {
                return _deleteCommonEventsCommand ?? (_deleteCommonEventsCommand = new AsyncCommand<IList, object>(
                    async (commonEvents, _) => { await _dataModel.DeleteAsync(commonEvents as IList); return null; },
                    (commonEvents) => { return (commonEvents != null) && (commonEvents.Count > 0); }));
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

        #region PickAddressFilterValueCommand
        private RelayCommand _PickAddressFilterValueCommand;
        public RelayCommand PickAddressFilterValueCommand
        {
            get
            {
                return _PickAddressFilterValueCommand ?? (_PickAddressFilterValueCommand = new RelayCommand(() =>
                {
                    this._dialogService.ShowSelectRoomAddressDialog(
                        this.AddressFilterValue,
                        (roomAddress) => { this.AddressFilterValue = roomAddress; });
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
            eventcommon commonEvent = obj as eventcommon;
            if (commonEvent != null && (commonEvent.time.Date >= BeginDateFilterValue.Value.Date))
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
            CommonEvents.Filter = filters.Filter;
        }

        public void AddBeginDateFilter()
        {
            // see Notes on Adding Filters:
            if (CanRemoveBeginDateFilter)
            {
                filters.RemoveFilter(BeginDateFilter);
                filters.AddFilter(BeginDateFilter);
                CommonEvents.Filter = filters.Filter;
            }
            else
            {
                filters.AddFilter(BeginDateFilter);
                CommonEvents.Filter = filters.Filter;
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
            eventcommon commonEvent = obj as eventcommon;
            if (commonEvent != null && (commonEvent.time.Date <= EndDateFilterValue.Value.Date))
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
            CommonEvents.Filter = filters.Filter;
        }

        public void AddEndDateFilter()
        {
            // see Notes on Adding Filters:
            if (CanRemoveEndDateFilter)
            {
                filters.RemoveFilter(EndDateFilter);
                filters.AddFilter(EndDateFilter);
                CommonEvents.Filter = filters.Filter;
            }
            else
            {
                filters.AddFilter(EndDateFilter);
                CommonEvents.Filter = filters.Filter;
                CanRemoveEndDateFilter = true;
            }
        }
        #endregion
        
        #region ProcessStateFilter
        #region RemoveProcessStateFilterCommand
        private ICommand _removeProcessStateFilterCommand;
        public ICommand RemoveProcessStateFilterCommand
        {
            get
            {
                return _removeProcessStateFilterCommand ?? (_removeProcessStateFilterCommand = new AsyncCommand<object>(
                    async (param, _) => { await RemoveProcessStateFilterAsync(); return null; },
                    () => CanRemoveProcessStateFilter));
            }
        }

        private bool _canRemoveProcessStateFilter;
        public bool CanRemoveProcessStateFilter
        {
            get { return _canRemoveProcessStateFilter; }
            private set { this.Set(ref _canRemoveProcessStateFilter, value); }
        }
        #endregion

        private int? _ProcessStateFilterValue;
        public int? ProcessStateFilterValue
        {
            get { return _ProcessStateFilterValue; }
            set
            {
                if (this.Set(ref _ProcessStateFilterValue, value))
                {
                    ApplyFilter(_ProcessStateFilterValue.HasValue ? FilterField.ProcessState : FilterField.None);
                }
            }
        }

        Predicate<object> _ProcessStateFilter;
        Predicate<object> ProcessStateFilter
        {
            get { return _ProcessStateFilter ?? (_ProcessStateFilter = new Predicate<object>(FilterByProcessState)); }
        }

        private bool FilterByProcessState(object obj)
        {
            eventcommon commonEvent = obj as eventcommon;
            if (commonEvent != null && (commonEvent.handlestatus == ProcessStateFilterValue))
            {
                return true;
            }
            return false;
        }

        private async Task RemoveProcessStateFilterAsync()
        {
            await Task.Run(() =>
            {
                filters.RemoveFilter(ProcessStateFilter);
                ProcessStateFilterValue = null;
                CanRemoveProcessStateFilter = false;
            });
            CommonEvents.Filter = filters.Filter;
        }

        public void AddProcessStateFilter()
        {
            // see Notes on Adding Filters:
            if (CanRemoveProcessStateFilter)
            {
                filters.RemoveFilter(ProcessStateFilter);
                filters.AddFilter(ProcessStateFilter);
                CommonEvents.Filter = filters.Filter;
            }
            else
            {
                filters.AddFilter(ProcessStateFilter);
                CommonEvents.Filter = filters.Filter;
                CanRemoveProcessStateFilter = true;
            }
        }
        #endregion
        
        #region AddressFilter
        #region RemoveAddressFilterCommand
        private ICommand _removeAddressFilterCommand;
        public ICommand RemoveAddressFilterCommand
        {
            get
            {
                return _removeAddressFilterCommand ?? (_removeAddressFilterCommand = new AsyncCommand<object>(
                    async (param, _) => { await RemoveAddressFilterAsync(); return null; },
                    () => CanRemoveAddressFilter));
            }
        }

        private bool _canRemoveAddressFilter;
        public bool CanRemoveAddressFilter
        {
            get { return _canRemoveAddressFilter; }
            private set { this.Set(ref _canRemoveAddressFilter, value); }
        }
        #endregion

        private string _AddressFilterValue;
        public string AddressFilterValue
        {
            get { return _AddressFilterValue; }
            set
            {
                if (this.Set(ref _AddressFilterValue, value))
                {
                    if (!string.IsNullOrWhiteSpace(_AddressFilterValue))
                        ApplyFilter(FilterField.Address);
                    else
                        RemoveAddressFilterCommand.Execute(null);
                }
            }
        }

        Predicate<object> _AddressFilter;
        Predicate<object> AddressFilter
        {
            get { return _AddressFilter ?? (_AddressFilter = new Predicate<object>(FilterByAddress)); }
        }

        private bool FilterByAddress(object obj)
        {
            eventcommon commonEvent = obj as eventcommon;
            if (commonEvent != null && commonEvent.srcaddr.StartsWith(AddressFilterValue))
            {
                return true;
            }
            return false;
        }

        private async Task RemoveAddressFilterAsync()
        {
            await Task.Run(() =>
            {
                filters.RemoveFilter(AddressFilter);
                AddressFilterValue = null;
                CanRemoveAddressFilter = false;
            });
            CommonEvents.Filter = filters.Filter;
        }

        public void AddAddressFilter()
        {
            // see Notes on Adding Filters:
            if (CanRemoveAddressFilter)
            {
                filters.RemoveFilter(AddressFilter);
                filters.AddFilter(AddressFilter);
                CommonEvents.Filter = filters.Filter;
            }
            else
            {
                filters.AddFilter(AddressFilter);
                CommonEvents.Filter = filters.Filter;
                CanRemoveAddressFilter = true;
            }
        }
        #endregion
        
        #region FilterBaseFunctions

        GroupFilter filters = new GroupFilter();

        private enum FilterField
        {
            BeginDate,
            EndDate,
            ProcessState,
            Address,
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

                case FilterField.ProcessState:
                    AddProcessStateFilter();
                    break;

                case FilterField.Address:
                    AddAddressFilter();
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
            await RemoveProcessStateFilterAsync();
            await RemoveAddressFilterAsync();
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

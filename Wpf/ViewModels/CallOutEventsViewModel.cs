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
    public class CallOutEventsViewModel : ValidatableViewModelBase
    {
        private readonly ICollectionModel<eventcallout> _dataModel;
        private readonly IDialogService _dialogService;
        private readonly IPrintService _printService;
        // 請使用 ObservableRangeCollection 來取代 ObservableCollection，
        // 主要是在實作refresh data 這功能時，使用此類別的ReplaceRange method可以避免DataGrid 跳動
        //private ObservableRangeCollection<Device> _devicesCollection;
        //private static object _lock = new object();

        // TODO: BUG...DateTime always 以 en-US 呈現
        public CallOutEventsViewModel(
            ICollectionModel<eventcallout> dataModel,
            IDialogService dialogService,
            IPrintService printService)
        {
            this._dataModel = dataModel;
            this._dialogService = dialogService;
            this._printService = printService;
            CallOutEvents = (ListCollectionView)CollectionViewSource.GetDefaultView((IList)_dataModel.Data);
            CallOutEvents.CurrentChanged += CallOutEvents_CurrentChanged;

            RefreshCommand.Execute(null);
        }

        private void CallOutEvents_CurrentChanged(object sender, EventArgs e)
        {
            this.DeleteCallOutEventsCommand.RaiseCanExecuteChanged();
        }

        #region CallOutEvents
        private ListCollectionView _callOutEvents;
        public ListCollectionView CallOutEvents
        {
            get { return _callOutEvents; }
            private set
            {
                this.Set(ref _callOutEvents, value);
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

        #region DeleteCallOutEventsCommand
        private IAsyncCommand _deleteCallOutEventsCommand;
        public IAsyncCommand DeleteCallOutEventsCommand
        {
            get
            {
                return _deleteCallOutEventsCommand ?? (_deleteCallOutEventsCommand = new AsyncCommand<IList, object>(
                    async (callOutEvents, _) => { await _dataModel.DeleteAsync(callOutEvents as IList); return null; },
                    (callOutEvents) => { return (callOutEvents != null) && (callOutEvents.Count > 0); }));
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

        #region PickSrcAddressFilterValueCommand
        private RelayCommand _PickSrcAddressFilterValueCommand;
        public RelayCommand PickSrcAddressFilterValueCommand
        {
            get
            {
                return _PickSrcAddressFilterValueCommand ?? (_PickSrcAddressFilterValueCommand = new RelayCommand(() =>
                {
                    this._dialogService.ShowSelectRoomAddressDialog(
                        this.SrcAddressFilterValue,
                        (roomAddress) => { this.SrcAddressFilterValue = roomAddress; });
                }));
            }
        }
        #endregion

        #region PickDstAddressFilterValueCommand
        private RelayCommand _PickDstAddressFilterValueCommand;
        public RelayCommand PickDstAddressFilterValueCommand
        {
            get
            {
                return _PickDstAddressFilterValueCommand ?? (_PickDstAddressFilterValueCommand = new RelayCommand(() =>
                {
                    this._dialogService.ShowSelectRoomAddressDialog(
                        this.DstAddressFilterValue,
                        (roomAddress) => { this.DstAddressFilterValue = roomAddress; });
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
            eventcallout @event = obj as eventcallout;
            if (@event != null && (@event.time.Date >= BeginDateFilterValue.Value.Date))
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
            CallOutEvents.Filter = filters.Filter;
        }

        public void AddBeginDateFilter()
        {
            // see Notes on Adding Filters:
            if (CanRemoveBeginDateFilter)
            {
                filters.RemoveFilter(BeginDateFilter);
                filters.AddFilter(BeginDateFilter);
                CallOutEvents.Filter = filters.Filter;
            }
            else
            {
                filters.AddFilter(BeginDateFilter);
                CallOutEvents.Filter = filters.Filter;
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
            eventcallout @event = obj as eventcallout;
            if (@event != null && (@event.time.Date <= EndDateFilterValue.Value.Date))
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
            CallOutEvents.Filter = filters.Filter;
        }

        public void AddEndDateFilter()
        {
            // see Notes on Adding Filters:
            if (CanRemoveEndDateFilter)
            {
                filters.RemoveFilter(EndDateFilter);
                filters.AddFilter(EndDateFilter);
                CallOutEvents.Filter = filters.Filter;
            }
            else
            {
                filters.AddFilter(EndDateFilter);
                CallOutEvents.Filter = filters.Filter;
                CanRemoveEndDateFilter = true;
            }
        }
        #endregion
        
        #region SrcAddressFilter
        #region RemoveSrcAddressFilterCommand
        private ICommand _removeSrcAddressFilterCommand;
        public ICommand RemoveSrcAddressFilterCommand
        {
            get
            {
                return _removeSrcAddressFilterCommand ?? (_removeSrcAddressFilterCommand = new AsyncCommand<object>(
                    async (param, _) => { await RemoveSrcAddressFilterAsync(); return null; },
                    () => CanRemoveSrcAddressFilter));
            }
        }

        private bool _canRemoveSrcAddressFilter;
        public bool CanRemoveSrcAddressFilter
        {
            get { return _canRemoveSrcAddressFilter; }
            private set { this.Set(ref _canRemoveSrcAddressFilter, value); }
        }
        #endregion

        private string _SrcAddressFilterValue;
        public string SrcAddressFilterValue
        {
            get { return _SrcAddressFilterValue; }
            set
            {
                if (this.Set(ref _SrcAddressFilterValue, value))
                {
                    if (!string.IsNullOrWhiteSpace(_SrcAddressFilterValue))
                        ApplyFilter(FilterField.SrcAddress);
                    else
                        RemoveSrcAddressFilterCommand.Execute(null);
                }
            }
        }

        Predicate<object> _SrcAddressFilter;
        Predicate<object> SrcAddressFilter
        {
            get { return _SrcAddressFilter ?? (_SrcAddressFilter = new Predicate<object>(FilterBySrcAddress)); }
        }

        private bool FilterBySrcAddress(object obj)
        {
            eventcallout callOutEvent = obj as eventcallout;
            if (callOutEvent != null && (callOutEvent.from.StartsWith(this.SrcAddressFilterValue)))
            {
                return true;
            }
            return false;
        }

        private async Task RemoveSrcAddressFilterAsync()
        {
            await Task.Run(() =>
            {
                filters.RemoveFilter(SrcAddressFilter);
                SrcAddressFilterValue = null;
                CanRemoveSrcAddressFilter = false;
            });
            CallOutEvents.Filter = filters.Filter;
        }

        public void AddSrcAddressFilter()
        {
            // see Notes on Adding Filters:
            if (CanRemoveSrcAddressFilter)
            {
                filters.RemoveFilter(SrcAddressFilter);
                filters.AddFilter(SrcAddressFilter);
                CallOutEvents.Filter = filters.Filter;
            }
            else
            {
                filters.AddFilter(SrcAddressFilter);
                CallOutEvents.Filter = filters.Filter;
                CanRemoveSrcAddressFilter = true;
            }
        }
        #endregion
        
        #region DstAddressFilter
        #region RemoveDstAddressFilterCommand
        private ICommand _removeDstAddressFilterCommand;
        public ICommand RemoveDstAddressFilterCommand
        {
            get
            {
                return _removeDstAddressFilterCommand ?? (_removeDstAddressFilterCommand = new AsyncCommand<object>(
                    async (param, _) => { await RemoveDstAddressFilterAsync(); return null; },
                    () => CanRemoveDstAddressFilter));
            }
        }

        private bool _canRemoveDstAddressFilter;
        public bool CanRemoveDstAddressFilter
        {
            get { return _canRemoveDstAddressFilter; }
            private set { this.Set(ref _canRemoveDstAddressFilter, value); }
        }
        #endregion

        private string _DstAddressFilterValue;
        public string DstAddressFilterValue
        {
            get { return _DstAddressFilterValue; }
            set
            {
                if (this.Set(ref _DstAddressFilterValue, value))
                {
                    if (!string.IsNullOrWhiteSpace(_DstAddressFilterValue))
                        ApplyFilter(FilterField.DstAddress);
                    else
                        RemoveDstAddressFilterCommand.Execute(null);
                }
            }
        }

        Predicate<object> _DstAddressFilter;
        Predicate<object> DstAddressFilter
        {
            get { return _DstAddressFilter ?? (_DstAddressFilter = new Predicate<object>(FilterByDstAddress)); }
        }

        private bool FilterByDstAddress(object obj)
        {
            eventcallout callOutEvent = obj as eventcallout;
            if (callOutEvent != null && (callOutEvent.to.StartsWith(this.DstAddressFilterValue)))
            {
                return true;
            }
            return false;
        }

        private async Task RemoveDstAddressFilterAsync()
        {
            await Task.Run(() =>
            {
                filters.RemoveFilter(DstAddressFilter);
                DstAddressFilterValue = null;
                CanRemoveDstAddressFilter = false;
            });
            CallOutEvents.Filter = filters.Filter;
        }

        public void AddDstAddressFilter()
        {
            // see Notes on Adding Filters:
            if (CanRemoveDstAddressFilter)
            {
                filters.RemoveFilter(DstAddressFilter);
                filters.AddFilter(DstAddressFilter);
                CallOutEvents.Filter = filters.Filter;
            }
            else
            {
                filters.AddFilter(DstAddressFilter);
                CallOutEvents.Filter = filters.Filter;
                CanRemoveDstAddressFilter = true;
            }
        }
        #endregion

        #region FilterBaseFunctions

        GroupFilter filters = new GroupFilter();

        private enum FilterField
        {
            BeginDate,
            EndDate,
            SrcAddress,
            DstAddress,
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

                case FilterField.SrcAddress:
                    AddSrcAddressFilter();
                    break;

                case FilterField.DstAddress:
                    AddDstAddressFilter();
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
            await RemoveSrcAddressFilterAsync();
            await RemoveDstAddressFilterAsync();
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

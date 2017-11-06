using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using ICMServer.Models;
using ICMServer.Services;
using ICMServer.Services.Data;
using ICMServer.WPF.Messages;
using ICMServer.WPF.Models;
using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace ICMServer.WPF.ViewModels
{
    public class AlarmsViewModel : ValidatableViewModelBase
    {
        private readonly ICollectionModel<eventwarn> _dataModel;
        private readonly IDeviceDataService _deviceDataService;
        private readonly IDialogService _dialogService;
        private readonly IPrintService _printService;
        // 請使用 ObservableRangeCollection 來取代 ObservableCollection，
        // 主要是在實作refresh data 這功能時，使用此類別的ReplaceRange method可以避免DataGrid 跳動
        //private ObservableRangeCollection<Device> _devicesCollection;
        //private static object _lock = new object();

        // TODO: BUG...DateTime always 以 en-US 呈現
        public AlarmsViewModel(
            ICollectionModel<eventwarn> dataModel,
            IDeviceDataService deviceDataService,
            IDialogService dialogService,
            IPrintService printService)
        {
            this._dialogService = dialogService;
            this._deviceDataService = deviceDataService;
            this._dataModel = dataModel;
            this._printService = printService;
            //this._dataModel.Data.CollectionChanged += _dataCollection_CollectionChanged;
            Alarms = (ListCollectionView)CollectionViewSource.GetDefaultView((IList)_dataModel.Data);
            Alarms.CurrentChanged += Alarms_CurrentChanged;
            Messenger.Default.Register<ReceivedAlarm>(this, (msg) =>
            {
                if (msg.Alarms != null)
                {
                    var seriousAlarmEvents = (from alarm in msg.Alarms
                                              where alarm.action == "trig"
                                              select alarm).ToList();
                    if (seriousAlarmEvents.Count() > 0)
                        this._dialogService.ShowAlarmDialog(/*seriousAlarmEvents*/);
                }
            });

            RefreshCommand.Execute(null);
        }

        private void Alarms_CurrentChanged(object sender, EventArgs e)
        {
            this.DeleteAlarmsCommand.RaiseCanExecuteChanged();
            this.EditAlarmCommand.RaiseCanExecuteChanged();
            this.DisableSafetyCommand.RaiseCanExecuteChanged();
        }

        #region Alarms
        private ListCollectionView _alarms;
        public ListCollectionView Alarms
        {
            get { return _alarms; }
            private set { this.Set(ref _alarms, value); }
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

        #region EditAlarmCommand
        private RelayCommand _editAlarmCommand;
        public RelayCommand EditAlarmCommand
        {
            get
            {
                return _editAlarmCommand ?? (_editAlarmCommand = new RelayCommand(
                        () => { this._dialogService.ShowEditAlarmDialog(Alarms.CurrentItem as eventwarn); },
                        () => { return (Alarms.CurrentItem as eventwarn) != null; }));
            }
        }
        #endregion

        #region DeleteAlarmsCommand
        private IAsyncCommand _deleteAlarmsCommand;
        public IAsyncCommand DeleteAlarmsCommand
        {
            get
            {
                return _deleteAlarmsCommand ?? (_deleteAlarmsCommand = new AsyncCommand<IList, object>(
                    async (advertisements, _) => { await _dataModel.DeleteAsync(advertisements as IList); return null; },
                    (advertisements) => { return (advertisements != null) && (advertisements.Count > 0); }));
            }
        }
        #endregion

        #region DisableSafetyCommand
        private IAsyncCommand _DisableSafetyCommand;
        public IAsyncCommand DisableSafetyCommand
        {
            get
            {
                return _DisableSafetyCommand ?? (_DisableSafetyCommand = AsyncCommand.Create(
                    async () =>
                    {
                        eventwarn alarm = Alarms.CurrentItem as eventwarn;
                        if (alarm != null)
                        {
                            var device = (await _deviceDataService.SelectAsync((d) => d.roomid == alarm.srcaddr).ConfigureAwait(false)).FirstOrDefault();
                            if (device != null)
                                await ICMServer.Net.HttpClient.SendDisableSaftiesAsync(device.ip, device.roomid).ConfigureAwait(false);
                        }
                    },
                    () => { return (Alarms.CurrentItem as eventwarn) != null; }));
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
            eventwarn alarm = obj as eventwarn;
            if (alarm != null && (alarm.time.Date >= BeginDateFilterValue.Value.Date))
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
            Alarms.Filter = filters.Filter;
        }

        public void AddBeginDateFilter()
        {
            // see Notes on Adding Filters:
            if (CanRemoveBeginDateFilter)
            {
                filters.RemoveFilter(BeginDateFilter);
                filters.AddFilter(BeginDateFilter);
                Alarms.Filter = filters.Filter;
            }
            else
            {
                filters.AddFilter(BeginDateFilter);
                Alarms.Filter = filters.Filter;
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
            eventwarn alarm = obj as eventwarn;
            if (alarm != null && (alarm.time.Date <= EndDateFilterValue.Value.Date))
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
            Alarms.Filter = filters.Filter;
        }

        public void AddEndDateFilter()
        {
            // see Notes on Adding Filters:
            if (CanRemoveEndDateFilter)
            {
                filters.RemoveFilter(EndDateFilter);
                filters.AddFilter(EndDateFilter);
                Alarms.Filter = filters.Filter;
            }
            else
            {
                filters.AddFilter(EndDateFilter);
                Alarms.Filter = filters.Filter;
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
            eventwarn alarm = obj as eventwarn;
            if (alarm != null && (alarm.handlestatus == ProcessStateFilterValue))
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
            Alarms.Filter = filters.Filter;
        }

        public void AddProcessStateFilter()
        {
            // see Notes on Adding Filters:
            if (CanRemoveProcessStateFilter)
            {
                filters.RemoveFilter(ProcessStateFilter);
                filters.AddFilter(ProcessStateFilter);
                Alarms.Filter = filters.Filter;
            }
            else
            {
                filters.AddFilter(ProcessStateFilter);
                Alarms.Filter = filters.Filter;
                CanRemoveProcessStateFilter = true;
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

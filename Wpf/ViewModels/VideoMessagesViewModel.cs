using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using ICMServer.Models;
using ICMServer.Services;
using ICMServer.WPF.Models;
using System;
using System.Collections;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace ICMServer.WPF.ViewModels
{
    public class VideoMessagesViewModel : ViewModelBase
    {
        private readonly ICollectionModel<leaveword> _dataModel;
        private readonly IDialogService _dialogService;

        public VideoMessagesViewModel(
            ICollectionModel<leaveword> dataModel,
            IDialogService dialogService)
        {
            this._dialogService = dialogService;
            this._dataModel = dataModel;
            VideoMessages = (ListCollectionView)CollectionViewSource.GetDefaultView((IList)_dataModel.Data);
            VideoMessages.CurrentChanged += VideoMessages_CurrentChanged;

            RefreshCommand.Execute(null);
        }

        private void VideoMessages_CurrentChanged(object sender, EventArgs e)
        {
            this.DeleteVideoMessagesCommand.RaiseCanExecuteChanged();
            this.PlayVideoMessageCommand.RaiseCanExecuteChanged();
        }

        #region VideoMessages
        private ListCollectionView _videoMessages;
        public ListCollectionView VideoMessages
        {
            get { return _videoMessages; }
            private set
            {
                this.Set(ref _videoMessages, value);
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

        #region DeleteVideoMessagesCommand
        private IAsyncCommand _deleteVideoMessagesCommand;
        public IAsyncCommand DeleteVideoMessagesCommand
        {
            get
            {
                return _deleteVideoMessagesCommand ?? (_deleteVideoMessagesCommand = new AsyncCommand<IList, object>(
                    async (videoMessages, _) => { await _dataModel.DeleteAsync(videoMessages as IList); return null; },
                    (videoMessages) => { return (videoMessages != null) && (videoMessages.Count > 0); }));
            }
        }
        #endregion

        #region PlayVideoMessageCommand
        private RelayCommand _PlayVideoMessageCommand;
        public RelayCommand PlayVideoMessageCommand
        {
            get
            {
                return _PlayVideoMessageCommand
                    ?? (_PlayVideoMessageCommand = new RelayCommand(
                    () => { this._dialogService.ShowPlayVideoMessageDialog(VideoMessages.CurrentItem as leaveword); },
                    () => { return (VideoMessages.CurrentItem as leaveword) != null; }));
            }
        }
        #endregion

        #region BeginDateFilter
        private bool FilterByBeginDate(object obj)
        {
            leaveword message = obj as leaveword;
            if (message != null)
            {
                DateTime dateTime;
                if (DateTime.TryParse(message.time, out dateTime))
                {
                    if (dateTime.Date >= BeginDateFilterValue.Value.Date)
                        return true;
                }
            }
            return false;
        }

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

        Predicate<object> _BeginDateFilter;
        Predicate<object> BeginDateFilter
        {
            get { return _BeginDateFilter ?? (_BeginDateFilter = new Predicate<object>(FilterByBeginDate)); }
        }

        private async Task RemoveBeginDateFilterAsync()
        {
            await Task.Run(() =>
            {
                filters.RemoveFilter(BeginDateFilter);
                BeginDateFilterValue = null;
                CanRemoveBeginDateFilter = false;
            });
            VideoMessages.Filter = filters.Filter;
        }

        public void AddBeginDateFilter()
        {
            // see Notes on Adding Filters:
            if (CanRemoveBeginDateFilter)
            {
                filters.RemoveFilter(BeginDateFilter);
                filters.AddFilter(BeginDateFilter);
                VideoMessages.Filter = filters.Filter;
            }
            else
            {
                filters.AddFilter(BeginDateFilter);
                VideoMessages.Filter = filters.Filter;
                CanRemoveBeginDateFilter = true;
            }
        }
        #endregion
        
        #region EndDateFilter
        private bool FilterByEndDate(object obj)
        {
            leaveword message = obj as leaveword;
            if (message != null)
            {
                DateTime dateTime;
                if (DateTime.TryParse(message.time, out dateTime))
                {
                    if (dateTime.Date <= EndDateFilterValue.Value.Date)
                        return true;
                }
            }
            return false;
        }

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

        Predicate<object> _EndDateFilter;
        Predicate<object> EndDateFilter
        {
            get { return _EndDateFilter ?? (_EndDateFilter = new Predicate<object>(FilterByEndDate)); }
        }

        private async Task RemoveEndDateFilterAsync()
        {
            await Task.Run(() =>
            {
                filters.RemoveFilter(EndDateFilter);
                EndDateFilterValue = null;
                CanRemoveEndDateFilter = false;
            });
            VideoMessages.Filter = filters.Filter;
        }

        public void AddEndDateFilter()
        {
            // see Notes on Adding Filters:
            if (CanRemoveEndDateFilter)
            {
                filters.RemoveFilter(EndDateFilter);
                filters.AddFilter(EndDateFilter);
                VideoMessages.Filter = filters.Filter;
            }
            else
            {
                filters.AddFilter(EndDateFilter);
                VideoMessages.Filter = filters.Filter;
                CanRemoveEndDateFilter = true;
            }
        }
        #endregion
        
        #region ReadStateFilter
        private bool FilterByReadState(object obj)
        {
            leaveword videoMsg = obj as leaveword;
            if (videoMsg != null)
            {
                bool readState = videoMsg.readflag.HasValue && videoMsg.readflag.Value != 0;
                return ReadStateFilterValue == readState;
            }
            return false;
        }

        private bool? _ReadStateFilterValue;
        public bool? ReadStateFilterValue
        {
            get { return _ReadStateFilterValue; }
            set
            {
                if (this.Set(ref _ReadStateFilterValue, value))
                {
                    ApplyFilter(_ReadStateFilterValue.HasValue ? FilterField.ReadState : FilterField.None);
                }
            }
        }

        #region RemoveReadStateFilterCommand
        private ICommand _removeReadStateFilterCommand;
        public ICommand RemoveReadStateFilterCommand
        {
            get
            {
                return _removeReadStateFilterCommand ?? (_removeReadStateFilterCommand = new AsyncCommand<object>(
                    async (param, _) => { await RemoveReadStateFilterAsync(); return null; },
                    () => CanRemoveReadStateFilter));
            }
        }

        private bool _canRemoveReadStateFilter;
        public bool CanRemoveReadStateFilter
        {
            get { return _canRemoveReadStateFilter; }
            private set { this.Set(ref _canRemoveReadStateFilter, value); }
        }
        #endregion

        Predicate<object> _ReadStateFilter;
        Predicate<object> ReadStateFilter
        {
            get { return _ReadStateFilter ?? (_ReadStateFilter = new Predicate<object>(FilterByReadState)); }
        }

        private async Task RemoveReadStateFilterAsync()
        {
            await Task.Run(() =>
            {
                filters.RemoveFilter(ReadStateFilter);
                ReadStateFilterValue = null;
                CanRemoveReadStateFilter = false;
            });
            VideoMessages.Filter = filters.Filter;
        }

        public void AddReadStateFilter()
        {
            // see Notes on Adding Filters:
            if (CanRemoveReadStateFilter)
            {
                filters.RemoveFilter(ReadStateFilter);
                filters.AddFilter(ReadStateFilter);
                VideoMessages.Filter = filters.Filter;
            }
            else
            {
                filters.AddFilter(ReadStateFilter);
                VideoMessages.Filter = filters.Filter;
                CanRemoveReadStateFilter = true;
            }
        }
        #endregion
        
        #region FilterBaseFunctions

        GroupFilter filters = new GroupFilter();

        private enum FilterField
        {
            BeginDate,
            EndDate,
            ReadState,
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

                case FilterField.ReadState:
                    AddReadStateFilter();
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
            await RemoveReadStateFilterAsync();
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

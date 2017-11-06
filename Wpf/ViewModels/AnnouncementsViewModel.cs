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
    public class AnnouncementsViewModel : ValidatableViewModelBase
    {
        private readonly ICollectionModel<Announcement> _dataModel;
        private readonly IAnnouncementsRoomsModel _announcementsRoomsDataModel;
        private readonly IDialogService _dialogService;

        public AnnouncementsViewModel(
            ICollectionModel<Announcement> dataModel,
            IAnnouncementsRoomsModel announcementsRoomsDataModel,
            IDialogService dialogService)
        {
            this._dialogService = dialogService;
            this._dataModel = dataModel;
            this._announcementsRoomsDataModel = announcementsRoomsDataModel;
            Announcements = (ListCollectionView)CollectionViewSource.GetDefaultView((IList)_dataModel.Data);
            Announcements.CurrentChanged += Announcements_CurrentChanged;
            DstRoomsPerAnnouncement = (ListCollectionView)CollectionViewSource.GetDefaultView((IList)_announcementsRoomsDataModel.Data);

            RefreshCommand.Execute(null);
        }

        private void Announcements_CurrentChanged(object sender, EventArgs e)
        {
            this.DisplayAnnouncementDetailCommand.RaiseCanExecuteChanged();
            this.DeleteAnnouncementsCommand.RaiseCanExecuteChanged();
            this._announcementsRoomsDataModel.SetAnnouncement(this.Announcements.CurrentItem as Announcement);
        }

        #region Announcements
        private ListCollectionView _announcements;
        public ListCollectionView Announcements
        {
            get { return _announcements; }
            private set { this.Set(ref _announcements, value); }
        }
        #endregion

        #region DstRoomsPerAnnouncement
        private ListCollectionView _DstRoomsPerAnnouncement;
        public ListCollectionView DstRoomsPerAnnouncement
        {
            get { return _DstRoomsPerAnnouncement; }
            private set
            {
                this.Set(ref _DstRoomsPerAnnouncement, value);
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

        #region DisplayAnnouncementDetailCommand
        private RelayCommand _displayAnnouncementDetailCommand;
        /// <summary>
        /// Gets the DisplayAnnouncementDetailCommand.
        /// </summary>
        public RelayCommand DisplayAnnouncementDetailCommand
        {
            get
            {
                return _displayAnnouncementDetailCommand
                    ?? (_displayAnnouncementDetailCommand = new RelayCommand(
                    () => { this._dialogService.ShowViewAnnouncementDialog(Announcements.CurrentItem as Announcement); },
                    () => { return (Announcements.CurrentItem as Announcement) != null; }));
            }
        }
        #endregion

        #region AddAnnouncementCommand
        private RelayCommand _addAnnouncementCommand;
        public RelayCommand AddAnnouncementCommand
        {
            get
            {
                return _addAnnouncementCommand ?? (_addAnnouncementCommand = new RelayCommand(
                        () => { this._dialogService.ShowAddAnnouncementDialog(); },
                        () => { return true; }));
            }
        }
        #endregion

        #region DeleteAnnouncementsCommand
        private IAsyncCommand _deleteAnnouncementsCommand;
        public IAsyncCommand DeleteAnnouncementsCommand
        {
            get
            {
                return _deleteAnnouncementsCommand ?? (_deleteAnnouncementsCommand = new AsyncCommand<IList, object>(
                    async (announcements, _) => { await _dataModel.DeleteAsync(announcements as IList); return null; },
                    (announcements) => { return (announcements != null) && (announcements.Count > 0); }));
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
                    //this._dialogService.ShowSelectRoomAddressDialog(
                    //    this.AddressFilterValue,
                    //    (roomAddress) => { this.AddressFilterValue = roomAddress; });
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
            Announcement announcement = obj as Announcement;
            if (announcement != null 
             && announcement.time.HasValue 
             && (announcement.time.Value.Date >= BeginDateFilterValue.Value.Date))
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
            Announcements.Filter = filters.Filter;
        }

        public void AddBeginDateFilter()
        {
            // see Notes on Adding Filters:
            if (CanRemoveBeginDateFilter)
            {
                filters.RemoveFilter(BeginDateFilter);
                filters.AddFilter(BeginDateFilter);
                Announcements.Filter = filters.Filter;
            }
            else
            {
                filters.AddFilter(BeginDateFilter);
                Announcements.Filter = filters.Filter;
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
            Announcement announcement = obj as Announcement;
            if (announcement != null
             && announcement.time.HasValue
             && (announcement.time.Value.Date <= EndDateFilterValue.Value.Date))
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
            Announcements.Filter = filters.Filter;
        }

        public void AddEndDateFilter()
        {
            // see Notes on Adding Filters:
            if (CanRemoveEndDateFilter)
            {
                filters.RemoveFilter(EndDateFilter);
                filters.AddFilter(EndDateFilter);
                Announcements.Filter = filters.Filter;
            }
            else
            {
                filters.AddFilter(EndDateFilter);
                Announcements.Filter = filters.Filter;
                CanRemoveEndDateFilter = true;
            }
        }
        #endregion
        
        //#region AddressFilter
        //#region RemoveAddressFilterCommand
        //private ICommand _removeAddressFilterCommand;
        //public ICommand RemoveAddressFilterCommand
        //{
        //    get
        //    {
        //        return _removeAddressFilterCommand ?? (_removeAddressFilterCommand = new AsyncCommand<object>(
        //            async (param, _) => { await RemoveAddressFilterAsync(); return null; },
        //            () => CanRemoveAddressFilter));
        //    }
        //}

        //private bool _canRemoveAddressFilter;
        //public bool CanRemoveAddressFilter
        //{
        //    get { return _canRemoveAddressFilter; }
        //    private set { this.Set(ref _canRemoveAddressFilter, value); }
        //}
        //#endregion

        //private string _AddressFilterValue;
        //public string AddressFilterValue
        //{
        //    get { return _AddressFilterValue; }
        //    set
        //    {
        //        if (this.Set(ref _AddressFilterValue, value))
        //        {
        //            if (!string.IsNullOrWhiteSpace(_AddressFilterValue))
        //                ApplyFilter(FilterField.Address);
        //            else
        //                RemoveAddressFilterCommand.Execute(null);
        //        }
        //    }
        //}

        //Predicate<object> _AddressFilter;
        //Predicate<object> AddressFilter
        //{
        //    get { return _AddressFilter ?? (_AddressFilter = new Predicate<object>(FilterByAddress)); }
        //}

        //private bool FilterByAddress(object obj)
        //{
        //    Announcement announcement = obj as Announcement;
        //    string dstDeviceAddress = (announcement != null && (announcement.dstaddr != null))
        //                             ? announcement.dstaddr : "";
        //    if (dstDeviceAddress.StartsWith(this.AddressFilterValue))
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        //private async Task RemoveAddressFilterAsync()
        //{
        //    await Task.Run(() =>
        //    {
        //        filters.RemoveFilter(AddressFilter);
        //        AddressFilterValue = null;
        //        CanRemoveAddressFilter = false;
        //    });
        //    Announcements.Filter = filters.Filter;
        //}

        //public void AddAddressFilter()
        //{
        //    // see Notes on Adding Filters:
        //    if (CanRemoveAddressFilter)
        //    {
        //        filters.RemoveFilter(AddressFilter);
        //        filters.AddFilter(AddressFilter);
        //        Announcements.Filter = filters.Filter;
        //    }
        //    else
        //    {
        //        filters.AddFilter(AddressFilter);
        //        Announcements.Filter = filters.Filter;
        //        CanRemoveAddressFilter = true;
        //    }
        //}
        //#endregion
        
        #region FilterBaseFunctions

        GroupFilter filters = new GroupFilter();

        private enum FilterField
        {
            BeginDate,
            EndDate,
            //Address,
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

                //case FilterField.Address:
                //    AddAddressFilter();
                //    break;

                default:
                    break;
            }
        }

        public async Task ResetFiltersAsync()
        {
            // clear filters 
            await RemoveBeginDateFilterAsync();
            await RemoveEndDateFilterAsync();
            //await RemoveAddressFilterAsync();
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

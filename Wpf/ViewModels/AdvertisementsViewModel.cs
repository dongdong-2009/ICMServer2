using GalaSoft.MvvmLight.CommandWpf;
using ICMServer.Models;
using ICMServer.Services;
using ICMServer.WPF.Models;
using System;
using System.Collections;
using System.Windows.Data;
using System.Windows.Input;

namespace ICMServer.WPF.ViewModels
{
    public class AdvertisementsViewModel : ValidatableViewModelBase
    {
        private readonly IAdvertisementsModel _dataModel;
        private readonly IDialogService _dialogService;
        // 請使用 ObservableRangeCollection 來取代 ObservableCollection，
        // 主要是在實作refresh data 這功能時，使用此類別的ReplaceRange method可以避免DataGrid 跳動
        //private ObservableRangeCollection<Device> _devicesCollection;
        //private static object _lock = new object();

        // TODO: BUG...DateTime always 以 en-US 呈現
        public AdvertisementsViewModel(
            IAdvertisementsModel dataModel,
            IDialogService dialogService)
        {
            this._dialogService = dialogService;
            this._dataModel = dataModel;
            Advertisements = (ListCollectionView)CollectionViewSource.GetDefaultView((IList)_dataModel.Data);
            Advertisements.CurrentChanged += Advertisements_CurrentChanged;

            this.BeginPlayTime = (DateTime.TryParse(Config.Instance.AdvertisementBeginTime, out this._beginPlayTime))
                               ? _beginPlayTime : new DateTime(0, 0, 0, 0, 0, 0);
            this.EndPlayTime = (DateTime.TryParse(Config.Instance.AdvertisementEndTime, out this._endPlayTime))
                               ? _endPlayTime : new DateTime(0, 0, 0, 23, 59, 59);
            RefreshCommand.Execute(null);   
        }

        private void Advertisements_CurrentChanged(object sender, EventArgs e)
        {
            this.DeleteAdvertisementsCommand.RaiseCanExecuteChanged();
        }

        #region Advertisements
        private ListCollectionView _advertisements;
        public ListCollectionView Advertisements
        {
            get { return _advertisements; }
            private set { this.Set(ref _advertisements, value); }
        }
        #endregion

        #region BeginPlayTime
        private DateTime _beginPlayTime;
        public DateTime BeginPlayTime
        {
            get { return _beginPlayTime; }
            set
            {
                if (this.Set(ref _beginPlayTime, value))
                {
                    Config.Instance.AdvertisementBeginTime = _beginPlayTime.ToString("HH:mm:ss");
                }
            }
        }
        #endregion

        #region EndPlayTime
        private DateTime _endPlayTime;
        public DateTime EndPlayTime
        {
            get { return _endPlayTime; }
            set
            {
                if (this.Set(ref _endPlayTime, value))
                {
                    Config.Instance.AdvertisementEndTime = _endPlayTime.ToString("HH:mm:ss");
                }
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

        #region AddAdvertisementCommand
        private RelayCommand _addAdvertisementCommand;
        public RelayCommand AddAdvertisementCommand
        {
            get
            {
                return _addAdvertisementCommand ?? (_addAdvertisementCommand = new RelayCommand(
                        () => { this._dialogService.ShowAddAdvertisementDialog(); },
                        () => { return true; }));
            }
        }
        #endregion

        #region DeleteAdvertisementsCommand
        private IAsyncCommand _deleteAdvertisementsCommand;
        public IAsyncCommand DeleteAdvertisementsCommand
        {
            get
            {
                return _deleteAdvertisementsCommand ?? (_deleteAdvertisementsCommand = new AsyncCommand<IList, object>(
                    async (advertisements, _) => { await _dataModel.DeleteAsync(advertisements as IList); return null; },
                    (advertisements) => { return (advertisements != null) && (advertisements.Count > 0); }));
            }
        }
        #endregion

        #region MoveUpCommand
        private IAsyncCommand _moveUpCommand;
        public IAsyncCommand MoveUpCommand
        {
            get
            {
                return _moveUpCommand ?? (_moveUpCommand = AsyncCommand.Create(
                    async () =>
                    {
                        await this._dataModel.MoveUpAsync(Advertisements.CurrentItem as advertisement);
                        Advertisements.MoveCurrentToPrevious();
                    },
                    () =>
                    {
                        advertisement data = Advertisements.CurrentItem as advertisement;
                        return (data != null) && (data.C_no > 1);
                    }));
            }
        }
        #endregion

        #region MoveDownCommand
        private IAsyncCommand _moveDownCommand;
        public IAsyncCommand MoveDownCommand
        {
            get
            {
                return _moveDownCommand ?? (_moveDownCommand = AsyncCommand.Create(
                    async () =>
                    {
                        await this._dataModel.MoveDownAsync(Advertisements.CurrentItem as advertisement);
                        Advertisements.MoveCurrentToNext();
                    },
                    () =>
                    {
                        advertisement data = Advertisements.CurrentItem as advertisement;
                        return (data != null) && (data.C_no < _dataModel.Data.Count);
                    }));
            }
        }

        #endregion
    }
}

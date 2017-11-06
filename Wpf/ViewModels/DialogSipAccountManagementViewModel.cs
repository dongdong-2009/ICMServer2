using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using ICMServer.Models;
using ICMServer.Net;
using ICMServer.Services;
using ICMServer.WPF.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace ICMServer.WPF.ViewModels
{
    public class DialogSipAccountManagementViewModel : ViewModelBase
    {
        private readonly ICollectionModel<sipaccount> _dataModel;
        private readonly IDialogService _dialogService;

        public DialogSipAccountManagementViewModel(
            ICollectionModel<sipaccount> dataModel,
            IDialogService dialogService)
        {
            this._dialogService = dialogService;
            this._dataModel = dataModel;

            SipAccounts = new ListCollectionView((IList)_dataModel.Data);
            RoomAddressFilterValue = "";
            RoomAddress = "00-00-00-00-00";

            UnsyncedSipAccounts = new ListCollectionView((IList)_dataModel.Data);
            UnsyncedSipAccounts.Filter = new Predicate<object>(FilterBySyncStatus);
        }

        #region SipAccounts
        private ListCollectionView _SipAccounts;
        public ListCollectionView SipAccounts
        {
            get { return _SipAccounts; }
            private set { this.Set(ref _SipAccounts, value); }
        }
        #endregion

        #region UnsyncedSipAccounts
        private ListCollectionView _UnsyncedSipAccounts;
        public ListCollectionView UnsyncedSipAccounts
        {
            get { return _UnsyncedSipAccounts; }
            private set { this.Set(ref _UnsyncedSipAccounts, value); }
        }
        #endregion

        #region RoomAddress
        private string _RoomAddress;
        public string RoomAddress
        {
            get { return _RoomAddress; }
            set { this.Set(ref _RoomAddress, value); }
        }
        #endregion

        #region GetSipAccountsByRoomAddressCommand
        private RelayCommand _GetSipAccountsByRoomAddressCommand;
        public RelayCommand GetSipAccountsByRoomAddressCommand
        {
            get
            {
                return _GetSipAccountsByRoomAddressCommand ?? (_GetSipAccountsByRoomAddressCommand = new RelayCommand(
                    () => { RoomAddressFilterValue = RoomAddress; }));
            }
        }
        #endregion

        #region AddSipAccountCommand
        private RelayCommand _AddSipAccountCommand;
        public RelayCommand AddSipAccountCommand
        {
            get
            {
                return _AddSipAccountCommand ?? (_AddSipAccountCommand = new RelayCommand(() =>
                {
                    this._dialogService.ShowAddSipAccountDialog(this.RoomAddress);
                },
                () =>
                {
                    return this.RoomAddress != null && Regex.Match(this.RoomAddress, @"^\w{2}-\w{2}-\w{2}-\w{2}-\w{2}$").Length > 0;
                }));
            }
        }
        #endregion

        #region DeleteSipAccountsCommand
        private IAsyncCommand _deleteSipAccountsCommand;
        public IAsyncCommand DeleteSipAccountsCommand
        {
            get
            {
                return _deleteSipAccountsCommand ?? (_deleteSipAccountsCommand = new AsyncCommand<IList, object>(
                    async (sipAccounts, _) => { await _dataModel.DeleteAsync(sipAccounts as IList); return null; },
                    (sipAccounts) => { return (sipAccounts != null) && (sipAccounts.Count > 0); }));
            }
        }
        #endregion

        #region SyncSipAccountsCommand
        private IAsyncCommand _SyncSipAccountsCommand;
        public IAsyncCommand SyncSipAccountsCommand
        {
            get
            {
                return _SyncSipAccountsCommand ?? (_SyncSipAccountsCommand = AsyncCommand.Create(
                    () => 
                    {
                        this._dialogService.ShowSyncSipAccountsDialog();
                        return null;
                    },
                    () => { return this.UnsyncedSipAccounts.Count > 0; }));
            }
        }
        #endregion

        #region DisplayQRCodeCommand
        private IAsyncCommand _DisplayQRCodeCommand;
        public IAsyncCommand DisplayQRCodeCommand
        {
            get
            {
                return _DisplayQRCodeCommand ?? (_DisplayQRCodeCommand = AsyncCommand.Create(
                    async () =>
                    {
                        var account = SipAccounts.CurrentItem as sipaccount;
                        if (account != null)
                        {
                            string strToBeEncoded = "";
                            const string endl = "\n";
                            switch (Config.Instance.CloudSolution)
                            {
                                case CloudSolution.SIPServer:
                                    string sipServerIP = Config.Instance.SIPServerIP;
                                    strToBeEncoded = account.C_password + endl + sipServerIP;
                                    break;

                                case CloudSolution.PPHook:
                                    string result = await HttpClient.GetPPHookServiceTokenAsync("8001");
                                    PPHookServiceToken pphookServiceToken = JsonConvert.DeserializeObject<PPHookServiceToken>(result);
                                    string token = pphookServiceToken.token;
                                    string icmServerIP = Config.Instance.LocalIP;

                                    strToBeEncoded = account.C_user + endl +
                                                     account.C_password + endl +
                                                     icmServerIP + endl +
                                                     token + endl +
                                                     account.C_usergroup;
                                    break;
                            }
                            this._dialogService.ShowDisplayQRCodeDialog(strToBeEncoded);
                        }
                    },
                    () => { return (SipAccounts.CurrentItem as sipaccount) != null; }));
            }
        }
        #endregion

        #region RoomAddressFilter
        private bool FilterByRoomAddress(object obj)
        {
            sipaccount account = obj as sipaccount;
            if (account != null && (account.C_room == RoomAddressFilterValue))
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
                if (value == null)
                    value = "";

                if (this.Set(ref _RoomAddressFilterValue, value))
                {
                    ApplyFilter(FilterField.RoomAddress);
                }
            }
        }

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
            SipAccounts.Filter = filters.Filter;
        }

        public void AddRoomAddressFilter()
        {
            // see Notes on Adding Filters:
            if (CanRemoveRoomAddressFilter)
            {
                filters.RemoveFilter(RoomAddressFilter);
                filters.AddFilter(RoomAddressFilter);
                SipAccounts.Filter = filters.Filter;
            }
            else
            {
                filters.AddFilter(RoomAddressFilter);
                SipAccounts.Filter = filters.Filter;
                CanRemoveRoomAddressFilter = true;
            }
        }
        #endregion

        #region SyncStatusFilter
        private bool FilterBySyncStatus(object obj)
        {
            sipaccount account = obj as sipaccount;
            if (account != null)
            {
                if (account.C_sync == null || account.C_sync == 0)
                    return true;
            }
            return false;
        }
        #endregion

        #region FilterBaseFunctions
        GroupFilter filters = new GroupFilter();

        private enum FilterField
        {
            RoomAddress,
            None
        }

        private void ApplyFilter(FilterField field)
        {
            switch (field)
            {
                case FilterField.RoomAddress:
                    AddRoomAddressFilter();
                    break;

                default:
                    break;
            }
        }
        
        #endregion
    }
}

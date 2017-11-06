using GalaSoft.MvvmLight;
using ICMServer.Models;
using ICMServer.Net;
using ICMServer.Services;
using ICMServer.WPF.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ICMServer.WPF.ViewModels
{
    public class DialogSyncSipAccountsViewModel : ViewModelBase
    {
        private readonly ICollectionModel<sipaccount> _dataModel;
        private readonly IDialogService _dialogService;

        public DialogSyncSipAccountsViewModel(
            ICollectionModel<sipaccount> dataModel,
            IDialogService dialogService)
        {
            this._dialogService = dialogService;
            this._dataModel = dataModel;

            SipAccounts = new ListCollectionView((IList)_dataModel.Data);
            using (SipAccounts.DeferRefresh())
            {
                SipAccounts.Filter = new Predicate<object>(FilterBySyncStatus);
                SipAccounts.SortDescriptions.Add(new SortDescription("C_room", ListSortDirection.Ascending));
                SipAccounts.SortDescriptions.Add(new SortDescription("C_user", ListSortDirection.Ascending));
                SipAccounts.GroupDescriptions.Add(new PropertyGroupDescription("C_room"));
            }
        }

        #region SipAccounts
        private ListCollectionView _SipAccounts;
        public ListCollectionView SipAccounts
        {
            get { return _SipAccounts; }
            private set { this.Set(ref _SipAccounts, value); }
        }
        #endregion

        #region SyncCommand
        private IAsyncCommand _SyncCommand;
        public IAsyncCommand SyncCommand
        {
            get
            {
                return _SyncCommand ?? (_SyncCommand = AsyncCommand.Create(
                    async () => { await SyncSipAccounts(); }, 
                    () => { return this.SipAccounts.Count > 0; }));
            }
        }
        #endregion

        private async Task SyncSipAccounts(
            CancellationToken cancelToken = new CancellationToken())
        {
            bool result = false;
            string sipServerIP = Config.Instance.SIPServerIP;
            int sipServerPort = Config.Instance.SIPServerPort;
            if (string.IsNullOrWhiteSpace(sipServerIP) || sipServerPort == 0)
            {
                throw new Exception(CulturesHelper.GetTextValue("PleaseSetTheIPAndPortNumberOfSIPServerFirst"));
            }

            // http://172.29.1.21:5050/AddSipAccount?sip_account=account1&sip_pwd=BqVXrqD0YS&apply=1
            // ring group
            // account1-account2-account3
            // http://172.29.1.21:5050/AddRingGroup?group_num=0000000000&group_list=account1-account2-account3
            //var unsyncedAccounts = this.SipAccounts.Cast<sipaccount>();
            var groups = this.SipAccounts.Groups.ToList();
            foreach (var group in groups)
            {
                result = false;
                string roomAddress = (string)((CollectionViewGroup)group).Name;
                List<string> accountNames = new List<string>();
                var accounts = (from a in this._dataModel.Data
                                where a.C_room == roomAddress
                                select a).ToList();

                foreach (var account in accounts)
                {
                    accountNames.Add(account.C_user);
                    if (account.C_sync == null || account.C_sync == 0)
                    {
                        result = await HttpClient.AddSipAccountAsync(sipServerIP, sipServerPort, account.C_user, account.C_password, cancelToken).ConfigureAwait(false);
                        if (result == false)
                            break;
                    }
                }

                if (!result || false == (result = await HttpClient.AddRingGroupAsync(sipServerIP, sipServerPort,
                    roomAddress.Replace("-", ""), 
                    string.Join("-", accountNames), 
                    cancelToken).ConfigureAwait(false)))
                {
                    continue;
                }

                foreach (var account in accounts)
                {
                    if (account.C_sync == null || account.C_sync == 0)
                    {
                        account.C_sync = 1;
                        await this._dataModel.UpdateAsync(account).ConfigureAwait(false);
                    }
                }
            }
        }

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
    }
}

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using ICMServer.Models;
using ICMServer.Net;
using ICMServer.Services;
using ICMServer.WPF.Models;
using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;

namespace ICMServer.WPF.ViewModels
{
    public class SipAccountsViewModel : ViewModelBase
    {
        private readonly ICollectionModel<sipaccount> _dataModel;
        private readonly IDialogService _dialogService;

        public SipAccountsViewModel(
            ICollectionModel<sipaccount> dataModel,
            IDialogService dialogService)
        {
            this._dialogService = dialogService;
            this._dataModel = dataModel;
            SipAccounts = (ListCollectionView)CollectionViewSource.GetDefaultView((IList)_dataModel.Data);
            using (SipAccounts.DeferRefresh())
            {
                SipAccounts.SortDescriptions.Add(new SortDescription("C_room", ListSortDirection.Ascending));
                SipAccounts.SortDescriptions.Add(new SortDescription("C_user", ListSortDirection.Ascending));
                SipAccounts.GroupDescriptions.Add(new PropertyGroupDescription("C_room"));
            }

            RefreshCommand.Execute(null);
        }

        #region SipAccounts
        private ListCollectionView _sipAccounts;
        public ListCollectionView SipAccounts
        {
            get { return _sipAccounts; }
            private set { this.Set(ref _sipAccounts, value); }
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

        #region RefreshOnlineStatusCommand
        private IAsyncCommand _RefreshOnlineStatusCommand;
        public IAsyncCommand RefreshOnlineStatusCommand
        {
            get
            {
                return _RefreshOnlineStatusCommand ?? (_RefreshOnlineStatusCommand = AsyncCommand.Create(
                    async () => 
                    {
                        try
                        {
                            string sipServerIP = Config.Instance.SIPServerIP;
                            int sipServerPort = Config.Instance.SIPServerPort;
                            if (string.IsNullOrWhiteSpace(sipServerIP) || sipServerPort == 0)
                            {
                                throw new Exception(CulturesHelper.GetTextValue("PleaseSetTheIPAndPortNumberOfSIPServerFirst"));
                            }

                            bool result = false;
                            var accounts = this.SipAccounts.Cast<sipaccount>().ToList();
                            foreach (var account in accounts)
                            {
                                account.C_registerstatus = (result = await HttpClient.IsSipAccountOnlineAsync(sipServerIP, sipServerPort, account.C_user))
                                                         ? 1 : 0;
                            }
                        }
                        catch (Exception e) { this._dialogService.ShowMessageBox(e.Message); }
                    },
                    () => { return this.SipAccounts.Count > 0; }));
            }
        }
        #endregion

        #region PushNotificationCommand
        private IAsyncCommand _PushNotificationCommand;
        public IAsyncCommand PushNotificationCommand
        {
            get
            {
                return _PushNotificationCommand ?? (_PushNotificationCommand = AsyncCommand.Create(
                    async () =>
                    {
                        try
                        {
                            string sipServerIP = Config.Instance.SIPServerIP;
                            int sipServerPort = Config.Instance.SIPServerPort;
                            if (string.IsNullOrWhiteSpace(sipServerIP) || sipServerPort == 0)
                            {
                                throw new Exception(CulturesHelper.GetTextValue("PleaseSetTheIPAndPortNumberOfSIPServerFirst"));
                            }

                            bool result = false;
                            var account = this.SipAccounts.CurrentItem as sipaccount;
                            if (account != null)
                            {
                                if (true == (result = await HttpClient.PushNotificationAsync(sipServerIP, sipServerPort, account.C_user)))
                                    this._dialogService.ShowMessageBox(CulturesHelper.GetTextValue("Success"));
                                else
                                    this._dialogService.ShowMessageBox(CulturesHelper.GetTextValue("Fail"));
                            }
                        }
                        catch (Exception e) { this._dialogService.ShowMessageBox(e.Message); }
                    },
                    () => { return null != (this.SipAccounts.CurrentItem as sipaccount); }));
            }
        }
        #endregion

        #region SipAccountManagementCommand
        private RelayCommand _SipAccountManagementCommand;
        public RelayCommand SipAccountManagementCommand
        {
            get
            {
                return _SipAccountManagementCommand ?? (_SipAccountManagementCommand = new RelayCommand(
                    () => { this._dialogService.ShowSipAccountManagementDialog(); }
                ));
            }
        }
        #endregion
    }
}

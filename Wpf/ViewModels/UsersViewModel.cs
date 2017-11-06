using GalaSoft.MvvmLight;
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
    public class UsersViewModel : ViewModelBase
    {
        private readonly ICollectionModel<user> _dataModel;
        private readonly IDialogService _dialogService;

        public UsersViewModel(
            ICollectionModel<user> dataModel,
            IDialogService dialogService)
        {
            this._dataModel = dataModel;
            this._dialogService = dialogService;

            Users = (ListCollectionView)CollectionViewSource.GetDefaultView((IList)_dataModel.Data);
            Users.CurrentChanged += Users_CurrentChanged;

            RefreshCommand.Execute(null);
        }

        private void Users_CurrentChanged(object sender, EventArgs e)
        {
            this.DeleteUsersCommand.RaiseCanExecuteChanged();
        }

        #region Users
        private ListCollectionView _users;
        public ListCollectionView Users
        {
            get { return _users; }
            private set { this.Set(ref _users, value); }
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

        #region AddUserCommand
        private RelayCommand _AddUserCommand;
        public RelayCommand AddUserCommand
        {
            get
            {
                return _AddUserCommand ?? (_AddUserCommand = new RelayCommand(
                        () =>
                        {
                            //this._dialogService.ShowAddUpgradeFileDialog();
                        },
                        () => { return true; }));
            }
        }
        #endregion

        #region EditUserCommand
        private RelayCommand _EditUserCommand;
        public RelayCommand EditUserCommand
        {
            get
            {
                return _EditUserCommand ?? (_EditUserCommand = new RelayCommand(
                        () =>
                        {
                            //this._dialogService.ShowEditDeviceDialog(Devices.CurrentItem as Device);
                        },
                        () => { return (Users.CurrentItem as Device) != null; }));
            }
        }
        #endregion

        #region DeleteUsersCommand
        private IAsyncCommand _DeleteUsersCommand;
        public IAsyncCommand DeleteUsersCommand
        {
            get
            {
                return _DeleteUsersCommand ?? (_DeleteUsersCommand = new AsyncCommand<IList, object>(
                    async (users, _) => { await _dataModel.DeleteAsync(users as IList); return null; },
                    (users) => { return (users != null) && (users.Count > 0); }));
            }
        }
        #endregion
    }
}

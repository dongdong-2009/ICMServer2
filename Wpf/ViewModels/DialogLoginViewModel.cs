using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using ICMServer.Models;
using ICMServer.Services;
using ICMServer.WPF.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace ICMServer.WPF.ViewModels
{
    public class DialogLoginViewModel : ViewModelBase
    {
        private readonly ICollectionModel<user> _dataModel;
        private readonly IDialogService _dialogService;

        public DialogLoginViewModel(
            ICollectionModel<user> dataModel,
            IDialogService dialogService)
        {
            this._dataModel = dataModel;
            this._dialogService = dialogService;
            this.CultureIndex = CulturesHelper.SupportedCultures.FindIndex(c => c.Name == Config.Instance.AppLanaguage);
            //this.CultureName = Config.Instance.AppLanaguage;
#if DEBUG
            this.Password = "123456";
#endif

            Users = (ListCollectionView)CollectionViewSource.GetDefaultView((IList)_dataModel.Data);
            //Users.CurrentChanged += Users_CurrentChanged;

            RefreshCommand.Execute(null);
        }

        //private string _cultureName;
        //public string CultureName
        //{
        //    get { return _cultureName; }
        //    set
        //    {
        //        if (this.Set(ref _cultureName, value))
        //        {
        //            CulturesHelper.ChangeCulture(CultureInfo.GetCultureInfo(value));
        //        }
        //    }
        //}

        private int _cultureIndex;
        public int CultureIndex
        {
            get { return _cultureIndex; }
            set
            {
                if (0 <= value && value < CulturesHelper.SupportedCultures.Count)
                {
                    if (this.Set(ref _cultureIndex, value))
                    {
                        CulturesHelper.CurrentCulture = CulturesHelper.SupportedCultures[_cultureIndex];
                    }
                }
            }
        }

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { this.Set(ref _userName, value); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { this.Set(ref _password, value); }
        }

        #region Users
        private ListCollectionView _users;
        public ListCollectionView Users
        {
            get { return _users; }
            private set { this.Set(ref _users, value); }
        }
        #endregion

        #region LoginCommand
        private IAsyncCommand _loginCommand;
        public IAsyncCommand LoginCommand
        {
            get
            {
                return _loginCommand ?? (_loginCommand = new AsyncCommand<Window, object>(
                    async (window, _) =>
                    {
                        var user = await Task.Run(() => _dataModel.Select(u => u.C_username == this.UserName).FirstOrDefault());
                        if (user != null)
                        {
                            //if (ICMServer.Security.MD5Encode(password) == user.C_password)
                            if (Password == user.C_password)
                            {
                                this.CloseDialog(window, true);
                            }
                            else
                                _dialogService.ShowMessageBox("密碼錯誤");
                        }
                        else
                            _dialogService.ShowMessageBox(String.Format("用戶名 {0} 不存在", UserName));
                        return null;
                    }));
            }
        }
        #endregion

        #region CloseCommand
        private ICommand _closeCommand;
        public ICommand CloseCommand
        {
            get
            {
                return _closeCommand ?? (_closeCommand = new RelayCommand<Window>((window) => 
                {
                    this.CloseDialog(window, false);
                }));
            }
        }
        #endregion

        private void CloseDialog(Window view, bool dialogResult)
        {
            if (view != null)
                view.DialogResult = dialogResult;
        }

        #region RefreshCommand
        private IAsyncCommand _refreshCommand;
        public IAsyncCommand RefreshCommand
        {
            get
            {
                return _refreshCommand ?? (_refreshCommand = AsyncCommand.Create(_dataModel.RefillDataAsync));
            }
        }
        #endregion
    }
}

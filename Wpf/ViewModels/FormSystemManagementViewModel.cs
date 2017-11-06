using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using ICMServer.Services;

namespace ICMServer.WPF.ViewModels
{
    public class FormSystemManagementViewModel : ViewModelBase
    {
        private readonly IDialogService _dialogService;

        public FormSystemManagementViewModel(IDialogService dialogService)
        {
            this._dialogService = dialogService;
        }

        #region UserManagementCommand
        private RelayCommand _UserManagementCommand;
        public RelayCommand UserManagementCommand
        {
            get
            {
                return _UserManagementCommand ?? (_UserManagementCommand = new RelayCommand(
                        () => { this._dialogService.ShowUserManagementDialog(); },
                        () => { return true; }));
            }
        }
        #endregion

        #region ConfigSystemSettingsCommand
        private RelayCommand _ConfigSystemSettingsCommand;
        public RelayCommand ConfigSystemSettingsCommand
        {
            get
            {
                return _ConfigSystemSettingsCommand ?? (_ConfigSystemSettingsCommand = new RelayCommand(
                        () => { this._dialogService.ShowConfigSystemSettingDialog(); },
                        () => { return true; }));
            }
        }
        #endregion

        #region BackupDataCommand
        private RelayCommand _BackupDataCommand;
        public RelayCommand BackupDataCommand
        {
            get
            {
                return _BackupDataCommand ?? (_BackupDataCommand = new RelayCommand(
                    () => { this._dialogService.ShowBackupDataDialog(); }));
            }
        }
        #endregion
    }
}

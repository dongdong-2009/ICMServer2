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
    public class UpgradeFilesViewModel : ViewModelBase
    {
        private readonly ICollectionModel<upgrade> _dataModel;
        private readonly IDialogService _dialogService;

        public UpgradeFilesViewModel(
            ICollectionModel<upgrade> dataModel,
            IDialogService dialogService)
        {
            this._dataModel = dataModel;
            this._dialogService = dialogService;
            UpgradeFiles = (ListCollectionView)CollectionViewSource.GetDefaultView((IList)_dataModel.Data);
            UpgradeFiles.CurrentChanged += UpgradeFiles_CurrentChanged;

            RefreshCommand.Execute(null);
        }

        private void UpgradeFiles_CurrentChanged(object sender, EventArgs e)
        {
            this.DeleteUpgradeFilesCommand.RaiseCanExecuteChanged();
        }

        #region UpgradeFiles
        private ListCollectionView _upgradeFiles;
        public ListCollectionView UpgradeFiles
        {
            get { return _upgradeFiles; }
            private set { this.Set(ref _upgradeFiles, value); }
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

        #region AddUpgradeFileCommand
        private RelayCommand _addUpgradeFileCommand;
        public RelayCommand AddUpgradeFileCommand
        {
            get
            {
                return _addUpgradeFileCommand ?? (_addUpgradeFileCommand = new RelayCommand(
                    () => { this._dialogService.ShowAddUpgradeFileDialog(); },
                    () => { return true; }));
            }
        }
        #endregion

        #region DeleteUpgradeFilesCommand
        private IAsyncCommand _deleteUpgradeFilesCommand;
        public IAsyncCommand DeleteUpgradeFilesCommand
        {
            get
            {
                return _deleteUpgradeFilesCommand ?? (_deleteUpgradeFilesCommand = new AsyncCommand<IList, object>(
                    async (upgradeFiles, _) => { await _dataModel.DeleteAsync(upgradeFiles as IList); return null; },
                    (upgradeFiles) => { return (upgradeFiles != null) && (upgradeFiles.Count > 0); }));
            }
        }
        #endregion

        #region SetToDefaultCommand
        private IAsyncCommand _SetToDefaultCommand;
        public IAsyncCommand SetToDefaultCommand
        {
            get
            {
                return _SetToDefaultCommand ?? (_SetToDefaultCommand = new AsyncCommand<IList, object>(
                    async (upgradeFiles, _) =>
                    {
                        upgrade upgradeFile = UpgradeFiles.CurrentItem as upgrade;
                        if (upgradeFile.is_default.HasValue == false || upgradeFile.is_default.Value == 0)
                        {
                            upgradeFile.is_default = 1;
                            await _dataModel.UpdateAsync(upgradeFile);
                        }
                        return null;
                    },
                    (upgradeFiles) => { return (upgradeFiles != null) && (upgradeFiles.Count == 1); }));
            }
        }
        #endregion

        #region UpgradeCommand
        private ICommand _UpgradeCommand;
        public ICommand UpgradeCommand
        {
            get
            {
                return _UpgradeCommand ?? (_UpgradeCommand = new RelayCommand<IList>(
                    (upgradeFiles) =>
                    {
                        upgrade upgradeFile = UpgradeFiles.CurrentItem as upgrade;
                        _dialogService.ShowSelectDevicesToBeUpgraded(upgradeFile);
                    },
                    (upgradeFiles) => { return (upgradeFiles != null) && (upgradeFiles.Count == 1); }));
            }
        }
        #endregion

        public string FtpServerRootPath
        {
            get { return Config.Instance.FTPServerRootDir; }
        }
    }
}

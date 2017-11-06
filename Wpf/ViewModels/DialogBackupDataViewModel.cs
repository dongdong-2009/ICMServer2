using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using ICMServer.Models;
using ICMServer.Services;
using System;

namespace ICMServer.WPF.ViewModels
{
    public class DialogBackupDataViewModel : ViewModelBase
    {
        private readonly IDialogService _dialogService;

        public DialogBackupDataViewModel(IDialogService dialogService)
        {
            this._dialogService = dialogService;
        }

        #region FilePath
        private string _filePath;
        public string FilePath
        {
            get { return _filePath; }
            private set
            {
                Set(ref _filePath, value);
                ExecuteCommand.RaiseCanExecuteChanged();
            }
        }
        #endregion

        #region IsUpdating
        private bool _isExecuting;
        public bool IsExecuting
        {
            get { return _isExecuting; }
            private set
            {
                Set(ref _isExecuting, value);
                PickFileCommand.RaiseCanExecuteChanged();
                ExecuteCommand.RaiseCanExecuteChanged();
            }
        }
        #endregion

        #region ExecuteCommand
        private IAsyncCommand _ExecuteCommand;
        public IAsyncCommand ExecuteCommand
        {
            get
            {
                return _ExecuteCommand ?? (_ExecuteCommand = AsyncCommand.Create(
                    () => 
                    {
                        try
                        {
                            IsExecuting = true;
                            switch (this.Operation)
                            {
                                case DatabaseBackupOperation.Backup:
                                    ICMDBContext.BackupDataToFile(this.FilePath);
                                    break;

                                case DatabaseBackupOperation.Restore:
                                    ICMDBContext.RestoreDataFromFile(this.FilePath);
                                    break;
                            }
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        finally
                        {
                            IsExecuting = false;
                        }
                        return null;
                    },
                    () => { return !string.IsNullOrEmpty(this.FilePath); }));
            }
        }
        #endregion

        #region PickFileCommand
        private RelayCommand _pickFileCommand;
        public RelayCommand PickFileCommand
        {
            get
            {
                return _pickFileCommand ?? (_pickFileCommand = new RelayCommand(() =>
                    {
                        switch (this.Operation)
                        {
                            case DatabaseBackupOperation.Backup:
                                _dialogService.ShowSaveFileDialog(
                                    "Mysql File(*.sql)|*.sql",
                                    "icmdb.sql",
                                    (PickedFilePath) => { this.FilePath = PickedFilePath; });
                                break;

                            case DatabaseBackupOperation.Restore:
                                _dialogService.ShowOpenFileDialog(
                                    "Mysql File(*.sql)|*.sql",
                                    "icmdb.sql",
                                    (PickedFilePath) => { this.FilePath = PickedFilePath; });
                                break;
                        }
                    },
                    () =>
                    {
                        return !IsExecuting;
                    }));
            }
        }
        #endregion

        #region Operation
        private DatabaseBackupOperation _Operation;
        public DatabaseBackupOperation Operation
        {
            get { return _Operation; }
            set { this.Set(ref _Operation, value); }
        }
        #endregion
    }

    public enum DatabaseBackupOperation
    {
        Backup,
        Restore
    }
}

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using ICMServer.Models;
using ICMServer.Services;
using ICMServer.WPF.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace ICMServer.WPF.ViewModels
{
    public class UpgradeTasksViewModel : ViewModelBase
    {
        private readonly IUpgradeTasksModel _dataModel;
        private readonly IDialogService _dialogService;

        public UpgradeTasksViewModel(
            IUpgradeTasksModel dataModel,
            IDialogService dialogService)
        {
            this._dataModel = dataModel;
            this._dialogService = dialogService;
            UpgradeTasks = (ListCollectionView)CollectionViewSource.GetDefaultView((IList)_dataModel.Data);
            UpgradeTasks.CurrentChanged += UpgradeTasks_CurrentChanged;

            RefreshCommand.Execute(null);
        }

        private void UpgradeTasks_CurrentChanged(object sender, EventArgs e)
        {
            this.DeleteUpgradeTasksCommand.RaiseCanExecuteChanged();
        }

        #region UpgradeTasks
        private ListCollectionView _upgradeTasks;
        public ListCollectionView UpgradeTasks
        {
            get { return _upgradeTasks; }
            private set { this.Set(ref _upgradeTasks, value); }
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

        #region DeleteUpgradeTasksCommand
        private IAsyncCommand _deleteUpgradeTasksCommand;
        public IAsyncCommand DeleteUpgradeTasksCommand
        {
            get
            {
                return _deleteUpgradeTasksCommand ?? (_deleteUpgradeTasksCommand = new AsyncCommand<IList, object>(
                    async (upgradeTasks, _) => { await MarkUpgradeTasksDeleted(upgradeTasks as IList); return null; },
                    (upgradeTasks) => { return (upgradeTasks != null) && (upgradeTasks.Count > 0); }));
            }
        }
        #endregion

        private async Task MarkUpgradeTasksDeleted(IList tasks)
        {
            if (tasks == null || tasks.Count == 0)
                return;

            List<UpgradeTask> tasksToBeRemoved = new List<UpgradeTask>();
            foreach (var task in tasks)
                tasksToBeRemoved.Add(task as UpgradeTask);

            List<Task> updateTasks = new List<Task>();
            foreach (var task in tasksToBeRemoved)
            {
                var upgradeTask = task as UpgradeTask;
                if (upgradeTask != null)
                {
                    upgradeTask.DeletedByUser = true;
                    updateTasks.Add(_dataModel.UpdateAsync(upgradeTask, t => t.DeletedByUser));
                }
            }
            await Task.Run(() => Task.WaitAll(updateTasks.ToArray()));
        }
    }
}

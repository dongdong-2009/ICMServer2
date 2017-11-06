using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using ICMServer.Models;
using ICMServer.Services;
using ICMServer.WPF.Models;
using System;
using System.Collections;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace ICMServer.WPF.ViewModels
{
    public class ResidentsViewModel : ViewModelBase
    {
        private readonly ICollectionModel<holderinfo> _dataModel;
        private readonly IDialogService _dialogService;

        public ResidentsViewModel(
            ICollectionModel<holderinfo> dataModel,
            IDialogService dialogService)
        {
            this._dialogService = dialogService;
            this._dataModel = dataModel;
            Residents = (ListCollectionView)CollectionViewSource.GetDefaultView((IList)_dataModel.Data);
            Residents.CurrentChanged += Residents_CurrentChanged;

            RefreshCommand.Execute(null);
        }

        private void Residents_CurrentChanged(object sender, EventArgs e)
        {
            this.EditResidentCommand.RaiseCanExecuteChanged();
            this.DeleteResidentsCommand.RaiseCanExecuteChanged();
        }

        #region Residents
        private ListCollectionView _residents;
        public ListCollectionView Residents
        {
            get { return _residents; }
            private set
            {
                this.Set(ref _residents, value);
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

        #region AddResidentCommand
        private RelayCommand _addResidentCommand;
        public RelayCommand AddResidentCommand
        {
            get
            {
                return _addResidentCommand ?? (_addResidentCommand = new RelayCommand(
                        () => { this._dialogService.ShowAddResidentDialog(); },
                        () => { return true; }));
            }
        }
        #endregion

        #region EditResidentCommand
        private RelayCommand _editResidentCommand;
        public RelayCommand EditResidentCommand
        {
            get
            {
                return _editResidentCommand ?? (_editResidentCommand = new RelayCommand(
                        () => 
                        {
                            this._dialogService.ShowEditResidentDialog(Residents.CurrentItem as holderinfo);
                        },
                        () => { return (Residents.CurrentItem as holderinfo) != null; }));
            }
        }
        #endregion

        #region DeleteResidentsCommand
        private IAsyncCommand _deleteResidentsCommand;
        public IAsyncCommand DeleteResidentsCommand
        {
            get
            {
                return _deleteResidentsCommand ?? (_deleteResidentsCommand = new AsyncCommand<IList, object>(
                    async (residents, _) => { await _dataModel.DeleteAsync(residents as IList); return null; },
                    (residents) => { return (residents != null) && (residents.Count > 0); }));
            }
        }
        #endregion

        #region ResetFiltersCommand
        private ICommand _resetFiltersCommand;
        public ICommand ResetFiltersCommand
        {
            get
            {
                return _resetFiltersCommand ?? (_resetFiltersCommand = new AsyncCommand<object>(
                    async (param, _) => { await ResetFiltersAsync(); return null; },
                    () => { return true; }));
            }
        }
        #endregion

        #region RemoveIsHeadFilterCommand
        private ICommand _removeIsHeadFilterCommand;
        public ICommand RemoveIsHeadFilterCommand
        {
            get
            {
                return _removeIsHeadFilterCommand ?? (_removeIsHeadFilterCommand = new AsyncCommand<object>(
                    async (param, _) => { await RemoveIsHeadFilterAsync(); return null; },
                    () => CanRemoveIsHeadFilter));
            }
        }

        private bool _canRemoveIsHeadFilter;
        public bool CanRemoveIsHeadFilter
        {
            get { return _canRemoveIsHeadFilter; }
            set { Set(ref _canRemoveIsHeadFilter, value); }
        }
        #endregion

        #region RemoveNameFilterCommand
        private ICommand _removeNameFilterCommand;
        public ICommand RemoveNameFilterCommand
        {
            get
            {
                return _removeNameFilterCommand ?? (_removeNameFilterCommand = new AsyncCommand<object>(
                    async (param, _) => { await RemoveNameFilterAsync(); return null; },
                    () => CanRemoveNameFilter));
            }
        }

        private bool _canRemoveNameFilter;
        public bool CanRemoveNameFilter
        {
            get { return _canRemoveNameFilter; }
            set { Set(ref _canRemoveNameFilter, value); }
        }
        #endregion

        private bool? _isHeadFilterValue;
        public bool? IsHeadFilterValue
        {
            get { return _isHeadFilterValue; }
            set
            {
                if (this.Set(ref _isHeadFilterValue, value))
                {
                    ApplyFilter(_isHeadFilterValue.HasValue ? FilterField.IsHead : FilterField.None);
                }
            }
        }

        private string _nameFilterValue;
        public string NameFilterValue
        {
            get { return _nameFilterValue; }
            set
            {
                if (this.Set(ref _nameFilterValue, value))
                {
                    if (!string.IsNullOrWhiteSpace(_nameFilterValue))
                        ApplyFilter(FilterField.Name);
                    else
                        RemoveNameFilterCommand.Execute(null);
                }
            }
        }

        GroupFilter filters = new GroupFilter();

        private enum FilterField
        {
            IsHead,
            Name,
            None
        }

        private void ApplyFilter(FilterField field)
        {
            switch (field)
            {
                case FilterField.IsHead:
                    AddIsHeadFilter();
                    break;

                case FilterField.Name:
                    AddNameFilter();
                    break;

                default:
                    break;
            }
        }

        public async Task ResetFiltersAsync()
        {
            // clear filters 
            await RemoveIsHeadFilterAsync();
            await RemoveNameFilterAsync();
        }

        Predicate<object> _isHeadFilter;
        Predicate<object> IsHeadFilter
        {
            get { return _isHeadFilter ?? (_isHeadFilter = new Predicate<object>(FilterByIsHead)); }
        }

        Predicate<object> _nameFilter;
        Predicate<object> NameFilter
        {
            get { return _nameFilter ?? (_nameFilter = new Predicate<object>(FilterByName)); }
        }

        private bool FilterByIsHead(object obj)
        {
            holderinfo resident = obj as holderinfo;
            if (resident != null)
            {
                if (IsHeadFilterValue.HasValue && IsHeadFilterValue == true)
                    return resident.C_isholder.HasValue && resident.C_isholder != 0;
                else
                    return resident.C_isholder == null || resident.C_isholder == 0;
            }
            return false;
        }

        private bool FilterByName(object obj)
        {
            holderinfo resident = obj as holderinfo;
            if (resident != null && resident.C_name != null && resident.C_name.Contains(NameFilterValue))
            {
                return true;
            }
            return false;
        }

        public async Task RemoveIsHeadFilterAsync()
        {
            await Task.Run(() =>
            {
                filters.RemoveFilter(IsHeadFilter);
                IsHeadFilterValue = null;
                CanRemoveIsHeadFilter = false;
            });
            Residents.Filter = filters.Filter;
        }

        public async Task RemoveNameFilterAsync()
        {
            //DebugLog.TraceMessage("");
            await Task.Run(() =>
            {
                filters.RemoveFilter(NameFilter);
                //NameFilterValue = null;
                CanRemoveNameFilter = false;
            });
            //DebugLog.TraceMessage("");
            NameFilterValue = null;
            Residents.Filter = filters.Filter;
        }

        //public async void RemoveNameFilterAsync_()
        //{
        //    await RemoveNameFilterAsync();
        //}

        public void AddIsHeadFilter()
        {
            // see Notes on Adding Filters:
            if (CanRemoveIsHeadFilter)
            {
                filters.RemoveFilter(IsHeadFilter);
                filters.AddFilter(IsHeadFilter);
                Residents.Filter = filters.Filter;
            }
            else
            {
                filters.AddFilter(IsHeadFilter);
                Residents.Filter = filters.Filter;
                CanRemoveIsHeadFilter = true;
            }
        }

        public void AddNameFilter()
        {
            // see Notes on Adding Filters:
            if (CanRemoveNameFilter)
            {
                filters.RemoveFilter(NameFilter);
                filters.AddFilter(NameFilter);
                Residents.Filter = filters.Filter;
            }
            else
            {
                filters.AddFilter(NameFilter);
                Residents.Filter = filters.Filter;
                CanRemoveNameFilter = true;
            }
        }
    }
}

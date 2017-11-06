using FluentValidation;
using GalaSoft.MvvmLight;
using ICMServer.WPF.Models;
using ICMServer.WPF.Validators;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ICMServer.WPF.ViewModels
{
    public abstract class SingleDataViewModelBase<TModel, TVeiwModel> : ValidatableViewModelBase where TModel : class, new()
    {
        protected TModel _data;
        protected ICollectionModel<TModel> _dataModel;    // data access layer

#if DEBUG
        public SingleDataViewModelBase()
        {
            if (ViewModelBase.IsInDesignModeStatic)
            {
                InitSampleData();
            }
        }

        protected abstract void InitSampleData();
#endif

        /// <summary>
        /// Initializes a new instance of the SingleDataViewModelBase class.
        /// </summary>
        public SingleDataViewModelBase(
            IValidator<TVeiwModel> validator,
            ICollectionModel<TModel> dataModel,
            TModel data = null)
        {
            this._dataModel = dataModel;
            this._data = data;
            if (data != null)
            {
                ModelToViewModel();
            }
            else
            {
                InitDefaultValue();
            }
            if (validator != null)
                this.Validator = new FluentValidationAdapter<TVeiwModel>(validator);
        }

        #region ValidateCommand
        private ICommand _validateCommand;
        public ICommand ValidateCommand
        {
            get
            {
                return _validateCommand ?? (_validateCommand = AsyncCommand.Create<bool>(() => validationTasks.Enqueue<bool>(() => this.ValidateAsync())));
            }
        }
        #endregion

        #region UpdateCommand
        private IAsyncCommand _updateCommand;
        public IAsyncCommand UpdateCommand
        {
            get
            {
                return _updateCommand ?? (_updateCommand = new AsyncCommand<object>(
                    async (param, _) => { if (Validate()) { await UpdateAsync(); }; return null; },
                    (() => this.HasErrors == false)));
            }
        }
        #endregion

        #region AddCommand
        private IAsyncCommand _addCommand;
        public IAsyncCommand AddCommand
        {
            get
            {
                return _addCommand ?? (_addCommand = new AsyncCommand<object>(
                    async (param, _) => { if (Validate()) { await AddAsync(); }; return null; },
                     (() => this.HasErrors == false)));
            }
        }
        #endregion

        protected override void OnValidationStateChanged(IEnumerable<string> changedProperties)
        {
            base.OnValidationStateChanged(changedProperties);
            this.AddCommand.RaiseCanExecuteChanged();
            this.UpdateCommand.RaiseCanExecuteChanged();
        }

        protected virtual async Task UpdateAsync()
        {
            if (this._dataModel != null && this._data != null)
            {
                ViewModelToModel();
                await this._dataModel.UpdateAsync(this._data);
            }
        }

        protected virtual async Task AddAsync()
        {
            this._data = new TModel();
            if (this._dataModel != null && this._data != null)
            {
                ViewModelToModel();
                await this._dataModel.InsertAsync(this._data);
            }
        }

        protected abstract void ModelToViewModel();

        protected abstract void ViewModelToModel();

        protected abstract void InitDefaultValue();
    }
}

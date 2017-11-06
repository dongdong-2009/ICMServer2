using FluentValidation;
using GalaSoft.MvvmLight.CommandWpf;
using ICMServer.WPF.Validators;
using System;
using System.Windows;
using System.Windows.Input;

namespace ICMServer.WPF.ViewModels
{
    public class DialogInputIPAddressViewModel : ValidatableViewModelBase
    {
        public DialogInputIPAddressViewModel(
            IValidator<DialogInputIPAddressViewModel> validator)
        {
            this.Validator = new FluentValidationAdapter<DialogInputIPAddressViewModel>(validator); 
        }

        private string _IPAddress;
        public string IPAddress
        {
            get { return _IPAddress; }
            set { this.Set(ref _IPAddress, value); }
        }

        public event EventHandler OkClicked = delegate { };
        public event EventHandler CancelClicked = delegate { };

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

        #region OkCommand
        private ICommand _OkCommand;
        public ICommand OkCommand
        {
            get
            {
                return _OkCommand ?? (_OkCommand = new RelayCommand<object>(
                    (window) =>
                    {
                        this.OkClicked(this, EventArgs.Empty);
                        this.CloseDialog(window as Window, true);
                    },
                    (window) => this.HasErrors == false));
            }
        }
        #endregion

        #region CancelCommand
        private ICommand _CancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                return _CancelCommand ?? (_CancelCommand = new RelayCommand<object>(
                    (window) =>
                    {
                        this.CancelClicked(this, EventArgs.Empty);
                        this.CloseDialog(window as Window, false);
                    }));
            }
        }
        #endregion

        public void CloseDialog(Window view, bool dialogResult)
        {
            if (view != null)
                view.DialogResult = dialogResult;
        }
    }
}

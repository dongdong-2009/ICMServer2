using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Threading;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

public abstract class AsyncCommandBase : ObservableObject, IAsyncCommand
{
    public abstract bool CanExecute(object parameter);

    public abstract Task ExecuteAsync(object parameter);

    public async void Execute(object parameter)
    {
        await ExecuteAsync(parameter);
    }

    public event EventHandler CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    public void RaiseCanExecuteChanged()
    {
        DispatcherHelper.CheckBeginInvokeOnUI(
            () => CommandManager.InvalidateRequerySuggested());
    }
    //public event EventHandler CanExecuteChanged;

    //public void RaiseCanExecuteChanged()
    //{
    //    DispatcherHelper.CheckBeginInvokeOnUI(
    //        () => CanExecuteChanged?.Invoke(this, EventArgs.Empty));
    //}

    //private bool _isExecuting = false;
    //public bool IsExecuting
    //{
    //    get { return _isExecuting; }
    //    private set { Set(ref _isExecuting, value); }
    //}

    protected sealed class CancelAsyncCommand : ICommand
    {
        private CancellationTokenSource _cts = new CancellationTokenSource();
        private bool _commandExecuting;
        private AsyncCommandBase _asyncCommand;

        public CancelAsyncCommand(AsyncCommandBase asyncCommand)
        {
            this._asyncCommand = asyncCommand;
        }

        public CancellationToken Token { get { return _cts.Token; } }

        public void NotifyCommandStarting()
        {
            //this._asyncCommand.IsExecuting =
            _commandExecuting = true;
            if (!_cts.IsCancellationRequested)
                return;
            _cts = new CancellationTokenSource();
            RaiseCanExecuteChanged();
        }

        public void NotifyCommandFinished()
        {
            //this._asyncCommand.IsExecuting =
            _commandExecuting = false;
            RaiseCanExecuteChanged();
        }

        bool ICommand.CanExecute(object parameter)
        {
            return _commandExecuting && !_cts.IsCancellationRequested;
        }

        void ICommand.Execute(object parameter)
        {
            _cts.Cancel();
            RaiseCanExecuteChanged();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
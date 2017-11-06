using GalaSoft.MvvmLight.Helpers;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

public class AsyncCommand<TResult> : AsyncCommandBase
{
    private readonly Func<object, CancellationToken, Task<TResult>> _command;
    private readonly CancelAsyncCommand _cancelCommand;
    private NotifyTaskCompletion<TResult> _execution;
    private readonly WeakFunc<bool> _canExecute;

    public AsyncCommand(Func<object, CancellationToken, Task<TResult>> command)
        : this(command, null)
    {
    }

    public AsyncCommand(Func<object, CancellationToken, Task<TResult>> command, Func<bool> canExecute)
    {
        _command = command;
        _cancelCommand = new CancelAsyncCommand(this);
        if (canExecute != null)
        {
            _canExecute = new WeakFunc<bool>(canExecute);
        }
    }

    public override bool CanExecute(object parameter)
    {
        return (Execution == null || Execution.IsCompleted) 
            && (_canExecute == null || (_canExecute.IsStatic || _canExecute.IsAlive)
            && _canExecute.Execute());
    }

    public override async Task ExecuteAsync(object parameter)
    {
        _cancelCommand.NotifyCommandStarting();
        Execution = new NotifyTaskCompletion<TResult>(_command(parameter, _cancelCommand.Token));
        this.RaiseCanExecuteChanged();
        await Execution.TaskCompletion;
        _cancelCommand.NotifyCommandFinished();
        this.RaiseCanExecuteChanged();
    }

    public ICommand CancelCommand
    {
        get { return _cancelCommand; }
    }

    public NotifyTaskCompletion<TResult> Execution
    {
        get { return _execution; }
        private set
        {
            Set(ref _execution, value);
        }
    }
}

public class AsyncCommand<T, TResult> : AsyncCommandBase
{
    private readonly Func<T, CancellationToken, Task<TResult>> _command;
    private readonly CancelAsyncCommand _cancelCommand;
    private NotifyTaskCompletion<TResult> _execution;
    private readonly WeakFunc<T, bool> _canExecute;

    public AsyncCommand(Func<T, CancellationToken, Task<TResult>> command)
        : this(command, null)
    {
    }

    public AsyncCommand(Func<T, CancellationToken, Task<TResult>> command, Func<T, bool> canExecute)
    {
        _command = command;
        _cancelCommand = new CancelAsyncCommand(this);
        if (canExecute != null)
        {
            _canExecute = new WeakFunc<T, bool>(canExecute);
        }
    }

    public override bool CanExecute(object parameter)
    {
        return (Execution == null || Execution.IsCompleted)
            && (_canExecute == null || (_canExecute.IsStatic || _canExecute.IsAlive)
            && _canExecute.Execute((T)parameter));
    }

    public override async Task ExecuteAsync(object parameter)
    {
        _cancelCommand.NotifyCommandStarting();
        Execution = new NotifyTaskCompletion<TResult>(_command((T)parameter, _cancelCommand.Token));
        RaiseCanExecuteChanged();
        await Execution.TaskCompletion;
        _cancelCommand.NotifyCommandFinished();
        RaiseCanExecuteChanged();
    }

    public ICommand CancelCommand
    {
        get { return _cancelCommand; }
    }

    public NotifyTaskCompletion<TResult> Execution
    {
        get { return _execution; }
        private set
        {
            Set(ref _execution, value);
        }
    }
}

public static class AsyncCommand
{
    public static AsyncCommand<object> Create(
        Func<Task> execute, 
        Func<bool> canExecute = null)
    {
        return new AsyncCommand<object>(async (param, _) => { await execute(); return null; }, canExecute);
    }

    public static AsyncCommand<object> Create(
        Func<object, Task> execute,
        Func<bool> canExecute = null)
    {
        return new AsyncCommand<object>(async (param, _) => { await execute(param); return null; }, canExecute);
    }

    public static AsyncCommand<TResult> Create<TResult>(
        Func<Task<TResult>> execute, 
        Func<bool> canExecute = null)
    {
        return new AsyncCommand<TResult>((param, _) => execute(), canExecute);
    }

    public static AsyncCommand<TResult> Create<TResult>(
        Func<object, Task<TResult>> execute,
        Func<bool> canExecute = null)
    {
        return new AsyncCommand<TResult>((param, _) => execute(param), canExecute);
    }

    //public static AsyncCommand<object> Create(
    //    Func<CancellationToken, Task> execute, 
    //    Func<bool> canExecute = null)
    //{
    //    return new AsyncCommand<object>(async (param, token) => { await execute(token); return null; }, canExecute);
    //}

    //public static AsyncCommand<object> Create(
    //   Func<object, CancellationToken, Task> execute,
    //   Func<bool> canExecute = null)
    //{
    //    return new AsyncCommand<object>(async (param, token) => { await execute(param, token); return null; }, canExecute);
    //}

    //public static AsyncCommand<TResult> Create<TResult>(
    //    Func<CancellationToken, Task<TResult>> execute,
    //    Func<bool> canExecute = null)
    //{
    //    return new AsyncCommand<TResult>(async (param, token) => await execute(token), canExecute);
    //}

    //public static AsyncCommand<TResult> Create<TResult>(
    //    Func<object, CancellationToken, Task<TResult>> execute,
    //    Func<bool> canExecute = null)
    //{
    //    return new AsyncCommand<TResult>(async (param, token) => await execute(param, token), canExecute);
    //}
}
using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;

namespace WPFApp.UI
{
    public class ReplayCommand : ICommand
    {

        readonly Action<object>? _execute;

        readonly Predicate<object>? _canExecute;

        public ReplayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public ReplayCommand(Action<object> execute) : this(execute, null)
        {
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public void Execute(object? parameter)
        {
            _execute?.Invoke(parameter);
        }
    }
}

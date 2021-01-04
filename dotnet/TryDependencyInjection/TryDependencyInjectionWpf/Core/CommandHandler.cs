using System;
using System.Windows.Input;

namespace TryDependencyInjectionWpf.Core
{
    public class CommandHandler : ICommand
    {
        private readonly Action _action;
        private readonly Func<bool> _canExecute;

        public CommandHandler(Action action, Func<bool>? canExecute = null)
        {
            _action = action;
            _canExecute = canExecute ?? (() => true);
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute.Invoke();
        }

        public void Execute(object parameter)
        {
            _action.Invoke();
        }
    }
}

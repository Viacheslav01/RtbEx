using System;
using System.Windows.Input;

namespace RtbEx
{
    internal class DelegateCommand
        : ICommand
    {
        private readonly Action _action;
        private readonly Func<bool> _canBeExcecuted;

        public DelegateCommand(Action action)
            : this(action, null)
        {
        }

        public DelegateCommand(Action action, Func<bool> canBeExcecuted)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            _action = action;
            _canBeExcecuted = canBeExcecuted;
        }

        public bool CanExecute(object parameter)
        {
            return _canBeExcecuted == null
                || _canBeExcecuted();
        }

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                _action();
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}
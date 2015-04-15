using System;
using System.Windows.Input;

namespace TailChaser.UI.ViewModels
{
    public abstract class CommandViewModel : ViewModelBase
    {
        private RelayCommand _command;
        private Action<object> _action;
        private Predicate<object> _predicate;

        public virtual ICommand Command
        {
            get
            {
                if (_command == null)
                {
                    _command = new RelayCommand(_action, _predicate);
                }
                return _command;
            }
        }

        protected CommandViewModel(Action<object> command, Predicate<object> canExecute, string name)
        {
            Name = name;
            _action = command;
            _predicate = canExecute;
        }
    }
}
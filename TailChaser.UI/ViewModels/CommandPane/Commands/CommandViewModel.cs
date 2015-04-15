using System;
using System.Windows.Input;

namespace TailChaser.UI.ViewModels.CommandPane.Commands
{
    public abstract class CommandViewModel : ViewModelBase
    {
        private RelayCommand _command;
        private readonly Action<object> _action;
        private readonly Predicate<object> _predicate;

        public virtual ICommand Command
        {
            get { return _command ?? (_command = new RelayCommand(_action, _predicate)); }
        }

        protected CommandViewModel(Action<object> command, Predicate<object> canExecute, string name)
        {
            Name = name;
            _action = command;
            _predicate = canExecute;
        }
    }
}
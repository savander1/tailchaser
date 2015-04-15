using System;
using System.Windows.Input;

namespace TailChaser.UI.ViewModels
{
    public class DeleteCommandViewModel : CommandViewModel
    {
        
        private RelayCommand _command;
        private Action<object> _action;

        public override ICommand Command { 
            get
            {
                if (_command == null)
                {
                    _command = new RelayCommand(param => _action, param => this.CanExafute);
                }
            } 
        }
        public DeleteCommandViewModel(Action<object> command) : base("Delete")
        {
            _action = command;
        }
    }
}
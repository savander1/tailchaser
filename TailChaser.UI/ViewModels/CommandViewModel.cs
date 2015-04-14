using System;
using System.Windows.Input;

namespace TailChaser.UI.ViewModels
{
    public abstract class CommandViewModel : ViewModelBase
    {
        public ICommand Command { get; protected set; }

        protected CommandViewModel(string name, ICommand command)
        {
            if (command == null) throw new ArgumentNullException("command");
            Name = name;
        }
    }
}
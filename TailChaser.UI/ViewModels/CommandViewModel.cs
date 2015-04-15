using System;
using System.Windows.Input;

namespace TailChaser.UI.ViewModels
{
    public abstract class CommandViewModel : ViewModelBase
    {
        public abstract ICommand Command { get; }

        protected CommandViewModel(string name)
        {
            Name = name;
        }
    }
}
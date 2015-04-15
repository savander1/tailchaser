using System;
using System.Windows.Input;

namespace TailChaser.UI.ViewModels
{
    public class SaveCommandViewModel : CommandViewModel
    {
        public SaveCommandViewModel(Action<object> command, Predicate<object> canExecute) : base(command, canExecute, "Save")
        {
        }
    }
}
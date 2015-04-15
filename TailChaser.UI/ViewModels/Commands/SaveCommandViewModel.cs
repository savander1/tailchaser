using System;

namespace TailChaser.UI.ViewModels.Commands
{
    public class SaveCommandViewModel : CommandViewModel
    {
        public SaveCommandViewModel(Action<object> command, Predicate<object> canExecute) : base(command, canExecute, "Save")
        {
        }
    }
}
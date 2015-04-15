using System;

namespace TailChaser.UI.ViewModels.Commands
{
    public class DeleteCommandViewModel : CommandViewModel
    {

        public DeleteCommandViewModel(Action<object> command, Predicate<object> canExecute) : base(command, canExecute, "Delete")
        {

        }
    }
}
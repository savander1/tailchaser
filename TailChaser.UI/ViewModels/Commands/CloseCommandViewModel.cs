using System;

namespace TailChaser.UI.ViewModels.Commands
{
    public class CloseCommandViewModel : CommandViewModel
    {
        public CloseCommandViewModel(Action<object> command, Predicate<object> canExecute) : base(command, canExecute, "Close")
        {
        }
    }
}

using System;

namespace TailChaser.UI.ViewModels
{
    public class RenameCommandViewModel: CommandViewModel
    {
        public RenameCommandViewModel(Action<object> command, Predicate<object> canExecute)
            : base(command, canExecute, "Save")
        {
        }
    }
}

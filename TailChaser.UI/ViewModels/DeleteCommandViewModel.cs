using System;
using System.Windows.Input;

namespace TailChaser.UI.ViewModels
{
    public class DeleteCommandViewModel : CommandViewModel
    {

        public DeleteCommandViewModel(Action<object> command, Predicate<object> canExecute) : base(command, canExecute, "Delete")
        {

        }
    }
}
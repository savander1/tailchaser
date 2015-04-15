using System;

namespace TailChaser.UI.ViewModels.CommandPane.Commands
{
    public class SettingsCommandViewModel : CommandViewModel
    {
        public SettingsCommandViewModel(Action<object> command, Predicate<object> canExecute) : base(command, canExecute, "Settings")
        {
        }
    }
}
using System;
using System.Windows.Input;

namespace TailChaser.UI.ViewModels
{
    public class SettingsCommandViewModel : CommandViewModel
    {
        public SettingsCommandViewModel(Action<object> command, Predicate<object> canExecute) : base(command, canExecute, "Settings")
        {
        }
    }
}
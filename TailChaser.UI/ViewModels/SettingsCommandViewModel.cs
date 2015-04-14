using System.Windows.Input;

namespace TailChaser.UI.ViewModels
{
    public class SettingsCommandViewModel : CommandViewModel
    {
        public SettingsCommandViewModel(ICommand command) : base("Settings", command)
        {
        }
    }
}
using System.Windows.Input;

namespace TailChaser.UI.ViewModels
{
    public class SaveCommandViewModel : CommandViewModel
    {
        public SaveCommandViewModel(ICommand command) : base("Save", command)
        {
        }
    }
}
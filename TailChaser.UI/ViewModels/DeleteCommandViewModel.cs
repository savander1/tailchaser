using System.Windows.Input;

namespace TailChaser.UI.ViewModels
{
    public class DeleteCommandViewModel : CommandViewModel
    {
        public DeleteCommandViewModel(ICommand command) : base("Delete", command)
        {
        }
    }
}
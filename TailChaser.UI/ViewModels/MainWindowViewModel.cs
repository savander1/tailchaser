namespace TailChaser.UI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public CommandPaneViewModel CommandPane { get; private set; }

        public MainWindowViewModel()
        {
            Name = "TailChaser";
            CommandPane = new CommandPaneViewModel();
        }
    }
}

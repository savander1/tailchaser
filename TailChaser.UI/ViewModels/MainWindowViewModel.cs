using System.Collections.Generic;
using System.Collections.ObjectModel;
using TailChaser.UI.ViewModels.CommandPane;
using TailChaser.UI.ViewModels.FilePane;

namespace TailChaser.UI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public CommandPaneViewModel CommandPane { get; private set; }
        public ICollection<FilePaneViewModel> FilePanes { get; private set; }

        public MainWindowViewModel()
        {
            Name = "TailChaser";
            CommandPane = new CommandPaneViewModel();
            FilePanes = new ObservableCollection<FilePaneViewModel>();
        }
    }
}

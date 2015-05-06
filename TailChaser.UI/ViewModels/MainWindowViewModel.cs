using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TailChaser.UI.ViewModels.CommandPane;
using TailChaser.UI.ViewModels.FilePane;
using TailChaser.UI.ViewModels.MenuPane;

namespace TailChaser.UI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public CommandPaneViewModel CommandPane { get; private set; }
        public ICollection<FilePaneViewModel> FilePanes { get; private set; }
        public MenuPaneViewModel MenuPane { get; set; }

        public MainWindowViewModel()
        {
            Name = "Tail Chaser";
            CommandPane = new CommandPaneViewModel();
            FilePanes = new ObservableCollection<FilePaneViewModel>();
            MenuPane = new MenuPaneViewModel(this);
        }

        public void Close()
        {
            OnRequestClose();
        }
    }
}

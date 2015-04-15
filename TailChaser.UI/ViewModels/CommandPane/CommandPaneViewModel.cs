using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TailChaser.UI.ViewModels.CommandPane
{
    public class CommandPaneViewModel : ViewModelBase
    {
        public ICollection<CommandPaneItemViewModel> CommandPaneItems { get; private set; }

        public CommandPaneViewModel()
        {
            CommandPaneItems = new ObservableCollection<CommandPaneItemViewModel>();
        }

        public void AddItem(CommandPaneItemViewModel item)
        {
            item.SetParent(this);
            CommandPaneItems.Add(item);
        }
    }
}
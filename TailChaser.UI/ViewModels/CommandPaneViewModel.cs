using System;
using System.Collections.Generic;

namespace TailChaser.UI.ViewModels
{
    public class CommandPaneViewModel : ViewModelBase
    {
        public List<CommandPaneItemViewModel> CommandPaneItems { get; private set; }

        public CommandPaneViewModel()
        {
            CommandPaneItems = new List<CommandPaneItemViewModel>();
        }

        public void AddItem(CommandPaneItemViewModel item)
        {
            item.SetParent(this);
            CommandPaneItems.Add(item);
        }
    }
}
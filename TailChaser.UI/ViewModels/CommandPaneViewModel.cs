using System;
using System.Collections.Generic;

namespace TailChaser.UI.ViewModels
{
    public class CommandPaneViewModel : ViewModelBase
    {
        public Guid ItemId { get; set; }
        public List<CommandPaneItemViewModel> CommandPaneItems { get; private set; }

        public CommandPaneViewModel(Guid itemId)
        {
            ItemId = itemId;
            CommandPaneItems = new List<CommandPaneItemViewModel>();
        }

        public void AddItem(CommandPaneItemViewModel item)
        {
            CommandPaneItems.Add(item);
        }
    }
}
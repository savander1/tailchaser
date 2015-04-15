using System;
using TailChaser.Entity.Configuration;
using TailChaser.UI.ViewModels.Commands;

namespace TailChaser.UI.ViewModels.CommandPane
{
    public class ContainerCommandPaneItemViewModel : CommandPaneItemViewModel
    {
        public ContainerCommandPaneItemViewModel(Item item)
            : base(item)
        {
            var deleteItem = new DeleteCommandViewModel(x => ((CommandPaneViewModel)Parent).CommandPaneItems.Remove(this), null);
            var renameItem = new RenameCommandViewModel(x => Parent.Name = (string)x, null);
            AddCommands(deleteItem, renameItem);
        }
    }
}
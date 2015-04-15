using System;
using TailChaser.UI.ViewModels.CommandPane.Commands;

namespace TailChaser.UI.ViewModels.CommandPane
{
    public class FileCommandPaneItemViewModel : CommandPaneItemViewModel
    {
        public FileCommandPaneItemViewModel(Guid itemId)
            : base(itemId)
        {
            var deleteItem = new DeleteCommandViewModel(x => Parent.CommandPaneItems.Remove(this), null);
            var renameItem = new RenameCommandViewModel(x => Parent.Name = (string)x, null);
            var saveItem = new SaveCommandViewModel(null, null);
            var settings = new SettingsCommandViewModel(x => Parent.Name = x.ToString(), null);
            AddCommands(deleteItem, renameItem, saveItem, settings);
        }
    }
}
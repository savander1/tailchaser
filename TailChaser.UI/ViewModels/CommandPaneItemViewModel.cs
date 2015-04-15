using System;
using System.Collections.Generic;

namespace TailChaser.UI.ViewModels
{
    public abstract class CommandPaneItemViewModel : ViewModelBase
    {
        protected CommandPaneViewModel Parent;
        public Guid ItemId { get; set; }
        public List<CommandViewModel> Commands { get; private set; }

        protected CommandPaneItemViewModel(Guid itemId)
        {
            Commands = new List<CommandViewModel>();
            ItemId = itemId;
        }

        public void SetParent(CommandPaneViewModel parent)
        {
            Parent = parent;
        }

        public void AddCommand(CommandViewModel command)
        {
            Commands.Add(command);
        }

        protected void AddCommands(params CommandViewModel[] commands)
        {
            foreach (var command in commands)
            {
                AddCommand(command);
            }
        }
    }

    public class ContainerCommandPaneItemViewModel : CommandPaneItemViewModel
    {
        public ContainerCommandPaneItemViewModel(Guid itemId)
            : base(itemId)
        {
            var deleteItem = new DeleteCommandViewModel(x => Parent.CommandPaneItems.Remove(this), null);
            var renameItem = new RenameCommandViewModel(x => Parent.Name = (string)x, null);
            AddCommands(deleteItem, renameItem);
        }
    }

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
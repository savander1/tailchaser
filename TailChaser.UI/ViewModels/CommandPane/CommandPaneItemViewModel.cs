using System.Collections.Generic;
using System.Collections.ObjectModel;
using TailChaser.Entity.Configuration;
using TailChaser.UI.ViewModels.CommandPane.Commands;

namespace TailChaser.UI.ViewModels.CommandPane
{
    public abstract class CommandPaneItemViewModel : ViewModelBase
    {
        protected CommandPaneViewModel Parent;
        public Item Item { get; private set; }
        public ICollection<CommandViewModel> Commands { get; private set; }

        protected CommandPaneItemViewModel(Item item)
        {
            Commands = new ObservableCollection<CommandViewModel>();
            Item = item;
        }

        protected internal void SetParent(CommandPaneViewModel parent)
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
}
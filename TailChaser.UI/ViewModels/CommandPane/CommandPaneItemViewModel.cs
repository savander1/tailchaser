using System;
using System.Collections.Generic;
using TailChaser.UI.ViewModels.CommandPane.Commands;

namespace TailChaser.UI.ViewModels.CommandPane
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
}
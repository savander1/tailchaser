using System;
using System.Collections.Generic;

namespace TailChaser.UI.ViewModels
{
    public class CommandPaneItemViewModel : ViewModelBase
    {
        public Guid ItemId { get; set; }
        public List<CommandViewModel> Commands { get; private set; }

        public CommandPaneItemViewModel(Guid itemId)
        {
            Commands = new List<CommandViewModel>();
            ItemId = itemId;
        }

        public void AddCommand(CommandViewModel command)
        {
            Commands.Add(command);
        }
    }

}
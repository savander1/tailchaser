using System.Collections.Generic;
using System.Collections.ObjectModel;
using TailChaser.UI.ViewModels.Commands;

namespace TailChaser.UI.ViewModels.CommandPane
{
    public abstract class CommandPaneItemViewModelBase : ViewModelBase
    {
        protected ViewModelBase Parent;
        
        public ICollection<CommandViewModel> Commands { get; protected set; }

        protected CommandPaneItemViewModelBase()
        {
            Commands = new ObservableCollection<CommandViewModel>();
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
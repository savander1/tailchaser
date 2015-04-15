using System.Collections.ObjectModel;
using TailChaser.Entity.Configuration;

namespace TailChaser.UI.ViewModels.CommandPane
{
    public abstract class CommandPaneItemViewModel : CommandPaneItemViewModelBase
    {
        public Item Item { get; private set; }
        protected CommandPaneItemViewModel(Item item)
        {
            Item = item;
        }

        protected internal void SetParent(CommandPaneViewModel parent)
        {
            Parent = parent;
        }
    }
}
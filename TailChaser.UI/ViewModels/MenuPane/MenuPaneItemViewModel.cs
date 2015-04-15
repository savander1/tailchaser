using TailChaser.UI.ViewModels.CommandPane;

namespace TailChaser.UI.ViewModels.MenuPane
{
    public abstract class MenuPaneItemViewModel : CommandPaneItemViewModelBase
    {
        protected MenuPaneItemViewModel(MainWindowViewModel parent)
        {
            Parent = parent;
        }
    }

}

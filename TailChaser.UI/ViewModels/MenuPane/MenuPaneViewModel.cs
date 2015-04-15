using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TailChaser.UI.ViewModels.MenuPane
{
    public class MenuPaneViewModel : ViewModelBase
    {
        public ICollection<MenuPaneItemViewModel> MenuPaneItems { get; private set; }

        public MenuPaneViewModel(MainWindowViewModel parent)
        {
            MenuPaneItems = new Collection<MenuPaneItemViewModel>
                {
                    new FileMenuPaneItemViewModel(parent)
                };
        }
    }
}

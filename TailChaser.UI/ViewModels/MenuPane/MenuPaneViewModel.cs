using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TailChaser.UI.ViewModels.MenuPane
{
    public class MenuPaneViewModel : ViewModelBase
    {
        public ICollection<MenuPaneItemViewModel> MenuPaneItems { get; set; }

        public MenuPaneViewModel(MainWindowViewModel parent)
        {
            MenuPaneItems = new ObservableCollection<MenuPaneItemViewModel>
                {
                    new FileMenuPaneItemViewModel(parent)
                };
        }
    }
}

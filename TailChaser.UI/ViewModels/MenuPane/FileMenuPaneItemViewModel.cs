using TailChaser.UI.ViewModels.Commands;

namespace TailChaser.UI.ViewModels.MenuPane
{
    public class FileMenuPaneItemViewModel : MenuPaneItemViewModel
    {
        public FileMenuPaneItemViewModel(MainWindowViewModel parent) : base(parent)
        {
            Name = "File";
            var saveItem = new SaveCommandViewModel(null, null);
            var closeItem = new CloseCommandViewModel(x => ((MainWindowViewModel)Parent).Close(), null);
            AddCommands(saveItem, closeItem);
        }
    }
}
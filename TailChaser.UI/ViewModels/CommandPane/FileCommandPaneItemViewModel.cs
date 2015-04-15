
using TailChaser.Entity.Configuration;
using TailChaser.UI.Dialogs;
using TailChaser.UI.ViewModels.CommandPane.Commands;
using TailChaser.UI.ViewModels.FilePane;

namespace TailChaser.UI.ViewModels.CommandPane
{
    public class FileCommandPaneItemViewModel : CommandPaneItemViewModel
    {
        public FileCommandPaneItemViewModel(Item item)
            : base(item)
        {
            var deleteItem = new DeleteCommandViewModel(x => Parent.CommandPaneItems.Remove(this), null);
            var renameItem = new RenameCommandViewModel(x => Parent.Name = (string)x, null);
            var saveItem = new SaveCommandViewModel(null, null);
            var settings = new SettingsCommandViewModel(FileSettings, null);
            AddCommands(deleteItem, renameItem, saveItem, settings);
        }

        private void FileSettings(object o)
        {
            var file = (File) Item;

            var viewModel = new FileSettingsViewModel(file);
            var dialog = new FileSettingsDialog(viewModel);

            dialog.FileOk += (sender, args) =>
                {
                    ((File) Item).PresentationSettings = args.Settings;
                    ((File)Item).Font = args.Font;
                    ((File)Item).FontSize = args.Size;
                    ((FileSettingsDialog) sender).Close();
                };

            dialog.ShowDialog();
        }
    }
}
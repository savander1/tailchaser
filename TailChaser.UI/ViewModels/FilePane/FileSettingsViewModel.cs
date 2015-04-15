using System.Collections.ObjectModel;
using TailChaser.Entity.Configuration;

namespace TailChaser.UI.ViewModels.FilePane
{
    public class FileSettingsViewModel : ViewModelBase
    {
        private readonly File _file;

        public ObservableCollection<Settings> Settings
        {
            get { return new ObservableCollection<Settings>(_file.PresentationSettings); }
            set { _file.PresentationSettings = value; }
        }

        public double FontSize
        {
            get { return _file.FontSize; }
            set { _file.FontSize = value; }
        }

        public string Font
        {
            get { return _file.Font; }
            set { _file.Font = value; }
        }

        public FileSettingsViewModel(File file)
        {
            _file = file;
        }
    }
}

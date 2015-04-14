using TailChaser.UI.Loaders;
using TailChaser.ViewModel;

namespace TailChaser.UI
{
    public class ViewModelLoader
    {
        private readonly ConfigLoader _configLoader;

        public ViewModelLoader(ConfigLoader configLoader)
        {
            _configLoader = configLoader;
        }

        public FileTreeViewModel GetTreeView()
        {
            var config = _configLoader.LoadConfiguration();

            return new FileTreeViewModel();
        }
    }
}

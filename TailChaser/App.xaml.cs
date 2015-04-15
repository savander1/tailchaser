using System.Windows;
using TailChaser.UI.Loaders;

namespace TailChaser
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var configLoader = new ConfigLoader();
            var config = configLoader.LoadConfiguration();
            
            var window = new MainWindow {DataContext = config};
            window.Show();
        }
    }
}

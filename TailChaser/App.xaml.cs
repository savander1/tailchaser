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

            var window = new UI.Views.MainWindow();
            var configLoader = new ConfigLoader();
            var config = configLoader.LoadConfiguration();

            config.RequestClose += window.Close;

            window.DataContext = config;

           window.Show();
        }
    }

}

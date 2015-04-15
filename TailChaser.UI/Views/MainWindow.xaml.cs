using System.Windows;

namespace TailChaser.UI.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        

        //protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        //{
        //    if (NeedsWarning(_configLoader.LoadConfiguration(), _settings))
        //    {
        //        MessageBoxResult result =
        //            MessageBox.Show("Your application settings have changed. Do you want to save your settings?",
        //                            "Warning", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
        //        if (result == MessageBoxResult.Yes)
        //        {
        //            _configLoader.SaveConfiguration(_settings);
        //            _fileManager.Dispose();
        //        }
        //        else if (result == MessageBoxResult.No)
        //        {
        //            _fileManager.Dispose();
        //        }
        //        else if (result == MessageBoxResult.Cancel)
        //        {
        //            e.Cancel = true;
        //        }
        //    }
        //}

        

    }
}

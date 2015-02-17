using System;
using System.Windows;
using Microsoft.Win32;
using TailChaser.Code;
using TailChaser.Entity;

namespace TailChaser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IGroupOpener _groupOpener;
        private readonly ConfigLoader _configLoader;
        private static Configuration _configuration;

        public MainWindow()
        {
            _groupOpener = new GroupOpener(); // TODO: use DI
            _configLoader = new ConfigLoader();
            InitializeComponent();
            LoadConfiguration();
            BindTree();
        }

        private void LoadConfiguration()
        {
            _configuration = _configLoader.LoadConfiguration();
            
        }

        private void NewMachine_Click(object sender, RoutedEventArgs e)
        {
            var machine = new Machine();
            _configuration.Machines.Add(machine);
            BindTree();
        }

        private void OpenGrouping_Click(object sender, RoutedEventArgs e)
        {
            var fileBrowserDialog = new OpenFileDialog
                {
                    DefaultExt = ".cfg",
                    Filter = "Machine (*.cfg) | All Files (*.*)",
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    Multiselect = true,
                    
                };


            fileBrowserDialog.ShowDialog();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            _configLoader.SaveConfiguration(_configuration);
        }

        private void BindTree()
        {
            MachineTreeView.ItemsSource = _configuration.Machines;
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (NeedsWarning(_configLoader.LoadConfiguration(), _configuration))
            {
                
            }
        }

        private bool NeedsWarning(Configuration savedConfiguration, Configuration currentConfiguration)
        {
            return !savedConfiguration.Equals(currentConfiguration);
        }
    }
}

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
            var machine = new Machine("New Machine");
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

        private void UiElement_OnMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var element = Mouse.DirectlyOver as Border;
            var contextMenu = new ContextMenu();

            if (element == null) return;

            if (element.DataContext.GetType() == typeof (Machine))
            {
                element.ContextMenu = contextMenu;
                contextMenu.Items.Add(new MenuItem { Header = "Add" });
                contextMenu.Items.Add(new MenuItem().Header = "Rename");
                contextMenu.Items.Add(new MenuItem().Header = "Delete");
            }
        }
    }
}

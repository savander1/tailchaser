using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
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
        private readonly ConfigLoader _configLoader;
        private static Configuration _configuration;

        public MainWindow()
        {
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
                MessageBoxResult result = MessageBox.Show("Your application settings have changed. Do you want to save your settings?", "Warning", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.OK)
                {
                    _configLoader.SaveConfiguration(_configuration);
                }
            }
        }

        private bool NeedsWarning(Configuration savedConfiguration, Configuration currentConfiguration)
        {
            return !savedConfiguration.ToString().Equals(currentConfiguration.ToString(), StringComparison.InvariantCultureIgnoreCase);
        }

        private void UiElement_OnMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var element = Mouse.DirectlyOver as TextBlock;
            
            if (element == null) return;
            //var source = (TreeViewItem)((TextBlock)e.OriginalSource).TemplatedParent;

            if (element.DataContext.GetType() == typeof (Machine) || element.DataContext.GetType() == typeof (Group))
            {
                element.ContextMenu = GetContextMenu(element.DataContext);
            }
        }

        private void ContextMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var item = (MenuItem) sender;
            var parameter = item.CommandParameter;

            if (item.Header.Equals(ContentMenuButtonType.Add.ToString()))
            {
                if (parameter.GetType() == typeof (Machine))
                {
                    _configuration.FindMachine((Machine) parameter).Groups.Add(new Group("New Group"));
                }
            }
            else if (item.Header.Equals(ContentMenuButtonType.Rename.ToString()))
            {
                
            }
            else if (item.Header.Equals(ContentMenuButtonType.Delete.ToString()))
            {
                if (parameter.GetType() == typeof(Machine))
                {
                    _configuration.FindMachine((Machine)parameter).Groups.Add(new Group("New Group"));
                    _configuration.Machines.Remove((Machine)parameter);
                }
            }
        }

        private ContextMenu GetContextMenu(object element)
        {
            var contextMenu = new ContextMenu();
            var add = new MenuItem {Header = ContentMenuButtonType.Add.ToString(), CommandParameter = element};
            add.Click += ContextMenuItem_Click;
            var rename = new MenuItem {Header = ContentMenuButtonType.Rename.ToString(), CommandParameter = element};
            rename.Click += ContextMenuItem_Click;
            var delete = new MenuItem {Header = ContentMenuButtonType.Delete.ToString(), CommandParameter = element};
            delete.Click += ContextMenuItem_Click;

            contextMenu.Items.Add(add);
            contextMenu.Items.Add(rename);
            contextMenu.Items.Add(delete);

            return contextMenu;
        }

        private void EventSetter_OnHandler(object sender, MouseButtonEventArgs e)
        {
            var element = Mouse.DirectlyOver as TextBlock;

            if (element == null) return;

            var parent = element.Parent;

            if (parent.GetType() == typeof (StackPanel))
            {
                var txt = (TextBox)((StackPanel)parent).Children[1];
                txt.Visibility = Visibility.Visible;
                txt.SelectedText = element.Text;
                txt.Focus();
                element.Visibility = Visibility.Collapsed;
            }
            

        }

        protected void Txtbox_LostFocus(object sender, RoutedEventArgs e)
        {
            var tb = (TextBlock)((StackPanel)((TextBox)sender).Parent).Children[0];
            tb.Text = ((TextBox)sender).Text;
            tb.Visibility = Visibility.Visible;
            ((TextBox)sender).Visibility = Visibility.Collapsed;
        }
    }

    internal enum ContentMenuButtonType
    {
        Add, Rename, Delete
    }
}

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
        private readonly ConfigLoader _configLoader;
        private static Configuration _settings;
        private readonly FileManager _fileManager;

        public MainWindow()
        {
            _configLoader = new ConfigLoader();
            _fileManager = new FileManager();
            InitializeComponent();
            LoadConfiguration();
            BindTree();
        }

        private void LoadConfiguration()
        {
            _settings = _configLoader.LoadConfiguration();
            StartWatchingTailedFiles();
        }

        private void StartWatchingTailedFiles()
        {
            foreach (var file in _settings.Files)
            {
                _fileManager.WatchFile(file);
            }
        }

        private void NewMachine_Click(object sender, RoutedEventArgs e)
        {
            var machine = new Machine("New Machine");
            _settings.Machines.Add(machine);
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
            _configLoader.SaveConfiguration(_settings);
        }

        private void BindTree()
        {
            MachineTreeView.ItemsSource = _settings.Machines;
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (NeedsWarning(_configLoader.LoadConfiguration(), _settings))
            {
                MessageBoxResult result =
                    MessageBox.Show("Your application settings have changed. Do you want to save your settings?",
                                    "Warning", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    _configLoader.SaveConfiguration(_settings);
                    _fileManager.Dispose();
                }
                else if (result == MessageBoxResult.No)
                {
                    _fileManager.Dispose();
                }
                else if (result == MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        private bool NeedsWarning(Configuration savedConfiguration, Configuration currentConfiguration)
        {
            return !savedConfiguration.Equals(currentConfiguration);
        }

        private void UiElement_OnMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var element = Mouse.DirectlyOver as TextBlock;
            
            if (element == null) return;

            element.ContextMenu = GetContextMenu(element.DataContext);
        }

        private void ContextMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var item = (MenuItem) sender;
            var parameter = item.CommandParameter;

            if (item.Header.Equals(ContentMenuButtonType.Add.ToString()))
            {
                if (parameter.GetType() == typeof (Machine))
                {
                    _settings.FindMachine(((Machine)parameter).Id).Groups.Add(new Group("New Group"));
                }
                if (parameter.GetType() == typeof(Group))
                {
                    var dialog = new OpenFileDialog
                        {
                            Filter = "Log Files (*.txt, *.log)|*.txt;*.log|All files (*.*)|*.*",
                            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer),
                            Multiselect = true,
                        };

                    dialog.FileOk += (o, args) =>
                        {
                            if (!args.Cancel)
                            {
                                foreach (var filename in dialog.FileNames)
                                {
                                    var file = new TailedFile(filename);
                                    _settings.FindGroup(((Group)parameter).Id).AddFile(file);
                                    _fileManager.WatchFile(file);
                                }
                            }
                        };

                    dialog.ShowDialog();
                }
            }
            else if (item.Header.Equals(ContentMenuButtonType.Delete.ToString()))
            {
                if (parameter.GetType() == typeof(Machine))
                {
                    _settings.FindMachine(((Machine)parameter).Id).Groups.Add(new Group("New Group"));
                    _settings.Machines.Remove((Machine)parameter);
                }
            }
        }

        private ContextMenu GetContextMenu(object element)
        {
            var contextMenu = new ContextMenu();
            var add = new MenuItem { Header = ContentMenuButtonType.Add.ToString(), CommandParameter = element };
            add.Click += ContextMenuItem_Click;
            var delete = new MenuItem { Header = ContentMenuButtonType.Delete.ToString(), CommandParameter = element };
            delete.Click += ContextMenuItem_Click;
            var settings = new MenuItem { Header = ContentMenuButtonType.Settings.ToString(), CommandParameter = element };
            settings.Click += ContextMenuItem_Click;

            if (element.GetType() == typeof(Machine) || element.GetType() == typeof(Group))
            {
                contextMenu.Items.Add(add);
                contextMenu.Items.Add(delete);
            }
            else if (element.GetType() == typeof (TailedFile))
            {
                contextMenu.Items.Add(settings);
                contextMenu.Items.Add(delete);
            }

            return contextMenu;
        }

        private void EventSetter_OnHandler(object sender, MouseButtonEventArgs e)
        {
            var element = e.OriginalSource as TextBlock;

            if (element == null) return;

            var parent = element.Parent;

            if (parent.GetType() == typeof (StackPanel))
            {
                var txt = (TextBox)((StackPanel)parent).Children[2];
                txt.Visibility = Visibility.Visible;
                txt.SelectAll();
                txt.Focus();
                txt.KeyUp += (o, args) =>
                    {
                        if (args.Key == Key.Enter)
                        {
                            ((StackPanel)parent).Children[1].Focus();
                        }
                    };
                element.Visibility = Visibility.Collapsed;
            }

            var item = (TreeViewItem) sender;
            var expandable = item.DataContext as IExpandable;
            if (expandable != null)
            {
                item.IsExpanded = expandable.Expanded;
            }
            
        }

        protected void Txtbox_LostFocus(object sender, RoutedEventArgs e)
        {
            var tb = (TextBlock)((StackPanel)((TextBox)sender).Parent).Children[1];
            tb.Text = ((TextBox)sender).Text;
            tb.Visibility = Visibility.Visible;
            ((TextBox)sender).Visibility = Visibility.Collapsed;
        }

        private void TreeviewItem_OnSelected(object sender, RoutedEventArgs e)
        {
            var item = (TreeViewItem) e.OriginalSource;

            if (item.Header.GetType() == typeof (TailedFile))
            {
                 //load file in rtf.
                ContentBox.DataContext = item.Header;
            }
        }

        private void ContentBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            ((TextBox) sender).CaretIndex = ((TextBox) sender).Text.Length;
            ((TextBox) sender).ScrollToEnd();
        }
    }

    internal enum ContentMenuButtonType
    {
        Add, Rename, Delete, Settings
    }
}

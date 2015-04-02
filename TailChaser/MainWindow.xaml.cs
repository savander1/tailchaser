using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;
using TailChaser.Entity;
using TailChaser.UI;
using TailChaser.UI.UiHelpers;

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
            _fileManager.UiUpdate += (s, e) => Dispatcher.Invoke(() => ContentBox_OnTextChanged(s, e));
            InitializeComponent();
            LoadConfiguration();
            BindTree();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            StartWatchingTailedFiles();
        }

        private void LoadConfiguration()
        {
            _settings = _configLoader.LoadConfiguration();
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

            element.ContextMenu = ContextMenuHelper.GetContextMenu(element.DataContext, ContextMenuItem_Click);
        }

        private void ContextMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var item = (MenuItem) sender;
            var parameter = item.CommandParameter;

            if (item.Header.Equals(ContextMenuButtonType.Add.ToString()))
            {
                AddClicked(parameter);
            }
            else if (item.Header.Equals(ContextMenuButtonType.Delete.ToString()))
            {
                DeleteClicked(parameter);
            }
            else if (item.Header.Equals(ContextMenuButtonType.Settings.ToString()))
            {
                SettingsClicked(parameter);
            }
        }

        private void AddClicked(object parameter)
        {
            if (parameter.GetType() == typeof (Machine))
            {
                _settings.FindMachine(((Machine)parameter).Id).Groups.Add(new Group("New Group", ((Machine)parameter).Id));
            }
            if (parameter.GetType() == typeof (Group))
            {
                var group = (Group) parameter;
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
                                var file = new TailedFile(filename, group.Id);
                                _settings.FindGroup(((Group) parameter).Id).AddFile(file);
                                _fileManager.WatchFile(file);
                            }
                        }
                    };

                dialog.ShowDialog();
            }
        }

        private void DeleteClicked(object parameter)
        {
            if (parameter.GetType() == typeof(Machine))
            {
                var machine = (Machine)parameter;
                foreach (var group in machine.Groups)
                {
                    foreach (var file in @group.Files)
                    {
                        _fileManager.UnWatchFile(file);
                    }
                }
                _settings.Machines.Remove(machine);
            }

            if (parameter.GetType() == typeof(Group))
            {
                var group = (Group)parameter;
                foreach (var file in @group.Files)
                {
                    _fileManager.UnWatchFile(file);
                }
                _settings.Machines.Single(x => x.Id == @group.ParentId).Groups.Remove(@group);
            }

            if (parameter.GetType() == typeof(TailedFile))
            {
                var file = (TailedFile)parameter;
                _fileManager.UnWatchFile(file);
                var group = _settings.FindGroup(file.ParentId);
                @group.Files.Remove(file);
            }
        }

        private void SettingsClicked(object parameter)
        {
            if (parameter.GetType() == typeof(TailedFile))
            {
                var file = (TailedFile)parameter;

                var dialog = new FileSettingsDialog(file.PresentationSettings);
                dialog.FileOk += (o, args) =>
                {
                    if (!args.Cancel)
                    {
                        file.PresentationSettings = dialog.Settings;
                    }
                };

                dialog.ShowDialog();
            }
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
                item.ExpandSubtree();
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
                ContentBox.DataContext = item.Header;
                ContentBox.ScrollToEnd(); 
            }
        }

        private void ContentBox_OnTextChanged(object sender, FileChangeEventArgs e)
        {
            ContentBox.DataContext = e.File;
            var meep = e.File.FileContent;
            ContentBox.Document = e.File.PresentedFile;
            ContentBox.ScrollToEnd();
        }

        private void ContentBox_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //Dispatcher.Invoke(() => { ((RichTextBox) sender).Document = ((TailedFile) e.NewValue).PresentedFile; });
        }
    }
}

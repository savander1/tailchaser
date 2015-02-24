﻿using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;
using TailChaser.Code;
using TailChaser.Entity;
using TailChaser.Tail;

namespace TailChaser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ConfigLoader _configLoader;
        private static Configuration _configuration;
        private readonly Tailer _tailer;

        public MainWindow()
        {
            _configLoader = new ConfigLoader();
            _tailer = new Tailer();
            InitializeComponent();
            LoadConfiguration();
            BindTree();
        }

        private void LoadConfiguration()
        {
            _configuration = _configLoader.LoadConfiguration();
            StartWatchingTailedFiles();
        }

        private void StartWatchingTailedFiles()
        {
            foreach (var file in from machine in _configuration.Machines 
                                 from @group in machine.Groups 
                                 from file in @group.Files 
                                 select file)
            {
                _tailer.TailFile(file.FullName);
            }
        }

        private void NewMachine_Click(object sender, RoutedEventArgs e)
        {
            var machine = new Machine("New Machine");
            _configuration.Machines.Add(machine);
            //BindTree();
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
                MessageBoxResult result = MessageBox.Show("Your application settings have changed. Do you want to save your settings?", "Warning", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    _configLoader.SaveConfiguration(_configuration);
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
                    _configuration.FindMachine(((Machine)parameter).Id).Groups.Add(new Group("New Group"));
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
                                    _configuration.FindGroup(((Group)parameter).Id).AddFile(new TailedFile(filename));
                                    _tailer.TailFile(filename);
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
                    _configuration.FindMachine(((Machine)parameter).Id).Groups.Add(new Group("New Group"));
                    _configuration.Machines.Remove((Machine)parameter);
                }
            }
        }

        private ContextMenu GetContextMenu(object element)
        {
            var contextMenu = new ContextMenu();
            var add = new MenuItem {Header = ContentMenuButtonType.Add.ToString(), CommandParameter = element};
            add.Click += ContextMenuItem_Click;
            var delete = new MenuItem {Header = ContentMenuButtonType.Delete.ToString(), CommandParameter = element};
            delete.Click += ContextMenuItem_Click;

            contextMenu.Items.Add(add);
            contextMenu.Items.Add(delete);

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
            //item.IsSelected = false;
//            item.ExpandSubtree();

            if (item.Header.GetType() == typeof (TailedFile))
            {
                 //load file in rtf.
                //item.IsSelected = true;

            }
            
        }
    }

    internal enum ContentMenuButtonType
    {
        Add, Rename, Delete
    }
}

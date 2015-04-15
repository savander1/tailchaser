using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TailChaser.Entity;
using TailChaser.Entity.Configuration;
using TailChaser.Entity.EventArgs;
using TailChaser.UI.UiHelpers;
using TailChaser.UI.ViewModels.FilePane;

namespace TailChaser.UI.Dialogs
{
    /// <summary>
    /// Interaction logic for FileSettingsDialog.xaml
    /// </summary>
    public partial class FileSettingsDialog : Window
    {
        public event FileSettingsDialogEventHandler FileOk;

        public FileSettingsViewModel Settings { get; private set; }

        public FileSettingsDialog(FileSettingsViewModel settings)
        {
            InitializeComponent();
            Settings = settings;
            Font.SelectedValue = Settings.Font;
            BindFontSize();
            BindSettings();
            SampleText.DataContext = Settings;
        }

        private void BindFontSize()
        {
            var doubles = new List<double>();

            for (var i = 8; i <= 20; i++)
            {
                doubles.Add(i);
            }
            for (var i = 22; i >= 40; i += 2)
            {
                doubles.Add(i);
            }
            for (var i = 48; i < 100; i += 8)
            {
                doubles.Add(i);
            }

            FontSize.ItemsSource = doubles;
            FontSize.SelectedValue = Settings.FontSize;
        }

        private void BindSettings(Settings selectedItem = null)
        {
            RegexList.ItemsSource = Settings.Settings;
            if (Settings.Settings.Any())
            {
                RegexList.SelectedItem = selectedItem ?? Settings.Settings.First();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var cancelled = ((Button) sender).Content.Equals("Cancel");

            if (FileOk != null)
            {
                FileOk(this, new FileSettingsDialogEventArgs(cancelled, Settings.Settings, Settings.FontSize, Settings.Font));
            }

            Close();
        }
   
        private void Font_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (FontFamily)((ComboBox) sender).SelectedItem;

            Settings.Font = item.FamilyNames.First().Value;
        }

        private void FontSize_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (double)((ComboBox)sender).SelectedItem;

            Settings.FontSize = item;
        }

        private void RegexList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (Settings) ((ListBox) sender).SelectedItem;
            if (item == null) return;

            var backColor = FilePresentationSettingsHelper.GetBackgroundColor(item);
           
            BackColor.Fill = new SolidColorBrush(backColor);
            ExpressionBox.DataContext = item;
            TextColor.DataContext = item;
        }

        private void ToolBar_OnClick(object sender, RoutedEventArgs e)
        {
            var button = (Button) sender;
            var selectedItem = (Settings)RegexList.SelectedItem;
            var currentIndex = Settings.Settings.IndexOf(selectedItem);

            switch (button.Name)
            {
                case "AddExpression":
                    var color = ((SolidColorBrush) BackColor.Fill).Color;
                    var setting = new Settings
                        {
                            Expression = ExpressionBox.Text,
                            Alpha = color.A,
                            Blue = color.B,
                            Green = color.G,
                            Red = color.R,
                            TextColor = 1
                        };
                    Settings.Settings.Add(setting);
                    BindSettings(setting);
                    break;
                case "RemoveExpression":
                    Settings.Settings.Remove(selectedItem);
                    BindSettings();
                    break;
                case "OrderExpressionUp":
                    if ((currentIndex + 1) <= (Settings.Settings.Count - 1))
                    {
                        Settings.Settings.Move(currentIndex, currentIndex + 1);
                    }
                    BindSettings(selectedItem);
                    break;
                case "OrderExpressionDown":
                    if ((currentIndex - 1) >= 0)
                    {
                        Settings.Settings.Move(currentIndex, currentIndex - 1);
                    }
                    BindSettings(selectedItem);
                    break;
                case "SaveExpression":
                    BindSettings(selectedItem);
                    break;
            }
        }

        private void TextColor_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var colorPicker = new Microsoft.Samples.CustomControls.ColorPickerDialog
                {
                    Owner = this
                };
            var selectedItem = (Settings)RegexList.SelectedItem;
            colorPicker.StartingColor = selectedItem != null
                                            ? FilePresentationSettingsHelper.GetBackgroundColor(selectedItem)
                                            : Color.FromRgb(255,255,255);

            var result = colorPicker.ShowDialog();
            if (result.HasValue && result.Value)
            {
                var color = colorPicker.SelectedColor;
                if (selectedItem != null)
                {
                    selectedItem.Alpha = color.A;
                    selectedItem.Blue = color.B;
                    selectedItem.Green = color.G;
                    selectedItem.Red = color.R;
                }
                BackColor.Fill = new SolidColorBrush(color);
            }
        }

        private void ToolBar_Loaded(object sender, RoutedEventArgs e)
        {
            var toolBar = sender as ToolBar;
            if (toolBar == null) return;
            var overflowGrid = toolBar.Template.FindName("OverflowGrid", toolBar) as FrameworkElement;
            if (overflowGrid != null)
            {
                overflowGrid.Visibility = Visibility.Collapsed;
            }
            var mainPanelBorder = toolBar.Template.FindName("MainPanelBorder", toolBar) as FrameworkElement;
            if (mainPanelBorder != null)
            {
                mainPanelBorder.Margin = new Thickness();
            }
        }
    }
}

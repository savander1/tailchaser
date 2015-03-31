using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TailChaser.Entity;

namespace TailChaser
{
    /// <summary>
    /// Interaction logic for FileSettingsDialog.xaml
    /// </summary>
    public partial class FileSettingsDialog : Window
    {
        public event FileSettingsDialogEventHandler FileOk;

        public FilePresentationSettings Settings { get; private set; }

        public FileSettingsDialog(FilePresentationSettings settings)
        {
            InitializeComponent();
            Settings = settings;
            Font.SelectedValue = Settings.FontFamily;
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

        private void BindSettings(FilePresentationSetting selectedItem = null)
        {
            RegexList.ItemsSource = Settings.FileSettings;
            if (Settings.FileSettings.Any())
            {
                RegexList.SelectedItem = selectedItem ?? Settings.FileSettings.First();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var cancelled = ((Button) sender).Content.Equals("Cancel");

            if (FileOk != null)
            {
                FileOk(this, new FileSettingsDialogEventHandlerArgs(cancelled, Settings));
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
            var item = (FilePresentationSetting) ((ListBox) sender).SelectedItem;
            if (item == null) return;

            var backColor= Color.FromArgb((byte) item.Alpha, (byte) item.Red, (byte) item.Green, (byte) item.Blue);
            var textColor = UI.ColorInverter.InvertColor(backColor);
            
            TextColor.Fill = new SolidColorBrush(textColor);
            BackColor.Fill = new SolidColorBrush(backColor);
            ExpressionBox.Text = item.Expression;

        }

        private void ToolBar_OnClick(object sender, RoutedEventArgs e)
        {
            var button = (Button) sender;
            var selectedItem = (FilePresentationSetting)RegexList.SelectedItem;
            var currentIndex = Settings.FileSettings.IndexOf(selectedItem);

            switch (button.Name)
            {
                case "AddExpression":
                    var color = ((SolidColorBrush) BackColor.Fill).Color;
                    var setting = new FilePresentationSetting
                        {
                            Expression = ExpressionBox.Text,
                            Alpha = color.A,
                            Blue = color.B,
                            Green = color.G,
                            Red = color.R
                        };
                    Settings.FileSettings.Add(setting);
                    BindSettings(setting);
                    break;
                case "RemoveExpression":
                    Settings.FileSettings.Remove(selectedItem);
                    BindSettings();
                    break;
                case "OrderExpressionUp":
                    if ((currentIndex + 1) <= (Settings.FileSettings.Count - 1))
                    {
                        Settings.FileSettings.Move(currentIndex, currentIndex + 1);
                    }
                    BindSettings(selectedItem);
                    break;
                case "OrderExpressionDown":
                    if ((currentIndex - 1) >= 0)
                    {
                        Settings.FileSettings.Move(currentIndex, currentIndex - 1);
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
            //open a color picker dialog
        }
    }
}

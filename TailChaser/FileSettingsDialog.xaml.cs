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
    }
}

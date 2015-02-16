using System;
using System.Collections.Generic;
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

        public MainWindow()
        {
            _groupOpener = new GroupOpener(); // TODO: use DI
            InitializeComponent();
        }

        private void NewGrouping_Click(object sender, RoutedEventArgs e)
        {
            var group = new Group();

        }

        private void OpenGrouping_Click(object sender, RoutedEventArgs e)
        {
            var fileBrowserDialog = new OpenFileDialog
                {
                    DefaultExt = ".cfg",
                    Filter = "Group (*.cfg) | All Files (*.*)",
                    InitialDirectory = Environment.SpecialFolder.MyDocuments.ToString(),
                    Multiselect = true,
                    
                };


            fileBrowserDialog.ShowDialog();
        }
    }
}

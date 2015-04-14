using System;
using System.ComponentModel;
using System.Windows;
using TailChaser.Entity;

namespace TailChaser.ViewModels
{
    public class TailedFileViewModel : ViewModelBase
    {
        private readonly TailedFile _file;
        public event CancelEventHandler Close;
        public event EventHandler<RoutedEventArgs> OnLoad;


        public TailedFileViewModel(TailedFile file)
        {
            if (file == null) throw new ArgumentNullException("file");
            _file = file;
        }

        protected virtual void OnOnLoad(RoutedEventArgs e)
        {
            var handler = OnLoad;
            if (handler != null) handler(this, e);
        }

        protected virtual void OnClose(CancelEventArgs e)
        {
            var handler = Close;
            if (handler != null) handler(this, e);
        }
    }
}

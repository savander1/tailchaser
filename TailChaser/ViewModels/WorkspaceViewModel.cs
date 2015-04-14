using System;
using System.ComponentModel;
using System.Windows.Input;

namespace TailChaser.ViewModels
{
    public abstract class WorkspaceViewModel : ViewModelBase
    {
        public event CancelEventHandler RequestClose;

        protected virtual void OnRequestClose(CancelEventArgs args)
        {
            var handler = RequestClose;
            if (handler != null) handler(this, args);
        }
    }
}

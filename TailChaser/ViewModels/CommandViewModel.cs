using System;
using System.Windows.Input;

namespace TailChaser.ViewModels
{
    public abstract class CommandViewModel : ViewModelBase, IRenamable
    {
        public ICommand Command { get; private set; }
        public string ImageSource { get; private set; }
        public bool IsRenaming { get; set; }
        public bool IsRenameable { get; protected set; }

        protected CommandViewModel(string displayName, string imgSrc, ICommand command)
        {
            if (command == null) throw new ArgumentNullException("command");

            DisplayName = displayName;
            Command = command;
            ImageSource = imgSrc;
        }
    }
}
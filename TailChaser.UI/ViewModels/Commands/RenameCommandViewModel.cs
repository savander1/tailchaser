﻿using System;

namespace TailChaser.UI.ViewModels.Commands
{
    public class RenameCommandViewModel: CommandViewModel
    {
        public RenameCommandViewModel(Action<object> command, Predicate<object> canExecute)
            : base(command, canExecute, "Save")
        {
        }
    }
}
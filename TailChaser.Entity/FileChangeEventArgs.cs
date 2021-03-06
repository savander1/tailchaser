﻿using System;
using System.Windows.Documents;

namespace TailChaser.Entity
{
    public class FileChangeEventArgs : EventArgs
    {
        public TailedFile File { get; private set; }

        public FileChangeEventArgs(TailedFile file)
        {
            File = file;
        }
    }
}

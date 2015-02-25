using System;
using System.Collections.Generic;
using DiffMatchPatch;

namespace TailChaser.Entity
{
    public class FileChangeEventArgs : EventArgs
    {
        public List<Patch> Patches;
    }
}

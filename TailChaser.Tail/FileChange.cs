using System;
using System.IO;

namespace TailChaser.Tail
{
    public class FileChange
    {
        public string FilePath { get; set; }
        public DateTime ChangeDetected { get; set; }
        public WatcherChangeTypes Type { get; set; }
    }
}

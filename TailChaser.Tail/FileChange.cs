using System;

namespace TailChaser.Tail
{
    public class FileChange
    {
        public string FilePath { get; set; }
        public DateTime ChangeDetected { get; set; }
    }
}

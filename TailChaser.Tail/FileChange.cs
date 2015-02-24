using System;
using System.IO;

namespace TailChaser.Tail
{
    public class FileChange
    {
        public string FilePath { get; set; }
        public DateTime ChangeDetected { get; set; }
        public WatcherChangeTypes Type { get; set; }

        public FileChange(string path, DateTime changed, WatcherChangeTypes type)
        {
            FilePath = path;
            ChangeDetected = changed;
            Type = type;
        }

        public FileChange(string path, WatcherChangeTypes type) : this(path, DateTime.UtcNow, type) {}

        public FileChange(string path) : this(path, WatcherChangeTypes.Created) {}

    }
}

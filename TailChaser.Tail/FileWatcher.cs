using System;
using System.Collections.Generic;
using System.IO;

namespace TailChaser.Tail
{
    internal class FileWatcher
    {
        private readonly Queue<FileChange> _queue;

        private readonly FileSystemWatcher _watcher;

        public FileWatcher(string file, Queue<FileChange> queue)
        {
            _queue = queue;
            _watcher = new FileSystemWatcher();
            InitWatcher(file);
            _queue.Enqueue(new FileChange{ChangeDetected = DateTime.UtcNow, FilePath = file});
            _watcher.EnableRaisingEvents = true;
        }

        private void InitWatcher(string path)
        {
            var folder = Path.GetDirectoryName(path);
            var filename = Path.GetFileName(path);

            _watcher.Path = folder;
            _watcher.Filter = filename;
            _watcher.NotifyFilter = NotifyFilters.LastWrite;
            _watcher.Changed += Watcher_Changed;
        }

        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            var evnt = new FileChange
                {
                    ChangeDetected = DateTime.UtcNow,
                    FilePath = e.FullPath,
                    Type = e.ChangeType
                };
            _queue.Enqueue(evnt);
        }


    }
}

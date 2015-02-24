using System;
using System.Collections.Generic;
using System.IO;

namespace TailChaser.Tail
{
    internal class FileWatcher
    {
        private readonly Stack<FileChange> _stack;

        private readonly FileSystemWatcher _watcher;

        public FileWatcher(string file, Stack<FileChange> stack)
        {
            _stack = stack;
            _watcher = new FileSystemWatcher();
            InitWatcher(file);
            _stack.Push(new FileChange(file));
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
            _stack.Push(new FileChange(e.FullPath, e.ChangeType));
        }

       
    }
}

using System.Collections.Generic;
using System.IO;
using System.Messaging;

namespace TailChaser.Tail
{
    internal class FileWatcher
    {
        private readonly Queue<string> _queue;

        private readonly FileSystemWatcher _watcher;

        public FileWatcher(string file, Queue<string> queue)
        {
            _queue = queue;
            _watcher = new FileSystemWatcher();
            InitWatcher(file);
            _queue.Enqueue();
            _watcher.EnableRaisingEvents = true;
        }

        private void InitWatcher(string path)
        {
            var folder = Path.GetDirectoryName(path);
            var filename = Path.GetFileName(path);

            _watcher.Path = folder;
            _watcher.Filter = filename;
            _watcher.NotifyFilter = NotifyFilters.LastWrite;
            _watcher.Changed += watcher_Changed;
        }

        private void watcher_Changed(object sender, FileSystemEventArgs e)
        {
            Quene.Send(e.FullPath, e.ChangeType.ToString());
        }


    }
}

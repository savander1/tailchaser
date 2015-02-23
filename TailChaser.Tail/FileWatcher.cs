using System.IO;
using System.Messaging;

namespace TailChaser.Tail
{
    internal class FileWatcher
    {
        private static readonly MessageQueue Quene = new MessageQueue(".\\Private$\\tailChaser");
        private readonly FileSystemWatcher _watcher;

        public FileWatcher(string file)
        {
            _watcher = new FileSystemWatcher(file);
            Quene.Send(file, "Start");
            _watcher.Changed += watcher_Changed;
        }

        private void watcher_Changed(object sender, FileSystemEventArgs e)
        {
            Quene.Send(e.FullPath, e.ChangeType.ToString());
        }
    }
}

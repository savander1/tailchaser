using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace TailChaser.Tail
{
    internal class FileWatcher
    {
        private static MessageQueue _quene = new MessageQueue(".\\Private$\\tailChaser");
        private FileSystemWatcher _watcher;

        public FileWatcher(string file)
        {
            _watcher = new FileSystemWatcher(file);
            _quene.Send(file, "Start");
            _watcher.Changed += watcher_Changed;
        }

        private void watcher_Changed(object sender, FileSystemEventArgs e)
        {
            _quene.Send(e.FullPath, e.ChangeType.ToString());
        }
    }
}

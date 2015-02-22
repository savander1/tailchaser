using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TailChaser.Tail
{
    public class Tailer
    {
        private static Dictionary<string, FileStruct> _watchedFiles;

        public Tailer()
        {
            _watchedFiles = new Dictionary<string,FileStruct>();
        }

        public void TailFile(string fullPath)
        {
            if (!_watchedFiles.ContainsKey(fullPath))
            {
                _watchedFiles.Add(fullPath, FileStruct.Create(fullPath));
            }
        }
    }

    internal class FileStruct
    {
        public FileWatcher Watcher {get; set;}
        public FileUpdater Tailer {get; set;}

        private FileStruct(FileWatcher watcher, FileUpdater updater)
        {
            Watcher = watcher;
            Tailer = updater;
        }

        public static FileStruct Create(string fullPath)
        {
            var watcher = new FileWatcher(fullPath);
            var tailer = new FileUpdater(fullPath);

            return new FileStruct(watcher, tailer);
        }

    }
}

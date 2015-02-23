using TailChaser.Tail.Interfaces;

namespace TailChaser.Tail
{
    internal class FileStruct
    {
        public FileWatcher Watcher {get; set;}
        public FileUpdater Tailer {get; set;}

        private FileStruct(FileWatcher watcher, FileUpdater updater)
        {
            Watcher = watcher;
            Tailer = updater;
        }

        public static FileStruct Create(string fullPath, OnFileUpdated onFileUpdated)
        {
            var watcher = new FileWatcher(fullPath);
            var tailer = new FileUpdater(fullPath, onFileUpdated);

            return new FileStruct(watcher, tailer);
        }
    }
}
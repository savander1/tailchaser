using System;
using System.IO;
using TailChaser.Entity.Interfaces;
using TailChaser.Tail.Interfaces;

namespace TailChaser.Tail
{
    public class FileTailer : ITail, IDisposable
    {
        private FileTailerSubject _tailerSubject;
        private FileSystemWatcher _watcher;

        public void TailFile(string filePath, IFileContentObserver fileContentObserver)
        {
            _tailerSubject = new FileTailerSubject(GetFileReader(), filePath);
            _tailerSubject.Subscribe(fileContentObserver);
            _watcher = CreateWatcher(filePath);
        }

        protected virtual IFileReaderAsync GetFileReader()
        {
            return new FileReaderAsync();
        }

        private FileSystemWatcher CreateWatcher(string path)
        {
            var folder = Path.GetDirectoryName(path);
            var filename = Path.GetFileName(path);

            var watcher = new FileSystemWatcher
                {
                    Path = folder,
                    Filter = filename,
                    NotifyFilter = NotifyFilters.LastWrite
                };
            watcher.Changed += Watcher_Changed;

            return watcher;
        }

        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            _tailerSubject.PublishFileChange();
        }

        public void Dispose()
        {
            _watcher.Dispose();
        }
    }
}

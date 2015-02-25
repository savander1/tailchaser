using System;
using System.IO;
using TailChaser.Entity.Interfaces;
using TailChaser.Tail.Interfaces;

namespace TailChaser.Tail
{
    public class FileTailer : ITail, IDisposable
    {
        private readonly IFileReaderAsync _reader;
        private FileTailerSubject _tailerSubject;
        private FileSystemWatcher _watcher;

        public FileTailer() : this(new FileReaderAsync()){}
        public FileTailer(IFileReaderAsync reader)
        {
            _reader = reader;
        }

        public void TailFile(string filePath, IFileContentObserver fileContentObserver)
        {
            _tailerSubject = new FileTailerSubject(_reader, filePath);
            _tailerSubject.Subscribe(fileContentObserver);
            _watcher = CreateWatcher(filePath);
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

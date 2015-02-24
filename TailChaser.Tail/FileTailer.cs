using System;
using System.IO;
using TailChaser.Tail.Interfaces;

namespace TailChaser.Tail
{
    public class FileTailer : ITail, IDisposable
    {
        private readonly IFileReaderAsync _reader;
        private Events.FileTailer _tailer;
        private FileSystemWatcher _watcher;
        private OnFileUpdated _onFileUpdated;

        public FileTailer() : this(new FileReaderAsync()){}
        public FileTailer(IFileReaderAsync reader)
        {
            _reader = reader;
        }

        public void TailFile(string filePath, OnFileUpdated onFileUpdated)
        {
            _tailer = new Events.FileTailer(_reader, filePath);
            _tailer.Subscribe();

            _watcher = CreateWatcher(filePath);
            _onFileUpdated = onFileUpdated;
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
            _tailer.PublishFileChange();
            _onFileUpdated(e.FullPath, _tailer.FileContent);
        }

        public void Dispose()
        {
            _watcher.Dispose();
        }
    }
}

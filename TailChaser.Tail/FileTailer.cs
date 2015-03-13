using System;
using System.IO;
using TailChaser.Entity;
using TailChaser.Tail.Interfaces;

namespace TailChaser.Tail
{
    public class FileTailer : ITail
    {
        private readonly IFileReaderAsync _fileReader;

        public event EventHandler<FileChangeEventArgs> UiUpdate;

        private FileSystemWatcher _watcher;
        private TailedFile _file;

        public FileTailer(IFileReaderAsync fileReader)
        {
            _fileReader = fileReader;
        }

        public void TailFile(TailedFile file)
        {
            _file = file;
           _fileReader.ReadFileContentsAsync(_file.FullName).ContinueWith(task =>
               {
                   _file.FileContent = task.Result;
               });
            _watcher = CreateWatcher(file.FullName);
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
                    NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size
                };
            watcher.Changed += Watcher_Changed;
            watcher.EnableRaisingEvents = true;

            return watcher;
        }

        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            _fileReader.ReadFileContentsAsync(_file.FullName).ContinueWith(task =>
                {
                    _file.FileContent = task.Result; 
                    // this needs to update the ui with changes
                    if (UiUpdate != null)
                    {
                        UiUpdate(this, new FileChangeEventArgs(_file));
                    }
                });
        }

        public void Dispose()
        {
            _watcher.Dispose();
        }
    }
}

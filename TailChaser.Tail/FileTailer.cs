using System;
using System.IO;
using DiffMatchPatch;
using TailChaser.Entity;
using TailChaser.Entity.Interfaces;
using TailChaser.Tail.Interfaces;

namespace TailChaser.Tail
{
    public class FileTailer : ITail, IFileTailerSubject, IDisposable
    {
        private readonly IFileReaderAsync _fileReader;
        private readonly diff_match_patch _diffMatchPatch;
        public static event FileChangeEventHandler FileChangeEvent;
        public delegate void FileChangeEventHandler(object sender, FileChangeEventArgs e);

        private FileSystemWatcher _watcher;
        private TailedFile _file;

        public FileTailer(IFileReaderAsync fileReader)
        {
            _fileReader = fileReader;
            _diffMatchPatch = new diff_match_patch();
        }

        public void TailFile(TailedFile file)
        {
            _file = file;
            _watcher = CreateWatcher(file.FullName);
            Subscribe(file);
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

            return watcher;
        }

        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            PublishFileChange();
        }

        public void Dispose()
        {
            _watcher.Dispose();
        }

        public async void Subscribe(TailedFile file)
        {
            _file.FileContent = await _fileReader.ReadFileContentsAsync(_file.FullName);
            FileChangeEvent += FileChangeHandler;
        }

        public void Unsubscribe()
        {
            FileChangeEvent -= FileChangeHandler;
        }

        public async void PublishFileChange()
        {
            var newContent = await _fileReader.ReadFileContentsAsync(_file.FullName);
            var diffs = _diffMatchPatch.diff_main(_file.FileContent, newContent, true);
            var patches = _diffMatchPatch.patch_make(diffs);

            var eventArgs = new FileChangeEventArgs
            {
                Patches = patches
            };
            FileChangeEvent(this, eventArgs);
        }

        public void FileChangeHandler(object sender, FileChangeEventArgs e)
        {
            var currentContent = _file.FileContent;
            _diffMatchPatch.patch_apply(e.Patches, currentContent);
            _file.FileContent = currentContent;
        }
    }
}

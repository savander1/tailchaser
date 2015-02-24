using System;
using System.Collections.Generic;
using DiffMatchPatch;
using TailChaser.Tail.Interfaces;

namespace TailChaser.Tail
{
    public class FileTailer
    {
        public FileWatcher Watcher { get; set; }
        public FileUpdater Updater { get; set; }
        private static IFileReaderAsync _fileReader;

        public string FileContent { get; private set; }
        public List<Patch> Patches { get; private set; }

    private static readonly object SyncRoot = new Object();
        private static volatile Stack<FileChange> _stack;
        public static Stack<FileChange> Stack
        {
            get
            {
                if (_stack == null)
                {
                    lock (SyncRoot)
                    {
                        if (_stack == null)
                        {
                            _stack = new Stack<FileChange>();
                        }
                    }
                }
                return _stack;
            }
        }

        private FileTailer(FileWatcher watcher)
        {
            Watcher = watcher;
            _fileReader = new FileReaderAsync();
            Patches = new List<Patch>();
        }

        public static FileTailer Create(string fullPath)
        {
            var watcher = new FileWatcher(fullPath, Stack);
            var fileTailer = new FileTailer(watcher);
            fileTailer.SetFileContent(fullPath);
            
            
            var updater = new FileUpdater(fullPath, Stack, fileTailer.FileContent, _fileReader);
            fileTailer.Updater = updater;
            

            

            return fileTailer;
        }

        private async void SetFileContent(string path)
        {
            FileContent = await _fileReader.ReadFileContentsAsync(path);
        }
    }
}
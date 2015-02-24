using System;
using System.Collections.Generic;
using TailChaser.Tail.Interfaces;

namespace TailChaser.Tail
{
    internal class FileTailer
    {
        public FileWatcher Watcher {get; set;}
        public FileUpdater Updater { get; set; }
        private readonly IFileReaderAsync _fileReader;

        public string FileContent { get; private set; }

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
           
        }

        public static FileTailer Create(string fullPath)
        {
            var watcher = new FileWatcher(fullPath, Stack);
            
            var fileStruct = new FileTailer(watcher);
            var updater = new FileUpdater(fullPath, Stack, diff => fileStruct.FileContent = (string)diff);
            fileStruct.Updater = updater;

            fileStruct.SetFileContent(fullPath);

            return fileStruct;
        }

        private async void SetFileContent(string path)
        {
            FileContent = await _fileReader.ReadFileContents(path);
        }
    }
}
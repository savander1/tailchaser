using System;
using System.Collections.Generic;
using TailChaser.Tail.Interfaces;

namespace TailChaser.Tail
{
    internal class FileStruct
    {
        public FileWatcher Watcher {get; set;}
        public FileUpdater Updater { get; set; }
        private readonly IFileReaderAsync _fileReader;

        public string FileContent { get; private set; }

        private static readonly object SyncRoot = new Object();
        private static volatile Queue<FileChange> _queue;
        public static Queue<FileChange> Queue
        {
            get
            {
                if (_queue == null)
                {
                    lock (SyncRoot)
                    {
                        if (_queue == null)
                        {
                            _queue = new Queue<FileChange>();
                        }
                    }
                }
                return _queue;
            }
        }

        private FileStruct(FileWatcher watcher)
        {
            Watcher = watcher;
            _fileReader = new FileReaderAsync();
           
        }

        public static FileStruct Create(string fullPath)
        {
            var watcher = new FileWatcher(fullPath, Queue);
            
            var fileStruct = new FileStruct(watcher);
            var updater = new FileUpdater(fullPath, Queue, diff => fileStruct.FileContent = (string)diff);
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
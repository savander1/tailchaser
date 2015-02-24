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

        private FileStruct(FileWatcher watcher)
        {
            Watcher = watcher;
            _fileReader = new FileReaderAsync();
           
        }

        public static FileStruct Create(string fullPath, Queue<FileChange> queue)
        {
            var watcher = new FileWatcher(fullPath, queue);
            
            var fileStruct = new FileStruct(watcher);
            var updater = new FileUpdater(fullPath, queue, diff => fileStruct.FileContent = (string)diff);
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
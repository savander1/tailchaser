using TailChaser.Tail.Interfaces;

namespace TailChaser.Tail
{
    internal class FileStruct
    {
        public FileWatcher Watcher {get; set;}
        public FileUpdater Tailer {get; set;}
        private readonly IFileReaderAsync _fileReader;

        public string FileContent { get; private set; }

        private FileStruct(FileWatcher watcher, FileUpdater updater)
        {
            Watcher = watcher;
            Tailer = updater;
            _fileReader = new FileReaderAsync();
        }

        public static FileStruct Create(string fullPath, OnFileUpdated onFileUpdated)
        {
            var watcher = new FileWatcher(fullPath);
            var tailer = new FileUpdater(fullPath, onFileUpdated);

            var fileStruct = new FileStruct(watcher, tailer);

            fileStruct.SetFileContent(fullPath);
            onFileUpdated(fileStruct.FileContent);

            return fileStruct;
        }

        private async void SetFileContent(string path)
        {
            FileContent = await _fileReader.ReadFileContents(path);
        }
    }
}
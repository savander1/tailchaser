using DiffMatchPatch;
using TailChaser.Entity;
using TailChaser.Entity.Interfaces;
using TailChaser.Tail.Interfaces;

namespace TailChaser.Tail
{
    internal class FileTailerSubject : IFileTailerSubject
    {
        public static event FileChangeEventHandler FileChangeEvent;
        public delegate void FileChangeEventHandler(object sender, FileChangeEventArgs e);

        public string FileContent { get; private set; }

        private readonly IFileReaderAsync _fileReader;
        private readonly string _filePath;
        private IFileContentObserver _contentMaintainer;
        private FileChangeEventHandler _fileChangeHandler;
        private readonly diff_match_patch _diffMatchPatch;

        public FileTailerSubject(IFileReaderAsync fileReader, string filePath)
        {
            _fileReader = fileReader;
            _filePath = filePath;
            _diffMatchPatch = new diff_match_patch();
        }

        public async void Subscribe(IFileContentObserver contentMaintainer)
        {
            FileContent = await _fileReader.ReadFileContentsAsync(_filePath);
            _contentMaintainer = contentMaintainer;
            _fileChangeHandler = FileChangeHandler;
            FileChangeEvent += _fileChangeHandler;
        }

        public void Unsubscribe()
        {
            FileChangeEvent -= _fileChangeHandler;
        }

        public async void PublishFileChange()
        {
            var newContent = await _fileReader.ReadFileContentsAsync(_filePath);
            var diffs = _diffMatchPatch.diff_main(FileContent, newContent, true);
            var patches = _diffMatchPatch.patch_make(diffs);

            var eventArgs = new FileChangeEventArgs
                {
                    Patches = patches
                };
            FileChangeEvent(this, eventArgs);
        }

        public void FileChangeHandler(object sender, FileChangeEventArgs e)
        {
            _contentMaintainer.UpdatFileContent(e.Patches);
            FileContent = _contentMaintainer.FileContent;
        }
    }
}

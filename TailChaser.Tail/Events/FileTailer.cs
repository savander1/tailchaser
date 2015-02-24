using System;
using System.Collections.Generic;
using DiffMatchPatch;
using TailChaser.Tail.Interfaces;

namespace TailChaser.Tail.Events
{
    internal class FileChangeEventArgs : EventArgs
    {
        public List<Patch> Patches;
    }

    internal class FileTailer : IFileTailer
    {
        private readonly IFileReaderAsync _fileReader;
        private readonly string _filePath;
        public static event FileChangeEventHandler FileChangeEvent;
        public delegate void FileChangeEventHandler(object sender, FileChangeEventArgs e);
        IFileContentMaintainer _callback;
        FileChangeEventHandler _fileChangeHandler;
        private readonly diff_match_patch _diffMatchPatch;

        public string FileContent { get; private set; }

        public FileTailer(IFileReaderAsync fileReader, string filePath)
        {
            _fileReader = fileReader;
            _filePath = filePath;
            _diffMatchPatch = new diff_match_patch();
        }

        public async void Subscribe()
        {
            FileContent = await _fileReader.ReadFileContentsAsync(_filePath);
            _callback = new FileContentMaintainer(FileContent);
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
            _callback.UpdatFileContent(e.Patches);
            FileContent = _callback.FileContent;
        }
    }

    internal interface IFileTailer
    {
        void Subscribe();
        void Unsubscribe();
        void PublishFileChange();
    }

    internal interface IFileContentMaintainer
    {
        void UpdatFileContent(List<Patch> patches);
        string FileContent { get; }
    }

    internal class FileContentMaintainer : IFileContentMaintainer
    {
        public string FileContent { get; private set; }

        public FileContentMaintainer(string fileContent)
        {
            FileContent = fileContent;
        }

        public void UpdatFileContent(List<Patch> patches)
        {
            var diffMatchPatch = new diff_match_patch();
            diffMatchPatch.patch_apply(patches, FileContent);
        }
    }
}

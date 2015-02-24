using System;
using System.Collections.Generic;
using DiffMatchPatch;

namespace TailChaser.Tail.Events
{
    internal class FileChangeEventArgs : EventArgs
    {
        public List<Patch> Patches;
    }

    internal class FileTailer : IFileTailer
    {
        public static event FileChangeEventHandler FileChangeEvent;
        public delegate void FileChangeEventHandler(object sender, FileChangeEventArgs e);
        IFileContentMaintainer _callback;
        FileChangeEventHandler _fileChangeHandler;

        public string FileContent { get; private set; }

        public FileTailer(string fileContent)
        {
            FileContent = fileContent;
        }

        public void Subscribe()
        {
            _callback = new FileContentMaintainer(FileContent);
            _fileChangeHandler = FileChangeHandler;
            FileChangeEvent += _fileChangeHandler;
        }

        public void Unsubscribe()
        {
            FileChangeEvent -= _fileChangeHandler;
        }

        public void PublishFileChange(List<Patch> patches)
        {
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
        void PublishFileChange(List<Patch> patches);
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

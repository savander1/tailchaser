using System;
using System.Collections.Generic;
using System.Threading;
using DiffMatchPatch;
using TailChaser.Tail.Interfaces;

namespace TailChaser.Tail
{
    public class FileUpdater
    {
        private readonly string _fullPath;
        private readonly Stack<FileChange> _stack;
        private readonly string _initialFileContent;
        private readonly IFileReaderAsync _reader;
        private readonly List<Patch> _patches;
        public List<Patch> Patches { get { return _patches; } }

        public FileUpdater(string fullPath, Stack<FileChange> stack, string initialFileContent, IFileReaderAsync reader)
        {
            _fullPath = fullPath;
            _stack = stack;
            _initialFileContent = initialFileContent;
            _reader = reader;
            _patches = new List<Patch>();
        }

        public async void StartWatchingQueue()
        {
            var lastChange = DateTime.Now;
            var lastFileContent = _initialFileContent;
            while (true)
            {
                var change = _stack.Pop();

                if (change != null)
                {
                    var currentContent = await _reader.ReadFileContentsAsync(_fullPath);
                    ProcessChange(lastFileContent, currentContent, change, lastChange);
                    lastChange = change.ChangeDetected;
                    lastFileContent = currentContent;
                }
                
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
        }

        private void ProcessChange(string lastFileContent, string currentContent, FileChange change, DateTime lastChange)
        {
            if (change.ChangeDetected > lastChange)
            {
                var diffMatchPatch = new diff_match_patch();
                var diffs = diffMatchPatch.diff_main(lastFileContent, currentContent);
                var patches = diffMatchPatch.patch_make(diffs);

                _patches.AddRange(patches);
            }
        }
    }
}
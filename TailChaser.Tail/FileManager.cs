using System;
using System.Collections.Generic;
using TailChaser.Entity;
using TailChaser.Entity.Configuration;
using TailChaser.Tail;

namespace TailChaser.UI
{
    public class FileManager : IDisposable
    {
        private static readonly Dictionary<Guid, FileTailer> WatchedFiles = new Dictionary<Guid, FileTailer>();

        public event EventHandler<FileChangeEventArgs> UiUpdate;

        public void WatchFile(File tailedFile)
        {
            if (!WatchedFiles.ContainsKey(tailedFile.Id))
            {
                var tailer = new FileTailer(new FileReaderAsync());
                if (UiUpdate != null)
                {
                    tailer.UiUpdate += UiUpdate;
                }
                tailer.TailFile(tailedFile);
                WatchedFiles.Add(tailedFile.Id, tailer);
            }
        }

        public void UnWatchFile(File tailedFile)
        {
            if (!WatchedFiles.ContainsKey(tailedFile.Id))
            {
                var tailer = WatchedFiles[tailedFile.Id];
                tailer.Dispose();
                WatchedFiles.Remove(tailedFile.Id);
            }
        }

        public void Dispose()
        {
            foreach (var tailer in WatchedFiles.Values)
            {
                tailer.Dispose();
            }
        }
    }
}

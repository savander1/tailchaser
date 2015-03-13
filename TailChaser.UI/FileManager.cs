using System;
using System.Collections.Generic;
using TailChaser.Entity;
using TailChaser.Tail;

namespace TailChaser.UI
{
    public class FileManager : IDisposable
    {
        private static readonly Dictionary<Guid, FileTailer> WatchedFiles = new Dictionary<Guid, FileTailer>();
        
        public void WatchFile(TailedFile tailedFile)
        {
            if (!WatchedFiles.ContainsKey(tailedFile.Id))
            {
                var tailer = new FileTailer(new FileReaderAsync());
                tailer.TailFile(tailedFile);
                WatchedFiles.Add(tailedFile.Id, tailer);
            }
        }

        public void UnWatchFile(TailedFile tailedFile)
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

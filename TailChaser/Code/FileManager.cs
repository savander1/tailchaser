using System;
using System.Collections.Generic;
using System.Windows.Documents;
using TailChaser.Entity;
using TailChaser.Tail;
using TailChaser.Tail.Interfaces;

namespace TailChaser.Code
{
    public class FileManager : IDisposable
    {
        private static readonly Dictionary<Guid, FileTailer> WatchedFiles = new Dictionary<Guid, FileTailer>();
        
        public void WatchFile(TailedFile tailedFile, OnFileUpdated onFileUpdated)
        {
            if (!WatchedFiles.ContainsKey(tailedFile.Id))
            {
                var tailer = new FileTailer();
                tailer.TailFile(tailedFile.FullName, onFileUpdated);
                WatchedFiles.Add(tailedFile.Id, tailer);
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

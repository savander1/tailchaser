using System;
using System.Collections.Generic;
using TailChaser.Entity;
using TailChaser.Tail;

namespace TailChaser.Code
{
    public class FileManager : IDisposable
    {
        // This class should subscribe to changes in the files and update FlowDocuments with the contents via patches.
        // This class will also eventually take a settings object to color the file contents via regex.

        private static readonly Dictionary<Guid, FileTailer> WatchedFiles = new Dictionary<Guid, FileTailer>();
        
        public void WatchFile(TailedFile tailedFile)
        {
            if (!WatchedFiles.ContainsKey(tailedFile.Id))
            {
                var tailer = new FileTailer();
                tailer.TailFile(tailedFile.FullName, new FileContentObserver(ref tailedFile));
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

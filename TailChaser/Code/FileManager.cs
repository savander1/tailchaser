using System;
using System.Collections.Generic;
using System.Windows.Documents;
using DiffMatchPatch;
using TailChaser.Entity;
using TailChaser.Entity.Interfaces;
using TailChaser.Tail;
using TailChaser.Tail.Interfaces;

namespace TailChaser.Code
{
    public class FileManager : IDisposable
    {
        // This class should subscribe to changes in the files and update FlowDocuments with the contents via patches.
        // This class will also eventually take a settings object to color the file contents via regex.

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

    public class FileContentObserver : IFileContentObserver
    {
        public string FileContent { get; private set; }

        public FileContentObserver(string fileContent)
        {
            
        }

        public void UpdatFileContent(List<Patch> patches)
        {
            throw new NotImplementedException();
        }

        
    }
}

using System;
using System.Collections.Generic;
using TailChaser.Tail.Interfaces;

namespace TailChaser.Tail
{
    public class Tailer : ITail
    {
        private static Dictionary<string, FileStruct> _watchedFiles;
        private static readonly object SyncRoot = new Object();
        private static volatile Queue<FileChange> _queue;
        public static Queue<FileChange> Queue
        {
            get
            {
                if (_queue == null)
                {
                    lock (SyncRoot)
                    {
                        if (_queue == null)
                        {
                            _queue = new Queue<FileChange>();
                        }
                    }
                }
                return _queue;
            }
        }

        public Tailer()
        {
            _watchedFiles = new Dictionary<string,FileStruct>();
        }

        public void TailFile(string filePath)
        {
            if (!_watchedFiles.ContainsKey(filePath))
            {
                _watchedFiles.Add(filePath, FileStruct.Create(filePath, Queue));
            }
        }
    }
}

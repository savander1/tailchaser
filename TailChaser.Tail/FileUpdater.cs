using System;
using System.Collections.Generic;
using System.Threading;

namespace TailChaser.Tail
{
    internal class FileUpdater
    {
        private readonly string _fullPath;
        private readonly Queue<FileChange> _queue;
        private readonly Func<object, string> _func;

        public FileUpdater(string fullPath, Queue<FileChange> queue, Func<object, string> func)
        {
            _fullPath = fullPath;
            _queue = queue;
            _func = func;
        }

        public void StartWatchingQueue()
        {
            while (true)
            {
                var change = _queue.Dequeue();

                if (change != null)
                {
                    _func(_fullPath);
                }
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
        }
    }
}
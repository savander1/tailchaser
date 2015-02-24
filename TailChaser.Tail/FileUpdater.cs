using System;
using System.Collections.Generic;
using System.Threading;

namespace TailChaser.Tail
{
    internal class FileUpdater
    {
        private readonly string _fullPath;
        private readonly Stack<FileChange> _stack;
        private readonly Func<object, string> _func;

        public FileUpdater(string fullPath, Stack<FileChange> stack, Func<object, string> func)
        {
            _fullPath = fullPath;
            _stack = stack;
            _func = func;
        }

        public void StartWatchingQueue()
        {
            while (true)
            {
                var change = _stack.Pop();

                if (change != null)
                {
                    _func(_fullPath);
                }
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
        }
    }
}
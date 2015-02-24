using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Threading;

namespace TailChaser.Tail
{
    internal class FileUpdater
    {
        private static readonly MessageQueue Quene = new MessageQueue(".\\Private$\\tailChaser");
        private readonly string _fullPath;
        private readonly Func<object, string> _func;

        public FileUpdater(string fullPath, Queue<string> queue, Func<object, string> func)
        {
            _fullPath = fullPath;
            _func = func;
        }

        public void StartWatchingQueue()
        {
            while (true)
            {
                var message = Quene.GetAllMessages()
                                   .Where(x => x.Body.Equals(_fullPath))
                                   .OrderByDescending(x => x.ArrivedTime)
                                   .FirstOrDefault();

                if (message != null)
                {
                    _func(_fullPath);
                }
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
        }
    }
}
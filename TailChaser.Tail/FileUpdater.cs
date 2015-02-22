using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TailChaser.Tail
{
    internal class FileUpdater
    {
        private static MessageQueue _quene = new MessageQueue(".\\Private$\\tailChaser");

        private string _file;
        private StringBuilder _builder;

        public FileUpdater(string file)
        {
            _file = file;
            _builder = new StringBuilder();
            StartPolling();
            
        }

        private void StartPolling()
        {

            ThreadPool.QueueUserWorkItem(ThreadProc);
            
        }

        private static void ThreadProc(object obj)
        {

        }

        
    }
}

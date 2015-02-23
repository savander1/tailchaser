using System.Messaging;
using System.Text;
using System.Threading;
using TailChaser.Tail.Interfaces;

namespace TailChaser.Tail
{
    internal class FileUpdater
    {
        private static readonly MessageQueue Quene = new MessageQueue(".\\Private$\\tailChaser");

        private string _file;
        private readonly OnFileUpdated _onFileUpdated;
        private StringBuilder _builder;
        private readonly IFileReaderAsync _fileReader;

        public FileUpdater(string file, OnFileUpdated onFileUpdated)
        {
            _file = file;
            _onFileUpdated = onFileUpdated;
            _builder = new StringBuilder();
            StartPolling();
            
        }

        private void StartPolling()
        {
            while (true)
            {
                
            }   
        }

        private static void ThreadProc(object obj)
        {

        }

        
    }
}

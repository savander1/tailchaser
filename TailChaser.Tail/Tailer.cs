using System;
using System.Collections.Generic;
using TailChaser.Tail.Interfaces;

namespace TailChaser.Tail
{
    public class Tailer : ITail
    {
        private static Dictionary<string, FileStruct> _watchedFiles;
        

        public Tailer()
        {
            _watchedFiles = new Dictionary<string,FileStruct>();
        }

        public void TailFile(string filePath)
        {
            if (!_watchedFiles.ContainsKey(filePath))
            {
                _watchedFiles.Add(filePath, FileStruct.Create(filePath));
            }
        }
    }
}

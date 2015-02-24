using System;
using System.Collections.Generic;
using TailChaser.Tail.Interfaces;

namespace TailChaser.Tail
{
    public class Tailer : ITail
    {
        private readonly IFileReaderAsync _reader;

        public Tailer(IFileReaderAsync reader)
        {
            _reader = reader;
        }

        public async void TailFile(string filePath)
        {
            var content = await _reader.ReadFileContentsAsync(filePath);
        }
    }
}

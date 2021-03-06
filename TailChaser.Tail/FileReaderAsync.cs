﻿using System.IO;
using System.Threading;
using System.Threading.Tasks;
using TailChaser.Tail.Interfaces;

namespace TailChaser.Tail
{
    public class FileReaderAsync : IFileReaderAsync
    {
        public Task<string> ReadFileContentsAsync(string filePath)
        {
            try
            {
                var filestream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                var streamReader = new StreamReader(filestream);
                return Task<string>.Factory.StartNew(() =>
                    {
                        var content = streamReader.ReadToEnd();
                        streamReader.Close();
                        streamReader.Dispose();
                        filestream.Close();
                        filestream.Dispose();
                        return content;
                    });
            }
            catch (FileNotFoundException)
            {
                return null;
            }
            catch (IOException)
            {
                Thread.Sleep(10);
                return ReadFileContentsAsync(filePath);
            }
        }

        public Task<string> ReadFileEndingAsync(string filePath)
        {
            try
            {
                var filestream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                var bytesBack = filestream.Length > 1024 ? 1024 : filestream.Length;
                filestream.Seek(-(bytesBack), SeekOrigin.End);
                var streamReader = new StreamReader(filestream);
                return Task<string>.Factory.StartNew(() =>
                    {
                        var ending = streamReader.ReadToEnd();
                        streamReader.Close();
                        streamReader.Dispose();
                        filestream.Close();
                        filestream.Dispose();
                        return ending;
                    });
            }
            catch (FileNotFoundException)
            {
                return null;
            }
            catch (IOException)
            {
                Thread.Sleep(10);
                return ReadFileEndingAsync(filePath);
            }
        } 
    }
}
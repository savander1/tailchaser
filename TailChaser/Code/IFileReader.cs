using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TailChaser.Code
{
    public interface IFileReader
    {
        byte[] ReadFile(string filePath);
        byte[] ReadFileDelta(string filePath, byte[] knownContents);
    }

    public class FileReader : IFileReader
    {
        public byte[] ReadFile(string filePath)
        {
            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                //fs.Read()
            }
            return new byte[0];
        }

        public byte[] ReadFileDelta(string filePath, byte[] knownContents)
        {
            throw new NotImplementedException();
        }
    }
}

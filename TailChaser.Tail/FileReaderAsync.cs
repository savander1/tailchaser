using System.IO;
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
                return Task<string>.Factory.StartNew(streamReader.ReadToEnd);
            }
            catch (FileNotFoundException)
            {
            }
            return null;
        }
    }
}
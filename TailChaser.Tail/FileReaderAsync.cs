using System.IO;
using System.Threading.Tasks;
using TailChaser.Tail.Interfaces;

namespace TailChaser.Tail
{
    public class FileReaderAsync : IFileReaderAsync
    {
        public Task<string> ReadFileContents(string filePath)
        {
            try
            {
                using (var filestream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (var streamReader = new StreamReader(filestream))
                    {
                        return streamReader.ReadToEndAsync();
                    }
                }
            }
            catch (FileNotFoundException)
            {
            }
            return null;
        }
    }
}
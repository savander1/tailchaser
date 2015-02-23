using System.Threading.Tasks;

namespace TailChaser.Tail.Interfaces
{
    internal interface IFileReaderAsync
    {
        Task<string> ReadFileContents(string filePath);
    }
}

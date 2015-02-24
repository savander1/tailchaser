using System.Threading.Tasks;

namespace TailChaser.Tail.Interfaces
{
    public interface IFileReaderAsync
    {
        Task<string> ReadFileContents(string filePath);
    }
}

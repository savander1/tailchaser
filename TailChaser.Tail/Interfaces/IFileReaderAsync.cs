using System.Threading.Tasks;

namespace TailChaser.Tail.Interfaces
{
    public interface IFileReaderAsync
    {
        Task<string> ReadFileContentsAsync(string filePath);
    }
}

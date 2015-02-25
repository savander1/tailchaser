using TailChaser.Entity.Interfaces;

namespace TailChaser.Tail.Interfaces
{
    internal interface ITail
    {
        void TailFile(string filePath, IFileContentObserver fileContentObserver);
    }
}

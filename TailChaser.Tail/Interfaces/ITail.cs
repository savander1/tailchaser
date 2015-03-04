using TailChaser.Entity;
using TailChaser.Entity.Interfaces;

namespace TailChaser.Tail.Interfaces
{
    internal interface ITail
    {
        void TailFile(TailedFile filePath, IFileContentObserver fileContentObserver);
    }
}

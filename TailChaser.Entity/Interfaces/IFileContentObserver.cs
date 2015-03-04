using System.Collections.Generic;
using DiffMatchPatch;

namespace TailChaser.Entity.Interfaces
{
    public interface IFileContentObserver
    {
        void UpdatFileContent(List<Patch> patches);
    }
}

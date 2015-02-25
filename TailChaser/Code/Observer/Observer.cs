using System.Collections.Generic;
using DiffMatchPatch;
using TailChaser.Entity.Interfaces;

namespace TailChaser.Code.Observer
{
    public abstract class Observer : IFileContentObserver
    {
        public abstract void UpdatFileContent(List<Patch> patches);
        public string FileContent { get; protected set; }
    }
}

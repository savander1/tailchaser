using System;
using TailChaser.Entity;

namespace TailChaser.Tail.Interfaces
{
    internal interface ITail : IDisposable 
    {
        void TailFile(TailedFile file);
    }
}

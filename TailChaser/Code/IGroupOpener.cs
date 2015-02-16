using System.Collections.Generic;
using System.IO;
using TailChaser.Entity;

namespace TailChaser.Code
{
    public interface IGroupOpener
    {
        ICollection<Group> OpenFiles(Stream[] fileStreams);
        Group OpenFile(Stream fileStreams);
    }
}
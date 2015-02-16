using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailChaser.Entity;

namespace TailChaser.Code
{
    public class GroupOpener : IGroupOpener
    {
        public ICollection<Group> OpenFiles(Stream[] fileStreams)
        {
            throw new NotImplementedException();
        }

        public Group OpenFile(Stream fileStreams)
        {
            throw new NotImplementedException();
        }
    }
}

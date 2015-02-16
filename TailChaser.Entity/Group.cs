using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TailChaser.Entity
{
    public class Group
    {
        public string Name { get; set; }
        public List<FileInfo> Files { get; set; }
    }
}

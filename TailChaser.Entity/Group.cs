using System.Collections.Generic;

namespace TailChaser.Entity
{
    public class Group
    {
        public string Name { get; set; }
        public List<TailedFile> Files { get; set; }

        public Group()
        {
            Files = new List<TailedFile>();
        }

        public Group(string name)
        {
            Name = name;
            Files = new List<TailedFile>();
        }
    }
}

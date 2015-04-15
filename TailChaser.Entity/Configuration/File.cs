
using System.Collections.Generic;

namespace TailChaser.Entity.Configuration
{
    public class File : Item
    {
        public string Font { get; set; }
        public double FontSize { get; set; }
        public ICollection<Settings> PresentationSettings { get; set; }

        public File(string name) : base(name)
        {
        }
    }
}

using System.Collections.Generic;

namespace TailChaser.Entity
{
    public class Machine
    {
        public string Name { get; set; }
        public List<Group> Groups { get; set; }

        public Machine()
        {
            Groups = new List<Group>();
        }

        public Machine(string machineName)
        {
            Name = machineName;
            Groups = new List<Group>();
        }
    }
}

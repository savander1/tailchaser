
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace TailChaser.Entity
{
    [DataContract]
    public class Configuration
    {
        [XmlArrayItem("machine", typeof(Machine))]
        [XmlArray("machines")]
        public ObservableCollection<Machine> Machines { get; set; } 

        public Configuration()
        {
            Machines = new ObservableCollection<Machine>();
        }

        public Machine FindMachine(Machine toFind)
        {
            return Machines.FirstOrDefault(x => x.Id.Equals(toFind.Id));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            foreach (var machine in Machines)
            {
                builder.Append(machine.Name);
                foreach (var group in machine.Groups)
                {
                    builder.Append(group.Name);
                    foreach (var file in group.Files)
                    {
                        builder.Append(file.Name);
                        builder.Append(file.FullName);
                    }
                }
            }
            return builder.ToString();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        protected bool Equals(Configuration other)
        {
            return Equals(Machines, other.Machines);
        }

        public override int GetHashCode()
        {
            return (Machines != null ? Machines.GetHashCode() : 0);
        }
    }
}

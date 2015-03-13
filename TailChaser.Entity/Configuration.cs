
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
using TailChaser.Entity.Extensions;

namespace TailChaser.Entity
{
    [DataContract]
    public class Configuration
    {
        [XmlArrayItem("machine", typeof(Machine))]
        [XmlArray("machines")]
        public ObservableCollection<Machine> Machines { get; set; }

        [XmlIgnore]
        public ObservableCollection<TailedFile> Files 
        { 
            get 
            { 
                var files = new List<TailedFile>();

                foreach (var group in Machines.SelectMany(machine => machine.Groups))
                {
                    files.AddRange(group.Files);
                }

                return new ObservableCollection<TailedFile>(files);
            }    
        }

        public Configuration()
        {
            Machines = new ObservableCollection<Machine>();
        }

        public Machine FindMachine(Guid machineId)
        {
            return Machines.FirstOrDefault(x => x.Id.Equals(machineId));
        }

        public Group FindGroup(Guid groupId)
        {
            return (from machine in Machines
                    where machine.FindGroup(groupId) != null
                    select machine.FindGroup(groupId))
                    .FirstOrDefault();
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
            var other = obj as Configuration;
            if (other == null) return false;

            var thisBytes = ToString().GetHash();
            var otherBytes = obj.ToString().GetHash();

            if (thisBytes.Length != otherBytes.Length)
            {
                return false;
            }

            return !thisBytes.Where((t, i) => t != otherBytes[i]).Any();
        }
    }
}

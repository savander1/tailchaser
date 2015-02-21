using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace TailChaser.Entity
{
    [DataContract]
    public class Machine : IItem, IExpandable
    {
        [DataMember]
        [XmlAttribute("name")]
        public string Name { get; set; }

        [DataMember]
        [XmlArrayItem("group", typeof(Group))]
        [XmlArray("groups")]
        public ObservableCollection<Group> Groups { get; set; }

        [DataMember]
        [XmlAttribute("uid")]
        public Guid Id { get; set; }

        [DataMember]
        [XmlAttribute("exp")]
        public bool Expanded { get; set; }

        public Machine(): this(string.Empty){}
       
        public Machine(string machineName)
        {
            Name = machineName;
            Groups = new ObservableCollection<Group>();
            Id = Guid.NewGuid();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        protected bool Equals(Machine other)
        {
            return string.Equals(Name, other.Name) && Equals(Groups, other.Groups);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ (Groups != null ? Groups.GetHashCode() : 0);
            }
        }
    }
}

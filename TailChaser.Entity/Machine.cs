using System;
using System.Collections.ObjectModel;
using System.Linq;
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

        [DataMember]
        [XmlAttribute("puid")]
        public Guid ParentId { get; set; }

        public Machine(): this(string.Empty){}
       
        public Machine(string machineName)
        {
            Name = machineName;
            Groups = new ObservableCollection<Group>();
            Id = Guid.NewGuid();
            ParentId = Guid.Empty;
        }

        public Group FindGroup(Guid groupId)
        {
            return Groups.FirstOrDefault(x => x.Id.Equals(groupId));
        }
    }
}

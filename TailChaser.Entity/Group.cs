using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace TailChaser.Entity
{
    [DataContract]
    public class Group : IItem, IExpandable
    {
        [DataMember]
        [XmlAttribute("name")]
        public string Name { get; set; }

        [DataMember]
        [XmlArrayItem("file", typeof(TailedFile))]
        [XmlArray("files")]
        public ObservableCollection<TailedFile> Files { get; set; }

        [DataMember]
        [XmlAttribute("uid")]
        public Guid Id { get; set; }

        [DataMember]
        [XmlAttribute("exp")]
        public bool Expanded { get; set; }

        public Group() :this (string.Empty) { }

        public Group(string name)
        {
            Name = name;
            Files = new ObservableCollection<TailedFile>();
            Id = Guid.NewGuid();
        }
    }
}

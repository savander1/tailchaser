using System;
using System.Collections.ObjectModel;
using System.Linq;
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
        [XmlAttribute("puid")]
        public Guid ParentId { get; set; }

        [DataMember]
        [XmlAttribute("exp")]
        public bool Expanded { get; set; }

        public Group() : this(string.Empty, Guid.Empty) { }

        public Group(Guid parentId) :this (string.Empty, parentId) { }

        public Group(string name, Guid parentId)
        {
            Name = name;
            Files = new ObservableCollection<TailedFile>();
            Id = Guid.NewGuid();
            ParentId = parentId;
        }

        public void AddFile(TailedFile file)
        {
            if (Files.All(x => x.FullName != file.FullName))
            {
                Files.Add(file);
            }
        }
    }
}

using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace TailChaser.Entity
{
    [DataContract]
    public class TailedFile : IItem
    {
        [DataMember]
        [XmlAttribute("name")]
        public string Name { get; set; }

        [DataMember]
        [XmlAttribute("fullname")]
        public string FullName { get; set; }

        [DataMember]
        [XmlAttribute("uid")]
        public Guid Id { get; set; }

        public TailedFile() : this(string.Empty, string.Empty) { }

        public TailedFile(string fullname) : this(Path.GetFileName(fullname), fullname) { }

        public TailedFile(string name, string fullName)
        {
            Name = name;
            FullName = fullName;
            Id = Guid.NewGuid();
        }
    }
}

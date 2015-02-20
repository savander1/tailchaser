using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace TailChaser.Entity
{
    [DataContract]
    public class TailedFile
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

        public TailedFile(string name, string fullName)
        {
            Name = name;
            FullName = fullName;
            Id = Guid.NewGuid();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        protected bool Equals(TailedFile other)
        {
            return string.Equals(Name, other.Name) && string.Equals(FullName, other.FullName);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ (FullName != null ? FullName.GetHashCode() : 0);
            }
        }
    }
}

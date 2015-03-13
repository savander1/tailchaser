using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using TailChaser.Entity.Annotations;

namespace TailChaser.Entity
{
    [DataContract]
    public class TailedFile : IItem, INotifyPropertyChanged
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

        [DataMember]
        [XmlAttribute("puid")]
        public Guid ParentId { get; set; }

        [DataMember]
        [XmlElement("settings")]
        public FilePresentationSettings PresentationSettings { get; set; }

        private string _content;
        [DataMember]
        [XmlIgnore]
        public string FileContent { 
            get { return _content; }
            set 
            { 
                _content = value;
                OnPropertyChanged();
            } 
        }

        public TailedFile() : this(Guid.Empty) { }

        public TailedFile(Guid parentId) : this(string.Empty, string.Empty, parentId) { }

        public TailedFile(string fullname, Guid parentId) : this(Path.GetFileName(fullname), fullname, parentId) { }

        public TailedFile(string name, string fullName, Guid parentId)
        {
            Name = name;
            FullName = fullName;
            Id = Guid.NewGuid();
            ParentId = parentId;
            PresentationSettings = new FilePresentationSettings();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

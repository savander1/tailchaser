using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace TailChaser.Entity
{
    [DataContract]
    public class FilePresentationSetting
    {
        [DataMember]
        [XmlAttribute("expression")]
        public string Expression { get; set; }

        [DataMember]
        [XmlAttribute("color")]
        public string Color { get; set; }
    }
}
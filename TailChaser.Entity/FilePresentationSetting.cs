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
        [XmlAttribute("a")]
        public int Alpha { get; set; }

        [DataMember]
        [XmlAttribute("r")]
        public int Red { get; set; }

        [DataMember]
        [XmlAttribute("g")]
        public int Green { get; set; }

        [DataMember]
        [XmlAttribute("b")]
        public int Blue { get; set; }

        [DataMember]
        [XmlAttribute("t")]
        public int TextColor { get; set; }
    }
}
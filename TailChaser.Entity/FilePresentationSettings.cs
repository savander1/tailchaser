using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Windows.Media;
using System.Xml.Serialization;

namespace TailChaser.Entity
{
    [DataContract]
    public class FilePresentationSettings
    {
        [DataMember]
        [XmlArrayItem("display_setting", typeof(FilePresentationSetting))]
        [XmlArray("display_settings")]
        public ObservableCollection<FilePresentationSetting> FileSettings { get; set; }

        [DataMember]
        [XmlElement("font-family")]
        public string Font { get; set; }

        [XmlIgnore]
        public FontFamily FontFamily { get { return new FontFamily(Font); } }

        [DataMember]
        [XmlElement("font-size")]
        public double FontSize { get; set; }

        public FilePresentationSettings()
        {
            FileSettings = new ObservableCollection<FilePresentationSetting>();
            Font = "Courier New";
            FontSize = 12.0;
        }
    }
}

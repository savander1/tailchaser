using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Windows.Media;
using System.Xml.Serialization;
using TailChaser.Entity.Annotations;

namespace TailChaser.Entity
{
    [DataContract]
    public class FilePresentationSettings : INotifyPropertyChanged
    {
        [DataMember]
        [XmlArrayItem("display_setting", typeof(FilePresentationSetting))]
        [XmlArray("display_settings")]
        public ObservableCollection<FilePresentationSetting> FileSettings { get; set; }

        private string _font;
        private double _fontSize;

        [DataMember]
        [XmlElement("font-family")]
        public string Font
        {
            get { return _font; } 
            set { 
                _font = value;
                OnPropertyChanged("FontFamily");
            }
        }

        [XmlIgnore]
        public FontFamily FontFamily { get { return new FontFamily(Font); } }

        [DataMember]
        [XmlElement("font-size")]
        public double FontSize
        {
            get { return _fontSize; }
            set 
            { 
                _fontSize = value; 
                OnPropertyChanged();
            }
        }

        public FilePresentationSettings()
        {
            FileSettings = new ObservableCollection<FilePresentationSetting>
                {
                    new FilePresentationSetting{Alpha = 255, Red = 255, Green = 0, Blue = 0, Expression=".*\\[Error\\].*"},
                    new FilePresentationSetting{Alpha = 255, Red = 0, Green = 255, Blue = 0, Expression=".*\\[Info\\].*"}
                };
            Font = "Courier New";
            FontSize = 12.0;
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

using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
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
                PresentFile();
                OnPropertyChanged();
            } 
        }

        private FlowDocument _document;
        [DataMember]
        [XmlIgnore]
        public FlowDocument PresentedFile
        {
            get { return _document; }
            set 
            {
                _document = value;
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

        private void PresentFile()
        {
            PresentedFile = new FlowDocument
            {
                FontFamily = PresentationSettings.FontFamily,
                FontSize = PresentationSettings.FontSize,
                MinPageWidth = 1000
            };

            if (FileContent != null)
            {
                var lines = FileContent.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var line in lines)
                {
                    var setting = GetSettingForLine(line);
                    var color = GetBackgroundColor(setting);
                    var textColor = GetForgroundColor(setting);
                    var inline = new Run(line);
                    var block = new Paragraph(inline)
                    {
                        Background = new SolidColorBrush(color),
                        Foreground = new SolidColorBrush(textColor),
                        Margin = new Thickness(0)
                    };
                    PresentedFile.Blocks.Add(block);
                }
            }
        }

        private FilePresentationSetting GetSettingForLine(string line)
        {
            foreach (var setting in PresentationSettings.FileSettings.Reverse())
            {
                if (Regex.IsMatch(line, setting.Expression, RegexOptions.IgnoreCase))
                {
                    return setting;
                }
            }
            return new FilePresentationSetting
            {
                Alpha = 255,
                Blue = 255,
                Green = 255,
                Red = 255,
                TextColor = 0
            };
        }

        private static Color GetBackgroundColor(FilePresentationSetting setting)
        {
            return Color.FromArgb((byte)setting.Alpha, (byte)setting.Red, (byte)setting.Green,
                                          (byte)setting.Blue);
        }

        private static Color GetForgroundColor(FilePresentationSetting setting)
        {
            return GetColorForText(setting.TextColor);
        }

        private static Color GetColorForText(int color)
        {
            return color == 0 ? Color.FromRgb(0, 0, 0) : Color.FromRgb(255, 255, 255);
        }
    }
}

using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using TailChaser.Entity;

namespace TailChaser.UI
{
    public class FilePresenter : IFilePresenter
    {
        private readonly TailedFile _file;

        public FilePresenter(TailedFile file)
        {
            _file = file;
        }

        public FlowDocument PresentFile()
        {
            var document = new FlowDocument
                {
                    FontFamily = _file.PresentationSettings.FontFamily,
                    FontSize = _file.PresentationSettings.FontSize,
                    MinPageWidth = 1000
                };

            if (_file.FileContent != null)
            {
                var lines = _file.FileContent.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var line in lines)
                {
                    var color = GetColorForLine(line);
                    var textColor = ColorInverter.InvertColor(color);
                    var inline = new Run(line);
                    var block = new Paragraph(inline)
                        {
                            Background = new SolidColorBrush(color),
                            Foreground = new SolidColorBrush(textColor),
                            Margin = new Thickness(0)
                        };
                    document.Blocks.Add(block);
                }
            }

            return document;
        }

        private Color GetColorForLine(string line)
        {
            foreach (var setting in _file.PresentationSettings.FileSettings.Reverse())
            {
                if (Regex.IsMatch(line, setting.Expression, RegexOptions.IgnoreCase))
                {
                    return Color.FromArgb((byte) setting.Alpha, (byte) setting.Red, (byte) setting.Green,
                                          (byte) setting.Blue);
                }
            }

            return Color.FromArgb(255, 255, 255, 255);
        }

    }
}

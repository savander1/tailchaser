using System;
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
                    var textColor = GetTextColor(color);
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
            foreach (var setting in _file.PresentationSettings.FileSettings)
            {
                if (Regex.IsMatch(line, setting.Expression, RegexOptions.IgnoreCase))
                {
                    return Color.FromArgb((byte) setting.Alpha, (byte) setting.Red, (byte) setting.Green,
                                          (byte) setting.Blue);
                }
            }

            return Color.FromArgb(255, 255, 255, 255);
        }

        private Color GetTextColor(Color color)
        {
            ColorPart highestPart;
            var value = HighestValue(color.R, color.G, color.B, out highestPart);
            var chroma =  value - LowestValue(color.R, color.G, color.B);
            var huePrime = GetHuePrime(chroma, highestPart, color);
            var hue = GetHue(huePrime);
            var saturation = GetSaturation(chroma, value);

            var invertedHsv = new Hsv {H = hue, S = saturation, V = value};

            return GetColor(invertedHsv, color.A);
        }

        

        private byte HighestValue(byte r, byte g, byte b, out ColorPart part)
        {
            var candidate1 = Math.Max(r, g);
            var candidate2 = Math.Max(r, b);

            var highest =  Math.Max(candidate1, candidate2);

            if (highest == r)
            {
                part = ColorPart.R;
            }
            else if (highest == g)
            {
                part = ColorPart.G;
            }
            else 
            {
                part = ColorPart.B;
            }

            return highest;
        }

        private byte GetHuePrime(int chroma, ColorPart highestPart, Color color)
        {
            if (chroma == 0) return 0;
            switch (highestPart)
            {
                case ColorPart.R:
                    return (byte) ((color.G - color.B)/chroma % 6);
                case ColorPart.G:
                    return (byte)((color.B - color.R) / chroma + 2);
                case ColorPart.B:
                    return (byte) ((color.R - color.B)/chroma + 4);
                default:
                    return 0;
            }
        }

        private byte GetSaturation(int chroma, byte value)
        {
            return (byte) (chroma > 0 ? chroma / value : 0);
        }

        private byte GetHue(byte huePrime)
        {
            var hue = huePrime * 60;

            var colorHue = hue < 0 ? (hue + 360) : hue;
            return (byte) (colorHue < 180 ? colorHue + 180 : colorHue - 180);
        }

        private byte LowestValue(byte r, byte g, byte b)
        {
            var candidate1 = Math.Min(r, g);
            var candidate2 = Math.Min(r, b);

            return Math.Min(candidate1, candidate2);
        }

        private Color GetColor(Hsv invertedHsv, byte alpha)
        {
            var chroma = (byte)(invertedHsv.S * invertedHsv.V);
            var huePrime = invertedHsv.H / 60.0;

            var x = (byte)(chroma * (1 - Math.Abs(huePrime % 2 - 1)));
            var modifier = (byte)(chroma - invertedHsv.V);

            if (huePrime.Between(0, 1))
            {
                return Color.FromArgb(alpha, (byte) (chroma + modifier), (byte) (x + modifier), modifier);
            }

            if (huePrime.Between(1, 2))
            {
                return Color.FromArgb(alpha, (byte) (x + modifier), modifier, (byte) (chroma + modifier));
            }
            if (huePrime.Between(2, 3))
            {
                return Color.FromArgb(alpha, modifier, (byte) (chroma + modifier), (byte)(x + modifier));
            }
            if (huePrime.Between(3, 4))
            {
                return Color.FromArgb(alpha, modifier, (byte) (x + modifier), (byte) (chroma + modifier));
            }
            if (huePrime.Between(4, 5))
            {
                return Color.FromArgb(alpha, (byte)(x + modifier), modifier, (byte)(chroma + modifier));
            }
            if (huePrime.Between(5,6, fullOpenRange: true))
            {
                return Color.FromArgb(alpha, (byte)(chroma + modifier), modifier, (byte)(x + modifier));
            }

            return Color.FromArgb(alpha, modifier, modifier, modifier);
        }

        private struct Hsv
        {
            public byte H;
            public byte S;
            public byte V;
        }

        private enum ColorPart {R,G,B}
    }

    internal static class DoubleExtensions
    {
        internal static bool Between(this double toTest, int lowerBound, int upperBound, bool fullOpenRange = false)
        {
            return fullOpenRange ? toTest >= lowerBound && toTest <= upperBound : toTest >= lowerBound && toTest < upperBound;
        }
    }
}

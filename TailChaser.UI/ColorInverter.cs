using System;
using System.Windows.Media;

namespace TailChaser.UI
{
    public static class ColorInverter
    {
        public static Color InvertColor(Color color)
        {
            ColorPart highestPart;
            var value = HighestValue(color.R, color.G, color.B, out highestPart);
            var chroma = value - LowestValue(color.R, color.G, color.B);
            var huePrime = GetHuePrime(chroma, highestPart, color);
            var hue = GetHue(huePrime);
            var saturation = GetSaturation(chroma, value);

            var invertedHsv = new Hsv { H = hue, S = saturation, V = value };

            return GetColor(invertedHsv, color.A);
        }



        private static byte HighestValue(byte r, byte g, byte b, out ColorPart part)
        {
            var candidate1 = Math.Max(r, g);
            var candidate2 = Math.Max(r, b);

            var highest = Math.Max(candidate1, candidate2);

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

        private static byte GetHuePrime(int chroma, ColorPart highestPart, Color color)
        {
            if (chroma == 0) return 0;
            switch (highestPart)
            {
                case ColorPart.R:
                    return (byte)((color.G - color.B) / chroma % 6);
                case ColorPart.G:
                    return (byte)((color.B - color.R) / chroma + 2);
                case ColorPart.B:
                    return (byte)((color.R - color.B) / chroma + 4);
                default:
                    return 0;
            }
        }

        private static byte GetSaturation(int chroma, byte value)
        {
            return (byte)(chroma > 0 ? chroma / value : 0);
        }

        private static byte GetHue(byte huePrime)
        {
            var hue = huePrime * 60;

            var colorHue = hue < 0 ? (hue + 360) : hue;
            return (byte)(colorHue < 180 ? colorHue + 180 : colorHue - 180);
        }

        private static byte LowestValue(byte r, byte g, byte b)
        {
            var candidate1 = Math.Min(r, g);
            var candidate2 = Math.Min(r, b);

            return Math.Min(candidate1, candidate2);
        }

        private static Color GetColor(Hsv invertedHsv, byte alpha)
        {
            var chroma = (byte)(invertedHsv.S * invertedHsv.V);
            var huePrime = invertedHsv.H / 60.0;

            var x = (byte)(chroma * (1 - Math.Abs(huePrime % 2 - 1)));
            var modifier = (byte)(chroma - invertedHsv.V);

            if (huePrime.Between(0, 1))
            {
                return Color.FromArgb(alpha, (byte)(chroma + modifier), (byte)(x + modifier), modifier);
            }

            if (huePrime.Between(1, 2))
            {
                return Color.FromArgb(alpha, (byte)(x + modifier), modifier, (byte)(chroma + modifier));
            }
            if (huePrime.Between(2, 3))
            {
                return Color.FromArgb(alpha, modifier, (byte)(chroma + modifier), (byte)(x + modifier));
            }
            if (huePrime.Between(3, 4))
            {
                return Color.FromArgb(alpha, modifier, (byte)(x + modifier), (byte)(chroma + modifier));
            }
            if (huePrime.Between(4, 5))
            {
                return Color.FromArgb(alpha, (byte)(x + modifier), modifier, (byte)(chroma + modifier));
            }
            if (huePrime.Between(5, 6, fullOpenRange: true))
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

        private enum ColorPart { R, G, B }


    }
}

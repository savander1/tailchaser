using System.Windows.Media;
using TailChaser.Entity.Configuration;
using TailChaser.UI.Enums;

namespace TailChaser.UI.UiHelpers
{
    public static class FilePresentationSettingsHelper
    {
        public static Color GetBackgroundColor(Settings setting)
        {
            return Color.FromArgb((byte)setting.Alpha, (byte)setting.Red, (byte)setting.Green,
                                          (byte)setting.Blue);
        }

        public static Color GetForgroundColor(Settings setting)
        {
            return GetColorForText((TextColor)setting.TextColor);
        }

        private static Color GetColorForText(TextColor color)
        {
            return color == TextColor.Dark ? Color.FromRgb(0, 0, 0) : Color.FromRgb(255, 255, 255);
        }
    }
}

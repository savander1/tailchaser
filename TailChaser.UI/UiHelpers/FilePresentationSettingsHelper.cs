using System.Windows.Media;
using TailChaser.Entity;

namespace TailChaser.UI.UiHelpers
{
    public static class FilePresentationSettingsHelper
    {
        public static Color GetBackgroundColor(FilePresentationSetting setting)
        {
            return Color.FromArgb((byte)setting.Alpha, (byte)setting.Red, (byte)setting.Green,
                                          (byte)setting.Blue);
        }

        public static Color GetForgroundColor(FilePresentationSetting setting)
        {
            return GetColorForText((TextColor) setting.TextColor);
        }

        private static Color GetColorForText(TextColor color)
        {
            return color == TextColor.Dark ? Color.FromRgb(0, 0, 0) : Color.FromRgb(255, 255, 255);
        }
    }
}

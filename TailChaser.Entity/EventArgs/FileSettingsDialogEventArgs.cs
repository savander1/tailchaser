using System.Collections.Generic;
using TailChaser.Entity.Configuration;

namespace TailChaser.Entity.EventArgs
{
    public class FileSettingsDialogEventArgs
    {
        public bool Cancel { get; private set; }
        public ICollection<Settings> Settings { get; private set; }
        public double Size { get; private set; }
        public string Font { get; set; }

        public FileSettingsDialogEventArgs(bool cancelled, ICollection<Settings> setting, double size, string font)
        {
            Cancel = cancelled;
            Settings = setting;
            Size = size;
            Font = font;
        }
    }
}
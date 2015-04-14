
namespace TailChaser.Entity.Configuration
{
    public class File : Item
    {
        public string Font { get; set; }
        public double FontSize { get; set; }
        public Settings PresentationSettings { get; set; }

        public File(string name) : base(name)
        {
        }
    }
}

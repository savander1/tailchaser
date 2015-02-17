namespace TailChaser.Entity
{
    public class TailedFile
    {
        public string Name { get; set; }
        public string FullName { get; set; }

        public TailedFile()
        {
            
        }

        public TailedFile(string name, string fullName)
        {
            Name = name;
            FullName = fullName;
        }
    }
}

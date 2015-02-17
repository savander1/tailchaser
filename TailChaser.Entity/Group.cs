using System.Collections.ObjectModel;

namespace TailChaser.Entity
{
    public class Group
    {
        public string Name { get; set; }
        public ObservableCollection<TailedFile> Files { get; set; }

        public Group()
        {
            Files = new ObservableCollection<TailedFile>();
        }

        public Group(string name)
        {
            Name = name;
            Files = new ObservableCollection<TailedFile>();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        protected bool Equals(Group other)
        {
            return string.Equals(Name, other.Name) && Equals(Files, other.Files);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ (Files != null ? Files.GetHashCode() : 0);
            }
        }
    }
}

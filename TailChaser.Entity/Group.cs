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
    }
}

using System.Collections.ObjectModel;

namespace TailChaser.Entity
{
    public class Machine
    {
        public string Name { get; set; }
        public ObservableCollection<Group> Groups { get; set; }

        public Machine()
        {
            Groups = new ObservableCollection<Group>();
        }

        public Machine(string machineName)
        {
            Name = machineName;
            Groups = new ObservableCollection<Group>();
        }
    }
}

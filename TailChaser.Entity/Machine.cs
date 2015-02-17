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

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        protected bool Equals(Machine other)
        {
            return string.Equals(Name, other.Name) && Equals(Groups, other.Groups);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ (Groups != null ? Groups.GetHashCode() : 0);
            }
        }
    }
}

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

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        protected bool Equals(TailedFile other)
        {
            return string.Equals(Name, other.Name) && string.Equals(FullName, other.FullName);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ (FullName != null ? FullName.GetHashCode() : 0);
            }
        }
    }
}

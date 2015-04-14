using System.Collections.Generic;

namespace TailChaser.Entity.Configuration
{
    public interface IParentItem
    {
        ICollection<Item> Children { get; }
        void AddChild(Item item);
    }
}
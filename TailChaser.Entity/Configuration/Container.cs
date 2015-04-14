using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TailChaser.Entity.Configuration
{
    public class Container : Item, IParentItem
    {
        public ICollection<Item> Children { get; set; }

        public Container(): this(string.Empty){
        }

        public Container(string machineName) : base(machineName)
        {
            Children = new Collection<Item>();
        }

        public void AddChild(Item item)
        {
            item.ParentId = Id;
            Children.Add(item);
        }
    }
}

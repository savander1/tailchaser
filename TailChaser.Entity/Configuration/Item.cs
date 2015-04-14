using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TailChaser.Entity.Configuration
{
    public abstract class Item: IItem 
    {
        public string Name { get; set; }
        public Guid Id { get; private set; }
        public Guid ParentId { get; set; }
        public ICollection<Item> Children { get; set; }

        protected Item(string name)
        {
            Name = name;
            Id = Guid.NewGuid();
            ParentId = Guid.Empty;
            Children = new Collection<Item>();
        }

        public void AddChild(Item item)
        {
            item.ParentId = Id;
            Children.Add(item);
        }
    }
}
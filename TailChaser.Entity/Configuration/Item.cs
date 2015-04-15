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
        

        protected Item(string name)
        {
            Name = name;
            Id = Guid.NewGuid();
            ParentId = Guid.Empty;
            
        }
    }
}
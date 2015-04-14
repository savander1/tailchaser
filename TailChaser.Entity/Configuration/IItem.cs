using System;

namespace TailChaser.Entity.Configuration
{
    public interface IItem
    {
        string Name { get; set; }
        Guid Id { get; }
        Guid ParentId { get; set; }
    }
}
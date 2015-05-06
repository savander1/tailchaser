using System;

namespace TailChaser.Entity
{
    public interface IItem
    {
        string Name { get; set; }
        Guid Id { get; set; }
        Guid ParentId { get; set; }
    }
}
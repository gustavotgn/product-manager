using Product.Domain.Interfaces;
using System;

namespace Product.Domain.Entities
{
    public abstract class BaseEntity<T> : ISoftDeleteEntity
    {
        public T Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        public bool IsActive { get; set; } = true;
        public int CreatedUserId { get; set; }
    }
}
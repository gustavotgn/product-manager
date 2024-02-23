using System;

namespace Product.Domain.Interfaces
{
    public interface ISoftDeleteEntity
    {
        public bool IsActive { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }

        public void UndoDelete()
        {
            IsActive = false;
            DeletedAt = null;
        }
    }
}

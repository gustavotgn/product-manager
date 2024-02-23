using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Product.Domain.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Infra.Data.Interceptors
{
    public class SoftDeleteInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            if (eventData.Context is null) return result;

            foreach (var entry in eventData.Context.ChangeTracker.Entries())
            {
                if (entry is not { State: EntityState.Deleted, Entity: ISoftDeleteEntity delete }) continue;
                entry.State = EntityState.Modified;
                delete.IsActive = false;
                delete.DeletedAt = DateTimeOffset.UtcNow;
            }
            return result;
        }

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            if (eventData.Context is null) return result;

            foreach (var entry in eventData.Context.ChangeTracker.Entries())
            {
                if (entry is not { State: EntityState.Deleted, Entity: ISoftDeleteEntity delete }) continue;
                entry.State = EntityState.Modified;
                delete.IsActive = false;
                delete.DeletedAt = DateTimeOffset.UtcNow;
            }
            return result;
        }
    }
}

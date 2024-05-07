using Crims.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crims.Data.Persistence.Interceptors
{
    internal class AuditionInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            if (eventData.Context is null)
            {
                return new ValueTask<InterceptionResult<int>>(result);
            }

            foreach (var entity in eventData.Context.ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entity.State)
                {
                    case EntityState.Added:
                        entity.Entity.CreatedAt = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entity.Entity.UpdatedAt = DateTime.UtcNow;
                        break;
                    case EntityState.Deleted:
                        entity.State = EntityState.Modified;
                        entity.Entity.IsDeleted = true;
                        entity.Entity.DeletedAt = DateTime.UtcNow;
                        break;
                }
            }
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
        public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            if (eventData.Context is null)
            {
                return new ValueTask<int>(result);
            }

            foreach (var entity in eventData.Context.ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entity.State)
                {
                    case EntityState.Added:
                        entity.Entity.CreatedAt = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entity.Entity.UpdatedAt = DateTime.UtcNow;
                        break;
                    case EntityState.Deleted:
                        entity.State = EntityState.Modified;
                        entity.Entity.IsDeleted = true;
                        entity.Entity.DeletedAt = DateTime.UtcNow;
                        break;
                }
            }
            return base.SavedChangesAsync(eventData, result, cancellationToken);
        }

        public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
        {
            if (eventData.Context is null)
                return result;

            foreach (var entity in eventData.Context.ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entity.State)
                {
                    case EntityState.Added:
                        entity.Entity.CreatedAt = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entity.Entity.UpdatedAt = DateTime.UtcNow;
                        break;
                    case EntityState.Deleted:
                        entity.State = EntityState.Modified;
                        entity.Entity.IsDeleted = true;
                        entity.Entity.DeletedAt = DateTime.UtcNow;
                        break;
                }
            }
            return base.SavedChanges(eventData, result);
        }
    }
}

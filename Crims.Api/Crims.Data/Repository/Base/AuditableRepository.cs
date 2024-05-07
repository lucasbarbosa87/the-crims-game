using Crims.Data.Entities;
using Crims.Data.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Crims.Data.Repository
{
    public class AuditableRepository<T>(ApplicationDbContext dbContext, ILogger<AuditableRepository<T>> logger) where T : AuditableEntity
    {
        internal readonly ApplicationDbContext DbContext = dbContext;
        internal readonly ILogger<AuditableRepository<T>> Logger = logger;
        internal readonly DbSet<T> DbSet = dbContext.Set<T>();

        public virtual async Task Delete(T obj, bool save = true)
        {
            try
            {
                DbSet.Remove(obj);
                if (save)
                    await DbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public virtual async Task<T> Add(T obj)
        {
            try
            {
                await DbSet.AddAsync(obj);
                await DbContext.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public virtual async Task<T> Update(T obj)
        {
            try
            {
                DbSet.Update(obj);
                await DbContext.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<List<T>> GetItems(Expression<Func<T, bool>>? predicate = null, bool withDeleted = false)
        {
            var query = DbSet.Where(where => where.IsDeleted == withDeleted);
            if (predicate == null)
            {
                return await query.ToListAsync();
            }
            return await query.Where(predicate).ToListAsync();
        }

        public async Task<T?> GetItem(Expression<Func<T, bool>> predicate)
        {
            var query = DbSet.Where(where => where.IsDeleted == false);
            return await query.FirstOrDefaultAsync(predicate);
        }
    }
}

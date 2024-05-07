using Crims.Data.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Crims.Data.Repository
{
    public class Repository<T> where T : class
    {
        internal readonly ApplicationDbContext DbContext;
        internal readonly ILogger<Repository<T>> Logger;
        internal readonly DbSet<T> DbSet;

        public Repository(ApplicationDbContext dbContext, ILogger<Repository<T>> logger)
        {
            DbContext = dbContext;
            DbSet = dbContext.Set<T>();
            Logger = logger;
        }

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

        public async Task<List<T>> GetItems(Expression<Func<T, bool>>? predicate)
        {
            if(predicate == null)
            {                
                return await DbSet.ToListAsync();
            }
            return await DbSet.Where(predicate).ToListAsync();
        }

        public async Task<T?> GetItem(Expression<Func<T, bool>> predicate)
        {
            return await DbSet.FirstOrDefaultAsync(predicate);
        }
    }
}

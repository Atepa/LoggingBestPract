using Microsoft.EntityFrameworkCore;
using LoggingBestPract.Application.Interfaces.Repositories;
using LoggingBestPract.Domain.Entities;
using System.Linq.Expressions;
using LoggingBestPract.Domain.Contexts;

namespace LoggingBestPract.Persistence.Repositories.MySQL
{
    public class BaseRepository<T, TKey> : IRepository<T, TKey> where T : Entity<TKey>//, new()
    {
        private readonly ApplicationDbContext context;

        public BaseRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
            return entity;
        }
        
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await context.Set<T>().AnyAsync(predicate);
        }

        public async Task DeleteAsync(T entity)
        {
            await Task.Run(() => { context.Set<T>().Remove(entity); });
        }
        
        public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = context.Set<T>();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = context.Set<T>();

            query = query.Where(predicate);

            if (includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            await Task.Run(() => { context.Set<T>().Update(entity); });
            return entity;
        }
    }
}

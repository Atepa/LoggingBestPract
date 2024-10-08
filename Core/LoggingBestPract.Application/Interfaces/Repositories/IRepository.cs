﻿using LoggingBestPract.Domain.Entities;
using System.Linq.Expressions;

namespace LoggingBestPract.Application.Interfaces.Repositories
{
    public interface IRepository<T, TKey> where T : Entity<TKey>//, new()
    {
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        
        Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties);
        
        Task<T> AddAsync(T entity);
        
        Task<T> UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

        Task<int> SaveChangesAsync();
    }
}

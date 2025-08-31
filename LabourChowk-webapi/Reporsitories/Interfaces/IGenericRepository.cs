using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LabourChowk_webapi.Reporsitories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        public Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");
        public Task<T?> GetByIdAsync(int id);
        public Task<T> AddAsync(T entity);
        public Task UpdateAsync(T entity);
        public Task DeleteAsync(T entity);
        public Task<bool> IsUniqueAsync(Expression<Func<T, bool>> predicate);

    }
}
using System.Linq.Expressions;
using LabourChowk_webapi.Data;
using LabourChowk_webapi.Reporsitories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LabourChowk_webapi.Repositories
{
    // Generic repository: reusable for any entity
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly LabourChowkContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(LabourChowkContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        // Get all rows for an entity
        // public async Task<List<T>> GetAllAsync()
        // {
        //     return await _dbSet.Include(j => j.WorkPoster).ToListAsync();
        // }

        public async Task<List<T>> GetAllAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<T> query = _context.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(
                new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }


        // Get a single row by ID
        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        // Add a new row
        public async Task<T> AddAsync(T entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        // Update an existing row
        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        // Delete a row
        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsUniqueAsync(Expression<Func<T, bool>> predicate)
        {
            return !await _dbSet.AnyAsync(predicate);
        }
    }
}

using FinancialGoalsManager.Core.Contracts.Repository.Generic;
using FinancialGoalsManager.Core.Entities;
using FinancialGoalsManager.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FinancialGoalsManager.Infrastructure.Persistence.Repository.Generic
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly SqlServerContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(SqlServerContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);

            return entity;
        }

        public async Task<IEnumerable<T>> GetAllASync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var item in includes)
            {
                query = query.Include(item);
            }
            
            return await query.ToListAsync();

        }


        public async Task<T?> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var item in includes)
            {
                query = query.Include(item);
            }
            return await query.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var existingEntity = _dbSet.SingleOrDefault(e => e.Id.Equals(entity.Id));

            if (existingEntity != null)
            {
                _context.Entry(existingEntity).CurrentValues.SetValues(entity);

                return entity;
            }

            return null;
        }

        public async Task DeleteAsync(T entity)
        {
            var existingEntity = _dbSet.Find(entity.Id);
            if (existingEntity == null)
            {
                throw new InvalidOperationException("Entity not found in the database.");
            }
            _dbSet.Remove(existingEntity);

        }

        public Task<bool> ExistsAsync(Guid id)
        {
            return _dbSet.AnyAsync(e => e.Id == id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}

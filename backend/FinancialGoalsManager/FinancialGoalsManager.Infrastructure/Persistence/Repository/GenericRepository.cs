using FinancialGoalsManager.Core.Contracts.Repository;
using FinancialGoalsManager.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FinancialGoalsManager.Infrastructure.Persistence.Repository
{
    public class GenericRepository<T> : IRepository<T> where T : class
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
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<IEnumerable<T>> GetAllASync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var item in includes)
            {
                query = query.Include(item);
            }
            return await query.AsNoTracking().ToListAsync();

        }

        public async Task<T?> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var item in includes)
            {
                query = query.Include(item);
            }
            return await query.AsNoTracking().FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var existingEntity = await _dbSet.FindAsync(EF.Property<Guid>(entity, "Id"));
            if (existingEntity == null)
            {
                throw new InvalidOperationException("Entity not found in the database.");
            }
            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteByAsync(T entity)
        {
            var existingEntity = _dbSet.Find(EF.Property<Guid>(entity, "Id"));
            if (existingEntity == null)
            {
                throw new InvalidOperationException("Entity not found in the database.");
            }
            _dbSet.Remove(existingEntity);
            await _context.SaveChangesAsync();

        }

        public Task<bool> ExistsAsync(Guid id)
        {
            return _dbSet.AnyAsync(e => EF.Property<Guid>(e, "Id") == id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}

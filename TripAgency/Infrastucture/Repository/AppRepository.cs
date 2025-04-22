using Infrastructure.Extension;
using Application.IReositosy;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace Infrastructure.Repository
{
    public class AppRepository<T>(ApplicationDbContext context) : IAppRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context = context;
        private readonly DbSet<T> _entities = context.Set<T>();

        public IQueryable<T> Table => _entities;
        #region Find
        public IQueryable<T> Find(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> query = _entities;
            if (navigationProperties is not null)
                foreach (var navigationProperty in navigationProperties)
                    query = query.Include(navigationProperty);

            return query.Where(predicate);
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> query = _entities;
            if (navigationProperties is not null)
                foreach (var navigationProperty in navigationProperties)
                    query = query.Include(navigationProperty);

            return await query.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindWithAllIncludeAsync(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _entities.IncludeAll(_context);
            return await query.Where(predicate).ToListAsync();
        }

        public IQueryable<T> FindWithComplexIncludes(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>> includeExpression)
        {
            IQueryable<T> query = includeExpression(_entities);
            return query.Where(predicate);
        }

        #endregion

        #region Get
        public IQueryable<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> query = _entities;
            if (navigationProperties is not null)
                foreach (var navigationProperty in navigationProperties)
                    query = query.Include(navigationProperty);

            return query;
        }

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> query = _entities;
            if (navigationProperties is not null)
                foreach (var navigationProperty in navigationProperties)
                    query = query.Include(navigationProperty);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllWithAllIncludeAsync()
        {
            IQueryable<T> query = _entities.IncludeAll(_context);
            return await query.ToListAsync();
        }

        #endregion

        public async Task<T> UpdateAsync(T entity)
        {
            _entities.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> RemoveAsync(T entity)
        {
            _entities.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> InsertAsync(T entity)
        {
            var newEntity = await _entities.AddAsync(entity);
            await _context.SaveChangesAsync();
            return newEntity.Entity;
        }
    }
}
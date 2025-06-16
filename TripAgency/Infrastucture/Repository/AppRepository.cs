using Application.IReositosy;
using Domain.Context;
using Infrastructure.Extension;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

public class AppRepository<T>(ApplicationDbContext context) : IAppRepository<T> where T : class
{
    private readonly ApplicationDbContext _context = context;
    private readonly DbSet<T> _entities = context.Set<T>();

    public IQueryable<T> Table => _entities;

    #region Find

    public IQueryable<T> Find(
        Expression<Func<T, bool>> predicate,
        bool asNoTracking = false,
        params Expression<Func<T, object>>[] navigationProperties)
    {
        IQueryable<T> query = _entities;

        if (asNoTracking)
            query = query.AsNoTracking();

        if (navigationProperties is not null && navigationProperties.Any())
        {
            foreach (var navigationProperty in navigationProperties)
                query = query.Include(navigationProperty);
        }

        return query.Where(predicate);
    }

    public async Task<IEnumerable<T>> FindAsync(
        Expression<Func<T, bool>> predicate,
        bool asNoTracking = false,
        params Expression<Func<T, object>>[] navigationProperties)
    {
        IQueryable<T> query = _entities;

        if (asNoTracking)
            query = query.AsNoTracking();

        if (navigationProperties is not null && navigationProperties.Any())
        {
            foreach (var navigationProperty in navigationProperties)
                query = query.Include(navigationProperty);
        }

        return await query.Where(predicate).ToListAsync();
    }

    public async Task<IEnumerable<T>> FindWithAllIncludeAsync(
        Expression<Func<T, bool>> predicate,
        bool asNoTracking = false)
    {
        IQueryable<T> query = _entities.IncludeAll(_context);

        if (asNoTracking)
            query = query.AsNoTracking();

        return await query.Where(predicate).ToListAsync();
    }

    public IQueryable<T> FindWithComplexIncludes(
        Expression<Func<T, bool>> predicate,
        Func<IQueryable<T>, IQueryable<T>> includeExpression,
        bool asNoTracking = false)
    {
        IQueryable<T> query = includeExpression(_entities);

        if (asNoTracking)
            query = query.AsNoTracking();

        return query.Where(predicate);
    }

    #endregion

    #region Get

    public IQueryable<T> GetAll(
        bool asNoTracking = false,
        params Expression<Func<T, object>>[] navigationProperties)
    {
        IQueryable<T> query = _entities;

        if (asNoTracking)
            query = query.AsNoTracking();

        if (navigationProperties is not null && navigationProperties.Any())
        {
            foreach (var navigationProperty in navigationProperties)
                query = query.Include(navigationProperty);
        }

        return query;
    }

    public async Task<IEnumerable<T>> GetAllAsync(
        bool asNoTracking = false,
        params Expression<Func<T, object>>[] navigationProperties)
    {
        IQueryable<T> query = _entities;

        if (asNoTracking)
            query = query.AsNoTracking();

        if (navigationProperties is not null && navigationProperties.Any())
        {
            foreach (var navigationProperty in navigationProperties)
                query = query.Include(navigationProperty);
        }

        return await query.ToListAsync();
    }

    public IQueryable<T> GetAllWithAllInclude(
        bool asNoTracking = false)
    {
        IQueryable<T> query = _entities.IncludeAll(_context);

        if (asNoTracking)
            query = query.AsNoTracking();

        return query;
    }

    public async Task<IEnumerable<T>> GetAllWithAllIncludeAsync(
        bool asNoTracking = false)
    {
        IQueryable<T> query = _entities.IncludeAll(_context);

        if (asNoTracking)
            query = query.AsNoTracking();

        return await query.ToListAsync();
    }

    #endregion

    #region CRUD

    public async Task<T> InsertAsync(T entity, bool asNoTracking = false)
    {
        var newEntity = await _entities.AddAsync(entity);
        await _context.SaveChangesAsync();
        return newEntity.Entity;
    }

    public async Task<T> UpdateAsync(T entity, bool asNoTracking = false)
    {
        _context.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<T> RemoveAsync(T entity, bool asNoTracking = false)
    {
        _entities.Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    #endregion
}
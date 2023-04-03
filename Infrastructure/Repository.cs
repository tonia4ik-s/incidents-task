using System.Linq.Expressions;
using Ardalis.Specification.EntityFrameworkCore;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class Repository <TEntity> : RepositoryBase<TEntity>, IRepository<TEntity> where TEntity : class
{
    private readonly AppDbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public Repository(AppDbContext context) : base(context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> Get(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "")
    {
        IQueryable<TEntity> query = _dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        query = includeProperties.Split(new [] { ',' }, 
                StringSplitOptions.RemoveEmptyEntries)
            .Aggregate(query, (current, includeProperty) => 
                current.Include(includeProperty));

        return orderBy != null ? await orderBy(query).ToListAsync() : await query.ToListAsync();
    }

    public async Task<TEntity?> GetByKeyAsync<TKey>(TKey key)
    {
        return await _dbSet.FindAsync(key);
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        return (await _dbSet.AddAsync(entity)).Entity;
    }

    public async Task<IList<TEntity>> AddRangeAsync(IList<TEntity> entities)
    {
        await _dbSet.AddRangeAsync(entities);
        await _context.SaveChangesAsync();

        return entities.ToList();
    }

    public async Task UpdateAsync(TEntity entityToUpdate)
    {
        await Task.Run(() =>
        {
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        });
    }

    public async Task DeleteAsync(TEntity entityToDelete)
    {
        await Task.Run(() =>
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        });
    }

    public Task DeleteRange(IEnumerable<TEntity> entitiesToDelete)
    {
        _dbSet.RemoveRange(entitiesToDelete);
        return Task.CompletedTask;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}

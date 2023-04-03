using System.Linq.Expressions;

namespace Core.Interfaces;

public interface IRepository<TEntity>
{
    Task<IEnumerable<TEntity>> Get(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "");

    Task<TEntity?> GetByKeyAsync<TKey>(TKey key);

    Task<TEntity> AddAsync(TEntity entity);

    Task<IList<TEntity>> AddRangeAsync(IList<TEntity> entities);

    Task UpdateAsync(TEntity entityToUpdate);
    
    Task DeleteAsync(TEntity entityToDelete);

    Task DeleteRange(IEnumerable<TEntity> entitiesToDelete);

    Task<int> SaveChangesAsync();
}

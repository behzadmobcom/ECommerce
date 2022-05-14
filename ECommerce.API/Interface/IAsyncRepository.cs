using System.Linq.Expressions;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Interface;

public interface IAsyncRepository<TEntity> where TEntity : class, IBaseEntity<int>
{
    DbSet<TEntity> Entities { get; }
    IQueryable<TEntity> Table { get; }
    IQueryable<TEntity> TableNoTracking { get; }

    TEntity GetById(params object[] ids);
    Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken);
    Task<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] ids);

    Task<IEnumerable<TEntity>> Where(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken);

    void Add(TEntity entity, bool saveNow = true);
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true);
    void AddRange(IEnumerable<TEntity> entities, bool saveNow = true);
    Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true);

    void Update(TEntity entity, bool saveNow = true);
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true);
    void UpdateRange(IEnumerable<TEntity> entities, bool saveNow = true);
    Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true);

    void Delete(TEntity entity, bool saveNow = true);
    Task DeleteAsync(object id, CancellationToken cancellationToken, bool saveNow = true);
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true);
    void DeleteRange(IEnumerable<TEntity> entities, bool saveNow = true);
    Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true);

    void LoadCollection<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty)
        where TProperty : class;

    Task LoadCollectionAsync<TProperty>(TEntity entity,
        Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty, CancellationToken cancellationToken)
        where TProperty : class;

    void LoadReference<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty)
        where TProperty : class;

    Task LoadReferenceAsync<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty,
        CancellationToken cancellationToken) where TProperty : class;

    void Attach(TEntity entity);
    void Detach(TEntity entity);

    IQueryable<TEntity> GetAll(string includeProperties);
}
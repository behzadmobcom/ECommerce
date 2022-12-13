using System.Linq.Expressions;
using ECommerce.API.DataContext;
using ECommerce.API.Interface;
using ECommerce.API.Utilities;
using Ecommerce.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Repository;

public class AsyncRepository<TEntity> : IAsyncRepository<TEntity> where TEntity : class, IBaseEntity<int>
{
    protected SunflowerECommerceDbContext DbContext;

    public AsyncRepository(SunflowerECommerceDbContext dbContext)
    {
        DbContext = dbContext;
        Entities = DbContext.Set<TEntity>();
    }

    public DbSet<TEntity> Entities { get; }
    public virtual IQueryable<TEntity> Table => Entities;
    public virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();

    public IQueryable<TEntity> GetAll(string includeProperties)
    {
        IQueryable<TEntity> query = Entities;

        if (includeProperties != null)
            foreach (var includeProperty in includeProperties.Split
                         (new[] {','}, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

        return query;
    }

    
    public virtual TEntity GetByIdWithInclude(string includeProperties, int ids)
    {
        IQueryable<TEntity> query = Entities;
        query = query.Where(x => x.Id == ids);

        if (includeProperties != null)
            foreach (var includeProperty in includeProperties.Split
                         (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);    

        return query.FirstOrDefault(); 
    }

    public virtual async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await DbContext.SaveChangesAsync(cancellationToken);
    }

    #region GetById

    public virtual TEntity GetById(params object[] ids)
    {
        return Entities.Find(ids);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken)
    {
        return await Entities.AsNoTracking().OrderBy(on => on.Id).ToListAsync(cancellationToken);
        //return PagedList<TEntity>.ToPagedList(await Entities.AsNoTracking().OrderBy(on => on.Id).ToListAsync(cancellationToken),
        //    paginationParameters.PageNumber,
        //    paginationParameters.PageSize);
    }

    public virtual async Task<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] ids)
    {
        return await Entities.FindAsync(ids, cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> Where(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return await Entities.Where(predicate).ToListAsync(cancellationToken);
    }

    #endregion

    #region Add

    public virtual void Add(TEntity entity, bool saveNow = true)
    {
        Assert.NotNull(entity, nameof(entity));
        Entities.Add(entity);

        if (saveNow) DbContext.SaveChanges();
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken,
        bool saveNow = true)
    {
        Assert.NotNull(entity, nameof(entity));
        await Entities.AddAsync(entity, cancellationToken);

        if (saveNow) await DbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public virtual void AddRange(IEnumerable<TEntity> entities, bool saveNow = true)
    {
        Assert.NotNull(entities, nameof(entities));
        Entities.AddRange(entities);

        if (saveNow) DbContext.SaveChanges();
    }

    public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken,
        bool saveNow = true)
    {
        Assert.NotNull(entities, nameof(entities));
        await Entities.AddRangeAsync(entities, cancellationToken);

        if (saveNow) await DbContext.SaveChangesAsync(cancellationToken);
    }

    #endregion

    #region Update

    public virtual void Update(TEntity entity, bool saveNow = true)
    {
        Assert.NotNull(entity, nameof(entity));
        Entities.Update(entity);
        DbContext.SaveChanges();
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken,
        bool saveNow = true)
    {
        Assert.NotNull(entity, nameof(entity));
        //Entities.Update(entity);
        var entry = DbContext.Entry(entity);
        entry.State = EntityState.Modified;
        if (saveNow) await DbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public virtual void UpdateRange(IEnumerable<TEntity> entities, bool saveNow = true)
    {
        Assert.NotNull(entities, nameof(entities));
        Entities.UpdateRange(entities);

        if (saveNow) DbContext.SaveChanges();
    }

    public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken,
        bool saveNow = true)
    {
        Assert.NotNull(entities, nameof(entities));
        Entities.UpdateRange(entities);

        if (saveNow) await DbContext.SaveChangesAsync(cancellationToken);
    }

    #endregion

    #region Delete

    public virtual void Delete(TEntity entity, bool saveNow = true)
    {
        Assert.NotNull(entity, nameof(entity));
        Entities.Remove(entity);

        if (saveNow) DbContext.SaveChanges();
    }

    public virtual async Task DeleteAsync(object id, CancellationToken cancellationToken, bool saveNow = true)
    {
        var entity = await Entities.FindAsync(new[] {id}, cancellationToken);
        Entities.Remove(entity);

        if (saveNow) await DbContext.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
    {
        Assert.NotNull(entity, nameof(entity));
        Entities.Remove(entity);

        if (saveNow) await DbContext.SaveChangesAsync(cancellationToken);
    }

    public virtual void DeleteRange(IEnumerable<TEntity> entities, bool saveNow = true)
    {
        Assert.NotNull(entities, nameof(entities));
        Entities.RemoveRange(entities);

        if (saveNow) DbContext.SaveChanges();
    }

    public virtual async Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken,
        bool saveNow = true)
    {
        Assert.NotNull(entities, nameof(entities));
        Entities.RemoveRange(entities);

        if (saveNow) await DbContext.SaveChangesAsync(cancellationToken);
    }

    #endregion

    #region Explicit Loading

    public virtual async Task LoadCollectionAsync<TProperty>(TEntity entity,
        Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty, CancellationToken cancellationToken)
        where TProperty : class
    {
        Attach(entity);

        var collection = DbContext.Entry(entity).Collection(collectionProperty);
        if (!collection.IsLoaded) await collection.LoadAsync(cancellationToken);
    }

    public virtual void LoadCollection<TProperty>(TEntity entity,
        Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty) where TProperty : class
    {
        Attach(entity);
        var collection = DbContext.Entry(entity).Collection(collectionProperty);
        if (!collection.IsLoaded) collection.Load();
    }

    public virtual async Task LoadReferenceAsync<TProperty>(TEntity entity,
        Expression<Func<TEntity, TProperty>> referenceProperty, CancellationToken cancellationToken)
        where TProperty : class
    {
        Attach(entity);
        var reference = DbContext.Entry(entity).Reference(referenceProperty);
        if (!reference.IsLoaded) await reference.LoadAsync(cancellationToken);
    }

    public virtual void LoadReference<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty)
        where TProperty : class
    {
        Attach(entity);
        var reference = DbContext.Entry(entity).Reference(referenceProperty);
        if (!reference.IsLoaded) reference.Load();
    }

    #endregion

    #region Attach & Detach

    public virtual void Detach(TEntity entity)
    {
        Assert.NotNull(entity, nameof(entity));
        var entry = DbContext.Entry(entity);
        if (entry != null) entry.State = EntityState.Detached;
    }

    public virtual void Attach(TEntity entity)
    {
        Assert.NotNull(entity, nameof(entity));
        if (DbContext.Entry(entity).State == EntityState.Detached) Entities.Attach(entity);
    }

    #endregion
}
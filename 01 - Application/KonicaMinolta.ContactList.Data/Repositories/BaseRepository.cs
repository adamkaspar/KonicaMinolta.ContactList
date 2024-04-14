using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using KonicaMinolta.Shared.Entities;

namespace KonicaMinolta.ContactList.Data.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly ContactListDbContext context;

    protected readonly DbSet<TEntity> set;

    public BaseRepository(ContactListDbContext context)
        => (this.context, this.set) = (context, context.Set<TEntity>());

    public virtual List<TEntity> GetAll()
        => set.ToList();

    public virtual Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        => set.ToListAsync(cancellationToken);

    public virtual List<TEntity> GetAllWithCondition(Func<TEntity, bool> condition)
    {
        return set
            .Where(condition)
            .ToList();
    }

    public virtual List<TEntity> PageAll(int skip, int take)
    {
        return set
            .OrderBy(entity => entity.Id)
            .Skip(skip)
            .Take(take)
            .ToList();
    }

    public virtual Task<List<TEntity>> PageAllAsync(int skip, int take, CancellationToken cancellationToken)
    {
        return set
            .OrderBy(entity => entity.Id)
            .Skip(skip)
            .Take(take)
            .ToListAsync(cancellationToken);
    }

    public virtual List<TEntity> PageAllWithCondition(int skip, int take, Func<TEntity, bool> condition)
    {
        return set
            .Where(condition)
            .OrderBy(entity => entity.Id)
            .Skip(skip)
            .Take(take)
            .ToList();
    }

    public virtual Task<List<TEntity>> PageAllWithConditionAsync(int skip, int take, Func<TEntity, bool> condition, CancellationToken cancellationToken)
    {
        return set
            .Where(condition)
            .OrderBy(entity => entity.Id)
            .Skip(skip)
            .Take(take)
            .AsQueryable()
            .ToListAsync(cancellationToken);
    }

    public virtual TEntity FindById(int id)
        => set.Find(id);

    public virtual ValueTask<TEntity> FindByIdAsync(int id, CancellationToken cancellationToken)
        => set.FindAsync(id, cancellationToken);

    public virtual EntityEntry<TEntity> Add(TEntity entity)
        => set.Add(entity);

    public virtual void Remove(int id)
    {
        var entity = FindById(id);

        if(entity is not null)
        {
            set.Remove(entity);
        }        
    }

    public virtual int SaveChanges()
        => context.SaveChanges();

    public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        => context.SaveChangesAsync(cancellationToken);
}
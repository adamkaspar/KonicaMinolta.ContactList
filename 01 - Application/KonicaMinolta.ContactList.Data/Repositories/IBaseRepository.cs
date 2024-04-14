using Microsoft.EntityFrameworkCore.ChangeTracking;
using KonicaMinolta.Shared.Entities;

namespace KonicaMinolta.ContactList.Data.Repositories;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    List<TEntity> GetAll();

    Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken);

    List<TEntity> GetAllWithCondition(Func<TEntity, bool> condition);

    List<TEntity> PageAll(int skip, int take);

    Task<List<TEntity>> PageAllAsync(int skip, int take, CancellationToken cancellationToken);

    List<TEntity> PageAllWithCondition(int skip, int take, Func<TEntity, bool> condition);

    Task<List<TEntity>> PageAllWithConditionAsync(int skip, int take, Func<TEntity, bool> condition, CancellationToken cancellationToken);

    TEntity FindById(int id);

    ValueTask<TEntity> FindByIdAsync(int id, CancellationToken cancellationToken);

    EntityEntry<TEntity> Add(TEntity entity);

    void Remove(int id);

    int SaveChanges();
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}

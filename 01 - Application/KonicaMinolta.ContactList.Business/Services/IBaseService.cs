using KonicaMinolta.Shared.Entities;

namespace KonicaMinolta.ContactList.Business.Services;

public interface IBaseService<TEntity, TDTOEntity, TDTOResultEntity> where TEntity : BaseEntity where TDTOResultEntity : class
{
    List<TDTOResultEntity> GetAll();

    Task<List<TDTOResultEntity>> GetAllAsync(CancellationToken cancellationToken);

    List<TDTOResultEntity> GetAllWithCondition(Func<TEntity, bool> condition);

    List<TDTOResultEntity> PageAll(int skip, int take);

    Task<List<TDTOResultEntity>> PageAllAsync(int skip, int take, CancellationToken cancellationToken);

    List<TDTOResultEntity> PageAllWithCondition(int skip, int take, Func<TEntity, bool> condition);

    Task<List<TDTOResultEntity>> PageAllWithConditionAsync(int skip, int take, Func<TEntity, bool> condition, CancellationToken cancellationToken);

    TDTOResultEntity FindById(int id);

    Task<TDTOResultEntity> FindByIdAsync(int id, CancellationToken cancellationToken);

    TDTOResultEntity Add(TDTOEntity dtoEntity);

    Task<TDTOResultEntity> AddAsync(TDTOEntity dtoEntity, CancellationToken cancellationToken);

    TDTOResultEntity Update(int id, TDTOEntity dtoEntity);

    Task<TDTOResultEntity> UpdateAsync(int id, TDTOEntity dtoEntity, CancellationToken cancellationToken);

    bool Remove(int id);

    Task<bool> RemoveAsync(int id, CancellationToken cancellationToken);
}
using AutoMapper;
using KonicaMinolta.Shared.Entities;
using KonicaMinolta.ContactList.Data.Repositories;

namespace KonicaMinolta.ContactList.Business.Services;

public class BaseService<TEntity, TDTOEntity, TDTOResultEntity> : IBaseService<TEntity, TDTOEntity, TDTOResultEntity> where TEntity : BaseEntity where TDTOResultEntity : class
{
    protected readonly IBaseRepository<TEntity> repository;

    protected readonly IMapper mapper;

    public BaseService(IBaseRepository<TEntity> repository, IMapper mapper) 
        => (this.repository, this.mapper) = (repository, mapper);

    public virtual List<TDTOResultEntity> GetAll()
        => mapper.Map<List<TDTOResultEntity>>(repository.GetAll());

    public virtual async Task<List<TDTOResultEntity>> GetAllAsync(CancellationToken cancellationToken) 
        => mapper.Map<List<TDTOResultEntity>>(await repository.GetAllAsync(cancellationToken));

    public virtual List<TDTOResultEntity> GetAllWithCondition(Func<TEntity, bool> condition) 
        => mapper.Map<List<TDTOResultEntity>>(repository.GetAllWithCondition(condition));

    public virtual List<TDTOResultEntity> PageAll(int skip, int take) 
        => mapper.Map<List<TDTOResultEntity>>(repository.PageAll(skip, take));

    public virtual async Task<List<TDTOResultEntity>> PageAllAsync(int skip, int take, CancellationToken cancellationToken)
        => mapper.Map<List<TDTOResultEntity>>(await repository.PageAllAsync(skip, take, cancellationToken));

    public virtual List<TDTOResultEntity> PageAllWithCondition(int skip, int take, Func<TEntity, bool> condition) 
        => mapper.Map<List<TDTOResultEntity>>(repository.PageAllWithCondition(skip, take, condition));

    public virtual async Task<List<TDTOResultEntity>> PageAllWithConditionAsync(int skip, int take, Func<TEntity, bool> condition, CancellationToken cancellationToken) 
        => mapper.Map<List<TDTOResultEntity>>(await repository.PageAllWithConditionAsync(skip, take, condition, cancellationToken));

    public virtual TDTOResultEntity FindById(int id) 
        => mapper.Map<TDTOResultEntity>(repository.FindById(id));

    public virtual async Task<TDTOResultEntity> FindByIdAsync(int id, CancellationToken cancellationToken) 
        => mapper.Map<TDTOResultEntity>(await repository.FindByIdAsync(id, cancellationToken));

    public virtual TDTOResultEntity Add(TDTOEntity dtoEntity)
    {   
        var result = repository.Add(mapper.Map<TEntity>(dtoEntity));

        repository.SaveChanges();

        return mapper.Map<TDTOResultEntity>(result);
    }
    public virtual async Task<TDTOResultEntity> AddAsync(TDTOEntity dtoEntity, CancellationToken cancellationToken)
    {        
        var result = repository.Add(mapper.Map<TEntity>(dtoEntity));

        await repository.SaveChangesAsync(cancellationToken);

        return mapper.Map<TDTOResultEntity>(result.Entity);
    }

    public virtual TDTOResultEntity Update(int id, TDTOEntity dtoEntity)
    {        
        var result = repository.FindById(id);

        if (result is not null)
        {
            mapper.Map(dtoEntity, result);

            repository.SaveChanges();

            return mapper.Map<TDTOResultEntity>(result);
        }

        return null;
    }
    public virtual async Task<TDTOResultEntity> UpdateAsync(int id, TDTOEntity dtoEntity, CancellationToken cancellationToken)
    {    
        var result = await repository.FindByIdAsync(id, cancellationToken);

        if (result is not null)
        {
            mapper.Map(dtoEntity, result);

            await repository.SaveChangesAsync(cancellationToken);

            return mapper.Map<TDTOResultEntity>(result);
        }

        return null;
    }

    public virtual bool Remove(int id)
    {        
        repository.Remove(id);

        return repository.SaveChanges() > 0;
    }
    public virtual async Task<bool> RemoveAsync(int id, CancellationToken cancellationToken)
    {        
        repository.Remove(id);

        return await repository.SaveChangesAsync(cancellationToken) > 0;
    }
}
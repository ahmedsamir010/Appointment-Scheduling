using Application.Repositories;
using Application.Services;
using Mapster;

namespace Infrastructre.Services;
public class BaseService<TEntity>(IUnitOfWork unitOfWork) : IBaseService<TEntity> where TEntity : class
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public Task<bool> CreateAsync(TEntity entity)
    {
        var repository = _unitOfWork.Repository<TEntity>().AddAsync(entity);
        return _unitOfWork.CompleteAsync().ContinueWith(task => task.Result > 0);
    }
    public Task<bool> DeleteAsync(TEntity entity)
    {
        var repository = _unitOfWork.Repository<TEntity>();
        repository.HardDeleteAsync(entity);
        return _unitOfWork.CompleteAsync().ContinueWith(task => task.Result > 0);
    }
    public Task<bool> UpdateAsync(TEntity entity)
    {
        var repository = _unitOfWork.Repository<TEntity>();
        repository.UpdateAsync(entity);
        return _unitOfWork.CompleteAsync().ContinueWith(task => task.Result > 0);
    }
    public async Task<IEnumerable<TDto>> GetAllAsync<TDto>()
    {
        var entities = await _unitOfWork.Repository<TEntity>().GetAllAsync();
        return entities.Adapt<IEnumerable<TDto>>();
    }
    public Task<TEntity?> GetByIdAsync(int id)
    {
        return _unitOfWork.Repository<TEntity>().GetByIdAsync(id);
    }
}
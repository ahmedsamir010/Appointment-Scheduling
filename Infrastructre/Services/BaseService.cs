using Application.Pagination;
using Application.Repositories;
using Application.RequestFilters;
using Application.Services;
using Infrastructre.Data;
using Mapster;

namespace Infrastructre.Services;

public class BaseService<TEntity>(IUnitOfWork unitOfWork) : IBaseService<TEntity> where TEntity : class
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<bool> CreateAsync(TEntity entity)
    {
        await _unitOfWork.Repository<TEntity>().AddAsync(entity);
        return await _unitOfWork.CompleteAsync() > 0;
    }

    public async Task<bool> DeleteAsync(TEntity entity)
    {
        await _unitOfWork.Repository<TEntity>().HardDeleteAsync(entity);
        return await _unitOfWork.CompleteAsync() > 0;
    }

    public async Task<bool> UpdateAsync(TEntity entity)
    {
        await _unitOfWork.Repository<TEntity>().UpdateAsync(entity);
        return await _unitOfWork.CompleteAsync() > 0;
    }

    public async Task<PaginatedList<TEntity>> GetAllAsync(RequestFilter requestFilter)
    {
        var entities =  _unitOfWork.Repository<TEntity>().GetQueryable();

        var paginatedEntities = await PaginatedList<TEntity>.CreateAsync(
            entities,
            requestFilter.PageNumber,
            requestFilter.PageSize
        );

        return paginatedEntities;
    }

    public Task<TEntity?> GetByIdAsync(int id)
    {
        return _unitOfWork.Repository<TEntity>().GetByIdAsync(id);
    }
}

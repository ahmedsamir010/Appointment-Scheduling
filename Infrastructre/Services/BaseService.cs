using Application.Pagination;
using Application.Repositories;
using Application.RequestFilters;
using Application.Services;
using System.Linq.Dynamic.Core;

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

    public async Task<PaginatedList<TEntity>> GetAllAsync(RequestFilter filter)
    {
        var entities = _unitOfWork.Repository<TEntity>().GetQueryable();

        if(filter.SearchValue is not null)

        entities = entities.Where(filter.SearchValue);

        if(filter.SortColumn is not null && filter.SortDirection is not null)
        entities = entities.OrderBy($"{filter.SortColumn} {filter.SortDirection}");

        return await PaginatedList<TEntity>.CreateAsync(entities, filter.PageNumber, filter.PageSize);
    }
    public Task<TEntity?> GetByIdAsync(int id)
    {
        return _unitOfWork.Repository<TEntity>().GetByIdAsync(id);
    }
}
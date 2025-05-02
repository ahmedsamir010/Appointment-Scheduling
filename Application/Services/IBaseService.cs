using Application.Pagination;
using Application.RequestFilters;

namespace Application.Services;
public interface IBaseService<TEntity> where TEntity : class
{
    Task<bool> CreateAsync(TEntity entity);
    Task<bool> DeleteAsync(TEntity entity);
    Task<bool> UpdateAsync(TEntity entity);
    Task<PaginatedList<TEntity>> GetAllAsync(RequestFilter requestFilter);
    Task<TEntity?> GetByIdAsync(int id);
}
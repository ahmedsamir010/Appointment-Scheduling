namespace Application.Services;
public interface IBaseService<TEntity> where TEntity : class
{
    Task<bool> CreateAsync(TEntity entity);
    Task<bool> DeleteAsync(TEntity entity);
    Task<bool> UpdateAsync(TEntity entity);
    Task<IEnumerable<TDto>> GetAllAsync<TDto>();
    Task<TEntity?> GetByIdAsync(int id);
}


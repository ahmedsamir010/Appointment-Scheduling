namespace Application.Repositories;
public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<TEntity> AddAsync(TEntity entity);
    Task HardDeleteAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(int id);
}

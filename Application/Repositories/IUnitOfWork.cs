using Microsoft.EntityFrameworkCore.Storage;

namespace Application.Repositories;
public interface IUnitOfWork : IAsyncDisposable
{
    Task<int> CompleteAsync();
    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;
    Task<IDbContextTransaction> BeginTransactionAsync();
}
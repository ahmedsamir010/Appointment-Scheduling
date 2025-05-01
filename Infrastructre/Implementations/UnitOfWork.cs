using Application.Repositories;
using Infrastructre.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Concurrent;
using System.Data;

namespace Infrastructre.Implementations;

public class UnitOfWork(ApplicationDbContext dbContext) : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly ConcurrentDictionary<Type, object> _repositories = new();

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        var type = typeof(TEntity);

        if (_repositories.TryGetValue(type, out var repo))
            return (IGenericRepository<TEntity>)repo;

        var newRepo = new GenericRepository<TEntity>(_dbContext);
        _repositories.TryAdd(type, newRepo);

        return newRepo;
    }

    public async Task<int> CompleteAsync()
        => await _dbContext.SaveChangesAsync();

    public async ValueTask DisposeAsync()
    {
        await _dbContext.DisposeAsync();
        GC.SuppressFinalize(this);
    }
    public async Task<IDbContextTransaction> BeginTransactionAsync()
         => await _dbContext.Database.BeginTransactionAsync(IsolationLevel.Serializable);
    
}

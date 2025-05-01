using Microsoft.EntityFrameworkCore;
using Application.Repositories;
using Infrastructre.Data;

namespace Infrastructre.Implementations;
public class GenericRepository<T>(ApplicationDbContext dbContext) : IGenericRepository<T> where T : class
{
    public async Task<T> AddAsync(T entity)
    {
        await dbContext.Set<T>().AddAsync(entity);
        return entity;
    }
    public Task HardDeleteAsync(T entity)
    {
        dbContext.Set<T>().Remove(entity);
        return Task.CompletedTask;
    }
    public Task UpdateAsync(T entity)
    {
        dbContext.Set<T>().Update(entity);
        return Task.CompletedTask;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
        => await dbContext.Set<T>().AsNoTracking().ToListAsync();

    public async Task<T?> GetByIdAsync(int id)
        => await dbContext.Set<T>().FindAsync(id);
    public IQueryable<T> GetQueryable()
        => dbContext.Set<T>().AsQueryable();

}

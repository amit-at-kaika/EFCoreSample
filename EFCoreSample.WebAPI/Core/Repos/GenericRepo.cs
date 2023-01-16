using EFCoreSample.WebAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace EFCoreSample.WebAPI.Core;

public class GenericRepo<T> : IGenericRepo<T> where T : class
{
    protected ApiDbContext _dbContext;
    internal DbSet<T> _dbSet;
    protected readonly ILogger _logger;

    public GenericRepo(ApiDbContext dbContext, ILogger logger)
    {
        _dbContext = dbContext;
        _logger = logger;
        _dbSet = _dbContext.Set<T>();
    } 
    
    public virtual async Task<bool> Add(T obj)
    {
        await _dbSet.AddAsync(obj);
        return true;
    }

    public virtual async Task<bool> Delete(T obj)
    {
       _dbSet.Remove(obj);
        return true;
    }

    public virtual async Task<IEnumerable<T>> GetAll()
    {
        return await _dbSet.ToListAsync();
    }

    public virtual async Task<T?> GetById(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task<bool> Update(T obj)
    {
        _dbSet.Update(obj);
        return true;
    }
}   
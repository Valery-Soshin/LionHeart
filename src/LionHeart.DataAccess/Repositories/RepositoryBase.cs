using LionHeart.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LionHeart.DataAccess.Repositories;

public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly ApplicationDbContext _dbContext;
    protected readonly DbSet<TEntity> _dbSet;

    public RepositoryBase(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<TEntity>();
    }

    public virtual async Task<TEntity?> GetById(string id)
    {
        return await _dbSet.FindAsync(id);
    }
    public virtual Task<List<TEntity>> GetAll()
    {                       
        return _dbSet.ToListAsync();
    }
    public virtual Task<int> Add(TEntity entity)
    {
        _dbSet.Add(entity);
        return SaveChangesAsync();
    }
    public virtual Task<int> AddRange(IEnumerable<TEntity> entities)
    {
        _dbSet.AddRange(entities);
        return SaveChangesAsync();
    }
    public virtual Task<int> Update(TEntity entity)
    {
        _dbSet.Update(entity);
        return SaveChangesAsync();
    }
    public virtual Task<int> UpdateRange(IEnumerable<TEntity> entities)
    {
        _dbSet.UpdateRange(entities);
        return SaveChangesAsync();
    }
    public virtual Task<int> Remove(TEntity entity)
    {
        _dbSet.Remove(entity);
        return SaveChangesAsync();
    }
    public virtual Task<int> RemoveRange(IEnumerable<TEntity> entities)
    {
        _dbSet.RemoveRange(entities);
        return SaveChangesAsync();
    }
    protected virtual async Task<int> SaveChangesAsync()
    {
        int result = -1;
        try
        {
            result = await _dbContext.SaveChangesAsync();
        }
        catch { }

        _dbContext.ChangeTracker.Clear();
        return result;
    }

    public void Dispose()
    {
        _dbContext?.Dispose();
    }
}
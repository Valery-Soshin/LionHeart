using LionHeart.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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
    public virtual Task<int> Update(TEntity entity)
    {
        _dbSet.Update(entity);
        return SaveChangesAsync();
    }
    public virtual Task<int> Remove(TEntity entity)
    {
        _dbSet.Remove(entity);
        return SaveChangesAsync();
    }
    public async Task<int> Remove(string id)
    {
        var type = _dbSet.EntityType.ClrType;
        var entry = await _dbContext.FindAsync(type, id);
        _dbContext.Remove(entry);
        return await SaveChangesAsync();
    }
    protected virtual Task<int> SaveChangesAsync()
    {
        return _dbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        _dbContext?.Dispose();
    }
}
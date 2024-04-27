using LionHeart.Core.Interfaces.Repositories;
using LionHeart.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LionHeart.DataAccess.Repositories;

public abstract class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly ApplicationDbContext _dbContext;
    protected readonly DbSet<TEntity> _dbSet;

    public RepositoryBase(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<TEntity>();
    }

    public abstract Task<TEntity?> GetById(string id);
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
    protected async Task<PagedResponse<T>> BuildPagination<T>(IQueryable<T> totalRecordsQuery,
                                                             IQueryable<T> entitiesQuery,
                                                             int pageNumber,
                                                             int pageSize,
                                                             Expression<Func<T, bool>>? filter = null)
    {
        if (filter is not null)
        {
            totalRecordsQuery = totalRecordsQuery.Where(filter);
            entitiesQuery = entitiesQuery.Where(filter);
        }

        var totalRecords = await totalRecordsQuery
            .CountAsync();

        if (pageNumber <= 0) pageNumber = 1;
        var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
        if (pageNumber > totalPages) pageNumber = totalPages;

        var entites = await entitiesQuery
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResponse<T>(entites, pageNumber, totalPages);
    }

    public void Dispose()
    {
        _dbContext?.Dispose();
    }
}
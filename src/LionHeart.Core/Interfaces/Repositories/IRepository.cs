namespace LionHeart.Core.Interfaces.Repositories;

public interface IRepository<TEntity> : IDisposable where TEntity: class
{
    Task<TEntity?> GetById(string id);
    Task<int> Add(TEntity entity);
    Task<int> AddRange(IEnumerable<TEntity> entities);
    Task<int> Update(TEntity entity);
    Task<int> UpdateRange(IEnumerable<TEntity> entities);
    Task<int> Remove(TEntity entity);
    Task<int> RemoveRange(IEnumerable<TEntity> entities);
}
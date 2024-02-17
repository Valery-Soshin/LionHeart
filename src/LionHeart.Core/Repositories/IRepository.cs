namespace LionHeart.Core.Repositories;

public interface IRepository<TEntity> : IDisposable where TEntity: class
{
    Task<TEntity?> GetById(string id);
    Task<List<TEntity>> GetAll();
    Task<int> Add(TEntity entity);
    Task<int> Update(TEntity entity);
    Task<int> Remove(TEntity entity);
    Task<int> Remove(string id);
}
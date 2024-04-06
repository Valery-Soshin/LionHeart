namespace LionHeart.Core.Interfaces.Repositories;

public interface IUnitOfWork
{
    Task BeginTransaction();
    Task Commit();
    Task Rollback();
}
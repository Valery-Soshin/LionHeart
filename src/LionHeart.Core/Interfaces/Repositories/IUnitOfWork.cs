namespace LionHeart.Core.Interfaces.Repositories;

public interface IUnitOfWork
{
    bool IsTransactionActive { get; }
    Task BeginTransaction();
    Task Commit();
    Task Rollback();
}
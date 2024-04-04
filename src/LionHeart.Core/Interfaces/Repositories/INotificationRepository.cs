using LionHeart.Core.Models;

namespace LionHeart.Core.Interfaces.Repositories;

public interface INotificationRepository : IRepository<Notification>
{
    Task<List<Notification>> GetNotificationsByUserId(string userId);
    Task<int> Count(string userId);
}
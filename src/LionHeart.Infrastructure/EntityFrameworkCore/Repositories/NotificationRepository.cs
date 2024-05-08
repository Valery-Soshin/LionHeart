using LionHeart.Core.Interfaces.Repositories;
using LionHeart.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace LionHeart.Infrastructure.EntityFrameworkCore.Repositories;

public class NotificationRepository(ApplicationDbContext dbContext) : RepositoryBase<Notification>(dbContext), INotificationRepository
{
    public override Task<Notification?> GetById(string id)
    {
        return _dbContext.Notifications.AsNoTracking()
            .FirstOrDefaultAsync(n => n.Id == id);
    }
    public Task<List<Notification>> GetNotificationsByUserId(string userId)
    {
        return _dbContext.Notifications.AsNoTracking()
            .Where(n => n.UserId == userId)
            .ToListAsync();
    }
    public Task<int> Count(string userId)
    {
        return _dbContext.Notifications.AsNoTracking()
            .CountAsync(n => n.UserId == userId);
    }
}
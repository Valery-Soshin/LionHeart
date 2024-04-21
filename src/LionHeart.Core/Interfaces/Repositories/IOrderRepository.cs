using LionHeart.Core.Models;
using System.Linq.Expressions;

namespace LionHeart.Core.Interfaces.Repositories;

public interface IOrderRepository : IRepository<Order>
{
    Task<PagedResponse<Order>> GetOrdersByFilter(int pageNumber, int pageSize, Expression<Func<Order, bool>> filter);
    Task<bool> Any(string userId);
    Task<bool> Exists(string userId, string productId);
}
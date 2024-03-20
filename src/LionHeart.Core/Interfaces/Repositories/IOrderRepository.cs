using LionHeart.Core.Models;

namespace LionHeart.Core.Interfaces.Repositories;

public interface IOrderRepository : IRepository<Order>
{
	Task<List<Order>> GetOrdersByUserId(string userId);
	Task<bool> Any(string userId);
	Task<bool> Exists(string userId, string productId);
}
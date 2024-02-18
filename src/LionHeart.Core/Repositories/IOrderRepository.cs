using LionHeart.Core.Models;

namespace LionHeart.Core.Repositories;

public interface IOrderRepository : IRepository<Order>
{
	Task<List<Order>> GetOrdersByUserId(string userId);
	Task<bool> Any(string userId);
}
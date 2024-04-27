using LionHeart.Core.Models;
using System.Linq.Expressions;

namespace LionHeart.Core.Interfaces.Repositories;

public interface IOrderItemRepository : IRepository<OrderItem>
{
    Task<PagedResponse<OrderItem>> GetOrderItemsByFilter(int pageNumber, int pageSize, Expression<Func<OrderItem, bool>> filter);
}
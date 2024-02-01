using LionHeart.Core.Enums;
using LionHeart.Core.Models;

namespace LionHeart.Core.Repositories;

public interface IMarkedProductRepository : IRepository<MarkedProduct>
{
    Task<List<MarkedProduct>> GetAllByCustomerId(string customerId, Mark mark);
    Task<MarkedProduct?> GetByCustomerIdProductId(string customerId, string productId, Mark mark);
}
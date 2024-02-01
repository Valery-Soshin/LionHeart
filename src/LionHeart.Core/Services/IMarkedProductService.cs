using LionHeart.Core.Enums;
using LionHeart.Core.Models;

namespace LionHeart.Core.Services;

public interface IMarkedProductService
{
    Task<MarkedProduct?> GetById(string id);
    Task<MarkedProduct?> GetByCustomerIdProductId(string customerId, string productId, Mark mark);
    Task<List<MarkedProduct>> GetAll();
    Task<List<MarkedProduct>> GetAllByCustomerId(string customerId, Mark mark);
    Task<int> Add(MarkedProduct product);
    Task<int> Update(MarkedProduct product);
    Task<int> Remove(MarkedProduct product);
}
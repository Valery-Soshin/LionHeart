using LionHeart.Core.Dtos.Orders;
using LionHeart.Core.Models;
using LionHeart.Core.Response;

namespace LionHeart.Core.Interfaces.Services;

public interface IOrderService
{
    Task<IBaseResponse<Order>> GetById(string id);
    Task<IBaseResponse<List<Order>>> GetOrdersByUserId(string userId);
    Task<IBaseResponse<List<Order>>> GetAll();
    Task<IBaseResponse<Order>> Add(CreateOrderDto dto);
    Task<IBaseResponse<bool>> Any(string userId);
    Task<IBaseResponse<bool>> Exists(string userId, string productId);
}
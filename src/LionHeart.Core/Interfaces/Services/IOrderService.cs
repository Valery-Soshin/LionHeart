using LionHeart.Core.Dtos.Order;
using LionHeart.Core.Models;
using LionHeart.Core.Result;

namespace LionHeart.Core.Interfaces.Services;

public interface IOrderService
{
    Task<Result<Order>> GetById(string id);
    Task<Result<List<Order>>> GetOrdersByUserId(string userId);
    Task<Result<List<Order>>> GetAll();
    Task<Result<Order>> Add(AddOrderDto dto);
    Task<Result<bool>> Any(string userId);
    Task<Result<bool>> Exists(string userId, string productId);
}
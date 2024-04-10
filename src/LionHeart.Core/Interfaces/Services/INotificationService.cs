using LionHeart.Core.Dtos.Notification;
using LionHeart.Core.Models;
using LionHeart.Core.Result;

namespace LionHeart.Core.Interfaces.Services;

public interface INotificationService
{
    Task<Result<Notification>> Add(AddNotificationDto dto);
    Task<Result<List<Notification>>> AddRange(List<AddNotificationDto> dtos);
    Task<Result<Notification>> Remove(string id);
    Task<Result<List<Notification>>> GetNotificationsByUserId(string userId);
    Task<Result<int>> Count(string userId);
}
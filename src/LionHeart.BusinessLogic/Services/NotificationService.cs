using LionHeart.BusinessLogic.Resources;
using LionHeart.Core.Dtos.Notification;
using LionHeart.Core.Interfaces.Repositories;
using LionHeart.Core.Interfaces.Services;
using LionHeart.Core.Models;
using LionHeart.Core.Result;

namespace LionHeart.BusinessLogic.Services;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _notificationRepository;

    public NotificationService(INotificationRepository notificationRepository)
    {
        _notificationRepository = notificationRepository;
    }

    public async Task<Result<Notification>> Add(AddNotificationDto dto)
    {
        try
        {
            var notification = new Notification
            {
                UserId = dto.UserId,
                Content = dto.Content
            };
            var result = await _notificationRepository.Add(notification);
            if (result <= 0)
            {
                return new Result<Notification>
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.NotificationNotCreated
                };
            }
            return new Result<Notification>
            {
                IsCompleted = true,
                Data = notification
            };
        }
        catch
        {
            return new Result<Notification>
            {
                IsCompleted = false,
                ErrorMessage = ErrorMessage.InternalServerError
            };
        }
    }
    public async Task<Result<Notification>> Remove(string id)
    {
        try
        {
            var notification = await _notificationRepository.GetById(id);
            if (notification is null)
            {
                return new Result<Notification>
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.NotificationNotFound
                };
            }
            var result = await _notificationRepository.Remove(notification);
            if (result <= 0)
            {
                return new Result<Notification>
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.NotificationNotRemoved
                };
            }
            return new Result<Notification>
            {
                IsCompleted = true,
                Data = notification
            };
        }
        catch
        {
            return new Result<Notification>
            {
                IsCompleted = false,
                ErrorMessage = ErrorMessage.InternalServerError
            };
        }
    }
    public async Task<Result<List<Notification>>> GetNotificationsByUserId(string userId)
    {
        try
        {
            var notifications = await _notificationRepository.GetNotificationsByUserId(userId);
            if (notifications is null)
            {
                return new Result<List<Notification>>
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.NotificationsNotFound
                };
            }

            return new Result<List<Notification>>
            {
                IsCompleted = true,
                Data = notifications
            };
        }
        catch
        {
            return new Result<List<Notification>>
            {
                IsCompleted = false,
                ErrorMessage = ErrorMessage.InternalServerError
            };
        }
    }
    public async Task<Result<int>> Count(string userId)
    {
        try
        {
            return new Result<int>
            {
                IsCompleted = true,
                Data = await _notificationRepository.Count(userId)
            };
        }
        catch
        {
            return new Result<int>
            {
                IsCompleted = false,
                ErrorMessage = ErrorMessage.InternalServerError
            };
        }
    }
}
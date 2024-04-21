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
                Content = dto.Content,
                LinkToAction = dto.LinkToAction,
                CreatedAt = dto.CreatedAt
            };
            var result = await _notificationRepository.Add(notification);
            if (result <= 0)
            {
                return Result<Notification>.Failure(ErrorMessage.NotificationNotCreated);
            }
            return Result<Notification>.Success(notification);
        }
        catch
        {
            return Result<Notification>.Failure(ErrorMessage.InternalServerError);
        }
    }
    public async Task<Result<List<Notification>>> AddRange(List<AddNotificationDto> dtos)
    {
        try
        {
            var notifications = dtos.Select(d => new Notification()
            {
                UserId = d.UserId,
                Content = d.Content,
                LinkToAction = d.LinkToAction,
                CreatedAt = d.CreatedAt
            }).ToList();
            var result = await _notificationRepository.AddRange(notifications);
            if (result <= 0)
            {
                return Result<List<Notification>>.Failure(ErrorMessage.NotificationsNotCreated);
            }
            return Result<List<Notification>>.Success(notifications);
        }
        catch
        {
            return Result<List<Notification>>.Failure(ErrorMessage.InternalServerError);
        }
    }
    public async Task<Result<Notification>> Remove(string id)
    {
        try
        {
            var notification = await _notificationRepository.GetById(id);
            if (notification is null)
            {
                return Result<Notification>.Failure(ErrorMessage.NotificationNotFound);
            }
            var result = await _notificationRepository.Remove(notification);
            if (result <= 0)
            {
                return Result<Notification>.Failure(ErrorMessage.NotificationNotRemoved);
            }
            return Result<Notification>.Success(notification);
        }
        catch
        {
            return Result<Notification>.Failure(ErrorMessage.InternalServerError);
        }
    }
    public async Task<Result<List<Notification>>> GetNotificationsByUserId(string userId)
    {
        try
        {
            var notifications = await _notificationRepository.GetNotificationsByUserId(userId);
            return Result<List<Notification>>.Success(notifications);
        }
        catch
        {
            return Result<List<Notification>>.Failure(ErrorMessage.InternalServerError);
        }
    }
    public async Task<Result<int>> Count(string userId)
    {
        try
        {
            var result = await _notificationRepository.Count(userId);
            return Result<int>.Success(result);
        }
        catch
        {
            return Result<int>.Failure(ErrorMessage.InternalServerError);
        }
    }
}
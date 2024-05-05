using LionHeart.BusinessLogic.FluentValidations.Models;
using LionHeart.BusinessLogic.FluentValidations.Validators.Notification;
using LionHeart.BusinessLogic.Resources;
using LionHeart.Core.Dtos.Notification;
using LionHeart.Core.Interfaces.Repositories;
using LionHeart.Core.Interfaces.Services;
using LionHeart.Core.Models;
using LionHeart.Core.Results;
using LionHeart.Core.ValidationModels.Notification;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LionHeart.BusinessLogic.Services;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _notificationRepository;
    private readonly NotificationServiceValidators _validators;
    private readonly UserManager<User> _userManager;

    public NotificationService(INotificationRepository notificationRepository,
                               NotificationServiceValidators validators,
                               UserManager<User> userManager)
    {
        _notificationRepository = notificationRepository;
        _validators = validators;
        _userManager = userManager;
    }

    public async Task<Result<List<Notification>>> GetNotificationsByUserId(string userId)
    {
        try
        {
            var idValidationResult = _validators.IdValidator.Validate(new IdModel(userId));
            if (!idValidationResult.IsValid)
            {
                var errorMessages = idValidationResult.Errors.Select(e => e.ErrorMessage);
                return Result<List<Notification>>.Failure(errorMessages);
            }

            var notifications = await _notificationRepository.GetNotificationsByUserId(userId);
            return Result<List<Notification>>.Success(notifications);
        }
        catch
        {
            return Result<List<Notification>>.Failure(ErrorMessage.InternalServerError);
        }
    }
    public async Task<Result<Notification>> Add(AddNotificationDto dto)
    {
        try
        {
            var dtoValidationResult = _validators.AddNotificationDtoValidator.Validate(dto);
            if (!dtoValidationResult.IsValid)
            {
                var errorMessages = dtoValidationResult.Errors.Select(e => e.ErrorMessage);
                return Result<Notification>.Failure(errorMessages);
            }

            bool notificationAlreadyExists = await _notificationRepository
                .Exists(n => n.UserId == dto.UserId && n.Content == dto.Content);
            bool userExists = await _userManager.Users.AnyAsync(u => u.Id == dto.UserId);
            var validateAddModel = new ValidateAddModel()
            {
                NotificationAlreadyExists = notificationAlreadyExists,
                UserExists = userExists
            };
            var notificationValidatorResult = _validators.NotificationValidator.ValidateAdd(validateAddModel);
            if (notificationValidatorResult.IsFaulted)
            {
                return Result<Notification>.Failure(notificationValidatorResult.ErrorMessages);
            }

            var notification = new Notification
            {
                UserId = dto.UserId,
                Content = dto.Content,
                LinkToAction = dto.LinkToAction,
                CreatedAt = dto.CreatedAt
            };
            var notificationRepositoryResult = await _notificationRepository.Add(notification);
            if (notificationRepositoryResult <= 0)
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
            foreach (var dto in dtos)
            {
                var dtoValidationResult = _validators.AddNotificationDtoValidator.Validate(dto);
                if (!dtoValidationResult.IsValid)
                {
                    var errorMessages = dtoValidationResult.Errors.Select(e => e.ErrorMessage);
                    return Result<List<Notification>>.Failure(errorMessages);
                }

                bool notificationAlreadyExists = await _notificationRepository
                    .Exists(n => n.UserId == dto.UserId && n.Content == dto.Content);
                bool userExists = await _userManager.Users.AnyAsync(u => u.Id == dto.UserId);
                var validateAddModel = new ValidateAddModel()
                {
                    NotificationAlreadyExists = notificationAlreadyExists,
                    UserExists = userExists
                };
                var notificationValidatorResult = _validators.NotificationValidator.ValidateAdd(validateAddModel);
                if (notificationValidatorResult.IsFaulted)
                {
                    return Result<List<Notification>>.Failure(notificationValidatorResult.ErrorMessages);
                }
            }

            var notifications = dtos.Select(d => new Notification()
            {
                UserId = d.UserId,
                Content = d.Content,
                LinkToAction = d.LinkToAction,
                CreatedAt = d.CreatedAt
            }).ToList();
            var notificationRepositoryResult = await _notificationRepository.AddRange(notifications);
            if (notificationRepositoryResult <= 0)
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
            var idValidationResult = _validators.IdValidator.Validate(new IdModel(id));
            if (!idValidationResult.IsValid)
            {
                var errorMessages = idValidationResult.Errors.Select(e => e.ErrorMessage);
                return Result<Notification>.Failure(errorMessages);
            }

            var notification = await _notificationRepository.GetById(id);
            if (notification is null)
            {
                return Result<Notification>.Failure(ErrorMessage.NotificationNotFound);
            }
            var notificationRepositoryResult = await _notificationRepository.Remove(notification);
            if (notificationRepositoryResult <= 0)
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
    public async Task<Result<List<Notification>>> RemoveAll(string userId)
    {
        try
        {
            var idValidationResult = _validators.IdValidator.Validate(new IdModel(userId));
            if (!idValidationResult.IsValid)
            {
                var errorMessages = idValidationResult.Errors.Select(e => e.ErrorMessage);
                return Result<List<Notification>>.Failure(errorMessages);
            }

            var notifications = await _notificationRepository.GetNotificationsByUserId(userId);
            if (notifications.Count == 0)
            {
                return Result<List<Notification>>.Failure(ErrorMessage.NotificationsNotFound);
            }
            var notificationRepositoryResult = await _notificationRepository.RemoveRange(notifications);
            if (notificationRepositoryResult <= 0)
            {
                return Result<List<Notification>>.Failure(ErrorMessage.NotificationsNotRemoved);
            }
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
            var idValidationResult = _validators.IdValidator.Validate(new IdModel(userId));
            if (!idValidationResult.IsValid)
            {
                var errorMessages = idValidationResult.Errors.Select(e => e.ErrorMessage);
                return Result<int>.Failure(errorMessages);
            }

            int notificationCount = await _notificationRepository.Count(userId);
            return Result<int>.Success(notificationCount);
        }
        catch
        {
            return Result<int>.Failure(ErrorMessage.InternalServerError);
        }
    }
}
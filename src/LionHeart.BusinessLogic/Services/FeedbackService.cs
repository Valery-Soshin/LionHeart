using LionHeart.BusinessLogic.FluentValidations.Models;
using LionHeart.BusinessLogic.FluentValidations.Validators.Feedback;
using LionHeart.BusinessLogic.Helpers;
using LionHeart.BusinessLogic.Resources;
using LionHeart.Core.Dtos.Feedback;
using LionHeart.Core.Interfaces.Repositories;
using LionHeart.Core.Interfaces.Services;
using LionHeart.Core.Models;
using LionHeart.Core.Results;
using LionHeart.Core.ValidationModels.Feedback;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LionHeart.BusinessLogic.Services;

public class FeedbackService : IFeedbackService
{
    private readonly IFeedbackRepository _feedbackRepository;
    private readonly IProductRepository _productRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly FeedbackServiceValidators _validators;
    private readonly UserManager<User> _userManager;

    public FeedbackService(IFeedbackRepository feedbackRepository,
                           IProductRepository productRepository,
                           IOrderRepository orderRepository,
                           FeedbackServiceValidators validators,
                           UserManager<User> userManager)
    {
        _feedbackRepository = feedbackRepository;
        _productRepository = productRepository;
        _orderRepository = orderRepository;
        _validators = validators;
        _userManager = userManager;
    }

    public async Task<Result<Feedback>> GetById(string id)
    {
        try
        {
            var idValidationResult = _validators.IdValidator.Validate(new IdModel(id));
            if (!idValidationResult.IsValid)
            {
                var errorMessages = idValidationResult.Errors.Select(e => e.ErrorMessage);
                return Result<Feedback>.Failure(errorMessages);
            }

            var feedback = await _feedbackRepository.GetById(id);
            if (feedback is null)
            {
                return Result<Feedback>.Failure(ErrorMessage.FeedbackNotFound);
            }
            return Result<Feedback>.Success(feedback);
        }
        catch
        {
            return Result<Feedback>.Failure(ErrorMessage.InternalServerError);
        }
    }
    public async Task<Result<PagedResponse<Feedback>>> GetFeedbacksByUserId(string userId, int pageNumber)
    {
        try
        {
            var idValidationResult = _validators.IdValidator.Validate(new IdModel(userId));
            if (!idValidationResult.IsValid)
            {
                var errorMessages = idValidationResult.Errors.Select(e => e.ErrorMessage);
                return Result<PagedResponse<Feedback>>.Failure(errorMessages);
            }

            var page = await _feedbackRepository.GetFeedbacksByFilter(
                pageNumber, PageHelper.PageSize, f => f.UserId == userId);

            return Result<PagedResponse<Feedback>>.Success(page);
        }
        catch
        {
            return Result<PagedResponse<Feedback>>.Failure(ErrorMessage.InternalServerError);
        }
    }
    public async Task<Result<PagedResponse<Feedback>>> GetFeedbacksByProductId(string productId, int pageNumber)
    {
        try
        {
            var idValidationResult = _validators.IdValidator.Validate(new IdModel(productId));
            if (!idValidationResult.IsValid)
            {
                var errorMessages = idValidationResult.Errors.Select(e => e.ErrorMessage);
                return Result<PagedResponse<Feedback>>.Failure(errorMessages);
            }

            var page = await _feedbackRepository.GetFeedbacksByFilter(
                pageNumber, PageHelper.PageSize, f => f.ProductId == productId);

            return Result<PagedResponse<Feedback>>.Success(page);
        }
        catch
        {
            return Result<PagedResponse<Feedback>>.Failure(ErrorMessage.InternalServerError);
        }
    }
    public async Task<Result<Feedback>> Add(AddFeedbackDto dto)
    {
        try
        {
            var dtoValidationResult = _validators.AddFeedbackDtoValidator.Validate(dto);
            if (!dtoValidationResult.IsValid)
            {
                var errorMessages = dtoValidationResult.Errors.Select(e => e.ErrorMessage);
                return Result<Feedback>.Failure(errorMessages);
            }

            bool feedbackAlreadyExists = await _feedbackRepository
                .Exists(f => f.UserId == dto.UserId && f.ProductId == dto.ProductId);
            bool userExists = await _userManager.Users.AnyAsync(u => u.Id == dto.UserId);
            bool productExists = await _productRepository.Exists(p => p.Id == dto.ProductId);
            var validateAddModel = new ValidateAddModel()
            {
                FeedbackAlreadyExists = feedbackAlreadyExists,
                UserExists = userExists,
                ProductExists = productExists
            };
            var feedbackValidatorResult = _validators.FeedbackValidator.ValidateAdd(validateAddModel);
            if (feedbackValidatorResult.IsFaulted)
            {
                return Result<Feedback>.Failure(feedbackValidatorResult.ErrorMessages);
            }

            var feedbackServiceResult = await HasFeedbackPending(dto.UserId, dto.ProductId);
            if (feedbackServiceResult.IsFaulted)
            {
                return Result<Feedback>.Failure(feedbackServiceResult.ErrorMessages);
            }
            bool hasFeedbackPending = feedbackServiceResult.Value;
            if (!hasFeedbackPending)
            {
                return Result<Feedback>.Failure(ErrorMessage.UserHasNotFeedbackPending);
            }

            var feedback = new Feedback
            {
                ProductId = dto.ProductId,
                UserId = dto.UserId,
                Rating = dto.Rating,
                Content = dto.Content,
                CreatedAt = dto.CreatedAt
            };
            var feedbackRepositoryResult = await _feedbackRepository.Add(feedback);
            if (feedbackRepositoryResult <= 0)
            {
                return Result<Feedback>.Failure(ErrorMessage.FeedbackNotCreated);
            }
            return Result<Feedback>.Success(feedback);
        }
        catch
        {
            return Result<Feedback>.Failure(ErrorMessage.InternalServerError);
        }
    }
    public async Task<Result<Feedback>> Remove(string id)
    {
        try
        {
            var idValidationResult = _validators.IdValidator.Validate(new IdModel(id));
            if (!idValidationResult.IsValid)
            {
                var errorMessages = idValidationResult.Errors.Select(e => e.ErrorMessage);
                return Result<Feedback>.Failure(errorMessages);
            }

            bool feedbackExists = await _feedbackRepository.Exists(f => f.Id == id);
            var validateRemoveModel = new ValidateRemoveModel()
            {
                FeedbackExists = feedbackExists
            };
            var feedbackValidatorResult = _validators.FeedbackValidator.ValidateRemove(validateRemoveModel);
            if (feedbackValidatorResult.IsFaulted)
            {
                return Result<Feedback>.Failure(feedbackValidatorResult.ErrorMessages);
            }

            var feedback = await _feedbackRepository.GetById(id);
            if (feedback is null)
            {
                return Result<Feedback>.Failure(ErrorMessage.FeedbackNotFound);
            }
            var feedbackRepositoryResult = await _feedbackRepository.Remove(feedback);
            if (feedbackRepositoryResult <= 0)
            {
                return Result<Feedback>.Failure(ErrorMessage.FeedbackNotRemoved);
            }
            return Result<Feedback>.Success(feedback);
        }
        catch
        {
            return Result<Feedback>.Failure(ErrorMessage.InternalServerError);
        }
    }
    public async Task<Result<bool>> HasFeedbackPending(string userId, string productId)
    {
        try
        {
            bool orderExists = await _orderRepository.Exists(userId, productId);
            bool feedbackExists = await _feedbackRepository.Exists(userId, productId);
            bool hasFeedbackPending = !feedbackExists && orderExists;
            return Result<bool>.Success(hasFeedbackPending);
        }
        catch
        {
            return Result<bool>.Failure(ErrorMessage.InternalServerError);
        }
    }
}
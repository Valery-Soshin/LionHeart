using LionHeart.BusinessLogic.Helpers;
using LionHeart.BusinessLogic.Resources;
using LionHeart.Core.Dtos.Feedback;
using LionHeart.Core.Interfaces.Repositories;
using LionHeart.Core.Interfaces.Services;
using LionHeart.Core.Models;
using LionHeart.Core.Result;

namespace LionHeart.BusinessLogic.Services;

public class FeedbackService : IFeedbackService
{
    private readonly IFeedbackRepository _feedbackRepository;
    private readonly IOrderService _orderService;

    public FeedbackService(IFeedbackRepository feedbackRepository,
                           IOrderService orderService)
    {
        _feedbackRepository = feedbackRepository;
        _orderService = orderService;
    }

    public async Task<Result<Feedback>> GetById(string id)
    {
        try
        {
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
            var feedbackServiceResult = await HasFeedbackPending(dto.UserId, dto.ProductId);
            if (feedbackServiceResult.IsFaulted) return Result<Feedback>.Failure(feedbackServiceResult.ErrorMessages);
            var hasFeedbackPending = feedbackServiceResult.Value;
            if (!hasFeedbackPending) return Result<Feedback>.Failure(ErrorMessage.UserHasNotFeedbackPending);

            var feedback = new Feedback
            {
                ProductId = dto.ProductId,
                UserId = dto.UserId,
                Rating = dto.Rating,
                Content = dto.Content,
                CreatedAt = dto.CreatedAt
            };
            var result = await _feedbackRepository.Add(feedback);
            if (result <= 0) return Result<Feedback>.Failure(ErrorMessage.FeedbackNotCreated);

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
            var feedback = await _feedbackRepository.GetById(id);
            if (feedback is null)
            {
                return Result<Feedback>.Failure(ErrorMessage.FeedbackNotFound);
            }
            var result = await _feedbackRepository.Remove(feedback);
            if (result <= 0)
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
            var orderServiceResult = await _orderService.Exists(userId, productId);
            if (orderServiceResult.IsFaulted)
            {
                var errorMessages = orderServiceResult.ErrorMessages.ToList();
                errorMessages.Add(ErrorMessage.OrderNotFound);
                return Result<bool>.Failure(errorMessages);
            }
            var feedbackRepositoryResult = await _feedbackRepository.Exists(userId, productId);

            var hasFeedbackPending = !feedbackRepositoryResult && orderServiceResult.Value;

            return Result<bool>.Success(hasFeedbackPending);
        }
        catch
        {
            return Result<bool>.Failure(ErrorMessage.InternalServerError);
        }
    }
}
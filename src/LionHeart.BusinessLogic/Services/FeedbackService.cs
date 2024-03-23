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
    private readonly IOrderRepository _orderRepository;

    public FeedbackService(IFeedbackRepository feedbackRepository,
                           IOrderRepository orderRepository)
    {
        _feedbackRepository = feedbackRepository;
        _orderRepository = orderRepository;
    }

    public async Task<Result<Feedback>> GetById(string id)
    {
        try
        {
            var feedback = await _feedbackRepository.GetById(id);
            if (feedback is null)
            {
                return new Result<Feedback>
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.FeedbackNotFound
                };
            }
            return new Result<Feedback>
            {
                IsCompleted = true,
                Data = feedback
            };
        }
        catch
        {
            return new Result<Feedback>
            {
                IsCompleted = false,
                ErrorMessage = ErrorMessage.InternalServerError
            };
        }
    }
    public async Task<Result<Feedback>> Add(AddFeedbackDto dto)
    {
        try
        {
            var feedback = new Feedback
            {
                ProductId = dto.ProductId,
                UserId = dto.UserId,
                Rating = dto.Rating,
                Content = dto.Content,
                CreatedAt = dto.CreatedAt
            };
            var result = await _feedbackRepository.Add(feedback);
            if (result <= 0)
            {
                return new Result<Feedback>
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.FeedbackNotCreated
                };
            }
            return new Result<Feedback>
            {
                IsCompleted = true,
                Data = feedback
            };
        }
        catch
        {
            return new Result<Feedback>
            {
                IsCompleted = false,
                ErrorMessage = ErrorMessage.InternalServerError
            };
        }
    }
    public async Task<Result<Feedback>> Remove(RemoveFeedbackDto dto)
    {
        try
        {
            var feedback = await _feedbackRepository.GetById(dto.Id);
            if (feedback is null)
            {
                return new Result<Feedback>
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.FeedbackNotFound
                };
            }
            var result = await _feedbackRepository.Remove(feedback);
            if (result <= 0)
            {
                return new Result<Feedback>
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.FeedbackNotRemoved
                };
            }
            return new Result<Feedback>
            {
                IsCompleted = true,
                Data = feedback
            };
        }
        catch
        {
            return new Result<Feedback>
            {
                IsCompleted = false,
                ErrorMessage = ErrorMessage.InternalServerError
            };
        }

    }
    public async Task<Result<bool>> HasFeedbackPending(string userId, string productId)
    {
        try
        {
            var hasFeedbackPending = !await _feedbackRepository.Exists(userId, productId) &&
                    await _orderRepository.Exists(userId, productId);

            return new Result<bool>
            {
                IsCompleted = true,
                Data = hasFeedbackPending
            };
        }
        catch
        {
            return new Result<bool>
            {
                IsCompleted = false,
                ErrorMessage = ErrorMessage.InternalServerError
            };
        }
    }
}
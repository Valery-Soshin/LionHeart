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
    public async Task<Result<PagedResponse<Feedback>>> GetFeedbacksByUserIdWithPagination(string userId, int pageNumber)
    {
        try
        {
            const int pageSize = 10;
            var pagedResponse = await _feedbackRepository.GetFeedbacksByUserIdWithPagination(userId, pageNumber, pageSize);
            if (pagedResponse is null)
            {
                return new Result<PagedResponse<Feedback>>
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.FeedbacksNotFound
                };
            }
            return new Result<PagedResponse<Feedback>>
            {
                IsCompleted = true,
                Data = pagedResponse
            };
        }
        catch
        {
            return new Result<PagedResponse<Feedback>>
            {
                IsCompleted = false,
                ErrorMessage = ErrorMessage.InternalServerError
            };
        }
    }
    public async Task<Result<PagedResponse<Feedback>>> GetFeedbacksByProductIdWithPagination(string productId, int pageNumber)
    {
        try
        {
            const int pageSize = 10;
            var pagedResponse = await _feedbackRepository.GetFeedbacksByProductIdWithPagination(productId, pageNumber, pageSize);
            if (pagedResponse is null)
            {
                return new Result<PagedResponse<Feedback>>
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.FeedbacksNotFound
                };
            }
            return new Result<PagedResponse<Feedback>>
            {
                IsCompleted = true,
                Data = pagedResponse
            };
        }
        catch
        {
            return new Result<PagedResponse<Feedback>>
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
    public async Task<Result<Feedback>> Remove(string id)
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
            var orderServiceResult = await _orderService.Exists(userId, productId);
            if (orderServiceResult.IsFaulted)
            {
                return new Result<bool>()
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.OrderNotFound
                };
            }

            var hasFeedbackPending = !await _feedbackRepository.Exists(userId, productId) &&
                orderServiceResult.Data;

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
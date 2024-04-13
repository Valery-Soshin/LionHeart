using LionHeart.Core.Dtos.Feedback;
using LionHeart.Core.Models;
using LionHeart.Core.Result;

namespace LionHeart.Core.Interfaces.Services;

public interface IFeedbackService
{
    Task<Result<Feedback>> GetById(string id);
    Task<Result<PagedResponse<Feedback>>> GetFeedbacksByUserIdWithPagination(string userId, int pageNumber);
    Task<Result<PagedResponse<Feedback>>> GetFeedbacksByProductIdWithPagination(string productId, int pageNumber);
    Task<Result<Feedback>> Add(AddFeedbackDto dto);
    Task<Result<Feedback>> Remove(string id);
    Task<Result<bool>> HasFeedbackPending(string userId, string productId);
}
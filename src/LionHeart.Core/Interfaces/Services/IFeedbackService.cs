using LionHeart.Core.Dtos.Feedback;
using LionHeart.Core.Models;
using LionHeart.Core.Result;

namespace LionHeart.Core.Interfaces.Services;

public interface IFeedbackService
{
    Task<Result<Feedback>> GetById(string id);
    Task<Result<Feedback>> Add(AddFeedbackDto dto);
    Task<Result<Feedback>> Remove(RemoveFeedbackDto dto);
    Task<Result<bool>> HasFeedbackPending(string userId, string productId);
}
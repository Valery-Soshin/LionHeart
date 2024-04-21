using LionHeart.Core.Interfaces.Services;
using LionHeart.Web.Models.Feedbacks;
using Microsoft.AspNetCore.Mvc;

namespace LionHeart.Web.Components;

public class ListFeedbacksViewComponent : ViewComponent
{
    private readonly IFeedbackService _feedbackService;

    public ListFeedbacksViewComponent(IFeedbackService feedbackService)
    {
        _feedbackService = feedbackService;
    }

    public async Task<IViewComponentResult> InvokeAsync(string productId, int pageNumber)
    {
        var feedbackServiceResult = await _feedbackService.GetFeedbacksByProductId(productId, pageNumber);
        if (feedbackServiceResult.IsFaulted) return View("/Views/Products/Components/ListProducts/Error.csthml", feedbackServiceResult.ErrorMessages);
        var page = feedbackServiceResult.Value;

        var model = new ListFeedbacksViewModel
        {
            ProductId = productId,
            PageNumber = page.PageNumber,
            HasNextPage = page.HasNextPage,
            HasPreviousPage = page.HasPreviousPage,
            Feedbacks = page.Entities.Select(e => new ListFeedbacksItemViewModel()
            {
                FirstName = e.User.Email,
                Rating = (int)e.Rating,
                Content = e.Content,
                CreatedAt = e.CreatedAt
            }).ToList()
        };
        return View("/Views/Feedbacks/Components/ListFeedbacks/default.cshtml", model);
    }
}
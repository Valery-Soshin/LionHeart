using LionHeart.Core.Dtos.Feedback;
using LionHeart.Core.Interfaces.Services;
using LionHeart.Core.Models;
using LionHeart.Web.Models.Feedback;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LionHeart.Web.Controllers;

public class FeedbacksController : MainController
{
    private readonly IFeedbackService _feedbackService;
    private readonly IProductService _productService;
    private readonly UserManager<User> _userManager;

    public FeedbacksController(IFeedbackService feedbackService,
                               IProductService productService,
                               UserManager<User> userManager)
    {
        _feedbackService = feedbackService;
        _productService = productService;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> CreateFeedback(string productId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var productServiceResult = await _productService.GetById(productId);
        if (productServiceResult.IsFaulted) return BadRequest(productServiceResult.ErrorMessages);
        var product = productServiceResult.Value;

        ViewData["ProductId"] = product.Id;
        ViewData["ProductName"] = product.Name;

        return View();
    }
    [HttpPost]
    public async Task<IActionResult> CreateFeedback(CreateFeedbackViewModel model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var userId = _userManager.GetUserId(User);
        if (userId is null) return Unauthorized();

        var dto = new AddFeedbackDto
        {
            ProductId = model.ProductId,
            UserId = userId,
            Rating = model.Rating,
            Content = model.Content,
            CreatedAt = DateTimeOffset.UtcNow
        };
        var feedbackServiceResult = await _feedbackService.Add(dto);
        if (feedbackServiceResult.IsFaulted) return BadRequest(feedbackServiceResult.ErrorMessages);

        return Redirect("/Products");
    }
}
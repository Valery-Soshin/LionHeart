﻿using LionHeart.Core.Dtos.Feedback;
using LionHeart.Core.Interfaces.Services;
using LionHeart.Core.Models;
using LionHeart.Web.Models.Feedbacks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LionHeart.Web.Controllers;

public class FeedbacksController : Controller   
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
        if (!ModelState.IsValid) return BadRequest();

        var result = await _productService.GetById(productId);
        if (result.IsFaulted) return BadRequest(result.ErrorMessage);

        var product = result.Data;
        if (product is null) return BadRequest();

        ViewData["ProductId"] = product.Id;
        ViewData["ProductName"] = product.Name;

        return View();
    }
    [HttpPost]
    public async Task<IActionResult> CreateFeedback(CreateFeedbackViewModel model)
    {
        if (!ModelState.IsValid) return BadRequest();

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
        var result = await _feedbackService.Add(dto);
        if (result.IsFaulted) return BadRequest(result.ErrorMessage); 

        return Redirect("/Products");
    }
}
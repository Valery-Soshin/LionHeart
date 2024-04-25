using LionHeart.Core.Interfaces.Services;
using LionHeart.Web.Models.Company;
using Microsoft.AspNetCore.Mvc;

namespace LionHeart.Web.Controllers;

public class CompaniesController : MainController
{
    private readonly ICompanyService _companyService;
    private readonly IProductService _productService;

    public CompaniesController(ICompanyService companyService,
                               IProductService productService)
    {
        _companyService = companyService;
        _productService = productService;
    }

    public async Task<IActionResult> ShowCompany(string id, int pageNumber = 1)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var companyServiceResult = await _companyService.GetById(id);
        if (companyServiceResult.IsFaulted) return BadRequest(companyServiceResult.ErrorMessages);
        var company = companyServiceResult.Value;

        var productServiceResult = await _productService.GetProductsByCompanyId(company.Id, pageNumber);
        if (productServiceResult.IsFaulted) return BadRequest(productServiceResult.ErrorMessages);
        var page = productServiceResult.Value;

        var model = new ShowCompanyViewModel
        {
            Id = company.Id,
            Name = company.Name,
            UserId = company.User.Id,
            UserName = company.User.Email,
            CreatedAt = company.CreatedAt,
            Page = page
        };
        return View(model);
    }
}
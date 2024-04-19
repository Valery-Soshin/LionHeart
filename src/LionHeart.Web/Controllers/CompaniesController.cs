using LionHeart.Core.Interfaces.Services;
using LionHeart.Web.Models.Company;
using Microsoft.AspNetCore.Mvc;

namespace LionHeart.Web.Controllers;

public class CompaniesController : Controller
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
        if (companyServiceResult.IsFaulted) return BadRequest(companyServiceResult.ErrorMessage);
        var company = companyServiceResult.Data;
        if (company is null) return BadRequest();

        var productServiceResult = await _productService.GetProductsByCompanyId(company.Id, pageNumber);
        if (productServiceResult.IsFaulted) return BadRequest(productServiceResult.ErrorMessage);
        var page = productServiceResult.Data;
        if (page is null) return BadRequest();

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
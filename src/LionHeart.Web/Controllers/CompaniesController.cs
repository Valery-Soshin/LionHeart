using LionHeart.Core.Interfaces.Services;
using LionHeart.Web.Models.Company;
using Microsoft.AspNetCore.Mvc;

namespace LionHeart.Web.Controllers;

public class CompaniesController : Controller
{
    private readonly ICompanyService _companyService;

    public CompaniesController(ICompanyService companyService)
    {
        _companyService = companyService;
    }

    public async Task<IActionResult> ShowCompany(string id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var companyServiceResult = await _companyService.GetById(id);
        if (companyServiceResult.IsFaulted) return BadRequest(companyServiceResult.ErrorMessage);
        var company = companyServiceResult.Data;
        if (company is null) return BadRequest();

        var model = new ShowCompanyViewModel
        {
            Id = company.Id,
            Name = company.Name,
            UserId = company.User.Id,
            UserName = company.User.Email,
            CreatedAt = company.CreatedAt
        };
        return View(model);
    }
}
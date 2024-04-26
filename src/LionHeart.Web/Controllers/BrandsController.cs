using LionHeart.Core.Interfaces.Services;
using LionHeart.Web.Models.Brand;
using Microsoft.AspNetCore.Mvc;

namespace LionHeart.Web.Controllers;

public class BrandsController : MainController
{
    private readonly IBrandService _brandService;
    private readonly IProductService _productService;

    public BrandsController(IBrandService brandService,
                            IProductService productService)
    {
        _brandService = brandService;
        _productService = productService;
    }

    public async Task<IActionResult> ShowBrand(string id, int pageNumber = 1)
    {
        if (!ModelState.IsValid) return Warning(ModelState);

        var brandServiceResult = await _brandService.GetById(id);
        if (brandServiceResult.IsFaulted) return Warning(brandServiceResult.ErrorMessages);
        var brand = brandServiceResult.Value;

        var productServiceResult = await _productService.GetProductsByBrandId(brand.Id, pageNumber);
        if (productServiceResult.IsFaulted) return Warning(productServiceResult.ErrorMessages);
        var page = productServiceResult.Value;

        var model = new ShowBrandViewModel()
        {
            Name = brand.Name,
            Page = page
        };
        return View(model);
    }
}
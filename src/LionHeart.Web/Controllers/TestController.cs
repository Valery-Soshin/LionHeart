using LionHeart.Core.Models;
using LionHeart.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LionHeart.Web.Controllers
{
    public class TestController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _applicationDbContext;

        public TestController(UserManager<User> userManager,
                              RoleManager<IdentityRole> roleManager,
                              ApplicationDbContext applicationDbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IActionResult> AddUsersAndProducts()
        {
            _applicationDbContext.Database.EnsureDeleted();
            _applicationDbContext.Database.EnsureCreated();

            await _roleManager.CreateAsync(new IdentityRole("Supplier"));
            await _roleManager.CreateAsync(new IdentityRole("Customer"));

            var supplier = new User
            {
                UserName = "supplier@yandex.ru",
                Email = "supplier@yandex.ru",
            };
            await _userManager.CreateAsync(supplier, "Supplier2024-");
            await _userManager.AddToRoleAsync(supplier, "Customer");
            await _userManager.AddToRoleAsync(supplier, "Supplier");
            await CreateProducts(supplier);

            var user = new User
            {
                UserName = "valerius-soshin@yandex.ru",
                Email = "valerius-soshin@yandex.ru",
            };
            await _userManager.CreateAsync(user, "Customer2024-");
            await _userManager.AddToRoleAsync(user, "Customer");

            return Redirect("/Home/Index");
        }

        public async Task<IActionResult> AddCategories()
        {

            var products = new Category()
            {
                Name = "Продукты",
                SubCategories =
                [
                    new Category() { Name = "Мясная продукция"},
                    new Category() { Name = "Десткое питание"},
                    new Category() { Name = "Чай и кофе"},
                    new Category() { Name = "Здоровое питание"},
                    new Category() { Name = "Молочные продукты"}
                ]
            };

            var electronics = new Category()
            {
                Name = "Электроника",
                SubCategories = 
                [
                    new Category() { Name = "Ноутбуки"},
                    new Category() { Name = "ПК"},
                    new Category() { Name = "Игровые консоли"},
                    new Category() { Name = "Наушники"},
                ]
            };
            _applicationDbContext.Categories.AddRange(products, electronics);
            await _applicationDbContext.SaveChangesAsync();

            return Ok();
        }

        public IActionResult TestEventInView()
        {
            return View();
        }

        private Task CreateProducts(User supplier)
        {
            var category = new Category()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Одежда"
            };

            _applicationDbContext.Categories.Add(category);

            for (int i = 0; i < 30; i++)
            {
                var product = new Product()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = supplier.Id,
                    CategoryId = category.Id,
                    Name = "Футболка",
                    Price = 1250,
                    Description = "Красивая и удобная футболка.",
                    Specifications = "Размер - XXL",
                    CreatedAt = DateTimeOffset.UtcNow,
                    Image = new Image
                    {
                        FileName = "img1.jpg"
                    }
                };
                var product2 = new Product()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = supplier.Id,
                    CategoryId = category.Id,
                    Name = "Мяч",
                    Price = 1250,
                    Description = "Футбольный мяч, может быть использован даже во время дождя.",
                    Specifications = "",
                    CreatedAt = DateTimeOffset.UtcNow,
                    Image = new Image
                    {
                        FileName = "img2.jpg"
                    }
                };

                _applicationDbContext.AddRange(product, product2);

                for (int k = 0; k < 50; k++)
                {
                    var productUnit = new ProductUnit
                    {
                        Id = Guid.NewGuid().ToString(),
                        ProductId = product.Id,
                        SaleStatus = Core.Enums.SaleStatus.Available,
                        CreatedAt = DateTimeOffset.UtcNow
                    };

                    var productUnit2 = new ProductUnit
                    {
                        Id = Guid.NewGuid().ToString(),
                        ProductId = product2.Id,
                        SaleStatus = Core.Enums.SaleStatus.Available,
                        CreatedAt = DateTimeOffset.UtcNow
                    };

                    _applicationDbContext.ProductUnits.AddRange(productUnit, productUnit2);
                }
            }

            return _applicationDbContext.SaveChangesAsync();
        }
    }
}
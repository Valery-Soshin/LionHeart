using LionHeart.Core.Models;

namespace LionHeart.Web.Models.Products
{
    public class SupplierProductViewModel
    {
        public Product Product { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
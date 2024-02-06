namespace LionHeart.Web.Models.Orders;

public class CreateOrderViewModel
{
    public string UserId { get; set; } = null!;
    public decimal FullPrice { get; set; }
    public List<ProductInBasketViewModel> Products{ get; set; } = [];
}

public class ProductInBasketViewModel
{
    public string ProductId { get; set; } = null!;
    public ProductViewModel Information { get; set; } = null!;
    public int Quantity { get; set; } = 1;
    public int TotalPrice { get; set; }
}
public class ProductViewModel
{
    public string Price { get; set; } = null!;
    public string Name { get; set; } = null!;
}
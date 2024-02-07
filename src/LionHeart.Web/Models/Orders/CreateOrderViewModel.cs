namespace LionHeart.Web.Models.Orders;

public class CreateOrderViewModel
{
    public List<CreateOrderDataViewModel> Entries { get; set; } = [];
}

public class CreateOrderDataViewModel
{
    public string ProductId { get; set; } = null!;
    public int ProductQuantity { get; set; }
}
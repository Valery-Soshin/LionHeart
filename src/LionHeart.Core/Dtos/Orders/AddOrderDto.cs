namespace LionHeart.Core.Dtos.Orders;

public class AddOrderDto
{
    public string UserId { get; set; } = null!;
    public decimal BasketTotalPrice { get; set; }
    public List<AddOrderProductDto> Products { get; set; } = [];
    public DateTimeOffset CreateAt { get; set; }
}

public class AddOrderProductDto
{
    public string ProductId { get; set; } = null!;
    public int ProductQuantity { get; set; } = 1;
    public decimal ProductTotalPrice { get; set; }
}
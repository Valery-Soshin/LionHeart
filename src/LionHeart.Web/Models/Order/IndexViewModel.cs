namespace LionHeart.Web.Models.Order;

public class IndexViewModel
{
    public int PageNumber { get; set; }
    public bool HasPreviousPage { get; set; }
    public bool HasNextPage { get; set; }
    public List<IndexOrderViewModel> Orders { get; set; } = [];
}

public class IndexOrderViewModel
{
    public string ProductName { get; set; } = null!;
    public decimal ProductPrice { get; set; }
    public int ProductQuantity{ get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
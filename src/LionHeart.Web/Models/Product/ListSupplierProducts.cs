namespace LionHeart.Web.Models.Product;

public class ListSupplierProductsViewModel
{
    public int PageNumber { get; set; }
    public bool HasPreviousPage { get; set; }
    public bool HasNextPage { get; set; }
    public List<SupplierProductViewModel> Products { get; set; } = [];
}

public class SupplierProductViewModel
{
    public string Id { get; set; } = null!;
    public string CategoryId { get; set; } = null!;
    public string CategoryName { get; set; } = null!;
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public string Description { get; set; } = null!;
    public string Specifications { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public int Quantity { get; set; }
}
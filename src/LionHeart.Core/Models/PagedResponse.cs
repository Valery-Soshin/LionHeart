namespace LionHeart.Core.Models;

public class PagedResponse
{
    public List<Product> Products { get; private set; } 
    public int PageNumber { get; private set; }
    public int TotalPages { get; private set; }

    public bool HasPreviousPage => (PageNumber > 1);
    public bool HasNextPage => (PageNumber < TotalPages);

    public PagedResponse(List<Product> products, int totalRecords, int pageNumber, int pageSize)
    {
        Products = products;
        PageNumber = pageNumber;
        TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
    }
}
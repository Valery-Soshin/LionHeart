namespace LionHeart.Web.Models;

public class PageViewModel
{
    public int PageNumber { get; private set; }
    public int TotalPages { get; private set; }

    public bool HasPreviousPage => (PageNumber > 1);
    public bool HasNextPage => (PageNumber < TotalPages);

    // pageNumber - номер страницы
    // pageSize - количество элементов на странице
    // count - всего элементов в бд
    public PageViewModel(int count, int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
    }
}
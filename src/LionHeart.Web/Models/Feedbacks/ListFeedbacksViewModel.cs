namespace LionHeart.Web.Models.Feedbacks;

public class ListFeedbacksViewModel
{
    public required string ProductId { get; init; }
    public required int PageNumber { get; init; }
    public required bool HasPreviousPage { get; init; }
    public required bool HasNextPage { get; init; }
    public List<ListFeedbacksItemViewModel> Feedbacks { get; set; } = [];
}

public class ListFeedbacksItemViewModel
{
    public string FirstName { get; set; } = null!;
    public int Rating { get; set; }
    public string Content { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
}
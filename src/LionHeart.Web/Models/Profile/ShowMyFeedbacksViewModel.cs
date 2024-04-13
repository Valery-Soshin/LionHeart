namespace LionHeart.Web.Models.Profile;

public class ShowMyFeedbacksViewModel
{
    public required int PageNumber { get; init; }
    public required bool HasPreviousPage { get; init; }
    public required bool HasNextPage { get; init; }
    public List<ShowMyFeedbacksItemViewModel> Feedbacks { get; set; } = [];
}

public class ShowMyFeedbacksItemViewModel
{
    public string Id { get; set; } = null!;
    public string ProductName { get; set; } = null!;
    public string ImageName { get; set; } = null!;
    public int Rating { get; set; }
    public string Content { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
}
namespace LionHeart.BusinessLogic.FluentValidations.Models;

public class IdModel
{
    public List<string> Ids { get; }

    public IdModel(params string[] ids)
    {
        Ids = ids.ToList();
    }
    public IdModel(List<string> ids)
    {
        Ids = ids;
    }
}
public record class DateTimeOffsetModel(DateTimeOffset DateTimeOffset);
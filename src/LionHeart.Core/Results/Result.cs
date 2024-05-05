namespace LionHeart.Core.Results;

public class Result
{
    public bool IsCompleted => ErrorMessages.Count == 0;
    public bool IsFaulted => !IsCompleted;
    public IReadOnlyList<string> ErrorMessages { get; } = [];

    public Result() { }
    public Result(List<string>? errorMessages)
    {
        ErrorMessages = errorMessages ?? [];
    }
    public static Result Success()
    {
        return new Result();
    }
    public static Result Failure(string errorMessage)
    {
        return new Result([errorMessage]);
    }
    public static Result Failure(IEnumerable<string> errorMessages)
    {
        return new Result(new List<string>(errorMessages));
    }
}
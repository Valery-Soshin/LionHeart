namespace LionHeart.Core.Results;

public class Result<T>
{
    public T Value { get; }
    public bool IsCompleted => ErrorMessages.Count == 0;
    public bool IsFaulted => !IsCompleted;
    public IReadOnlyList<string> ErrorMessages { get; } = [];

    private Result(T value, List<string>? errorMessages)
    {
        Value = value;
        ErrorMessages = errorMessages ?? [];
    }

    public static Result<T> Success(T value)
    {
        return new Result<T>(value, []);
    }
    public static Result<T> Failure(string errorMessage)
    {
        return new Result<T>(default!, [errorMessage]);
    }
    public static Result<T> Failure(IEnumerable<string> errorMessages)
    {
        return new Result<T>(default!, new List<string>(errorMessages));
    }
}
namespace LionHeart.Core.Result;

public class Result<T>
{
    public T? Data { get; init; }
    public required bool IsCompleted { get; init; }
    public bool IsFaulted => !IsCompleted;
    public string? ErrorMessage { get; init; }
}
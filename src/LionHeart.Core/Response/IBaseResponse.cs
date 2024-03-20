namespace LionHeart.Core.Response;

public interface IBaseResponse<T>
{
    T? Data { get; }
    bool IsCompleted { get; }
    bool IsFaulted { get; }
    string? Description { get; }
}
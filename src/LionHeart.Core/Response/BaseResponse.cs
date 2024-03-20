namespace LionHeart.Core.Response;

public class BaseResponse<T> : IBaseResponse<T>
{
    public T? Data { get; set; }
    public required bool IsCompleted { get; set; }
    public bool IsFaulted => !IsCompleted;
    public string? Description { get; set; }
}
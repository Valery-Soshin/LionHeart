namespace LionHeart.Core.Models;

public class Image(string fileName)
{
    public string Id { get; set; } = null!;
    public string FileName { get; set; } = fileName;
}
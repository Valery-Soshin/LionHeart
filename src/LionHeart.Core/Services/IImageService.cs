using Microsoft.AspNetCore.Http;

namespace LionHeart.Core.Services;

public interface IImageService
{
    Task Add(IFormFile file);
    void Remove(IFormFile file);
}
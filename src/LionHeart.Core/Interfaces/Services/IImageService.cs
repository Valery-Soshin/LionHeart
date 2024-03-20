using Microsoft.AspNetCore.Http;

namespace LionHeart.Core.Interfaces.Services;

public interface IImageService
{
    Task Add(IFormFile file);
    void Remove(IFormFile file);
}
using Microsoft.AspNetCore.Http;
using LionHeart.Core.Result;

namespace LionHeart.Core.Interfaces.Services;

public interface IImageService
{
    Task<Result<IFormFile>> Add(IFormFile file);
    Task<Result<IFormFile>> Remove(IFormFile file);
}
using Microsoft.AspNetCore.Http;
using LionHeart.Core.Result;

namespace LionHeart.Core.Interfaces.Services;

public interface IImageService
{
    Task<Result<string>> Add(IFormFile image);
}
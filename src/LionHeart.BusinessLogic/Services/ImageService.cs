using LionHeart.BusinessLogic.Resources;
using LionHeart.Core.Interfaces.Services;
using LionHeart.Core.Result;
using Microsoft.AspNetCore.Http;

namespace LionHeart.BusinessLogic.Services;

public class ImageService : IImageService
{
    private readonly string _folderPath;

    public ImageService()
    {
        _folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

        if (!Directory.Exists(_folderPath))
        {
            Directory.CreateDirectory(_folderPath);
        }
    }

    public async Task<Result<IFormFile>> Add(IFormFile file)
    {
        try
        {
            var pathWithFileName = Path.Combine(_folderPath, file.FileName);
            using var stream = new FileStream(pathWithFileName, FileMode.Create);
            await file.CopyToAsync(stream);

            return new Result<IFormFile>
            {
                IsCompleted = true,
                Data = file
            };
        }
        catch
        {
            return new Result<IFormFile>
            {
                IsCompleted = true,
                ErrorMessage = ErrorMessage.InternalServerError
            };
        }
    }
    public async Task<Result<IFormFile>> Remove(IFormFile file)
    {
        try
        {
            var pathWithFileName = Path.Combine(_folderPath, file.FileName);
            File.Delete(pathWithFileName);
            
            return new Result<IFormFile>
            {
                IsCompleted = true,
                Data = file
            };
        }
        catch
        {
            return new Result<IFormFile>
            {
                IsCompleted = true,
                ErrorMessage = ErrorMessage.InternalServerError
            };
        }
    }
}
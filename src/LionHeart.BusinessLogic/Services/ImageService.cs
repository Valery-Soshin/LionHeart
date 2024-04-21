using LionHeart.BusinessLogic.Resources;
using LionHeart.Core.Interfaces.Services;
using LionHeart.Core.Result;
using Microsoft.AspNetCore.Http;

namespace LionHeart.BusinessLogic.Services;

public class ImageService : IImageService
{
    private readonly string _folderPath = string.Empty;

    public ImageService()
    {
        try
        {
            _folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

            if (!Directory.Exists(_folderPath))
            {
                Directory.CreateDirectory(_folderPath);
            }
        }
        catch { }
    }

    public async Task<Result<string>> Add(IFormFile image)
    {
        try
        {
            var extension = image.ContentType.Split("/")[1];
            if (!ValidateExtension(extension))
            {
                return Result<string>.Failure(ErrorMessage.FileNotImage);
            }
            var separator = ".";
            var nameWithExtension = Path.GetRandomFileName() + separator + extension;
            var fullPath = Path.Combine(_folderPath, nameWithExtension);

            using var stream = new FileStream(fullPath, FileMode.Create);
            await image.CopyToAsync(stream);

            return Result<string>.Success(nameWithExtension);
        }
        catch
        {
            return Result<string>.Failure(ErrorMessage.InternalServerError);
        }
    }
    private bool ValidateExtension(string extension)
    {
        return extension is "png" or "jpeg";
    }
}
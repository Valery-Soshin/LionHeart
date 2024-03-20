using LionHeart.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace LionHeart.BusinessLogic.Services;

public class ImageService : IImageService
{
    private readonly string _folderPath;
    private readonly string _folderName = "images";
    private readonly ILogger<ImageService> _logger;

    public ImageService(ILogger<ImageService> logger)
    {
        _folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", _folderName);

        if (!Directory.Exists(_folderPath))
        {
            Directory.CreateDirectory(_folderPath);
        }

        _logger = logger;
    }

    public async Task Add(IFormFile file)
    {
        var pathWithFileName = Path.Combine(_folderPath, file.FileName);

        try
        {
            using var stream = new FileStream(pathWithFileName, FileMode.Create);
            await file.CopyToAsync(stream);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
    }
    public void Remove(IFormFile file)
    {
        var pathWithFileName = Path.Combine(_folderPath, file.FileName);

        try
        {
            File.Delete(pathWithFileName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
    }
}
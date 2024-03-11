using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace LionHeart.Core.Models;

public class ImageModel
{
    public string Id { get; set; } = null!;
    public string FileName { get; set; } = null!;
    [NotMapped]
    public IFormFile? File { get; set; }
}
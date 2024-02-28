using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using System.Text.Json;

namespace LionHeart.BusinessLogic.Services;

public class TranslateService
{
    private readonly IConfiguration _configuration;

    public TranslateService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<string> Translate(string message)
    {
        var client = new HttpClient();

        var token = _configuration["YandexIAMToken"];
        var folderId = "b1gotfjg1c6241j76ac7";
        var targetLanguage = "ru";
        var texts = new[] { message };

        client.DefaultRequestHeaders.Add("Accept", "application/json");
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

        var body = new
        {
            targetLanguageCode = targetLanguage,
            texts,
            folderId
        };

        var response = await client.PostAsJsonAsync("https://translate.api.cloud.yandex.net/translate/v2/translate",
            body);

        var json = await response.Content.ReadAsStringAsync();
        var translation = JsonSerializer.Deserialize<Translation>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        var result = translation?.Translations?.FirstOrDefault()?.Text;
        return result ?? json;
    }
}

public class Translation
{
    public List<Content> Translations { get; set; } = [];
}

public class Content
{
    public string Text { get; set; } = null!;
    public string DetectedLanguageCode { get; set; } = null!;
}
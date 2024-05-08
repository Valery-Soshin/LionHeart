using Microsoft.Extensions.Configuration;

namespace LionHeart.Infrastructure.GoogleReCaptcha;

public class GoogleReCaptchaValidator : IGoogleReCaptchaValidator
{
    private readonly IGoogleReCaptchaClient _captchaClient;
    private readonly IConfiguration _configuration;

    public GoogleReCaptchaValidator(IGoogleReCaptchaClient captchaClient,
                            IConfiguration configuration)
    {
        _captchaClient = captchaClient;
        _configuration = configuration;
    }

    public async Task<bool> IsValidCaptcha(string token)
    {
        var secretKey = _configuration["GoogleReCaptcha:SecretKey"];
        ArgumentNullException.ThrowIfNull(secretKey);

        var response = await _captchaClient.ValidateCaptcha(token, secretKey);
        return response.Success && response.Score >= 0.5;
    }
}
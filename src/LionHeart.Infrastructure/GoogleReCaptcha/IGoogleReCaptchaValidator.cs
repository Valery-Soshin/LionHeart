namespace LionHeart.Infrastructure.GoogleReCaptcha;

public interface IGoogleReCaptchaValidator
{
    Task<bool> IsValidCaptcha(string token);
}
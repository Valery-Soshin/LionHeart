using Refit;

namespace LionHeart.Infrastructure.GoogleReCaptcha;

public interface IGoogleReCaptchaClient
{
    [Post("")]
    Task<GoogleReCaptchaClientResponse> ValidateCaptcha([Query][AliasAs("response")] string token,
                                                        [Query][AliasAs("secret")] string secretKey);
}
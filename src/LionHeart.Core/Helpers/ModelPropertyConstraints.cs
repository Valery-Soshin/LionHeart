using System.Reflection.Metadata;

namespace LionHeart.Core.Helpers;

public static class ModelPropertyConstraints
{
    public const int IdLength = 36;

    #region Product

    public const int ProductNameMinLength = 4;
    public const int ProductNameMaxLength = 50;
    public const int ProductDescriptionMinLength = 100;
    public const int ProductDescriptionMaxLength = 500;
    public const int ProductSpecificationsMinLength = 100;
    public const int ProductSpecificationsMaxLength = 500;
    public const int ProductMaxPrice = 1000000;

    #endregion

    public const int BrandNameMinLength = 4;
    public const int BrandNameMaxLength = 50;

    public const int CategoryNameMinLength = 4;
    public const int CategoryNameMaxLength = 50;

    public const int CompanyNameMinLength = 4;
    public const int CompanyNameMaxLength = 50;

    public const int FeedbackContentMinLength = 50;
    public const int FeedbackContentMaxLength = 500;
}
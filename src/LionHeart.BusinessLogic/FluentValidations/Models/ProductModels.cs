namespace LionHeart.BusinessLogic.FluentValidations.Models;

public record class ProductNameModel(string Name);
public record class ProductPriceModel(decimal Price);
public record class ProductDescriptionModel(string Description);
public record class ProductSpecificationsModel(string Specifications);
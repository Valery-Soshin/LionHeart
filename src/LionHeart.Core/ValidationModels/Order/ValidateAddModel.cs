using LionHeart.Core.Dtos.Order;

namespace LionHeart.Core.ValidationModels.Order;

public class ValidateAddModel
{
    public required List<Models.Product> FoundDtoProductsInDb { get; init; }
    public required List<AddOrderProductDto> DtoProducts { get; init; }
}
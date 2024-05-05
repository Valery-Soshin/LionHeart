using LionHeart.BusinessLogic.Resources;
using LionHeart.Core.Interfaces.Validations;
using LionHeart.Core.Models;
using LionHeart.Core.Results;
using LionHeart.Core.ValidationModels.Order;

namespace LionHeart.BusinessLogic.CoreValidations;

public class OrderValidator : ValidatorBase, IOrderValidator
{
    public Result ValidateAdd(ValidateAddModel model)
    {
        var errorMessages = new List<string>();

        if (model.FoundDtoProductsInDb.Count != model.DtoProducts.Count)
            errorMessages.Add(ErrorMessage.ProductNotFound);

        if (model.FoundDtoProductsInDb.Exists(
            p => p.Units.Count < model.DtoProducts.Single(d => d.ProductId == p.Id).ProductQuantity))
            errorMessages.Add(ErrorMessage.ProductsNotEnough);

        return BuildResult(errorMessages);
    }
}
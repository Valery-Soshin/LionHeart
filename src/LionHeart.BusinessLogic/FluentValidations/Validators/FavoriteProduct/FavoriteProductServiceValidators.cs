using FluentValidation;
using LionHeart.BusinessLogic.FluentValidations.Models;
using LionHeart.Core.Interfaces.Validations;

namespace LionHeart.BusinessLogic.FluentValidations.Validators.FavoriteProduct;

public class FavoriteProductServiceValidators
{
    public IFavoriteProductValidator FavoriteProductValidator { get; }
    public IValidator<IdModel> IdValidator { get; }

    public FavoriteProductServiceValidators(IFavoriteProductValidator favoriteProductValidator,
                                            IValidator<IdModel> idValidator)
    {
        FavoriteProductValidator = favoriteProductValidator;
        IdValidator = idValidator;
    }
}
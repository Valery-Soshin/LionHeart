using FluentValidation;
using LionHeart.BusinessLogic.FluentValidations.Models;
using LionHeart.BusinessLogic.Resources;
using LionHeart.Core.Helpers;

namespace LionHeart.BusinessLogic.FluentValidations.Validators.Company.Property;

public class CompanyNameValidator : AbstractValidator<CompanyNameModel>
{
    public CompanyNameValidator()
    {
        RuleFor(m => m.Name)
            .NotEmpty()
            .MaximumLength(ModelPropertyConstraints.CompanyNameMinLength)
            .MaximumLength(ModelPropertyConstraints.CompanyNameMaxLength)
            .WithName(PropertyName.CompanyName);
    }
}
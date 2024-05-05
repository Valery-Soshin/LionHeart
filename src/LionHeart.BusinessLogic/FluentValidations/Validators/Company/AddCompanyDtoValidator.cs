using FluentValidation;
using LionHeart.BusinessLogic.FluentValidations.Models;
using LionHeart.BusinessLogic.FluentValidations.Validators.Company.Property;
using LionHeart.BusinessLogic.FluentValidations.Validators.Shared;
using LionHeart.Core.Dtos.Company;

namespace LionHeart.BusinessLogic.FluentValidations.Validators.Company;

public class AddCompanyDtoValidator : AbstractValidator<AddCompanyDto>
{
    public AddCompanyDtoValidator()
    {
        RuleFor(d => new CompanyNameModel(d.Name))
            .SetValidator(new CompanyNameValidator());

        RuleFor(d => new IdModel(d.UserId))
            .SetValidator(new IdValidator());

        RuleFor(d => new DateTimeOffsetModel(d.CreatedAt))
            .SetValidator(new DateTimeOffsetValidator());
    }
}
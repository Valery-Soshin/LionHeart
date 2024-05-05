using FluentValidation;
using LionHeart.BusinessLogic.FluentValidations.Models;
using LionHeart.Core.Dtos.Company;
using LionHeart.Core.Interfaces.Validations;

namespace LionHeart.BusinessLogic.FluentValidations.Validators.Company;

public class CompanyServiceValidators
{
    public ICompanyValidator CompanyValidator { get; }
    public IValidator<AddCompanyDto> AddCompanyDtoValidator { get; }
    public IValidator<IdModel> IdValidator { get; }

    public CompanyServiceValidators(ICompanyValidator companyValidator,
                                    IValidator<AddCompanyDto> addCompanyDtoValidator,
                                    IValidator<IdModel> idValidator)
    {
        CompanyValidator = companyValidator;
        AddCompanyDtoValidator = addCompanyDtoValidator;
        IdValidator = idValidator;
    }
}
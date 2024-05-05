using LionHeart.BusinessLogic.Resources;
using LionHeart.Core.Interfaces.Validations;
using LionHeart.Core.Results;
using LionHeart.Core.ValidationModels.Company;

namespace LionHeart.BusinessLogic.CoreValidations;

public class CompanyValidator : ValidatorBase, ICompanyValidator
{
    public Result ValidateAdd(ValidateAddModel model)
    {
        var errorMessages = new List<string>();
        if (model.CompanyAlreadyExists) errorMessages.Add(ErrorMessage.CompanyAlreadyExists);
        if (!model.UserExists) errorMessages.Add(ErrorMessage.UserNotFound);
        return BuildResult(errorMessages);
    }
}
using LionHeart.Core.Results;
using LionHeart.Core.ValidationModels.Company;

namespace LionHeart.Core.Interfaces.Validations;

public interface ICompanyValidator
{
    Result ValidateAdd(ValidateAddModel model);
}
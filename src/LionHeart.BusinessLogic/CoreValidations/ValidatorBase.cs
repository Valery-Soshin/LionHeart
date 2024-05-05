using LionHeart.Core.Results;

namespace LionHeart.BusinessLogic.CoreValidations;

public abstract class ValidatorBase
{
    protected virtual Result BuildResult(List<string> errorMessages)
    {
        ArgumentNullException.ThrowIfNull(errorMessages);

        return errorMessages.Count != 0 ? Result.Failure(errorMessages) : Result.Success();
    }
}
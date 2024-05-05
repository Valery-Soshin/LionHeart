namespace LionHeart.Core.ValidationModels.Company;

public class ValidateAddModel
{
    public required bool CompanyAlreadyExists { get; init; }
    public required bool UserExists { get; init; }
}
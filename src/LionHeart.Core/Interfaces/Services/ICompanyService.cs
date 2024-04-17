using LionHeart.Core.Dtos.Company;
using LionHeart.Core.Models;
using LionHeart.Core.Result;

namespace LionHeart.Core.Interfaces.Services;

public interface ICompanyService
{
    Task<Result<Company>> Add(AddCompanyDto dto);
}
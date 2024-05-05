using LionHeart.Core.Dtos.Company;
using LionHeart.Core.Models;
using LionHeart.Core.Results;

namespace LionHeart.Core.Interfaces.Services;

public interface ICompanyService
{
    Task<Result<Company>> GetById(string id);
    Task<Result<Company>> GetByUserId(string userId);
    Task<Result<Company>> Add(AddCompanyDto dto);
}
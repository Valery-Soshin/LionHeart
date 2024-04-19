using LionHeart.BusinessLogic.Resources;
using LionHeart.Core.Dtos.Company;
using LionHeart.Core.Interfaces.Repositories;
using LionHeart.Core.Interfaces.Services;
using LionHeart.Core.Models;
using LionHeart.Core.Result;

namespace LionHeart.BusinessLogic.Services;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _companyRepository;

    public CompanyService(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task<Result<Company>> GetById(string id)
    {
        try
        {
            var company = await _companyRepository.GetById(id);
            if (company is null)
            {
                return new Result<Company>
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.CompanyNotFound
                };
            }
            return new Result<Company>
            {
                IsCompleted = true,
                Data = company
            };
        }
        catch
        {
            return new Result<Company>
            {
                IsCompleted = false,
                ErrorMessage = ErrorMessage.InternalServerError
            };
        }
    }
    public async Task<Result<Company>> GetByUserId(string userId)
    {
        try
        {
            var company = await _companyRepository.GetByUserId(userId);
            if (company is null)
            {
                return new Result<Company>
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.CompanyNotFound
                };
            }
            return new Result<Company>
            {
                IsCompleted = true,
                Data = company
            };
        }
        catch
        {
            return new Result<Company>
            {
                IsCompleted = false,
                ErrorMessage = ErrorMessage.InternalServerError
            };
        }
    }
    public async Task<Result<Company>> Add(AddCompanyDto dto)
    {
        try
        {
            var company = new Company
            {
                Name = dto.Name,
                UserId = dto.UserId,
                CreatedAt = dto.CreatedAt
            };
            var result = await _companyRepository.Add(company);
            if (result <= 0)
            {
                return new Result<Company>
                {
                    IsCompleted = false,
                    ErrorMessage = ErrorMessage.CompanyNotFound
                };
            }
            return new Result<Company>
            {
                IsCompleted = true,
                Data = company
            };
        }
        catch
        {
            return new Result<Company>
            {
                IsCompleted = false,
                ErrorMessage = ErrorMessage.InternalServerError
            };
        }
    }
}
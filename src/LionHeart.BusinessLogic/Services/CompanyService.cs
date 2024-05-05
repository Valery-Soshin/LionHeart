using LionHeart.BusinessLogic.FluentValidations.Models;
using LionHeart.BusinessLogic.FluentValidations.Validators.Company;
using LionHeart.BusinessLogic.Resources;
using LionHeart.Core.Dtos.Company;
using LionHeart.Core.Interfaces.Repositories;
using LionHeart.Core.Interfaces.Services;
using LionHeart.Core.Models;
using LionHeart.Core.Results;
using LionHeart.Core.ValidationModels.Company;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LionHeart.BusinessLogic.Services;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _companyRepository;
    private readonly CompanyServiceValidators _validators;
    private readonly UserManager<User> _userManager;

    public CompanyService(ICompanyRepository companyRepository,
                          CompanyServiceValidators validators,
                          UserManager<User> userManager)
    {
        _companyRepository = companyRepository;
        _validators = validators;
        _userManager = userManager;
    }

    public async Task<Result<Company>> GetById(string id)
    {
        try
        {
            var idValidationResult = _validators.IdValidator.Validate(new IdModel(id));
            if (!idValidationResult.IsValid)
            {
                var errorMessages = idValidationResult.Errors.Select(e => e.ErrorMessage);
                return Result<Company>.Failure(errorMessages);
            }

            var company = await _companyRepository.GetById(id);
            if (company is null)
            {
                return Result<Company>.Failure(ErrorMessage.CompanyNotFound);
            }
            return Result<Company>.Success(company);
        }
        catch
        {
            return Result<Company>.Failure(ErrorMessage.InternalServerError);
        }
    }
    public async Task<Result<Company>> GetByUserId(string userId)
    {
        try
        {
            var idValidationResult = _validators.IdValidator.Validate(new IdModel(userId));
            if (!idValidationResult.IsValid)
            {
                var errorMessages = idValidationResult.Errors.Select(e => e.ErrorMessage);
                return Result<Company>.Failure(errorMessages);
            }

            var company = await _companyRepository.GetByUserId(userId);
            if (company is null)
            {
                return Result<Company>.Failure(ErrorMessage.CompanyNotFound);
            }
            return Result<Company>.Success(company);
        }
        catch
        {
            return Result<Company>.Failure(ErrorMessage.InternalServerError);
        }
    }
    public async Task<Result<Company>> Add(AddCompanyDto dto)
    {
        try
        {
            var dtoValidationResult = _validators.AddCompanyDtoValidator.Validate(dto);
            if (!dtoValidationResult.IsValid)
            {
                var errorMessages = dtoValidationResult.Errors.Select(e => e.ErrorMessage);
                return Result<Company>.Failure(errorMessages);
            }

            bool companyAlreadyExists = await _companyRepository.Exists(c => c.Name == dto.Name);
            bool userExists = await _userManager.Users.AnyAsync(u => u.Id == dto.UserId);
            var validateAddModel = new ValidateAddModel()
            {
                CompanyAlreadyExists = companyAlreadyExists,
                UserExists = userExists
            };
            var companyValidatorResult = _validators.CompanyValidator.ValidateAdd(validateAddModel);
            if (companyValidatorResult.IsFaulted)
            {
                return Result<Company>.Failure(companyValidatorResult.ErrorMessages);
            }

            var company = new Company
            {
                Name = dto.Name,
                UserId = dto.UserId,
                CreatedAt = dto.CreatedAt
            };
            var companyRepositoryResult = await _companyRepository.Add(company);
            if (companyRepositoryResult <= 0)
            {
                return Result<Company>.Failure(ErrorMessage.CompanyNotCreated);
            }
            return Result<Company>.Success(company);
        }
        catch
        {
            return Result<Company>.Failure(ErrorMessage.InternalServerError);
        }
    }
}
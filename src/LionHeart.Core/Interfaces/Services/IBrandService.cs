﻿using LionHeart.Core.Dtos.Brand;
using LionHeart.Core.Models;
using LionHeart.Core.Result;

namespace LionHeart.Core.Interfaces.Services;

public interface IBrandService
{
    Task<Result<Brand>> GetById(string id);
    Task<Result<Brand>> Add(AddBrandDto dto);
}
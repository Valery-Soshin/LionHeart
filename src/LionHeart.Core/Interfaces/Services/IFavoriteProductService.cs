﻿using LionHeart.Core.Dtos.FavoriteProduct;
using LionHeart.Core.Models;
using LionHeart.Core.Result;

namespace LionHeart.Core.Interfaces.Services;

public interface IFavoriteProductService
{
    Task<Result<FavoriteProduct>> GetById(string id);
    Task<Result<FavoriteProduct>> GetByUserIdProductId(string userId, string productId);
    Task<Result<List<FavoriteProduct>>> GetAllByUserId(string userId);
    Task<Result<FavoriteProduct>> Add(AddFavoriteProductDto dto);
    Task<Result<FavoriteProduct>> Remove(RemoveFavoriteProductDto dto);
    Task<Result<bool>> Any(string userId);
    Task<Result<bool>> Exists(string userId, string productId);
}
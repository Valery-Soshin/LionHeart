﻿using LionHeart.Core.Models;

namespace LionHeart.Core.Interfaces.Repositories;

public interface IFavoriteProductRepository : IRepository<FavoriteProduct>
{
    Task<FavoriteProduct?> GetByUserIdProductId(string userId, string productId);
    Task<List<FavoriteProduct>> GetAllByUserId(string userId);
    Task<bool> Any(string userId);
    Task<bool> Exists(string userId, string productId);
}
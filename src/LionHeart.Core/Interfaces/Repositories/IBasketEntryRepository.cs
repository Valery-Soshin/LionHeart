﻿using LionHeart.Core.Models;

namespace LionHeart.Core.Interfaces.Repositories;

public interface IBasketEntryRepository : IRepository<BasketEntry>
{
    Task<BasketEntry?> GetByAlternateKey(string userId, string productId);
    Task<List<BasketEntry>> GetEntriesByUserId(string userId);
    Task<List<BasketEntry>> Find(List<string> ids);
    Task<bool> Exists(string userId, string productId);
}
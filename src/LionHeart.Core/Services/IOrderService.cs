﻿using LionHeart.Core.Models;

namespace LionHeart.Core.Services;

public interface IOrderService
{
	Task<Order?> GetById(string id);
	Task<List<Order>> GetAll();
	Task<int> Add(Order order);
	Task<int> Update(Order order);
	Task<int> Remove(Order order);
}
﻿using LionHeart.Core.Enums;

namespace LionHeart.Core.Models;

public class ProductUnit
{
    public string Id { get; set; } = null!;
    public string ProductId { get; set; } = null!;
    public SaleStatus SaleStatus { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
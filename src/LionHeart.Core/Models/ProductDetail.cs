﻿using LionHeart.Core.Enums;

namespace LionHeart.Core.Models;

public class ProductDetail
{
    public string Id { get; set; } = null!;
    public Product Product { get; set; } = null!;
    public SaleStatus SaleStatus { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? SoldOn { get; set; }
}
﻿namespace LionHeart.Core.Models;

public class OrderItem
{
    public string Id { get; set; } = null!;
    public string OrderId { get; set; } = null!;
    public string ProductId { get; set; } = null!;
    public decimal ProductPrice { get; set; }
    public int ProductQuantity { get; set; }
    public List<OrderItemDetail> Details { get; set; } = [];
}
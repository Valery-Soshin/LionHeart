﻿namespace LionHeart.Core.Dtos.Order;

public class AddOrderDto
{
    public string UserId { get; set; } = null!;
    public decimal BasketTotalPrice { get; set; }
    public DateTimeOffset CreateAt { get; set; }
    public List<AddOrderProductDto> Products { get; set; } = [];
}

public class AddOrderProductDto
{
    public string EntryId { get; set; } = null!;
    public string ProductId { get; set; } = null!;
    public int ProductQuantity { get; set; } = 1;
    public decimal ProductTotalPrice { get; set; }
}
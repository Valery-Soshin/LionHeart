﻿namespace LionHeart.Web.Models.Products;

public class EditProductViewModel
{
    public string Id { get; set; } = null!;
    public string CategoryId { get; set; } = null!;
    public string? CategoryName { get; set; } = null!;
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public string Description { get; set; } = null!;
    public string Specifications { get; set; } = null!;
    public int Quantity { get; set; }
}
﻿using LionHeart.Core.Enums;

namespace LionHeart.Core.Models;

public class Feedback
{
    public string Id { get; set; } = null!;
    public User Customer { get; set; } = null!;
    public string ProductId { get; set; } = null!;
    public Rating Rating { get; set; }
    public string Content { get; set; } = null!;
}
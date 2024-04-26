﻿namespace LionHeart.Core.Models;

public class PagedResponse<T>
{
    public List<T> Entities { get; private set; } 
    public int PageNumber { get; private set; }
    public int TotalPages { get; private set; }

    public bool HasPreviousPage => (PageNumber > 1);
    public bool HasNextPage => (PageNumber < TotalPages);

    public PagedResponse(List<T> entities, int pageNumber, int totalPages)
    {
        Entities = entities;
        PageNumber = pageNumber;
        TotalPages = totalPages;
    }
}
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace LionHeart.Web.Models.Error;

public class InformationViewModel
{
    public IEnumerable Messages { get; set; }
    public string? ReturnUrl { get; set; }

    public InformationViewModel(IEnumerable messages)
    {
        Messages = messages;
    }  
    public InformationViewModel(IEnumerable messages, string? returnUrl)
    {
        Messages = messages;
        ReturnUrl = returnUrl;
    }
}
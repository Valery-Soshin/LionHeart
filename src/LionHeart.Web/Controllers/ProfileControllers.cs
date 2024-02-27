using LionHeart.Core.Models;
using LionHeart.Web.Models.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace LionHeart.Web.Controllers;

[Authorize(Roles = "Customer")]
public class ProfileController : Controller
{
    private readonly UserManager<User> _userManager;

    public ProfileController(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);

        return View(user);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        ViewBag.User = user;

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditViewModel model)
    {
        var user = await _userManager.FindByIdAsync(model.Id);

        if (ModelState.IsValid)
        {
            if (user is not null)
            {
                user.UserName = model.Email;
                user.Email = model.Email;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.PhoneNumber = model.PhoneNumber;

                if (model.Password is not null && model.CurrentPassword is not null)
                {
                    var result = await _userManager
                        .ChangePasswordAsync(user, model.CurrentPassword, model.Password);

                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            string translatedText = await Translate(error.Description);

                            ModelState.AddModelError("", translatedText);
                        }
                    }

                    ViewBag.User = user;
                    ViewBag.ShowModal = "true";
                    return View("Edit");
                }

                await _userManager.UpdateAsync(user);

                return RedirectToAction("Index", new { UserId = model.Id });
            }
        }

        foreach (var value in ModelState.Values)
        {
            foreach (var error in value.Errors)
            {
                string translatedText = await Translate(error.ErrorMessage);

                ModelState.AddModelError("", translatedText);
            }
        }

        ViewBag.User = user;
        ViewBag.ShowModal = "true";
        return View("Edit");
    }

    private static async Task<string> Translate(string message)
    {
        var client = new HttpClient();

        var token = "t1.9euelZrOmZSXip7Mk5SXyY-ekZKVlO3rnpWal82Kx5GKzoyJypvPksidm8rl8_cgRChR-e8VMDl1_t3z92ByJVH57xUwOXX-zef1656Vmo_GzJGUlYuQj52Mkc7Gy4uX7_zF656Vmo_GzJGUlYuQj52Mkc7Gy4uX.oTFgRunZ4FxKcS7tyg70dE4TyEN8qaGjV5DAHMfpUKXy6JbvfHMjmci9s78Cl9OzEmBmOix9WqtYE9Z79yjTDA";
        var folderId = "b1gotfjg1c6241j76ac7";
        var targetLanguage = "ru";
        var texts = new[] { message };

        client.DefaultRequestHeaders.Add("Accept", "application/json");
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

        var body = new
        {
            targetLanguageCode = targetLanguage,
            texts,
            folderId
        };

        var response = await client.PostAsJsonAsync("https://translate.api.cloud.yandex.net/translate/v2/translate",
            body);

        var json = await response.Content.ReadAsStringAsync();
        var translation = JsonSerializer.Deserialize<Translation>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        var result = translation.Translations.FirstOrDefault().Text;
        return result;
    }

    [HttpGet]
    public async Task<IActionResult> Remove(string userId)
    {
        return null;
    }

    [HttpGet]
    public IActionResult EditError()
    {
        return View();
    }
}

public class Translation
{
    public Content[] Translations { get; set; }
}

public class Content
{
    public string Text { get; set; }
    public string DetectedLanguageCode { get; set; }
}
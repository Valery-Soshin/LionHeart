﻿using LionHeart.Core.Models;
using LionHeart.Infrastructure.GoogleReCaptcha;
using LionHeart.Web.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LionHeart.Web.Controllers;

[AllowAnonymous]
public class AuthController : MainController
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IGoogleReCaptchaValidator _captchaValidator;

    public AuthController(UserManager<User> userManager,
                          RoleManager<IdentityRole> roleManager,
                          SignInManager<User> signInManager,
                          IGoogleReCaptchaValidator captchaValidator)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _captchaValidator = captchaValidator;
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewBag.ReturnUrL = returnUrl;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
    {
        if (!await _captchaValidator.IsValidCaptcha(model.Captcha))
        {
            return Content("Captcha validation failed");
        }

        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(
                model.Email,
                model.Password,
                model.RememberMe,
                lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return returnUrl is not null
                    ? Redirect(returnUrl)
                    : Redirect("/Products");
            }
        }
        return View();
    }

    [HttpGet]
    public IActionResult RegisterUser()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> RegisterUser(RegisterUserViewModel model)
    {
        if (!await _captchaValidator.IsValidCaptcha(model.Captcha))
        {
            return Content("Captcha validation failed");
        }

        if (ModelState.IsValid)
        {
            var user = new User
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };  

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Customer");
                await _signInManager.SignInAsync(user, model.RemeberMe);
                return Redirect("/Products");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
        return View();
    }

    [HttpPost] 
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Redirect("/Products");
    }
}
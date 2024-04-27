using LionHeart.BusinessLogic.DependencyInjection;
using LionHeart.DataAccess.DependencyInjection;
using LionHeart.Web.Configurations;
using LionHeart.Web.Helpers;
using Microsoft.AspNetCore.Mvc.Authorization;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseNLog();

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new AuthorizeFilter());
});

builder.Services.AddBusinessLogic(builder.Configuration);
builder.Services.AddDataAccess(builder.Configuration);

builder.Services.AddCookieSettings();
builder.WebHost.AddKestrelSettings();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseLoggingRequestTimeMiddleware();

app.Run();
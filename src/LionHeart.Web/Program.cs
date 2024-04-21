using LionHeart.BusinessLogic.DependencyInjection;
using LionHeart.DataAccess.DependencyInjection;
using LionHeart.Web.Helpers;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseNLog();
builder.Services.AddControllersWithViews();
builder.Services.AddBusinessLogic(builder.Configuration);
builder.Services.AddDataAccess(builder.Configuration);

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Auth/Login";
    options.AccessDeniedPath = "/Home/Error";
});
builder.WebHost.UseKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 1500000; // 1464 kb or 1,43 mb
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
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
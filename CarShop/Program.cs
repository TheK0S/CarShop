using CarShop.DI;
using CarShop.Interfaces;
using CarShop.Models;
using CarShop.Services;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/account/login";
        options.LogoutPath = "/account/logout";
        options.AccessDeniedPath = "/accessdenied";
    });
builder.Services.AddAuthorization();

builder.Configuration.AddUserSecrets("3c88f461-1a0d-4ce6-a501-dac8ad8dae28");
builder.Configuration.AddJsonFile("appsettings.json");

builder.Services.AddMessanger(builder.Configuration);
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<ICarService, CarService>();
builder.Services.AddTransient<IShopCartService, ShopCartService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.Map("/novapost", mailApp =>
{
    mailApp.Run(async context =>
    {
        await context.Response.WriteAsync(JsonConvert.SerializeObject(new NovaPost()));
    });
});

app.Map("/ukrpost", mailApp =>
{
    mailApp.Run(async context =>
    {
        await context.Response.WriteAsync(JsonConvert.SerializeObject(new UkrPost()));
    });
});

app.Map("/Data", [Authorize] (HttpContext context) =>
    $"Name: {context.User.FindFirstValue(ClaimTypes.Name)}\n" +
    $"Role: {context.User.FindFirstValue(ClaimTypes.Role)}\n" +
    $"Id: {context.User.FindFirstValue(ClaimTypes.NameIdentifier)}\n");

app.Map("/accessdenied", async (HttpContext context) =>
{
    context.Response.StatusCode = 403;
    await context.Response.WriteAsync("Access Denied");
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

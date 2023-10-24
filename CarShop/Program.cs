using CarShop.DI;
using CarShop.Interfaces;
using CarShop.Models;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMemoryCache();
builder.Services.AddSession();

builder.Configuration.AddJsonFile("appsettings.json");

builder.Services.AddScoped<IMessanger, Messanger>();
builder.Services.AddTransient<MessageFactory>();
builder.Services.AddScoped<SmtpMailClient>();
builder.Services.AddScoped<SmtpSettings>(provider =>
{
    return new SmtpSettings() { Host = "smtp.gmail.com", Port = 587, Login = "turchakkonstantin@gmail.com", Password = "jwjn owcn prki keuj" };
});
builder.Services.AddTransient<CategoryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

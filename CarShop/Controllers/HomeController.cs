using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarShop.Models;
using System.Diagnostics;
using Newtonsoft.Json;
using CarShop.ViewModels;
using CarShop.Services;
using CarShop.Interfaces;
using System.Security.Claims;
using System.Net;

namespace CarShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICarService _carService;
        private readonly ICategoryService _categoryService;
        private readonly IUserService _userService;
        private readonly IShopCartService _shopCartService;
        public HomeController(ICarService carService, ICategoryService categoryService, IUserService userService, IShopCartService shopCartService)
        {
            _carService = carService;
            _categoryService = categoryService;
            _userService = userService;
            _shopCartService = shopCartService;
        }
        public async Task<IActionResult> Index()
        {
            var topSaleCars = await _carService.GetFavouriteCarsAsync();
            var discountCars = await _carService.GetFavouriteCarsAsync();

            HomeIndexViewModel viewModel = new HomeIndexViewModel()
            {
                TopSaleCars = topSaleCars.Data?.ToList(),
                DiscountCars = discountCars.Data?.ToList()
            };
            return View(viewModel);
        }

        public async Task<IActionResult> CarList()
        {
            var cars = await _carService.GetCarsAsync();
            var categories = await _categoryService.GetCategoriesAsync();
            ViewBag.Categories = categories.Data;

            return View(cars.Data);
        }

        public async Task<IActionResult> PersonalArea()
        {
            string errorMessage = "";
            int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int currentUserId);

            var responseUser = await _userService.GetUserAsync(currentUserId);
            if (responseUser.StatusCode != HttpStatusCode.OK)
                errorMessage += responseUser.Message ?? $"Error loading user data. StatusCode: {responseUser.StatusCode}\n";

            User user = responseUser.Data;
            if (user == null)
                return View();

            var responseOrders = await _shopCartService.GetUserOrders(user.Id);
            if (responseOrders.StatusCode != HttpStatusCode.OK)
                errorMessage += responseOrders.Message ?? $"Error loading orders data. StatusCode: {responseOrders.StatusCode}\n";

            List<Order> orders = responseOrders.Data;

            PersonalAreaViewModel model = new()
            {
                UserName = user.UserName ?? "No name",
                Email = user.Email ?? "No email",
                CreateDate = user.Created,
                Orders = orders,
                ErrorMessage = errorMessage
            };
            

            return View(model);
        }
    }
}
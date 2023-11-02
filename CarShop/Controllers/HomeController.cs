using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarShop.Models;
using System.Diagnostics;
using Newtonsoft.Json;
using CarShop.ViewModels;
using CarShop.Services;
using CarShop.Interfaces;

namespace CarShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICarService _carService;
        private readonly ICategoryService _categoryService;
        public HomeController(ICarService carService, ICategoryService categoryService)
        {
            _carService = carService;
            _categoryService = categoryService;
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
    }
}
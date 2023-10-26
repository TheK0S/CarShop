using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarShop.Models;
using System.Diagnostics;
using Newtonsoft.Json;
using CarShop.ViewModels;

namespace CarShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient httpClient = new HttpClient();
        public async Task<IActionResult> Index()
        {
            var topSaleCars = await httpClient.GetFromJsonAsync<List<Car>>($"{Api.apiUri}cars/favourite");
            var discountCars = await httpClient.GetFromJsonAsync<List<Car>>($"{Api.apiUri}cars/favourite");

            HomeIndexViewModel viewModel = new HomeIndexViewModel()
            {
                TopSaleCars = topSaleCars,
                DiscountCars = discountCars

            };
            return View(viewModel);
        }

        public async Task<IActionResult> CarList()
        {
            return View(await httpClient.GetFromJsonAsync<IEnumerable<Car>>($"{Api.apiUri}cars"));
        }
    }
}
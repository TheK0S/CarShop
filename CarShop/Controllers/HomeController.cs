using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarShop.Models;
using System.Diagnostics;
using Newtonsoft.Json;

namespace CarShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient httpClient = new HttpClient();
        public async Task<IActionResult> Index()
        {
            var topSaleCars = await httpClient.GetFromJsonAsync<IEnumerable<Car>>($"{Api.apiUri}cars/favourite");
            var discountCars = await httpClient.GetFromJsonAsync<IEnumerable<Car>>($"{Api.apiUri}cars/favourite");

            HomeIndexViewModel viewModel = new HomeIndexViewModel()
            {
                TopSaleCars = topSaleCars != null ? topSaleCars.ToList() : new List<Car>(),
                DiscountCars = discountCars != null ? discountCars.ToList() : new List<Car>()

            };
            return View(viewModel);
        }

        public async Task<IActionResult> CarList()
        {
            return View(await httpClient.GetFromJsonAsync<IEnumerable<Car>>($"{Api.apiUri}cars"));
        }
    }
}
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
            return View(await httpClient.GetFromJsonAsync<IEnumerable<Car>>($"{Api.apiUri}cars/favourite"));
        }

        public async Task<IActionResult> CarList()
        {
            return View(await httpClient.GetFromJsonAsync<IEnumerable<Car>>($"{Api.apiUri}cars"));
        }
    }
}
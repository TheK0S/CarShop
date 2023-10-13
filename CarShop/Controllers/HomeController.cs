using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarShop.Models;
using System.Diagnostics;
using Newtonsoft.Json;

namespace CarShop.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CarList()
        {
            HttpClient httpClient = new();

            return View(await httpClient.GetFromJsonAsync<IEnumerable<Car>>($"{Api.apiUri}cars"));
        }
        public IActionResult CarInfo()
        {
            return View();
        }
    }
}
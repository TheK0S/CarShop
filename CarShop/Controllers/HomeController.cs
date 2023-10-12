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
            var response = await Api.GetApiResponse("car");

            string jsonContent = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<IEnumerable<Car>>(jsonContent);

            return View(result ?? new List<Car>());
        }
        public IActionResult CarInfo()
        {
            return View();
        }
    }
}
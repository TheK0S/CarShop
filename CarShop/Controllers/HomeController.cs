using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarShop.Models;
using System.Diagnostics;

namespace CarShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext db;

        public HomeController(AppDbContext dbContext) { db = dbContext; }


        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CarList()
        {
            return db.Car != null ?
                          View(await db.Car.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Car'  is null.");
        }
        public IActionResult CarInfo()
        {
            return View();
        }
    }
}
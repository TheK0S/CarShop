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

        //List<Car> cars = new()
        //{
        //    new Car
        //    {
        //        Id = 1,
        //        Name = "Mercedes C class",
        //        Description = "Is really comfortable car",
        //        Title = "Car",
        //        Url = "https://www.topgear.com/sites/default/files/2021/11/Mercedes_C300D_0000.jpg",
        //        Price = 45000,
        //    },
        //    new Car
        //    {
        //        Id = 2,
        //        Name = "BMW X6",
        //        Description = "Really bold car",
        //        Title = "Car",
        //        Url = "https://news.vip-car.org/files/news/2023/novye-bmw-x5-i-bmw-x6-2024-obzor-osnovnyh-izmeneniy/bmw-x6-3.jpg",
        //        Price = 37000,
        //    },new Car
        //    {
        //        Id = 3,
        //        Name = "Tesla model S",
        //        Description = "This is super electro car",
        //        Title = "Car",
        //        Url = "https://ecoautoinfo.com/uploads/images/default/sss222.jpg",
        //        Price = 42000,
        //    },new Car
        //    {
        //        Id = 4,
        //        Name = "Mazda CX5",
        //        Description = "City car",
        //        Title = "Car",
        //        Url = "https://i.infocar.ua/i/2/5166/80677/1024x.jpg",
        //        Price = 32000,
        //    },new Car
        //    {
        //        Id = 5,
        //        Name = "Wolksvagen touareg 2021",
        //        Description = "Beautiful car and all",
        //        Title = "Car",
        //        Url = "https://cdn4.riastatic.com/photosnew/auto/photo/volkswagen_touareg__461761024f.jpg",
        //        Price = 38000,
        //    },
        //    new Car
        //    {
        //        Id = 1,
        //        Name = "Mercedes C class",
        //        Description = "Is really comfortable car",
        //        Title = "Car",
        //        Url = "https://www.topgear.com/sites/default/files/2021/11/Mercedes_C300D_0000.jpg",
        //        Price = 45000,
        //    },
        //    new Car
        //    {
        //        Id = 2,
        //        Name = "BMW X6",
        //        Description = "Really bold car",
        //        Title = "Car",
        //        Url = "https://news.vip-car.org/files/news/2023/novye-bmw-x5-i-bmw-x6-2024-obzor-osnovnyh-izmeneniy/bmw-x6-3.jpg",
        //        Price = 37000,
        //    },new Car
        //    {
        //        Id = 3,
        //        Name = "Tesla model S",
        //        Description = "This is super electro car",
        //        Title = "Car",
        //        Url = "https://ecoautoinfo.com/uploads/images/default/sss222.jpg",
        //        Price = 42000,
        //    },new Car
        //    {
        //        Id = 4,
        //        Name = "Mazda CX5",
        //        Description = "City car",
        //        Title = "Car",
        //        Url = "https://i.infocar.ua/i/2/5166/80677/1024x.jpg",
        //        Price = 32000,
        //    },new Car
        //    {
        //        Id = 5,
        //        Name = "Wolksvagen touareg 2021",
        //        Description = "Beautiful car and all",
        //        Title = "Car",
        //        Url = "https://cdn4.riastatic.com/photosnew/auto/photo/volkswagen_touareg__461761024f.jpg",
        //        Price = 38000,
        //    },
        //    new Car
        //    {
        //        Id = 1,
        //        Name = "Mercedes C class",
        //        Description = "Is really comfortable car",
        //        Title = "Car",
        //        Url = "https://www.topgear.com/sites/default/files/2021/11/Mercedes_C300D_0000.jpg",
        //        Price = 45000,
        //    },
        //};

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
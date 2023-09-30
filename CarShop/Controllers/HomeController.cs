using CarShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CarShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext db;

        public HomeController(AppDbContext db) { this.db = db; }

        List<Car> cars = new List<Car>
        {
            new Car
            {
                Id = 1,
                Name = "Mercedes C class",
                Description = "Is really comfortable car",
                Title = "Car",
                Url = "https://www.topgear.com/sites/default/files/2021/11/Mercedes_C300D_0000.jpg",
                Price = 45000,
            },
            new Car
            {
                Id = 2,
                Name = "BMW X6",
                Description = "Really bold car",
                Title = "Car",
                Url = "https://news.vip-car.org/files/news/2023/novye-bmw-x5-i-bmw-x6-2024-obzor-osnovnyh-izmeneniy/bmw-x6-3.jpg",
                Price = 37000,
            },new Car
            {
                Id = 3,
                Name = "Tesla model S",
                Description = "This is super electro car",
                Title = "Car",
                Url = "https://ecoautoinfo.com/uploads/images/default/sss222.jpg",
                Price = 42000,
            },new Car
            {
                Id = 4,
                Name = "Mazda CX5",
                Description = "City car",
                Title = "Car",
                Url = "https://i.infocar.ua/i/2/5166/80677/1024x.jpg",
                Price = 32000,
            },new Car
            {
                Id = 5,
                Name = "Wolksvagen touareg 2021",
                Description = "Beautiful car and all",
                Title = "Car",
                Url = "https://cdn4.riastatic.com/photosnew/auto/photo/volkswagen_touareg__461761024f.jpg",
                Price = 38000,
            },
            new Car
            {
                Id = 1,
                Name = "Mercedes C class",
                Description = "Is really comfortable car",
                Title = "Car",
                Url = "https://www.topgear.com/sites/default/files/2021/11/Mercedes_C300D_0000.jpg",
                Price = 45000,
            },
            new Car
            {
                Id = 2,
                Name = "BMW X6",
                Description = "Really bold car",
                Title = "Car",
                Url = "https://news.vip-car.org/files/news/2023/novye-bmw-x5-i-bmw-x6-2024-obzor-osnovnyh-izmeneniy/bmw-x6-3.jpg",
                Price = 37000,
            },new Car
            {
                Id = 3,
                Name = "Tesla model S",
                Description = "This is super electro car",
                Title = "Car",
                Url = "https://ecoautoinfo.com/uploads/images/default/sss222.jpg",
                Price = 42000,
            },new Car
            {
                Id = 4,
                Name = "Mazda CX5",
                Description = "City car",
                Title = "Car",
                Url = "https://i.infocar.ua/i/2/5166/80677/1024x.jpg",
                Price = 32000,
            },new Car
            {
                Id = 5,
                Name = "Wolksvagen touareg 2021",
                Description = "Beautiful car and all",
                Title = "Car",
                Url = "https://cdn4.riastatic.com/photosnew/auto/photo/volkswagen_touareg__461761024f.jpg",
                Price = 38000,
            },
            new Car
            {
                Id = 1,
                Name = "Mercedes C class",
                Description = "Is really comfortable car",
                Title = "Car",
                Url = "https://www.topgear.com/sites/default/files/2021/11/Mercedes_C300D_0000.jpg",
                Price = 45000,
            },
        };

        //public IActionResult Index()
        //{
        //    ViewBag.cars = cars;
        //    return View();
        //}

        public async Task<IActionResult> Index(int delete = 0, int edit = 0)
        {
            if(delete != 0)
            {
                var car = await db.Cars.FindAsync(delete);

                if(car != null)
                {
                    db.Cars.Remove(car);
                    await db.SaveChangesAsync();
                }                
            }
            else if (edit != 0)
            {
                var car = await db.Cars.FindAsync(edit);

                if(car == null) return NotFound();

                return View(car);
            }

            return View(await db.Cars.ToListAsync());
        }

        public IActionResult CarList()
        {
            ViewBag.cars = cars;
            return View();
        }
        public IActionResult CarInfo()
        {
            ViewBag.cars = cars;
            return View();
        }
    }
}
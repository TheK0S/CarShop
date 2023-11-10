using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarShop.Models;
using System.Runtime.ConstrainedExecution;
using System.Net;
using CarShop.Interfaces;
using CarShop.Services;
using Microsoft.AspNetCore.Routing;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace CarShop.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ICarService _carService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public CarsController(ICategoryService categoryService, ICarService carService, IWebHostEnvironment hostEnvironment)
        {
            _categoryService = categoryService;
            _carService = carService;
            _hostingEnvironment = hostEnvironment;
        }


        // GET: Cars
        public async Task<IActionResult> Index()
        {
            var response = await _carService.GetCarsAsync();
            return View(response.Data);
        }

        // GET: Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var response = await _carService.GetCarAsync((int)id);
            var car = response.Data;

            if (car == null)
                return NotFound();

            //messanger.SendMessage("Hello! It is car shop", new Models.User() { UserName = "Kos", Email = "turchak@sweetondale.cz" }, "Car shop");
            return View(car);
        }

        // GET: Cars/CarDetails/5
        public async Task<IActionResult> CarDetails(int? id)
        {
            if (id == null)
                return NotFound();

            var response = await _carService.GetCarAsync((int)id);
            var car = response.Data;

            if (car == null)
                return NotFound();

            return View(car);
        }

        // GET: Cars/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,LongDesc,ShortDesc,Title,Url,Price,IsFavourite,Count, CategoryId")] Car car)
        {
            if (!ModelState.IsValid)
                return View(car);

            var response = await _carService.CreateCarAsync(car);
            if (response.StatusCode == HttpStatusCode.Created)
                return RedirectToAction(nameof(Index));

            return Problem("Something happened wrong",null, (int)response.StatusCode);
        }

        // GET: Cars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var response = await _carService.GetCarAsync((int)id);
            var car = response.Data;

            if (car == null)
                return NotFound();

            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to. [Bind("Id,Name,ShortDesc,LongDesc,Title,Url,Price,IsFavourite,Count")]
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.  [Bind("Id,Name,ShortDesc,LongDesc,Title,Url,Price,CategoryId")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,LongDesc,ShortDesc,Title,Url,Price,IsFavourite,Count, CategoryId")] Car car)
        {
            if (id != car.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(car);

            var response = await _carService.UpdateCarAsync(car);

            if (response.StatusCode == HttpStatusCode.OK)
                return RedirectToAction(nameof(Index));

            return View(car);
        }

        // GET: Cars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var response = await _carService.GetCarAsync((int)id);
            var car = response.Data;

            if (car == null)
                return NotFound(response.Message);

            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id == null)
                return NotFound();

            var response = await _carService.GetCarAsync((int)id);
            var car = response.Data;

            if(car == null)
                return NotFound();

            var delResponse = await _carService.DeleteCarAsync((int)id);

            if (delResponse.Data)
                return RedirectToAction(nameof(Index));

            return StatusCode((int)delResponse.StatusCode, delResponse.Message);
        }

        [HttpPost("filter")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CarFilter(CarsFilter filter)
        {
            if (filter == null)
                return NotFound();

            var response = await _carService.PostFilterredCarsAsync(filter);
            if(response.StatusCode != HttpStatusCode.OK)
                return NotFound();

            var filterredCars = response.Data ?? new List<Car>();

            string htmlText = "";

            foreach (var car in filterredCars)
            {
                htmlText += $"" +
                    $"< div class=\"col-xxl-4 col-lg-6 col-md-12 p-2\">" +
                        $"<img src = \"{car.Url}\" alt=\"{car.Name}\">" +
                        $"<div class=\"text-start p-2\">" +
                            $"<h3 class=\"text-center\">{car.Name}</h3>" +
                            $"<p>{car.Title}</p>" +
                            $"<p>{car.ShortDesc}</p>" +
                            $"<p>Price: $ {car.Price}</p>" +
                            $"<a asp-controller=\"Cars\" asp-action=\"CarDetails\" asp-route-id=\"{car.Id}\" class=\"manageBtn btn btn-info text-center\">More details</a>" +
                            $"<a asp-controller= \"Cart\" asp-action= \"AddItem\" asp-route-carId= \"{car.Id}\" class=\"manageBtn btn btn-success text-center\">Bye</a>" +
                        $"</div>" +
                    $"</div>";
            }

            return StatusCode(200, htmlText);
        }

        private string GetTextHtmlCarCard()
        {
            var pathPartialView = _hostingEnvironment.ContentRootPath + "/Views/Shared/_CarCard.cshtml";

            if (System.IO.File.Exists(pathPartialView))
            {
                var content = System.IO.File.ReadAllText(pathPartialView);
                // Делайте что-то с содержимым файла
                return content;
            }
            else
            {
                // Обработка ситуации, когда файл не найден
                return string.Empty;
            }
        }
    }
}

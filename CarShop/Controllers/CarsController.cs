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

namespace CarShop.Controllers
{
    public class CarsController : Controller
    {
        private readonly HttpClient httpClient = new HttpClient();

        // GET: Cars
        public async Task<IActionResult> Index()
        {
            return View(await httpClient.GetFromJsonAsync<IEnumerable<Car>>($"{Api.apiUri}cars"));
        }

        // GET: Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var car = await httpClient.GetFromJsonAsync<Car>($"{Api.apiUri}cars/{id}");
            if (car == null)
                return NotFound();

            return View(car);
        }

        // GET: Cars/CarDetails/5
        public async Task<IActionResult> CarDetails(int? id)
        {
            if (id == null)
                return NotFound();

            var car = await httpClient.GetFromJsonAsync<Car>($"{Api.apiUri}cars/{id}");
            if (car == null)
                return NotFound();

            return View(car);
        }

        // GET: Cars/Create
        public async Task<IActionResult> Create()
        {
            var categories = await httpClient.GetFromJsonAsync<IEnumerable<Category>>($"{Api.apiUri}category");
            var categoriesDictionary = new Dictionary<int, string>();
            if (categories != null)
                foreach (var category in categories)
                    categoriesDictionary.Add(category.Id, category.Name ?? "no name");

            ViewBag.Categories = categoriesDictionary;

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

            await httpClient.PostAsJsonAsync($"{Api.apiUri}cars", car);
            if (car == null)
                return Problem("Something happened wrong");

            return RedirectToAction(nameof(Index));
        }

        // GET: Cars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var car = await httpClient.GetFromJsonAsync<Car>($"{Api.apiUri}cars/{id}");
            if (car == null)
                return NotFound();

            var categories = await httpClient.GetFromJsonAsync<IEnumerable<Category>>($"{Api.apiUri}category");
            var categoriesDictionary = new Dictionary<int, string>();
            if (categories != null)
                foreach (var category in categories)
                    categoriesDictionary.Add(category.Id, category.Name ?? "no name");

            ViewBag.Categories = categoriesDictionary;

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

            var response = await httpClient.PutAsJsonAsync($"{Api.apiUri}cars/{id}", car);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            return View(car);
        }

        // GET: Cars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var car = await httpClient.GetFromJsonAsync<Car>($"{Api.apiUri}cars/{id}");

            if (car == null)
                return NotFound();

            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id == null)
                return NotFound();

            var car = await httpClient.DeleteAsync($"{Api.apiUri}cars/{id}");

            if (car == null)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}

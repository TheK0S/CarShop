using CarShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarShop.Controllers
{
    [Authorize(Roles = "admin")]
    public class CategoriesController : Controller
    {
        private readonly HttpClient httpClient = new HttpClient();

        // GET: CategoriesController
        public async Task<IActionResult> Index()
        {
            return View(await httpClient.GetFromJsonAsync<IEnumerable<Category>>($"{Api.apiUri}category"));
        }

        // GET: CategoriesController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var category = await httpClient.GetFromJsonAsync<Category>($"{Api.apiUri}category/{id}");

            if (category == null)
                return NotFound();

            return View(category);
        }

        // GET: CategoriesController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CategoriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, Description")] Category category)
        {
            var response = await httpClient.PostAsJsonAsync($"{Api.apiUri}category", category);

            if(response == null)
                return View(category);

            return RedirectToAction(nameof(Index));
        }

        // GET: CategoriesController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var category = await httpClient.GetFromJsonAsync<Category>($"{Api.apiUri}Category/{id}");

            if (category == null)
                return NotFound();

            return View(category);
        }

        // POST: CategoriesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            if (category == null || category.Id != id)
                return NotFound();

            var response = await httpClient.PutAsJsonAsync($"{Api.apiUri}category/{id}", category);

            if (response == null)
                return BadRequest();

            return RedirectToAction(nameof(Index));
        }

        // DELETE: CategoriesController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var category = await httpClient.GetFromJsonAsync<Category>($"{Api.apiUri}category/{id}");

            return View(category);
        }

        // POST: CategoriesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            var category = await httpClient.DeleteAsync($"{Api.apiUri}category/{id}");

            if (category == null)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}

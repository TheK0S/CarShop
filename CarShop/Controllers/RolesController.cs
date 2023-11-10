using CarShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarShop.Controllers
{
    [Authorize(Roles = "admin")]
    public class RolesController : Controller
    {
        private HttpClient httpClient = new HttpClient();

        // GET: RolesController
        public async Task<ActionResult> Index()
        {
            return View(await httpClient.GetFromJsonAsync<IEnumerable<Role>>($"{Api.apiUri}roles"));
        }

        // GET: RolesController/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var role = await httpClient.GetFromJsonAsync<Role>($"{Api.apiUri}role/{id}");

            if (role == null)
                return NotFound();

            return View(role);
        }

        // GET: RolesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RolesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Id,Name")] Role role)
        {
            if (string.IsNullOrEmpty(role.Name))
                ModelState.AddModelError(nameof(role.Name), "Enter role name");

            if (ModelState.IsValid)
            {
                
                var response = await httpClient.PostAsJsonAsync($"{Api.apiUri}roles", role);

                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
                else
                    return BadRequest();
            }
            return View(role);
        }

        // GET: RolesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RolesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var role = await httpClient.GetFromJsonAsync<Role>($"{Api.apiUri}roles/{id}");

            if (role == null)
                return NotFound();

            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Role role)
        {
            if (id != role.Id)
                return NotFound();

            if (string.IsNullOrEmpty(role.Name))
                ModelState.AddModelError(nameof(role.Name), "Enter role name");

            if (ModelState.IsValid)
            {
                var response = await httpClient.PutAsJsonAsync($"{Api.apiUri}roles/{id}", role);

                if (response == null)
                    return NotFound();

                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }

        // GET: RolesController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return BadRequest();

            var role = await httpClient.GetFromJsonAsync<Role>($"{Api.apiUri}roles/{id}");

            if (role == null)
                return NotFound();

            return View(role);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var role = await httpClient.GetFromJsonAsync<Role>($"{Api.apiUri}roles/{id}");
            if (role == null)
                return NotFound();

            await httpClient.DeleteAsync($"{Api.apiUri}roles/{id}");

            return RedirectToAction(nameof(Index));
        }
    }
}

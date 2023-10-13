using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarShop.Models;
using Newtonsoft.Json;

namespace CarShop.Controllers
{
    public class UsersController : Controller
    {
        private HttpClient httpClient = new HttpClient();

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await httpClient.GetFromJsonAsync<IEnumerable<User>>($"{Api.apiUri}user"));
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var user = await httpClient.GetFromJsonAsync<User>($"{Api.apiUri}user/{id}");

            if (user == null)
                return NotFound();

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,Password,Email,AccessLevel")] User user)
        {
            if (ModelState.IsValid)
            {
                //string apiUrl = $"user?id=0&UserName={user.UserName}&Password={user.Password}&Email={user.Email}&AccessLevel={user.AccessLevel}"; 
                var response = await httpClient.PostAsJsonAsync($"{Api.apiUri}user", user);

                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
                else
                    return NoContent();
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var user = await httpClient.GetFromJsonAsync<User>($"{Api.apiUri}user/{id}");

            if (user == null)
                return NotFound();

            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,Password,Email,AccessLevel")] User user)
        {
            if (id != user.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var response = await httpClient.PutAsJsonAsync($"{Api.apiUri}user/{id}", user);

                if (response == null)
                    return NotFound();

                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var user = await httpClient.GetFromJsonAsync<User>($"{Api.apiUri}user/{id}");
            if (user == null)
                return NotFound();

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await httpClient.GetFromJsonAsync<User>($"{Api.apiUri}user/{id}");
            if(user == null)
                return NotFound();

            await httpClient.DeleteAsync($"{Api.apiUri}user/{id}");

            return RedirectToAction(nameof(Index));
        }
    }
}

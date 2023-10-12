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
        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await GetUsers());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var users = await GetUsers();
            if (users == null)
                return NotFound();

            var user = users.FirstOrDefault(u => u.Id == id);
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
                string apiUrl = $"user?id=0&UserName={user.UserName}&Password={user.Password}&Email={user.Email}&AccessLevel={user.AccessLevel}"; 
                var response = await Api.GetApiResponse("apiUrl");

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
            //if (id == null || _context.User == null)
            //{
            //    return NotFound();
            //}

            //var user = await _context.User.FindAsync(id);
            //if (user == null)
            //{
            //    return NotFound();
            //}
            //return View(user);

            return View();
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,Password,Email,Created")] User user)
        {
            //if (id != user.Id)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(user);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!UserExists(user.Id))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
            //return View(user);

            return View();
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            //if (id == null || _context.User == null)
            //{
            //    return NotFound();
            //}

            //var user = await _context.User
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (user == null)
            //{
            //    return NotFound();
            //}

            //return View(user);
            return View();
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //if (_context.User == null)
            //{
            //    return Problem("Entity set 'AppDbContext.User'  is null.");
            //}
            //var user = await _context.User.FindAsync(id);
            //if (user != null)
            //{
            //    _context.User.Remove(user);
            //}

            //await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));

            return View();
        }

        private async Task<IEnumerable<User>> GetUsers()
        {
            var response = await Api.GetApiResponse("user");

            string jsonContent = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<IEnumerable<User>>(jsonContent);

            return result ?? new List<User>();
        }
    }
}

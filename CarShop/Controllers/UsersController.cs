﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarShop.Models;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

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
        public async Task<IActionResult> Create()
        {
            var rolesList = await httpClient.GetFromJsonAsync<List<Role>>($"{Api.apiUri}roles");

            ViewBag.Roles = new SelectList(rolesList, nameof(Role.Id), nameof(Role.Name));

            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,Password,Email,RoleId")] User user)
        {
            if (string.IsNullOrEmpty(user.UserName))
                ModelState.AddModelError(nameof(user.UserName), "Enter User name");

            if (string.IsNullOrEmpty(user.Password) || !Regex.IsMatch(user.Password, @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d).{8,}$"))
                ModelState.AddModelError(nameof(user.Password), "Password is too easy");

            if (string.IsNullOrEmpty(user.Email) || !Regex.IsMatch(user.Email, @"^[A-Za-z0-9\.\-_]+@[A-Za-z0-9\.\-_]+\.\w+$"))
                ModelState.AddModelError(nameof(user.Email), "Email is not valid");

            if (ModelState.IsValid)
            {
                //string apiUrl = $"user?id=0&UserName={user.UserName}&Password={user.Password}&Email={user.Email}&AccessLevel={user.AccessLevel}"; 
                var response = await httpClient.PostAsJsonAsync($"{Api.apiUri}user", user);

                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
                else
                    return NoContent();
            }

            var rolesList = await httpClient.GetFromJsonAsync<List<Role>>($"{Api.apiUri}roles");

            ViewBag.Roles = new SelectList(rolesList, nameof(Role.Id), nameof(Role.Name), rolesList?.FirstOrDefault(r => r.Id == user.RoleId));

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

            var rolesList = await httpClient.GetFromJsonAsync<List<Role>>($"{Api.apiUri}roles");

            ViewBag.Roles = new SelectList(rolesList, nameof(Role.Id), nameof(Role.Name), rolesList?.FirstOrDefault(r => r.Id == user.RoleId));

            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,Password,Email,RoleId")] User user)
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

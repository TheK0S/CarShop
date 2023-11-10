using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarShop.Models;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using CarShop.Interfaces;
using System.Net;
using CarShop.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace CarShop.Controllers
{
    [Authorize(Roles = "admin")]
    public class UsersController : Controller
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        // GET: Users
        public async Task<IActionResult> Index()
        {
            var response = await _userService.GetUsersAsync();
            return View(response.Data);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var response = await _userService.GetUserAsync((int)id);

            var user = response.Data;

            if (user == null)
                return NotFound(response.Message);      

            return View(user);
        }

        // GET: Users/Create
        public async Task<IActionResult> Create()
        {
            var rolesList = await GetRoleList();

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
                user.Password = HashPasswordHelper.HashPasword(user.Password);

                var response = await _userService.CreateUserAsync(user);

                if (response.StatusCode == HttpStatusCode.Created)
                    return RedirectToAction(nameof(Index));
                else
                    return StatusCode((int)response.StatusCode, response.Message);
            }

            var rolesList = await GetRoleList();

            ViewBag.Roles = new SelectList(rolesList, nameof(Role.Id), nameof(Role.Name), rolesList?.FirstOrDefault(r => r.Id == user.RoleId));

            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var response = await _userService.GetUserAsync((int)id);
            var user = response.Data;

            if (user == null)
                return NotFound();

            var rolesList = await GetRoleList();

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
                var response = await _userService.UpdateUserAsync(user);

                if (!response.Data)
                    return StatusCode((int)response.StatusCode, response.Message);

                return RedirectToAction(nameof(Index));
            }

            var rolesList = await GetRoleList();

            ViewBag.Roles = new SelectList(rolesList, nameof(Role.Id), nameof(Role.Name), rolesList?.FirstOrDefault(r => r.Id == user.RoleId));

            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var response = await _userService.GetUserAsync((int)id);
            var user = response.Data;

            if (user == null)
                return StatusCode((int)response.StatusCode, response.Message);

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _userService.GetUserAsync(id);
            var user = response.Data;

            if (user == null)
                return StatusCode((int)response.StatusCode, response.Message);

            var delResponse = await _userService.DeleteUserAsync(id);

            if(delResponse.Data)
                return RedirectToAction(nameof(Index));

            return StatusCode((int)delResponse.StatusCode, delResponse.Message);
        }

        private async Task<List<Role>> GetRoleList()
        {
            List<Role> rolesList;

            using (HttpClient httpClient = new HttpClient())
            {
                rolesList = await httpClient.GetFromJsonAsync<List<Role>>($"{Api.apiUri}roles") ?? new List<Role>();
            }

            return rolesList;
        }
    }
}

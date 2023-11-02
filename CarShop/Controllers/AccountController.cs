using CarShop.Interfaces;
using CarShop.Models;
using CarShop.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;
using System.Net;

namespace CarShop.Controllers
{
    public class AccountController : Controller
    {
        //private string cookieKey = "Auth";
        private IMemoryCache _memoryCache;
        private IMessanger _messanger;
        private IAccountService _accountService;

        public AccountController(IMemoryCache memoryCache, IMessanger messanger, IAccountService accountService)
        {
            _memoryCache = memoryCache;
            _messanger = messanger;
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Register() => View();


        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _accountService.Register(model);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(response.Data));

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", response.Message);
            }
            return View(model);            
        }

        [HttpGet]
        public IActionResult Login() => View();


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _accountService.Login(model);

                if(response.StatusCode == HttpStatusCode.OK)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(response.Data));

                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _accountService.ChangePassword(model);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return Json(new { description = response.Message });
                }
            }
            var modelError = ModelState.Values.SelectMany(v => v.Errors);

            return StatusCode(StatusCodes.Status500InternalServerError, new { modelError.FirstOrDefault().ErrorMessage });
        }


        //public ActionResult SetCookie()
        //{
        //    CookieOptions options = new CookieOptions();
        //    options.Expires = DateTime.Now.AddHours(1);
        //    Response.Cookies.Append(cookieKey, "value", options);


        //    HttpContext.Session.SetString("sessionKey", "value");
        //    string value = HttpContext.Session.GetString("sessionKey");


        //    if (!_memoryCache.TryGetValue("saved_list", out object valueCache))
        //    {
        //        valueCache = LoadData();

        //        //save to cathe without lifetime
        //        //memoryCache.Set("saved_list", valueCache);

        //        //save to cathe with lifetime = 10 second
        //        //memoryCache.Set("saved_list", valueCache, TimeSpan.FromSeconds(10));

        //        //save to cathe with time window = 5 second, and if data no using durin this time data will be deleted 
        //        _memoryCache.Set("saved_list", valueCache, new MemoryCacheEntryOptions() { SlidingExpiration = TimeSpan.FromSeconds(5)});
        //    }

        //    string message = "some text";
        //    _messanger.SendMessage(message, new User(), "title");

        //    return View();
        //}
    }
}

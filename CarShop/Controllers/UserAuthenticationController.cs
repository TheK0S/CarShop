using CarShop.Interfaces;
using CarShop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CarShop.Controllers
{
    public class UserAuthenticationController : Controller
    {
        private string cookieKey = "Auth";
        private IMemoryCache memoryCache;
        private IMessanger messanger;

        public UserAuthenticationController(IMemoryCache memoryCache, IMessanger messanger)
        {
            this.memoryCache = memoryCache;
            this.messanger = messanger;
        }
        // GET: UserAuthenticationController
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult SetCookie()
        {
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddHours(1);
            Response.Cookies.Append(cookieKey, "value", options);


            HttpContext.Session.SetString("sessionKey", "value");
            string value = HttpContext.Session.GetString("sessionKey");


            if (!memoryCache.TryGetValue("saved_list", out object valueCache))
            {
                valueCache = LoadData();

                //save to cathe without lifetime
                //memoryCache.Set("saved_list", valueCache);

                //save to cathe with lifetime = 10 second
                //memoryCache.Set("saved_list", valueCache, TimeSpan.FromSeconds(10));

                //save to cathe with time window = 5 second, and if data no using durin this time data will be deleted 
                memoryCache.Set("saved_list", valueCache, new MemoryCacheEntryOptions() { SlidingExpiration = TimeSpan.FromSeconds(5)});
            }

            string message = "some text";
            messanger.SendMessage(message, new User(), "title");

            return View();
        }

        private object? LoadData()
        {
            return new object();
        }
    }
}

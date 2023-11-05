using CarShop.Interfaces;
using CarShop.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarShop.Controllers
{
    public class CartController : Controller
    {
        private readonly IShopCartService _shopCartService;

        public CartController(IShopCartService shopCartService)
        {
            _shopCartService = shopCartService;
        }
        public async Task<IActionResult> Index()
        {
            int.TryParse(User.FindFirstValue("UserId"), out int userId);
            var cart = await _shopCartService.GetShopCartByUserId(userId);
            return View(cart);
        }
    }
}

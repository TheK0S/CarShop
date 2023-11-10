using Azure;
using CarShop.Interfaces;
using CarShop.Models;
using CarShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace CarShop.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IShopCartService _shopCartService;
        private readonly ICarService _carService;
        private readonly IUserService _userService;
        private readonly IMessanger _messanger;

        public CartController(IShopCartService shopCartService, ICarService carService, IUserService userService, IMessanger messanger)
        {
            _shopCartService = shopCartService;
            _carService = carService;
            _userService = userService;
            _messanger = messanger;
        }
        public async Task<IActionResult> Index()
        {            
            int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId);
            var response = await _shopCartService.GetShopCartByUserId(userId);

            return View(response.Data);
        }

        public async Task<ActionResult> RemoveItem(int id)
        {
            await _shopCartService.RemoveItem(id);

            return RedirectToAction("Index", "Cart");
        }

        public async Task<ActionResult> AddItem(int carId)
        {
            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int currentUserId);
            var shopCartResponse = await _shopCartService.GetShopCartByUserId(currentUserId);
            var carResponse = await _carService.GetCarAsync(carId);
            var cart = shopCartResponse.Data;
            var car = carResponse.Data;
            if (cart == null || car == null)
                return RedirectToAction("Index", "Home");

            var item = new ShopCartItem 
            { 
                ShopCartId = cart.Id,
                OrderId = null,
                CarId = car.Id,
                Car = null,
                Price = car.Price,
                Count = 1
            };

            await _shopCartService.AddItem(item);

            return RedirectToAction("Index", "Cart");
        }

        [HttpGet]
        public IActionResult CreateOrder()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrder(CreateOrderViewModel model)
        {
            if(model == null)
                return Problem("Something happened wrong", null, 404);

            if (!ModelState.IsValid)
                return View();

            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId);
            var response = await _shopCartService.CreateOrder(userId);

            bool isOrderCreated = response.Data;
            if (!isOrderCreated)
                return Problem("Something happened wrong", null, 404);

            int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int currentUserId);
            var userResponse = await _userService.GetUserAsync(currentUserId);
            User currentUser = userResponse.Data;

            if (currentUser != null)
            {
                string message = $"Dear {currentUser.UserName} your order is created.\n" +
                    $"Delivery company: {model.Post}.\n" +
                    $"Delivery address: {model.DeliveryCity}, {model.DeliveryAddress}.\n" +
                    $"Payment method: {model.PaymentMethod}.\n" +
                    $"Thank you for choosing our store!";

                _messanger.SendMessage(message, currentUser, "Car shop order");
            }

            return View("OrderCreated");
        }
    }
}

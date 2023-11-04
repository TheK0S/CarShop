using CarShop.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarShop.Controllers
{
    public class CartController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ICarService _carService;

        public CartController(ICategoryService categoryService, ICarService carService)
        {
            _categoryService = categoryService;
            _carService = carService;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}

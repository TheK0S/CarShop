using CarShop.Models;

namespace CarShop.ViewModels
{
    public class HomeIndexViewModel
    {
        public List<Car>? TopSaleCars { get; set; }
        public List<Car>? DiscountCars { get; set; }
    }
}

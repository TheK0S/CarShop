using CarShop.Models;

namespace CarShop.ViewModels
{
    public class CreateOrderViewModel
    {
        public int UserId { get; set; }
        public string DeliveryAdress { get; set; }
        public string PaymentMethod { get; set; }
    }
}

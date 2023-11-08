using CarShop.Models;
using Microsoft.Build.Framework;

namespace CarShop.ViewModels
{
    public class CreateOrderViewModel
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string DeliveryAdress { get; set; }
        [Required]
        public string PaymentMethod { get; set; }
    }
}

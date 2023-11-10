using CarShop.Models;
using Microsoft.Build.Framework;

namespace CarShop.ViewModels
{
    public class CreateOrderViewModel
    {
        [Required]
        public string? Post { get; set; }
        [Required]
        public string? DeliveryCity { get; set; }
        [Required]
        public string? DeliveryAddress { get; set; }
        [Required]
        public string? PaymentMethod { get; set; }
    }
}

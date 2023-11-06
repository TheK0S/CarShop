using CarShop.Models;

namespace CarShop.ViewModels
{
    public class PersonalAreaViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime CreateDate { get; set; }
        public List<Order>? Orders { get; set; }
        public string ErrorMessage { get; set; }
    }
}

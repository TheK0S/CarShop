
namespace CarShop.Models
{
    public class ShopCart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<ShopCartItem>? Items { get; set; }
    }
}

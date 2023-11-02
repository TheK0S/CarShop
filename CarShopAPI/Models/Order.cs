namespace CarShopAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime DateTime { get; set; }
        public List<ShopCartItem>? shopCartItems { get; set; }

    }
}

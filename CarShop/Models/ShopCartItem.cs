namespace CarShop.Models
{
    public class ShopCartItem
    {
        public int ItemId { get; set; }
        public Car Car { get; set; }
        public int Price { get; set; }
        public string ShopCartId { get; set; }
    }
}

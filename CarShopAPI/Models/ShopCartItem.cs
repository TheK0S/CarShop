namespace CarShopAPI.Models
{
    public class ShopCartItem
    {
        public int Id { get; set; }
        public Car Car { get; set; }
        public uint Price { get; set; }
        public string ShopCartId { get; set; }
    }
}

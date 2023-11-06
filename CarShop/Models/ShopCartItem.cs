namespace CarShop.Models
{
    public class ShopCartItem
    {
        public int Id { get; set; }
        public int ShopCartId { get; set; }
        public int? OrderId { get; set; }
        public int CarId { get; set; }
        public Car? Car { get; set; }
        public uint Price { get; set; }
        public int Count { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;

namespace CarShopAPI.Models
{
    public class ShopCart
    {
        private readonly AppDbContext _db;
        public ShopCart(AppDbContext db)
        {
            _db = db;
        }
        public string Id { get; set; }
        public List<ShopCartItem> Items { get; set; }
        public static ShopCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<AppDbContext>();
            string shopCartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", shopCartId);

            return new ShopCart(context) { Id = shopCartId };
        }

        public async Task AddToCart(Car car, int amount)
        {
            _db.ShopCartItem.Add(new ShopCartItem
            {
                ShopCartId = Id,
                Car = car,
                Price = car.Price
            });

            await _db.SaveChangesAsync();
        }

        public List<ShopCartItem> GetItems()
        {
            return _db.ShopCartItem.Where(c => c.ShopCartId == Id).Include(s => s.Car).ToList();
        }
    }
}

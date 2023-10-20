using Microsoft.EntityFrameworkCore;

namespace CarShopAPI.Models
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Car> Car { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<ShopCartItem> ShopCartItem { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Server=DESKTOP-K60TA32\\SQLEXPRESS;Database=CarShopAPI;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True;");
    }
}

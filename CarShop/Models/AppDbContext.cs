using Microsoft.EntityFrameworkCore;


namespace CarShop.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Car> Car { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Server=DESKTOP-HHO6PH0;Database=CarShop;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True;");
    }
}

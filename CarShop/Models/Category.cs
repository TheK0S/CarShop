using System.ComponentModel.DataAnnotations;

namespace CarShop.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<Car>? Cars { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace CarShop.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? ShortDesc { get; set; }
        public string? LongDesc { get; set; }
        public string? Title { get; set; }
        public string? Url { get; set; }
        public uint Price { get; set; } = 0;
        public bool IsFavourite { get; set; } = false;
        public uint Count { get; set; } = 0;
        public virtual Category? Category { get; set; }
    }
}

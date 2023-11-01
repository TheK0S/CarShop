using System.ComponentModel.DataAnnotations;

namespace CarShop.Models
{
    public class CarsFilter
    {
        public uint Price { get; set; } = 0;
        public bool IsFavourite { get; set; } = false;
        public uint Count { get; set; } = 0;
        public int CategoryId { get; set; }
    }
}

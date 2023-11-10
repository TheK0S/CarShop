using System.ComponentModel.DataAnnotations;

namespace CarShopAPI.Models
{
    public class CarsFilter
    {
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
        public List<string>? SelectedCategories { get; set; }
        public bool IsFavourite { get; set; }
    }
}

using CarShop.Models;

namespace CarShop.ViewModels
{
    public class CategoryOptionsViewModel
    {
        public int SelectedCategoryId { get; set; }
        public List<Category>? Categories { get; set; }
    }
}

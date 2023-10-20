using CarShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarShop.Components
{
    public class CategoryOptions : ViewComponent
    {
        private readonly CategoryService service;

        public CategoryOptions(CategoryService service)
        {
            this.service = service;
        }

        public async Task<IViewComponentResult> InvokeAsync(int selectedCategoryId)
        {
            List<Category> categories = await service.GetCategoriesAsync();

            CategoryOptionsViewModel viewModel = new CategoryOptionsViewModel()
            {
                SelectedCategoryId = selectedCategoryId,
                Categories = categories
            };

            return View(viewModel);
        }
    }
}

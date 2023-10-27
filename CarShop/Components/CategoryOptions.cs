using CarShop.Interfaces;
using CarShop.Models;
using CarShop.Services;
using CarShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CarShop.Components
{
    public class CategoryOptions : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public CategoryOptions(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int selectedCategoryId)
        {
            var response = await _categoryService.GetCategoriesAsync();
            List<Category> categories = response.Data.ToList();

            CategoryOptionsViewModel viewModel = new CategoryOptionsViewModel()
            {
                SelectedCategoryId = selectedCategoryId,
                Categories = categories
            };

            return View(viewModel);
        }
    }
}

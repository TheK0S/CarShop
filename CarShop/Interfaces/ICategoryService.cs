using CarShop.Models;

namespace CarShop.Interfaces
{
    public interface ICategoryService
    {
        Task<BaseResponse<IEnumerable<Category>>> GetCategoriesAsync();
        Task<BaseResponse<Category>> GetCategoryAsync(int id);
        Task<BaseResponse<bool>> CreateCategoryAsync(Category category);
        Task<BaseResponse<bool>> UpdateCategoryAsync(Category category);
        Task<BaseResponse<bool>> DeleteCategoryAsync(int categoryId);
    }
}

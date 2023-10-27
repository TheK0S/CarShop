using CarShop.Interfaces;
using CarShop.Models;

namespace CarShop.Services
{
    public class CategoryService : ICategoryService
    {
        public async Task<BaseResponse<IEnumerable<Category>>> GetCategoriesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<Category>> GetCategoryAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<bool>> CreateCategoryAsync(Category user)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<bool>> UpdateCategoryAsync(Category user)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<bool>> DeleteCategoryAsync(int categoryId)
        {
            throw new NotImplementedException();
        }
    }
}

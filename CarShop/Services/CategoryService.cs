using CarShop.Interfaces;
using CarShop.Models;
using System.Net.Http;
using System.Text.Json;

namespace CarShop.Services
{
    public class CategoryService : ICategoryService
    {
        HttpClient httpClient = new HttpClient();

        public async Task<BaseResponse<IEnumerable<Category>>> GetCategoriesAsync()
        {
            var response = await httpClient.GetAsync($"{Api.apiUri}category");

            var baseResponse = new BaseResponse<IEnumerable<Category>>() { StatusCode = response.StatusCode };

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    baseResponse.Data = await response.Content.ReadFromJsonAsync<IEnumerable<Category>>();
                }
            }
            catch (JsonException ex)
            {
                baseResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                baseResponse.Message = ex.Message;
            }
            catch (Exception ex)
            {
                baseResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                baseResponse.Message = ex.Message;
            }

            return baseResponse;
        }

        public async Task<BaseResponse<Category>> GetCategoryAsync(int id)
        {
            var response = await httpClient.GetAsync($"{Api.apiUri}category/{id}");

            var baseResponse = new BaseResponse<Category>() { StatusCode = response.StatusCode };

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    baseResponse.Data = await response.Content.ReadFromJsonAsync<Category>();
                }
            }
            catch (JsonException ex)
            {
                baseResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                baseResponse.Message = ex.Message;
            }
            catch (Exception ex)
            {
                baseResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                baseResponse.Message = ex.Message;
            }

            return baseResponse;
        }

        public async Task<BaseResponse<bool>> CreateCategoryAsync(Category category)
        {
            var response = await httpClient.PostAsJsonAsync($"{Api.apiUri}category",category);

            var baseResponse = new BaseResponse<bool>()
            {
                Data = response.IsSuccessStatusCode,
                Message = await response.Content.ReadAsStringAsync(),
                StatusCode = response.StatusCode,
            };

            return baseResponse;
        }

        public async Task<BaseResponse<bool>> UpdateCategoryAsync(Category category)
        {
            var response = await httpClient.PutAsJsonAsync($"{Api.apiUri}category", category);

            var baseResponse = new BaseResponse<bool>()
            {
                Data = response.IsSuccessStatusCode,
                Message = await response.Content.ReadAsStringAsync(),
                StatusCode = response.StatusCode,
            };

            return baseResponse;
        }

        public async Task<BaseResponse<bool>> DeleteCategoryAsync(int categoryId)
        {
            var response = await httpClient.DeleteAsync($"{Api.apiUri}category/{categoryId}");

            var baseResponse = new BaseResponse<bool>()
            {
                Data = response.IsSuccessStatusCode,
                Message = await response.Content.ReadAsStringAsync(),
                StatusCode = response.StatusCode,
            };

            return baseResponse;
        }
    }
}

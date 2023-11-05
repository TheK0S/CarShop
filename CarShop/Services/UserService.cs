using CarShop.Interfaces;
using CarShop.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;

namespace CarShop.Services
{
    public class UserService : IUserService
    {
        HttpClient httpClient = new HttpClient();
        public async Task<BaseResponse<IEnumerable<User>>> GetUsersAsync()
        {
            var response = await httpClient.GetAsync($"{Api.apiUri}user");

            var baseResponse = new BaseResponse<IEnumerable<User>>() { StatusCode = response.StatusCode };

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    baseResponse.Data = await response.Content.ReadFromJsonAsync<IEnumerable<User>>();
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

        public async Task<BaseResponse<User>> GetUserAsync(int id)
        {
            var response = await httpClient.GetAsync($"{Api.apiUri}user/{id}");

            var baseResponse = new BaseResponse<User>() { StatusCode = response.StatusCode};
            
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    baseResponse.Data = await response.Content.ReadFromJsonAsync<User>();
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

        public async Task<BaseResponse<User>> CreateUserAsync(User user)
        {
            var response = await httpClient.PostAsJsonAsync($"{Api.apiUri}user", user);
            var createdUser = await response.Content.ReadFromJsonAsync<User>();

            var baseResponse = new BaseResponse<User>() 
            {
                Data = createdUser,
                Message = await response.Content.ReadAsStringAsync(),
                StatusCode = response.StatusCode                
            };

            return baseResponse;
        }

        public async Task<BaseResponse<bool>> UpdateUserAsync(User user)
        {
            var response = await httpClient.PutAsJsonAsync($"{Api.apiUri}user/{user.Id}", user);

            var baseResponse = new BaseResponse<bool>()
            {
                Data = response.IsSuccessStatusCode,
                Message = await response.Content.ReadAsStringAsync(),
                StatusCode = response.StatusCode
            };

            return baseResponse;
        }

        public async Task<BaseResponse<bool>> DeleteUserAsync(int userId)
        {
            var response = await httpClient.DeleteAsync($"{Api.apiUri}user/{userId}");

            var baseResponse = new BaseResponse<bool>()
            {
                Data = response.IsSuccessStatusCode,
                Message = await response.Content.ReadAsStringAsync(),
                StatusCode = response.StatusCode
            };

            return baseResponse;
        }
    }
}

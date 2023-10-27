using CarShop.Helpers;
using CarShop.Interfaces;
using CarShop.Models;
using CarShop.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarShop.Services
{
    public class AccountService : IAccountService
    {
        public Task<int> Login(LoginViewModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Register(RegisterViewModel model)
        {
            HttpClient httpClient = new HttpClient();
            try
            {
                var users = await httpClient.GetFromJsonAsync<IEnumerable<User>>($"{Api.apiUri}user");
                var user = users?.FirstOrDefault(u => u.UserName == model.Name);

                if (user != null)
                {
                    return StatusCodes.Status409Conflict;
                }

                user = new User 
                { 
                    UserName = model.Name,
                    Email = model.Email,
                    Password = HashPasswordHelper.HashPasword(model.Password)
                };

                await httpClient.PostAsJsonAsync<User>($"{Api.apiUri}user", user);
            }
            catch (Exception)
            {
                return StatusCodes.Status500InternalServerError;
            }
            return 0;
        }

        public Task<bool> ChangePassword(ChangePasswordViewModel model)
        {
            throw new NotImplementedException();
        }

        Task<BaseResponse<IEnumerable<User>>> IAccountService.Register(RegisterViewModel model)
        {
            throw new NotImplementedException();
        }

        Task<BaseResponse<bool>> IAccountService.Login(LoginViewModel model)
        {
            throw new NotImplementedException();
        }

        Task<BaseResponse<bool>> IAccountService.ChangePassword(ChangePasswordViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}

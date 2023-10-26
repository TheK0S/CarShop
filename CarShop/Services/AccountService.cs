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

                user = new User { UserName = model.Name, Email = model.Email, Password = model.Password };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<bool> ChangePassword(ChangePasswordViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}

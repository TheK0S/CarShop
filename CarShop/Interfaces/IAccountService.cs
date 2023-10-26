using CarShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarShop.Interfaces
{
    public interface IAccountService
    {
        Task<int> Register(RegisterViewModel model);

        Task<int> Login(LoginViewModel model);

        Task<bool> ChangePassword(ChangePasswordViewModel model);
    }
}

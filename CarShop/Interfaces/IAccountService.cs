using CarShop.Models;
using CarShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarShop.Interfaces
{
    public interface IAccountService
    {
        Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model);

        Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model);

        Task<BaseResponse<ClaimsIdentity>> ChangePassword(ChangePasswordViewModel model);
    }
}

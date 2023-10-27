using CarShop.Models;
using CarShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarShop.Interfaces
{
    public interface IAccountService
    {
        Task<BaseResponse<IEnumerable<User>>> Register(RegisterViewModel model);

        Task<BaseResponse<bool>> Login(LoginViewModel model);

        Task<BaseResponse<bool>> ChangePassword(ChangePasswordViewModel model);
    }
}

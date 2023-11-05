using CarShop.Helpers;
using CarShop.Interfaces;
using CarShop.Models;
using CarShop.ViewModels;
using System.Net;
using System.Security.Claims;

namespace CarShop.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserService _userService;
        private readonly IShopCartService _shopCartService;

        public AccountService(IUserService userService, IShopCartService shopCartService)
        {
            _userService = userService;
            _shopCartService = shopCartService;
        }

        public async Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model)
        {
            var users = await _userService.GetUsersAsync();
            var user = users.Data?.FirstOrDefault(u => u.UserName == model.Name);

            if (user == null)
            {
                return new BaseResponse<ClaimsIdentity>
                {
                    Message = "User not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            if (user.Password != HashPasswordHelper.HashPasword(model.Password))
            {
                return new BaseResponse<ClaimsIdentity>
                {
                    Message = "Incorrect password",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            var result = await Authenticate(user);

            return new BaseResponse<ClaimsIdentity>
            {
                Data = result,
                StatusCode = HttpStatusCode.OK
            };
        }

        public async Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model)
        {
            HttpClient httpClient = new HttpClient();
            try
            {
                var users = await httpClient.GetFromJsonAsync<IEnumerable<User>>($"{Api.apiUri}user");
                var user = users?.FirstOrDefault(u => u.UserName == model.Name);

                if (user != null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Message = "There is already a user with this login",
                        StatusCode = HttpStatusCode.InternalServerError
                    };
                }

                user = new User
                { 
                    UserName = model.Name,
                    Email = model.Email,
                    Password = HashPasswordHelper.HashPasword(model.Password),
                    RoleId = model.RoleId
                };

                var baseResponse = await _userService.CreateUserAsync(user);

                if (baseResponse.StatusCode != HttpStatusCode.Created || baseResponse.Data == null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Message = "User is not added",
                        StatusCode = HttpStatusCode.InternalServerError
                    };
                }

                User createdUser = baseResponse.Data;

                await _shopCartService.CreateShopCart(createdUser.Id);

                var result = await Authenticate(user);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    Message = "User is added",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Message = ex.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<bool>> ChangePassword(ChangePasswordViewModel model)
        {
            var users = await _userService.GetUsersAsync();
            var user = users.Data.FirstOrDefault(u => u.UserName == model.UserName);

            if (user == null)
            {
                return new BaseResponse<bool>()
                {
                    Data = false,
                    Message = "User not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            user.Password = HashPasswordHelper.HashPasword(model.NewPassword);

            var response = await _userService.UpdateUserAsync(user);

            if(response.StatusCode == HttpStatusCode.OK)
            {
                return new BaseResponse<bool>()
                {
                    Message = "Password updated",
                    StatusCode = HttpStatusCode.OK,
                };
            }

            return new BaseResponse<bool>()
            {
                Message = "Password is not updated",
                StatusCode = HttpStatusCode.NotFound,
            };
        }        

        private async Task<ClaimsIdentity> Authenticate(User user)
        {
            List<Role> roles;
            using (HttpClient httpClient = new HttpClient())
            {
                roles = await httpClient.GetFromJsonAsync<List<Role>>($"{Api.apiUri}roles") ?? new List<Role>();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, roles.FirstOrDefault(r => r.Id == user.RoleId).Name),
                new Claim("UserId", user.Id.ToString()),
            };
            return new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }
    }
}

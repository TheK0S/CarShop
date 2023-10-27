using CarShop.Models;

namespace CarShop.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponse<IEnumerable<User>>> GetUsersAsync();
        Task<BaseResponse<User>> GetUserAsync(int id);
        Task<BaseResponse<bool>> CreateUserAsync(User user);
        Task<BaseResponse<bool>> UpdateUserAsync(User user);
        Task<BaseResponse<bool>> DeleteUserAsync(int userId);
    }
}

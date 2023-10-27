using CarShop.Models;

namespace CarShop.Interfaces
{
    public interface ICarService
    {
        Task<BaseResponse<IEnumerable<Car>>> GetCarsAsync();
        Task<BaseResponse<Car>> GetCarAsync(int id);
        Task<BaseResponse<bool>> CreateCarAsync(Car car);
        Task<BaseResponse<bool>> UpdateCarAsync(Car car);
        Task<BaseResponse<bool>> DeleteCarAsync(int carId);
    }
}

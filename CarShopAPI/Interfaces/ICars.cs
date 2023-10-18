using CarShopAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarShopAPI.Interfaces
{
    public interface ICars
    {
        Task<IEnumerable<Car>> GetCars();
        Task<Car> GetCar(int id);
        Task<bool> PostCar(Car car);
        Task<bool> PutCar(int id, Car car);
        Task<bool> DeleteCar(int id);
    }
}

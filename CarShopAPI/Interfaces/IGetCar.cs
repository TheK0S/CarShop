using CarShopAPI.Models;

namespace CarShopAPI.Interfaces
{
    public interface IGetCar
    {
        IEnumerable<Car> GetCars { get; }
        IEnumerable<Car> GetFavCars { get; }
        Car GetCar(int carId);
    }
}

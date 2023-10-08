using CarShopAPI.Interfaces;
using CarShopAPI.Models;

namespace CarShopAPI.Mocks
{
    public class MockCar : IGetCar
    {
        private readonly IGetCategory _category = new MockCategory();
        public IEnumerable<Car> GetCars { get => new List<Car>(); }
        public IEnumerable<Car> GetFavCars { get => new List<Car>(); }

        public Car GetCar(int carId)
        {
            throw new NotImplementedException();
        }
    }
}

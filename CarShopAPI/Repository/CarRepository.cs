using CarShopAPI.Interfaces;
using CarShopAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarShopAPI.Repository
{
    public class CarRepository : ICars
    {
        private readonly AppDbContext _db;
        public CarRepository() { }        

        public async Task<Car> GetCar(int id)
        {
            if (_db.Car == null)
                throw new NullReferenceException(); 

            var car = await _db.Car.FindAsync(id);

            if (car == null)
                return new Car();

            return car;
        }

        public async Task<IEnumerable<Car>> GetCars()
        {
            if (_db.Car == null)
                throw new NullReferenceException();

            return await _db.Car.ToListAsync();
        }

        public async Task<bool> PostCar(Car car)
        {
            if (_db.Car == null)
                throw new NullReferenceException();

            _db.Car.Add(car);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> PutCar(int id, Car car)
        {
            if (id != car.Id)
                return false;

            _db.Entry(car).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
                    return false;
                else
                {
                    throw;
                }
            }

            return true;
        }
        public async Task<bool> DeleteCar(int id)
        {
            if (_db.Car == null)
                return false;

            var car = await _db.Car.FindAsync(id);
            if (car == null)
                return false;

            _db.Car.Remove(car);
            await _db.SaveChangesAsync();

            return true;
        }

        private bool CarExists(int id)
        {
            return (_db.Car?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

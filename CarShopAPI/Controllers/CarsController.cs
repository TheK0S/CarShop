using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarShopAPI.Models;

namespace CarShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public CarsController(AppDbContext context)
        {
            _db = context;
        }

        // GET: api/Cars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
          if (_db.Car == null)
          {
              return NotFound();
          }
            return await _db.Car.ToListAsync();
        }

        // GET: api/Cars/Favourite
        [HttpGet("Favourite")]
        public async Task<ActionResult<List<Car>>> GetFavouriteCars()
        {
            if (_db.Car == null)
            {
                return NotFound();
            }

            List<Car> cars = await _db.Car.ToListAsync();

            return cars.Where(c => c.IsFavourite == true).ToList() ?? new List<Car>();
        }

        // GET: api/Cars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
          if (_db.Car == null)
          {
              return NotFound();
          }
            var car = await _db.Car.FindAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }

        // POST: api/Cars
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
            if (_db.Car == null)
            {
                return Problem("Entity set 'CarShopDbContext.Car'  is null.");
            }

            await _db.Car.AddAsync(car);

            try
            {
                await _db.SaveChangesAsync();
                return StatusCode(201);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }            
        }

        // PUT: api/Cars/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id, Car car)
        {
            if (id != car.Id)
            {
                return BadRequest();
            }

            _db.Entry(car).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return StatusCode(500);
                }
            }

            return Ok();
        }        

        // DELETE: api/Cars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            if (_db.Car == null)
            {
                return NotFound();
            }
            var car = await _db.Car.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            _db.Car.Remove(car);

            try
            {
                await _db.SaveChangesAsync();
                return StatusCode(200);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        private bool CarExists(int id)
        {
            return (_db.Car?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

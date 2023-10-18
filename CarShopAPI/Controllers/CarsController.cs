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
        private readonly CarShopDbContext _db;

        public CarsController(CarShopDbContext context)
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

        //[HttpPost]
        //public async Task<ActionResult<Car>> PostCar([Bind("Name,ShortDesc,LongDesc,Title,Url,Price,IsFavourite,Count")] Car car)
        //{
        //    if (_db.Car == null)
        //    {
        //        return Problem("Entity set 'CarShopDbContext.User'  is null.");
        //    }

        //    _db.Car.Add(car);
        //    await _db.SaveChangesAsync();

        //    return CreatedAtAction("GetCar", new { id = car.Id }, car);
        //}

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
                    throw;
                }
            }

            return Ok();
        }

        // POST: api/Cars
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Car>> PostCar([FromBody] Car car)
        {
          if (_db.Car == null)
          {
              return Problem("Entity set 'CarShopDbContext.Car'  is null.");
          }

            //car.Category = await _db.Category.FindAsync(car.Category.Id);
            await _db.SaveChangesAsync();

            return CreatedAtAction("GetCar", new { id = car.Id }, car);
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
            await _db.SaveChangesAsync();

            return NoContent();
        }

        private bool CarExists(int id)
        {
            return (_db.Car?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

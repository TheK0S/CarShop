using CarShopAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CarShopDbContext _db;
        public CategoryController(CarShopDbContext context)
        {
            _db = context;
        }

        //GET: api/cars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            if(_db.Category == null) return NotFound();

            return await _db.Category.ToListAsync();
        }

        //GET: api/cars/id
        [HttpGet("id")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            if (_db.Category == null) return NotFound();

            var category = await _db.Category.FindAsync(id);

            if (category == null) return NotFound();

            return Ok(category);
        }

        //PUT: api/cars/id
        [HttpPut("id")]
        public async Task<IActionResult> PutCategory(int id, Car car)
        {
            if(id != car.Id) return BadRequest();

            _db.Entry(car).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }




        private bool CategoryExists(int id)
        {
            return (_db.Category?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

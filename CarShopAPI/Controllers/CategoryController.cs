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
        private readonly AppDbContext _db;
        public CategoryController(AppDbContext context)
        {
            _db = context;
        }

        //GET: api/category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            if (_db.Category == null) return NotFound();

            return await _db.Category.ToListAsync();
        }

        //GET: api/category/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            if (_db.Category == null) return NotFound();

            var category = await _db.Category.FindAsync(id);

            if (category == null) return NotFound();

            return category;
        }

        //PUT: api/category/id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            if (id != category.Id) return BadRequest();

            _db.Entry(category).State = EntityState.Modified;

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

        //POST: api/category
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            if (_db.Category == null)
            {
                return Problem("Entity set 'CarShopDbContext.Car'  is null.");
            }
            _db.Category.Add(category);
            await _db.SaveChangesAsync();

            return CreatedAtAction("GetCar", new { id = category.Id }, category);
        }

        // DELETE: api/category/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (_db.Category == null)
            {
                return NotFound();
            }
            var category = await _db.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _db.Category.Remove(category);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(int id)
        {
            return (_db.Category?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

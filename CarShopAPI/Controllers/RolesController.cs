using CarShopAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly AppDbContext _db;

        public RolesController(AppDbContext db)
        {
            _db = db;
        }


        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            if (_db.Role == null)
                return NotFound();

            return await _db.Role.ToListAsync();
        }

        // GET api/Roles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetRole(int id)
        {
            if (_db.Role == null)
                return NotFound();

            var role = await _db.Role.FindAsync(id);

            if (role == null)
                return NotFound();        

            return role;
        }

        // POST api/Roles
        [HttpPost]
        public async Task<ActionResult<Role>> PostRole([FromBody] Role role)
        {
            if (_db.Role == null)
                return Problem("Entity set 'CarShopDbContext.Roles'  is null.");

            await _db.Role.AddAsync(role);
            await _db.SaveChangesAsync();

            return CreatedAtAction("GetRole", new { id = role.Id }, role);
        }

        // PUT api/Roles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(int id, Role role)
        {
            if (id != role.Id)
            {
                return BadRequest();
            }

            _db.Entry(role).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleExists(id))
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

        // DELETE api/Roles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            if (_db.Role == null)
                return NotFound();

            var role = await _db.Role.FindAsync(id);
            if (role == null)
                return NotFound();

            _db.Role.Remove(role);

            try
            {
                await _db.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        private bool RoleExists(int id)
        {
            return (_db.Role?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

using CarShopAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _db;
        public OrdersController(AppDbContext db)
        {
            _db = db;
        }

        // GET: api/<OrdersController>
        [HttpGet]
        public ActionResult<List<Order>> GetOrders()
        {
            if (_db.Order == null)
                return NotFound();

            var orders = _db.Order.ToList();
            if (orders.Count == 0)
                return NotFound();

            return orders;
        }

        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            if (_db.Order == null)
                return NotFound();

            var order = await _db.Order.FindAsync(id);
            if (order == null)
                return NotFound();

            return order;
        }

        // POST api/<OrdersController>
        [HttpPost]
        public async Task<ActionResult> CreateOrder(Order order)
        {
            if(order == null)
                return BadRequest();

            await _db.Order.AddAsync(order);

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

        // PUT api/<OrdersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, Order order)
        {
            if (id != order.Id) return BadRequest();

            _db.Entry(order).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();

                return StatusCode(200);
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE api/<OrdersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (_db.Order == null)
                return NotFound();

            var order = await _db.Order.FindAsync(id);

            if (order == null)
                return NotFound();

            _db.Order.Remove(order);

            try
            {
                await _db.SaveChangesAsync();

                return StatusCode(200);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}

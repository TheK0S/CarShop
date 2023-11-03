using CarShopAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopCartController : ControllerBase
    {
        private readonly AppDbContext _db;
        public ShopCartController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet("{id}")]
        public ActionResult<ShopCart> GetCart(int cartId)
        {
            if(cartId == 0 || _db.ShopCart == null)
                return NotFound();

            var shopCart = _db.ShopCart.Where(c => c.Id == cartId).FirstOrDefault();

            if (shopCart == null)
                return NotFound();

            shopCart.Items = _db.ShopCartItem.Where(i => i.ShopCartId == cartId).ToList();

            return shopCart;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCart(ShopCart shopCart)
        {
            if (shopCart == null || _db.ShopCart == null)
                return NotFound();

            _db.ShopCart.Add(shopCart);

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

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCart(int id, ShopCart shopCart)
        {
            if (id != shopCart.Id) return BadRequest();

            _db.Entry(shopCart).State = EntityState.Modified;

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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            if (_db.ShopCart == null)
                return NotFound();

            var shopCart = await _db.ShopCart.FindAsync(id);

            if (shopCart == null)
                return NotFound();

            _db.ShopCart.Remove(shopCart);

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

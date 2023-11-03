using CarShopAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopCartItemsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public ShopCartItemsController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<ShopCartItem>>> GetCartItems(int shopCartId)
        {
            if (shopCartId == 0 || _db.ShopCartItem == null)
                return NotFound();

            var shopCartItems = _db.ShopCartItem.Where(i => i.ShopCartId == shopCartId);
            var listShopCartItems = await shopCartItems.ToListAsync();

            if(listShopCartItems.Count == 0)
                return NotFound();

            return listShopCartItems;
        }

        [HttpGet("{id}")]
        public ActionResult<ShopCartItem> GetCartItem(int itemId)
        {
            if (itemId == 0 || _db.ShopCartItem == null)
                return NotFound();

            var shopCartItem = _db.ShopCartItem.Where(c => c.Id == itemId).FirstOrDefault();

            if (shopCartItem == null)
                return NotFound();

            return shopCartItem;
        }
        //==================================================================
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

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

        [HttpGet]
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
        
        [HttpPost]
        public async Task<IActionResult> CreateCartItem(ShopCartItem shopCartItem)
        {
            if (shopCartItem == null || _db.ShopCartItem == null)
                return NotFound();

            _db.ShopCartItem.Add(shopCartItem);

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
        public async Task<IActionResult> PutCartItem(int id, ShopCartItem shopCartItem)
        {
            if (id != shopCartItem.Id) return BadRequest();

            _db.Entry(shopCartItem).State = EntityState.Modified;

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
        public async Task<IActionResult> DeleteCartItem(int id)
        {
            if (_db.ShopCartItem == null)
                return NotFound();

            var shopCartItem = await _db.ShopCartItem.FindAsync(id);

            if (shopCartItem == null)
                return NotFound();

            _db.ShopCartItem.Remove(shopCartItem);

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

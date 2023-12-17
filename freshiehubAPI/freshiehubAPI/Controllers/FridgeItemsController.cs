using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using freshiehubAPI.Models;
namespace freshie_webAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FridgeItemsController : ControllerBase
    {
        private readonly FreshieDbContext _context;

        public FridgeItemsController(FreshieDbContext context)
        {
            _context = context;
        }

        // GET: api/FridgeItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FridgeItem>>> GetFridgeItems()
        {
          if (_context.FridgeItems == null)
          {
              return NotFound();
          }
            return await _context.FridgeItems.ToListAsync();
        }

        // GET: api/FridgeItems/10000001
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<FridgeItem>>> GetFridgeItem(int userId)
        {
            if (_context.FridgeItems == null)
            {
                return NotFound();
            }
            var fridgeItems = await _context.FridgeItems.Where(u => u.UserId == userId).ToListAsync();

            if (fridgeItems == null || !fridgeItems.Any())
            {
                return NotFound("User doesn't have any items.");
            }

            return fridgeItems;
        }
        // GET: api/FridgeItems/products/10000001
        [HttpGet("products/{userId}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetFridgeProducts(int userId)
        {
            if (_context.FridgeItems == null)
            {
                return NotFound();
            }
            var fridgeItems = await _context.FridgeItems.Where(u => u.UserId == userId).ToListAsync();

            if (fridgeItems == null || !fridgeItems.Any())
            {
                return NotFound("User doesn't have any items.");
            }

            var userProducts = new List<Product>();
            foreach (var item in fridgeItems)
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                if (product != null)
                {
                    userProducts.Add(product);
                }
            }

            return userProducts;
        }
        // PUT: api/FridgeItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutFridgeItem(FridgeItem fridgeItem)
        {
            _context.Entry(fridgeItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FridgeItemExists(fridgeItem.Id))
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

        // POST: api/FridgeItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FridgeItem>> AddProduct([FromBody] AddProductModel model)
        {
            if (_context.FridgeItems == null)
            {
                return Problem("Entity set 'FreshieDbContext.FridgeItems'  is null.");
            }

            FridgeItem item = new FridgeItem { ProductId = model.Product.ProductId, UserId = model.UserId, ExpirationDate = model.ExpirationDate };
            _context.FridgeItems.Add(item);
            await _context.SaveChangesAsync();

            return Ok("The item was added successfully.");
        }

        // DELETE: api/FridgeItems/5
        [HttpDelete]
        public async Task<IActionResult> DeleteFridgeItem([FromBody] DeleteProductModel model)
        {
            if (_context.FridgeItems == null)
            {
                return NotFound();
            }

            var fridgeItem = await _context.FridgeItems.FirstOrDefaultAsync(u => u.UserId == model.UserId && u.ProductId == model.Product.ProductId);

            if (fridgeItem == null)
            {
                return NotFound();
            }

            _context.FridgeItems.Remove(fridgeItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }



        private bool FridgeItemExists(int id)
        {
            return (_context.FridgeItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

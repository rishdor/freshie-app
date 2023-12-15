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
    public class GroceriesListsController : ControllerBase
    {
        private readonly FreshieDbContext _context;

        public GroceriesListsController(FreshieDbContext context)
        {
            _context = context;
        }

        // GET: api/GroceriesLists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroceriesList>>> GetGroceriesLists()
        {
          if (_context.GroceriesLists == null)
          {
              return NotFound();
          }
            return await _context.GroceriesLists.ToListAsync();
        }

        // GET: api/GroceriesLists/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<GroceriesList>> GetGroceriesList(int id)
        //{
        //  if (_context.GroceriesLists == null)
        //  {
        //      return NotFound();
        //  }
        //    var groceriesList = await _context.GroceriesLists.FindAsync(id);

        //    if (groceriesList == null)
        //    {
        //        return NotFound();
        //    }

        //    return groceriesList;
        //}

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetGroceries(int id)
        {
            var items = await _context.GroceriesLists.Where(u => u.UserId == id).ToListAsync();

            if (items == null || !items.Any())
            {
                return NotFound("User doesn't have any groceries.");
            }

            var products = new List<Product>();
            foreach (var item in items)
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                if (product != null)
                {
                    products.Add(product);
                }
            }

            return products;
        }

        // PUT: api/GroceriesLists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutGroceriesList(int id, GroceriesList groceriesList)
        //{
        //    if (id != groceriesList.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(groceriesList).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!GroceriesListExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/GroceriesLists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> AddGroceriesItem([FromBody] AddGroceriesModel model)
        {
            if (_context.FridgeItems == null)
            {
                return Problem("Entity set 'FreshieDbContext.FridgeItems'  is null.");
            }
            var existingItem = await _context.GroceriesLists.FirstOrDefaultAsync(i => i.UserId == model.UserId && i.ProductId == model.Product.ProductId);
            if (existingItem != null)
            {
                return BadRequest("The product already exists in the groceries list.");
            }
            GroceriesList item = new GroceriesList { ProductId = model.Product.ProductId, UserId = model.UserId };
            _context.GroceriesLists.Add(item);
            await _context.SaveChangesAsync();

            return Ok("The groceries item was added successfully.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteGroceriesItem([FromBody] DeleteGroceriesModel model)
        {
            if (_context.FridgeItems == null)
            {
                return NotFound();
            }

            var groceriesItem = await _context.GroceriesLists.FirstOrDefaultAsync(u => u.UserId == model.UserId && u.ProductId == model.Product.ProductId);

            if (groceriesItem == null)
            {
                return NotFound();
            }

            _context.GroceriesLists.Remove(groceriesItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GroceriesListExists(int id)
        {
            return (_context.GroceriesLists?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

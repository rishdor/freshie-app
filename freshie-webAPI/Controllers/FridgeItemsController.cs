using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using freshie_webAPI.Models;

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

        // GET: api/FridgeItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FridgeItem>> GetFridgeItem(int id)
        {
          if (_context.FridgeItems == null)
          {
              return NotFound();
          }
            var fridgeItem = await _context.FridgeItems.FindAsync(id);

            if (fridgeItem == null)
            {
                return NotFound();
            }

            return fridgeItem;
        }

        // PUT: api/FridgeItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFridgeItem(int id, FridgeItem fridgeItem)
        {
            if (id != fridgeItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(fridgeItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FridgeItemExists(id))
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
        public async Task<ActionResult<FridgeItem>> PostFridgeItem(FridgeItem fridgeItem)
        {
          if (_context.FridgeItems == null)
          {
              return Problem("Entity set 'FreshieDbContext.FridgeItems'  is null.");
          }
            _context.FridgeItems.Add(fridgeItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFridgeItem", new { id = fridgeItem.Id }, fridgeItem);
        }

        // DELETE: api/FridgeItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFridgeItem(int id)
        {
            if (_context.FridgeItems == null)
            {
                return NotFound();
            }
            var fridgeItem = await _context.FridgeItems.FindAsync(id);
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

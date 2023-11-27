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
    public class DietsController : ControllerBase
    {
        private readonly FreshieDbContext _context;

        public DietsController(FreshieDbContext context)
        {
            _context = context;
        }

        // GET: api/Diets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Diet>>> GetDiets()
        {
          if (_context.Diets == null)
          {
              return NotFound();
          }
            return await _context.Diets.ToListAsync();
        }

        // GET: api/Diets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Diet>> GetDiet(int id)
        {
          if (_context.Diets == null)
          {
              return NotFound();
          }
            var diet = await _context.Diets.FindAsync(id);

            if (diet == null)
            {
                return NotFound();
            }

            return diet;
        }

        // PUT: api/Diets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDiet(int id, Diet diet)
        {
            if (id != diet.DietId)
            {
                return BadRequest();
            }

            _context.Entry(diet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DietExists(id))
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

        // POST: api/Diets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Diet>> PostDiet(Diet diet)
        {
          if (_context.Diets == null)
          {
              return Problem("Entity set 'FreshieDbContext.Diets'  is null.");
          }
            _context.Diets.Add(diet);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDiet", new { id = diet.DietId }, diet);
        }

        // DELETE: api/Diets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiet(int id)
        {
            if (_context.Diets == null)
            {
                return NotFound();
            }
            var diet = await _context.Diets.FindAsync(id);
            if (diet == null)
            {
                return NotFound();
            }

            _context.Diets.Remove(diet);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DietExists(int id)
        {
            return (_context.Diets?.Any(e => e.DietId == id)).GetValueOrDefault();
        }
    }
}

using freshie_webAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace freshie_webAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuisinesController : ControllerBase
    {
        private readonly FreshieDbContext _context;

        public CuisinesController(FreshieDbContext context)
        {
            _context = context;
        }
        // GET: api/<CuisinesController>
        [HttpGet]

        public async Task<ActionResult<IEnumerable<Cuisine>>> GetCuisines()
        {
            if (_context.Cuisines == null)
            {
                return NotFound();
            }
            return await _context.Cuisines.ToListAsync();
        }

         // GET: api/Cuisines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cuisine>> GetCuisine(int id)
        {
          if (_context.Cuisines == null)
          {
              return NotFound();
          }
            var cuisine = await _context.Cuisines.FindAsync(id);

            if (cuisine == null)
            {
                return NotFound();
            }

            return cuisine;
        }

        // PUT: api/Cuisines/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCuisine(int id, Cuisine cuisine)
        {
            if (id != cuisine.CuisineId)
            {
                return BadRequest();
            }

            _context.Entry(cuisine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CuisineExists(id))
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

        // POST: api/Cuisines
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cuisine>> PostCuisine(Cuisine cuisine)
        {
            cuisine.CuisineId = 0;
          if (_context.Cuisines == null)
          {
              return Problem("Entity set 'FreshieDbContext.Cuisines'  is null.");
          }
            _context.Cuisines.Add(cuisine);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCuisine", new { id = cuisine.CuisineId }, cuisine);
        }

        // DELETE: api/Cuisines/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCuisine(int id)
        {
            if (_context.Cuisines == null)
            {
                return NotFound();
            }
            var cuisine = await _context.Cuisines.FindAsync(id);
            if (cuisine == null)
            {
                return NotFound();
            }

            _context.Cuisines.Remove(cuisine);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CuisineExists(int id)
        {
            return (_context.Cuisines?.Any(e => e.CuisineId == id)).GetValueOrDefault();
        }
    }
}

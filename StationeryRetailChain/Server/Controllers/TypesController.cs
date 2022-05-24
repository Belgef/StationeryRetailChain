using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StationeryRetailChain.Server.Data;
using StationeryRetailChain.Shared.Models;
using Type = StationeryRetailChain.Shared.Models.Type;

namespace StationeryRetailChain.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypesController : ControllerBase
    {
        private readonly StationeryRetailChainContext _context;

        public TypesController(StationeryRetailChainContext context)
        {
            _context = context;
        }

        // GET: api/Types
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Type>>> GetTypes()
        {
          if (_context.Types == null)
          {
              return NotFound();
          }
            return await _context.Types.Include(t=>t.Subcategory).ThenInclude(s=>s.Category).ToListAsync();
        }

        // GET: api/Types/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Type>> GetType(int id)
        {
          if (_context.Types == null)
          {
              return NotFound();
          }
            var @type = await _context.Types.Include(t => t.Subcategory).ThenInclude(s => s.Category).FirstOrDefaultAsync(t=>t.TypeId==id);

            if (@type == null)
            {
                return NotFound();
            }

            return @type;
        }

        // PUT: api/Types/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutType(int id, Type @type)
        {
            if (id != @type.TypeId)
            {
                return BadRequest();
            }

            _context.Entry(@type).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeExists(id))
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

        // POST: api/Types
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Type>> PostType(Type @type)
        {
          if (_context.Types == null)
          {
              return Problem("Entity set 'StationeryRetailChainContext.Types'  is null.");
          }
            _context.Types.Add(@type);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetType", new { id = @type.TypeId }, @type);
        }

        // DELETE: api/Types/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteType(int id)
        {
            if (_context.Types == null)
            {
                return NotFound();
            }
            var @type = await _context.Types.FindAsync(id);
            if (@type == null)
            {
                return NotFound();
            }

            _context.Types.Remove(@type);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TypeExists(int id)
        {
            return (_context.Types?.Any(e => e.TypeId == id)).GetValueOrDefault();
        }
    }
}

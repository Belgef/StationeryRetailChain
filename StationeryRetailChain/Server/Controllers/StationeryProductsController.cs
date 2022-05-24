using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StationeryRetailChain.Server.Data;
using StationeryRetailChain.Shared.Models;

namespace StationeryRetailChain.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationeryProductsController : ControllerBase
    {
        private readonly StationeryRetailChainContext _context;

        public StationeryProductsController(StationeryRetailChainContext context)
        {
            _context = context;
        }

        // GET: api/StationeryProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StationeryProduct>>> GetStationeryProducts()
        {
            if (_context.StationeryProducts == null)
            {
                return NotFound();
            }
            return await _context.StationeryProducts.ToListAsync();
        }

        // GET: api/StationeryProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StationeryProduct>> GetStationeryProduct(int id)
        {
            if (_context.StationeryProducts == null)
            {
                return NotFound();
            }
            var stationeryProduct = await _context.StationeryProducts.Include(s => s.Manufacturer).Include(s => s.Color).
                Include(s => s.Type).ThenInclude(t => t.Subcategory).ThenInclude(s => s.Category).FirstOrDefaultAsync(s => s.StationeryProductId == id);

            if (stationeryProduct == null)
            {
                return NotFound();
            }

            return stationeryProduct;
        }

        // PUT: api/StationeryProducts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStationeryProduct(int id, StationeryProduct stationeryProduct)
        {
            if (id != stationeryProduct.StationeryProductId)
            {
                return BadRequest();
            }

            _context.Entry(stationeryProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StationeryProductExists(id))
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

        // POST: api/StationeryProducts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StationeryProduct>> PostStationeryProduct(StationeryProduct stationeryProduct)
        {
            if (_context.StationeryProducts == null)
            {
                return Problem("Entity set 'StationeryRetailChainContext.StationeryProducts'  is null.");
            }
            _context.StationeryProducts.Add(stationeryProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStationeryProduct", new { id = stationeryProduct.StationeryProductId }, stationeryProduct);
        }

        // DELETE: api/StationeryProducts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStationeryProduct(int id)
        {
            if (_context.StationeryProducts == null)
            {
                return NotFound();
            }
            if (_context.StockProducts.Any(sp => sp.StationeryProductId == id) || _context.ShipmentSupplies.Any(ss => ss.StationeryProductId == id))
            {
                return Problem("Product is used and cannot be deleted.");
            }
            var stationeryProduct = await _context.StationeryProducts.FindAsync(id);
            if (stationeryProduct == null)
            {
                return NotFound();
            }

            _context.StationeryProducts.Remove(stationeryProduct);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StationeryProductExists(int id)
        {
            return (_context.StationeryProducts?.Any(e => e.StationeryProductId == id)).GetValueOrDefault();
        }
    }
}

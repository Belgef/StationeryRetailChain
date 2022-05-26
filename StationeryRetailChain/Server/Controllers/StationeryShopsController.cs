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
    public class StationeryShopsController : ControllerBase
    {
        private readonly StationeryRetailChainContext _context;

        public StationeryShopsController(StationeryRetailChainContext context)
        {
            _context = context;
        }

        // GET: api/StationeryShops
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StationeryShop>>> GetStationeryShops()
        {
          if (_context.StationeryShops == null)
          {
              return NotFound();
          }
            return await _context.StationeryShops.Include(e=>e.City).ThenInclude(e=>e.State).ThenInclude(e=>e.Country).ToListAsync();
        }

        // GET: api/StationeryShops/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StationeryShop>> GetStationeryShop(int? id)
        {
          if (_context.StationeryShops == null)
          {
              return NotFound();
          }
            var stationeryShop = await _context.StationeryShops.FindAsync(id);

            if (stationeryShop == null)
            {
                return NotFound();
            }

            return stationeryShop;
        }

        // PUT: api/StationeryShops/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStationeryShop(int? id, StationeryShop stationeryShop)
        {
            if (id != stationeryShop.ShopId)
            {
                return BadRequest();
            }

            _context.Entry(stationeryShop).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StationeryShopExists(id))
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

        // POST: api/StationeryShops
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StationeryShop>> PostStationeryShop(StationeryShop stationeryShop)
        {
          if (_context.StationeryShops == null)
          {
              return Problem("Entity set 'StationeryRetailChainContext.StationeryShops'  is null.");
          }
            _context.StationeryShops.Add(stationeryShop);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStationeryShop", new { id = stationeryShop.ShopId }, stationeryShop);
        }

        // DELETE: api/StationeryShops/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStationeryShop(int? id)
        {
            if (_context.StationeryShops == null)
            {
                return NotFound();
            }
            var stationeryShop = await _context.StationeryShops.FindAsync(id);
            if (stationeryShop == null)
            {
                return NotFound();
            }

            _context.StationeryShops.Remove(stationeryShop);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StationeryShopExists(int? id)
        {
            return (_context.StationeryShops?.Any(e => e.ShopId == id)).GetValueOrDefault();
        }
    }
}

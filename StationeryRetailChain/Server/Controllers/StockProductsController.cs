﻿using System;
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
    public class StockProductsController : ControllerBase
    {
        private readonly StationeryRetailChainContext _context;

        public StockProductsController(StationeryRetailChainContext context)
        {
            _context = context;
        }

        // GET: api/StockProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockProduct>>> GetStockProducts(int shopId)
        {
            if (_context.StockProducts == null)
            {
                return NotFound();
            }
            if (shopId != 0)
            {
                if (!_context.StationeryShops.Any(e=>e.ShopId==shopId))
                    return NotFound();
                return await _context.StockProducts.Where(e => e.ShopId == shopId)
                .Include(e => e.StationeryProduct).ToListAsync();
            }
            else
                return await _context.StockProducts.Include(e => e.StationeryProduct).ToListAsync();
        }

        // GET: api/StockProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StockProduct>> GetStockProduct(int id)
        {
          if (_context.StockProducts == null)
          {
              return NotFound();
          }
            var stockProduct = await _context.StockProducts.FindAsync(id);

            if (stockProduct == null)
            {
                return NotFound();
            }

            return stockProduct;
        }

        // PUT: api/StockProducts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStockProduct(int id, StockProduct stockProduct)
        {
            if (id != stockProduct.StockProductId)
            {
                return BadRequest();
            }

            _context.Entry(stockProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockProductExists(id))
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

        // POST: api/StockProducts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StockProduct>> PostStockProduct(StockProduct stockProduct)
        {
          if (_context.StockProducts == null)
          {
              return Problem("Entity set 'StationeryRetailChainContext.StockProducts'  is null.");
          }
            _context.StockProducts.Add(stockProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStockProduct", new { id = stockProduct.StockProductId }, stockProduct);
        }

        // DELETE: api/StockProducts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStockProduct(int id)
        {
            if (_context.StockProducts == null)
            {
                return NotFound();
            }
            var stockProduct = await _context.StockProducts.FindAsync(id);
            if (stockProduct == null)
            {
                return NotFound();
            }

            _context.StockProducts.Remove(stockProduct);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StockProductExists(int id)
        {
            return (_context.StockProducts?.Any(e => e.StockProductId == id)).GetValueOrDefault();
        }
    }
}

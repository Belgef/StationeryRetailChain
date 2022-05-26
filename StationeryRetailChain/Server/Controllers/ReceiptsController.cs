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
    public class ReceiptsController : ControllerBase
    {
        private readonly StationeryRetailChainContext _context;

        public ReceiptsController(StationeryRetailChainContext context)
        {
            _context = context;
        }

        // GET: api/Receipts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Receipt>>> GetReceipts(int employeeId)
        {
            if (_context.Receipts == null)
            {
                return NotFound();
            }
            if (employeeId != 0) {
                Employee? emp = _context.Employees.Where(x => x.EmployeeId == employeeId).Include(e => e.Job).FirstOrDefault();
                if(emp == null)
                    return NotFound();
                if (!emp.Job.JobName.ToLower().Contains("seller"))
                    return Forbid();
                return await _context.Receipts.Where(e => e.SellerId == employeeId)
                .Include(e => e.Customer).Include(e => e.Items).ThenInclude(e => e.StockProduct)
                .ThenInclude(e => e.StationeryProduct).Include(e=>e.Seller).ThenInclude(e=>e.Shop)
                .ThenInclude(e=>e.City).ThenInclude(e=>e.State).ThenInclude(e=>e.Country).ToListAsync();
            }
            else
                return await _context.Receipts.Include(e => e.Customer).ToListAsync();
        }

        // GET: api/Receipts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Receipt>> GetReceipt(int id)
        {
          if (_context.Receipts == null)
          {
              return NotFound();
          }
            var receipt = await _context.Receipts.FirstOrDefaultAsync(e => e.ReceiptId == id);

            if (receipt == null)
            {
                return NotFound();
            }

            return receipt;
        }

        // PUT: api/Receipts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReceipt(int id, Receipt receipt)
        {
            if (id != receipt.ReceiptId)
            {
                return BadRequest();
            }
            _context.Entry(receipt).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReceiptExists(id))
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

        // POST: api/Receipts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Receipt>> PostReceipt(Receipt receipt)
        {
            if (_context.Receipts == null)
            {
                return Problem("Entity set 'StationeryRetailChainContext.Receipts'  is null.");
            }
            _context.Database.BeginTransaction();
            await _context.StationerySales.ForEachAsync(e => _context.Entry(e).State = EntityState.Detached);
            Receipt tempReceipt = new Receipt() { CustomerId = receipt.CustomerId, SellerId = receipt.SellerId, PurchaseDate = receipt.PurchaseDate };
            _context.Receipts.Add(tempReceipt);
            await _context.SaveChangesAsync();
            await _context.StationerySales.ForEachAsync(e => _context.Entry(e).State = EntityState.Unchanged);
            try { 
            foreach (var item in receipt.Items)
                _context.Database.ExecuteSqlRaw("EXEC PerformPurchase {0}, {1}, {2}", tempReceipt.ReceiptId, item.StockProductId, item.SellQuantity);
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
            }
            _context.Database.CommitTransaction();
            return CreatedAtAction("GetReceipt", new { id = tempReceipt.ReceiptId }, tempReceipt);
        }

        // DELETE: api/Receipts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReceipt(int id)
        {
            if (_context.Receipts == null)
            {
                return NotFound();
            }
            var receipt = await _context.Receipts.FindAsync(id);
            if (receipt == null)
            {
                return NotFound();
            }

            _context.Receipts.Remove(receipt);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReceiptExists(int id)
        {
            return (_context.Receipts?.Any(e => e.ReceiptId == id)).GetValueOrDefault();
        }
    }
}

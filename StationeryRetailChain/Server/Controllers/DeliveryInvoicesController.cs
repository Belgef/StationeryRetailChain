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
    public class DeliveryInvoicesController : ControllerBase
    {
        private readonly StationeryRetailChainContext _context;

        public DeliveryInvoicesController(StationeryRetailChainContext context)
        {
            _context = context;
        }

        // GET: api/DeliveryInvoices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeliveryInvoice>>> GetDeliveryInvoices(int employeeId)
        {
            if (_context.DeliveryInvoices == null)
            {
                return NotFound();
            }
            if (employeeId != 0)
            {
                Employee? emp = _context.Employees.Where(x => x.EmployeeId == employeeId).Include(e => e.Job).FirstOrDefault();
                if (emp == null)
                    return NotFound();
                return await _context.DeliveryInvoices.Where(e => e.AuthorId == employeeId)
                .Include(e => e.Shop).Include(e => e.StationerySupplies).ThenInclude(e=>e.StockProduct).ThenInclude(e => e.StationeryProduct)
                .Include(e=>e.StationerySupplies).ThenInclude(e=>e.ShipmentSupply).Include(e=>e.ShipmentInvoice)
                .Include(e => e.Author).ThenInclude(e => e.Shop)
                .ThenInclude(e => e.City).ThenInclude(e => e.State).ThenInclude(e => e.Country).ToListAsync();
            }
            else
                return await _context.DeliveryInvoices
                .Include(e => e.Shop).Include(e => e.StationerySupplies).ThenInclude(e => e.StockProduct).ThenInclude(e => e.StationeryProduct)
                .Include(e=>e.StationerySupplies).ThenInclude(e=>e.ShipmentSupply)
                .Include(e => e.ShipmentInvoice).ThenInclude(e => e.ShipmentSupplies).ThenInclude(e=>e.StationeryProduct)
                .Include(e => e.Author).ThenInclude(e => e.Shop)
                .ThenInclude(e => e.City).ThenInclude(e => e.State).ThenInclude(e => e.Country).ToListAsync();
        }

        // GET: api/DeliveryInvoices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DeliveryInvoice>> GetDeliveryInvoice(int id)
        {
          if (_context.DeliveryInvoices == null)
          {
              return NotFound();
          }
            var deliveryInvoice = await _context.DeliveryInvoices.FindAsync(id);

            if (deliveryInvoice == null)
            {
                return NotFound();
            }

            return deliveryInvoice;
        }

        // PUT: api/DeliveryInvoices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeliveryInvoice(int id, DeliveryInvoice deliveryInvoice)
        {
            if (id != deliveryInvoice.DeliveryInvoiceId)
            {
                return BadRequest();
            }

            _context.Entry(deliveryInvoice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeliveryInvoiceExists(id))
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

        // POST: api/DeliveryInvoices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DeliveryInvoice>> PostDeliveryInvoice(DeliveryInvoice deliveryInvoice)
        {
            if (_context.DeliveryInvoices == null)
            {
                return Problem("Entity set 'StationeryRetailChainContext.DeliveryInvoices'  is null.");
            }

            DeliveryInvoice temp = new DeliveryInvoice() { AuthorId=deliveryInvoice.AuthorId, CreationDate=deliveryInvoice.CreationDate, 
                ShipmentInvoiceId=deliveryInvoice.ShipmentInvoiceId, ShopId=deliveryInvoice.ShopId };
            _context.DeliveryInvoices.Add(temp);
            await _context.SaveChangesAsync();
            await _context.StationerySales.ForEachAsync(e => _context.Entry(e).State = EntityState.Unchanged);

            foreach (var item in deliveryInvoice.StationerySupplies)
                _context.Database.ExecuteSqlRaw("EXEC PerformSupply {0}, {1}, {2}, {3}", temp.DeliveryInvoiceId, item.StockProductId, item.ShipmentSupplyId, item.Quantity);

            return CreatedAtAction("GetDeliveryInvoice", new { id = temp.DeliveryInvoiceId }, deliveryInvoice);
        }

        // DELETE: api/DeliveryInvoices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeliveryInvoice(int id)
        {
            if (_context.DeliveryInvoices == null)
            {
                return NotFound();
            }
            var deliveryInvoice = await _context.DeliveryInvoices.FindAsync(id);
            if (deliveryInvoice == null)
            {
                return NotFound();
            }

            _context.DeliveryInvoices.Remove(deliveryInvoice);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DeliveryInvoiceExists(int id)
        {
            return (_context.DeliveryInvoices?.Any(e => e.DeliveryInvoiceId == id)).GetValueOrDefault();
        }
    }
}

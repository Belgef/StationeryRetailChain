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
    public class ShipmentInvoicesController : ControllerBase
    {
        private readonly StationeryRetailChainContext _context;

        public ShipmentInvoicesController(StationeryRetailChainContext context)
        {
            _context = context;
        }

        // GET: api/ShipmentInvoices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShipmentInvoice>>> GetShipmentInvoices(int employeeId)
        {
            if (_context.ShipmentInvoices == null)
            {
                return NotFound();
            }
            if (employeeId != 0)
            {
                Employee? emp = _context.Employees.Where(x => x.EmployeeId == employeeId).Include(e => e.Job).FirstOrDefault();
                if (emp == null)
                    return NotFound();
                if (!emp.Job.JobName.ToLower().Contains("supply"))
                    return Forbid();
                return await _context.ShipmentInvoices.Where(e => e.AuthorId == employeeId)
                .Include(e => e.Supplier).Include(e => e.ShipmentSupplies).ThenInclude(e => e.StationeryProduct)
                .Include(e => e.Author).ThenInclude(e => e.Shop)
                .ThenInclude(e => e.City).ThenInclude(e => e.State).ThenInclude(e => e.Country).ToListAsync();
            }
            else
                return await _context.ShipmentInvoices.Include(e => e.Supplier).Include(e=>e.ShipmentSupplies)
                    .ThenInclude(e=>e.StationeryProduct).Include(e => e.Author).ToListAsync();
        }

        // GET: api/ShipmentInvoices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShipmentInvoice>> GetShipmentInvoice(int id)
        {
            if (_context.ShipmentInvoices == null)
            {
                return NotFound();
            }
            var shipmentInvoice = await _context.ShipmentInvoices.FindAsync(id);

            if (shipmentInvoice == null)
            {
                return NotFound();
            }

            return shipmentInvoice;
        }

        // PUT: api/ShipmentInvoices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShipmentInvoice(int id, ShipmentInvoice shipmentInvoice)
        {
            if (id != shipmentInvoice.ShipmentInvoiceId)
            {
                return BadRequest();
            }

            _context.Entry(shipmentInvoice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShipmentInvoiceExists(id))
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

        // POST: api/ShipmentInvoices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ShipmentInvoice>> PostShipmentInvoice(ShipmentInvoice shipmentInvoice)
        {
            if (_context.ShipmentInvoices == null)
            {
                return Problem("Entity set 'StationeryRetailChainContext.ShipmentInvoices'  is null.");
            }
            ShipmentInvoice tempShipmentInvoice = new() { SupplierId = shipmentInvoice.SupplierId, AuthorId = shipmentInvoice.AuthorId, CreationDate = shipmentInvoice.CreationDate, TotalSum = shipmentInvoice.ShipmentSupplies.Sum(e => e.Quantity * e.Cost) };
            _context.ShipmentInvoices.Add(tempShipmentInvoice);
            await _context.SaveChangesAsync();

            foreach (var item in shipmentInvoice.ShipmentSupplies)
            {
                item.ShipmentInvoiceId = tempShipmentInvoice.ShipmentInvoiceId;
                _context.ShipmentSupplies.Add(item);
            }
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetShipmentInvoice", new { id = tempShipmentInvoice.ShipmentInvoiceId }, shipmentInvoice);
        }

        // DELETE: api/ShipmentInvoices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShipmentInvoice(int id)
        {
            if (_context.ShipmentInvoices == null)
            {
                return NotFound();
            }
            var shipmentInvoice = await _context.ShipmentInvoices.FindAsync(id);
            if (shipmentInvoice == null)
            {
                return NotFound();
            }

            _context.ShipmentInvoices.Remove(shipmentInvoice);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShipmentInvoiceExists(int id)
        {
            return (_context.ShipmentInvoices?.Any(e => e.ShipmentInvoiceId == id)).GetValueOrDefault();
        }
    }
}

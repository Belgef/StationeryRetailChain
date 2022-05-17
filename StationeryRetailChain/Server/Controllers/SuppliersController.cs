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
    public class SuppliersController : ControllerBase
    {
        private readonly StationeryRetailChainContext _context;

        public SuppliersController(StationeryRetailChainContext context)
        {
            _context = context;
        }

        // GET: api/Suppliers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierCompany>>> GetSupplierCompanies()
        {
          if (_context.SupplierCompanies == null)
          {
              return NotFound();
          }
            return await _context.SupplierCompanies.ToListAsync();
        }

        // GET: api/Suppliers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SupplierCompany>> GetSupplierCompany(int id)
        {
          if (_context.SupplierCompanies == null)
          {
              return NotFound();
          }
            var supplierCompany = await _context.SupplierCompanies.FindAsync(id);

            if (supplierCompany == null)
            {
                return NotFound();
            }

            return supplierCompany;
        }

        // PUT: api/Suppliers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSupplierCompany(int id, SupplierCompany supplierCompany)
        {
            if (id != supplierCompany.SupplierId)
            {
                return BadRequest();
            }

            _context.Entry(supplierCompany).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupplierCompanyExists(id))
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

        // POST: api/Suppliers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SupplierCompany>> PostSupplierCompany(SupplierCompany supplierCompany)
        {
          if (_context.SupplierCompanies == null)
          {
              return Problem("Entity set 'StationeryRetailChainContext.SupplierCompanies'  is null.");
          }
            _context.SupplierCompanies.Add(supplierCompany);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSupplierCompany", new { id = supplierCompany.SupplierId }, supplierCompany);
        }

        // DELETE: api/Suppliers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplierCompany(int id)
        {
            if (_context.SupplierCompanies == null)
            {
                return NotFound();
            }
            var supplierCompany = await _context.SupplierCompanies.FindAsync(id);
            if (supplierCompany == null)
            {
                return NotFound();
            }

            _context.SupplierCompanies.Remove(supplierCompany);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SupplierCompanyExists(int id)
        {
            return (_context.SupplierCompanies?.Any(e => e.SupplierId == id)).GetValueOrDefault();
        }
    }
}

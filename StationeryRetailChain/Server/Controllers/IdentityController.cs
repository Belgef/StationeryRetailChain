using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StationeryRetailChain.Server.Models;
using StationeryRetailChain.Shared.Models;

namespace StationeryRetailChain.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            if (_userManager.Users == null)
            {
                return NotFound();
            }
            return await _userManager.Users.Select(u=>new User() { UserId=u.Id, Email=u.Email, UserName=u.UserName.Replace('_',' ')}).ToListAsync();
        }

        //[HttpGet("{id}")]
        //public async Task<ActionResult<Category>> GetCategory(int? id)
        //{
        //    if (_context.Categories == null)
        //    {
        //        return NotFound();
        //    }
        //    var category = await _context.Categories.FindAsync(id);

        //    if (category == null)
        //    {
        //        return NotFound();
        //    }

        //    return category;
        //}

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutCategory(int? id, Category category)
        //{
        //    if (id != category.CategoryId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(category).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CategoryExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Category>> PostCategory(Category category)
        //{
        //    if (_context.Categories == null)
        //    {
        //        return Problem("Entity set 'StationeryRetailChainContext.Categories'  is null.");
        //    }
        //    _context.Categories.Add(category);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetCategory", new { id = category.CategoryId }, category);
        //}

        // DELETE: api/Categories/5
        [HttpDelete("{name}")]
        public async Task<IActionResult> DeleteUser(string? name)
        {
            if (_userManager.Users == null)
            {
                return NotFound();
            }
            var user = await _userManager.Users.FirstOrDefaultAsync(u=>u.UserName.ToLower().Replace('_',' ') == name.ToLower());
            if (user == null)
            {
                return NotFound();
            }

            await _userManager.DeleteAsync(user);

            return NoContent();
        }

        //private bool CategoryExists(int? id)
        //{
        //    return (_context.Categories?.Any(e => e.CategoryId == id)).GetValueOrDefault();
        //}
    }
}

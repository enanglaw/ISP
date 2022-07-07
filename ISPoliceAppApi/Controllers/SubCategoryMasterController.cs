using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ISPoliceAppApi.Data;
using ISPoliceAppApi.Models;

namespace ISPoliceAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryMasterController : ControllerBase
    {
        private readonly ISPoliceAppApiDbContext _context;

        public SubCategoryMasterController(ISPoliceAppApiDbContext context)
        {
            _context = context;
        }

        // GET: api/SubCategoryMaster
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubCategoryMaster>>> GetSubCategoryMaster()
        {
            return await _context.SubCategoryMaster.ToListAsync();
        }

        // GET: api/SubCategoryMaster/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubCategoryMaster>> GetSubCategoryMaster(int id)
        {
            var subCategoryMaster = await _context.SubCategoryMaster.FindAsync(id);

            if (subCategoryMaster == null)
            {
                return NotFound();
            }

            return subCategoryMaster;
        }

        // PUT: api/SubCategoryMaster/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubCategoryMaster(int id, SubCategoryMaster subCategoryMaster)
        {
            if (id != subCategoryMaster.SubCategoryId)
            {
                return BadRequest();
            }

            _context.Entry(subCategoryMaster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubCategoryMasterExists(id))
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

        // POST: api/SubCategoryMaster
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SubCategoryMaster>> PostSubCategoryMaster(SubCategoryMaster subCategoryMaster)
        {
            _context.SubCategoryMaster.Add(subCategoryMaster);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubCategoryMaster", new { id = subCategoryMaster.SubCategoryId }, subCategoryMaster);
        }

        // DELETE: api/SubCategoryMaster/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SubCategoryMaster>> DeleteSubCategoryMaster(int id)
        {
            var subCategoryMaster = await _context.SubCategoryMaster.FindAsync(id);
            if (subCategoryMaster == null)
            {
                return NotFound();
            }

            _context.SubCategoryMaster.Remove(subCategoryMaster);
            await _context.SaveChangesAsync();

            return subCategoryMaster;
        }

        private bool SubCategoryMasterExists(int id)
        {
            return _context.SubCategoryMaster.Any(e => e.SubCategoryId == id);
        }
    }
}

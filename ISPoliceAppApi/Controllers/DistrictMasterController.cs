using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ISPoliceAppApi.Data;
using ISPoliceAppApi.Models;
using ISPoliceAppApi.DTOs;
using AutoMapper;

namespace ISPoliceAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictMasterController : ControllerBase
    {
        private readonly ISPoliceAppApiDbContext _context;
        private readonly IMapper _mapper;


        public DistrictMasterController(ISPoliceAppApiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        // GET: api/DistrictMaster
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DistrictMaster>>> GetDistrictMaster()
        {
            return await _context.DistrictMaster.ToListAsync();
        }

        // GET: api/DistrictMaster/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DistrictMaster>> GetDistrictMaster(int id)
        {
            var districtMaster = await _context.DistrictMaster.FindAsync(id);

            if (districtMaster == null)
            {
                return NotFound();
            }

            return districtMaster;
        }

        // PUT: api/DistrictMaster/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDistrictMaster(int id, DistrictMaster districtMaster)
        {
            if (id != districtMaster.DistrictId)
            {
                return BadRequest();
            }

            _context.Entry(districtMaster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DistrictMasterExists(id))
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

        // POST: api/DistrictMaster
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<DistrictMaster>> PostDistrictMaster(DistrictMaster districtMaster)
        {
            _context.DistrictMaster.Add(districtMaster);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDistrictMaster", new { id = districtMaster.DistrictId }, districtMaster);
        }

        // DELETE: api/DistrictMaster/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DistrictMaster>> DeleteDistrictMaster(int id)
        {
            var districtMaster = await _context.DistrictMaster.FindAsync(id);
            if (districtMaster == null)
            {
                return NotFound();
            }

            _context.DistrictMaster.Remove(districtMaster);
            await _context.SaveChangesAsync();

            return districtMaster;
        }

        private bool DistrictMasterExists(int id)
        {
            return _context.DistrictMaster.Any(e => e.DistrictId == id);
        }

        [HttpGet("AllDistrictDropdown")]
        public async Task<ActionResult<IEnumerable<DistrictMaster>>> AllDistrictDropdown()
        {
            return await _context.DistrictMaster.Where(a=>a.IsActive==true).ToListAsync();
        }
    }
}

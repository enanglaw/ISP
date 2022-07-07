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
    public class StationMasterController : ControllerBase
    {
        private readonly ISPoliceAppApiDbContext _context;
        private readonly IMapper _mapper;

        public StationMasterController(ISPoliceAppApiDbContext context, IMapper mapper)
        {
            _mapper = mapper;

            _context = context;
        }

        // GET: api/StationMaster
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StationMaster>>> GetStationMaster()
        {
            return await _context.StationMaster.ToListAsync();
        }

        [HttpGet("AllStationsDropdown")]
        public async Task<ActionResult<IEnumerable<StationMaster>>> GetCategoryDropdown()
        {
            return await _context.StationMaster.Where(b=>b.IsActive==true).ToListAsync();
        }

        // GET: api/StationMaster/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StationMaster>> GetStationMaster(int id)
        {
            var stationMaster = await _context.StationMaster.FindAsync(id);

            if (stationMaster == null)
            {
                return NotFound();
            }

            return stationMaster;
        }

        // PUT: api/StationMaster/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStationMaster(int id, StationMaster stationMaster)
        {
            if (id != stationMaster.StationId)
            {
                return BadRequest();
            }

            _context.Entry(stationMaster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StationMasterExists(id))
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

        // POST: api/StationMaster
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<StationMaster>> PostStationMaster(StationMaster stationMaster)
        {
            _context.StationMaster.Add(stationMaster);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStationMaster", new { id = stationMaster.StationId }, stationMaster);
        }

        // DELETE: api/StationMaster/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<StationMaster>> DeleteStationMaster(int id)
        {
            var stationMaster = await _context.StationMaster.FindAsync(id);
            if (stationMaster == null)
            {
                return NotFound();
            }

            _context.StationMaster.Remove(stationMaster);
            await _context.SaveChangesAsync();

            return stationMaster;
        }

        private bool StationMasterExists(int id)
        {
            return _context.StationMaster.Any(e => e.StationId == id);
        }
    }
}

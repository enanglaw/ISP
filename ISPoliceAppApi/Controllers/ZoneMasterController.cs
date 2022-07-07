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
    public class ZoneMasterController : ControllerBase
    {
        private readonly ISPoliceAppApiDbContext _context;
        private readonly IMapper _mapper;

        public ZoneMasterController(ISPoliceAppApiDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        // GET: api/ZoneMaster
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ZoneMaster>>> GetZoneMaster()
        {
            return await _context.ZoneMaster.ToListAsync();
        }

        // GET: api/ZoneMaster/ZoneDropdown
        [HttpGet("ZoneDropdown")]
        public async Task<ActionResult<IEnumerable<ZoneDropdownDTO>>> GetZoneDropdown()
        {
            //   var zone = await _context.ZoneMaster.Include(x => x.DistrictMaster).ThenInclude(x => x.StationMaster).ToListAsync();
            //   return zone;
            var zone = await _context.ZoneMaster.Include(x => x.DistrictMaster).ThenInclude(x => x.StationMaster).ToListAsync();
            var zoneDto = _mapper.Map<List<ZoneDropdownDTO>>(zone);
            return zoneDto;
        }

        // GET: api/ZoneMaster/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ZoneMaster>> GetZoneMaster(int id)
        {
            var zoneMaster = await _context.ZoneMaster.FindAsync(id);

            if (zoneMaster == null)
            {
                return NotFound();
            }

            return zoneMaster;
        }

        // PUT: api/ZoneMaster/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutZoneMaster(int id, ZoneMaster zoneMaster)
        {
            if (id != zoneMaster.ZoneId)
            {
                return BadRequest();
            }

            _context.Entry(zoneMaster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZoneMasterExists(id))
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

        // POST: api/ZoneMaster
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ZoneMaster>> PostZoneMaster(ZoneMaster zoneMaster)
        {
            _context.ZoneMaster.Add(zoneMaster);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetZoneMaster", new { id = zoneMaster.ZoneId }, zoneMaster);
        }

        // DELETE: api/ZoneMaster/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ZoneMaster>> DeleteZoneMaster(int id)
        {
            var zoneMaster = await _context.ZoneMaster.FindAsync(id);
            if (zoneMaster == null)
            {
                return NotFound();
            }

            _context.ZoneMaster.Remove(zoneMaster);
            await _context.SaveChangesAsync();

            return zoneMaster;
        }

        private bool ZoneMasterExists(int id)
        {
            return _context.ZoneMaster.Any(e => e.ZoneId == id);
        }
        [HttpGet("ALlZoneDropdown")]
        public async Task<ActionResult<IEnumerable<ZoneDropdownDTO>>> GetAllZoneDropdown()
        {
            //   var zone = await _context.ZoneMaster.Include(x => x.DistrictMaster).ThenInclude(x => x.StationMaster).ToListAsync();
            //   return zone;
            var zone = await _context.ZoneMaster.Include(x => x.DistrictMaster).ThenInclude(x => x.StationMaster).ToListAsync();
            var zoneDto = _mapper.Map<List<ZoneDropdownDTO>>(zone);
            return zoneDto;
        }

    }
}

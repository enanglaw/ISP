using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ISPoliceAppApi.Data;
using ISPoliceAppApi.Models;
using AutoMapper;
using ISPoliceAppApi.DTOs;

namespace ISPoliceAppApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class VenuePermissionTypeController : ControllerBase
  {
    private readonly ISPoliceAppApiDbContext _context;
    private readonly IMapper _mapper;

    public VenuePermissionTypeController(ISPoliceAppApiDbContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;
    }

    // GET: api/VenuePermissionType
    [HttpGet]
    public async Task<ActionResult<IEnumerable<VenuePermissionType>>> GetVenuePermissionType()
    {
      return await _context.VenuePermissionType.Include(x => x.Venue).ToListAsync();
    }

    // GET: api/VenuePermissionType/Grid
    [HttpGet("Grid")]
    public async Task<ActionResult<IEnumerable<VenuePermissionTypeGridDTO>>> GetVenuePermissionTypeGrid()
    {
      var vpTypes = await _context.VenuePermissionType.Include(x => x.Venue).ToListAsync();
      var vpTypeForGrid = _mapper.Map<List<VenuePermissionTypeGridDTO>>(vpTypes);
      return vpTypeForGrid;
    }

    // GET: api/VenuePermissionType/5
    [HttpGet("{id}")]
    public async Task<ActionResult<VenuePermissionType>> GetVenuePermissionType(int id)
    {
      var venuePermissionType = await _context.VenuePermissionType.FindAsync(id);

      if (venuePermissionType == null)
      {
        return NotFound();
      }

      return venuePermissionType;
    }

    // PUT: api/VenuePermissionType/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPut("{id}")]
    public async Task<IActionResult> PutVenuePermissionType(int id, [FromForm] VenuePermissionTypeCreationDTO vpTypeCreationDTO)
    {
      var venuePermissionType = await _context.VenuePermissionType.FindAsync(id);
      if (venuePermissionType == null)
      {
        return NotFound();
      }
      if (id != venuePermissionType.VenuePermissionTypeId)
      {
        return BadRequest();
      }
      venuePermissionType = _mapper.Map(vpTypeCreationDTO, venuePermissionType);
      venuePermissionType.UpdatedOn = DateTime.Now;
      _context.Entry(venuePermissionType).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!VenuePermissionTypeExists(id))
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

    // POST: api/VenuePermissionType
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPost]
    public async Task<ActionResult<VenuePermissionType>> PostVenuePermissionType([FromForm] VenuePermissionTypeCreationDTO venuePermissionType)
    {
      var vpType = _mapper.Map<VenuePermissionTypeCreationDTO, VenuePermissionType>(venuePermissionType);
      _context.VenuePermissionType.Add(vpType);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetVenuePermissionType", new { id = vpType.VenuePermissionTypeId }, vpType);
    }

    // DELETE: api/VenuePermissionType/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<VenuePermissionType>> DeleteVenuePermissionType(int id)
    {
      var venuePermissionType = await _context.VenuePermissionType.FindAsync(id);
      if (venuePermissionType == null)
      {
        return NotFound();
      }

      // _context.VenuePermissionType.Remove(venuePermissionType);
      venuePermissionType.IsActive = false;
      venuePermissionType.UpdatedOn = DateTime.Now;
      _context.Entry(venuePermissionType).State = EntityState.Modified;
      await _context.SaveChangesAsync();

      return venuePermissionType;
    }

    // DELETE: api/UnDelete/5
    [HttpDelete("UnDelete/{id}")]
    public async Task<ActionResult<VenuePermissionType>> UnDeleteVenuePermissionType(int id)
    {
      var venuePermissionType = await _context.VenuePermissionType.FindAsync(id);
      if (venuePermissionType == null)
      {
        return NotFound();
      }

      // _context.VenuePermissionType.Remove(venuePermissionType);
      venuePermissionType.IsActive = true;
      venuePermissionType.UpdatedOn = DateTime.Now;
      _context.Entry(venuePermissionType).State = EntityState.Modified;
      await _context.SaveChangesAsync();

      return venuePermissionType;
    }

    private bool VenuePermissionTypeExists(int id)
    {
      return _context.VenuePermissionType.Any(e => e.VenuePermissionTypeId == id);
    }
  }
}

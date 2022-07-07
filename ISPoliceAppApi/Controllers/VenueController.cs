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
  [Route("api/venue")]
  [ApiController]
  public class VenueController : ControllerBase
  {
    private readonly ISPoliceAppApiDbContext _context;
    private readonly IMapper _mapper;

    public VenueController(ISPoliceAppApiDbContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;
    }

    // GET: api/Venue
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Venue>>> GetVenue()
    {
      return await _context.Venue.Include(x => x.VenuePermissionType).ToListAsync();
    }

    // GET: api/venue/VenueDropdown
    [HttpGet("VenueDropdown")]
    public async Task<ActionResult<IEnumerable<VenueDropdownDTO>>> GetVenueDropdown()
    {
      var venues = await _context.Venue.Include(x => x.VenuePermissionType).ToListAsync();
      var venueDto = _mapper.Map<List<VenueDropdownDTO>>(venues);
      return venueDto;
    }

    // GET: api/venue/VenueGrid
    [HttpGet("VenueGrid")]
    public async Task<ActionResult<IEnumerable<VenueGridDTO>>> GetVenueGrid()
    {
      var venues = await _context.Venue.Include(x => x.VenuePermissionType).ToListAsync();
      var venueDto = _mapper.Map<List<VenueGridDTO>>(venues);
      return venueDto;
    }

    // GET: api/Venue/5
    [HttpGet("{id}")]
    public async Task<ActionResult<VenueGridDTO>> GetVenue(int id)
    {
      var venue = await _context.Venue.FindAsync(id);
      var venueDto = _mapper.Map<VenueGridDTO>(venue);
      if (venue == null)
      {
        return NotFound();
      }

      return venueDto;
    }

    // PUT: api/Venue/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPut("{id}")]
    public async Task<IActionResult> PutVenue(int id, [FromForm] VenueCreationDTO venueCreationDTO)
    {
      var venue = await _context.Venue.FindAsync(id);
      if (venue == null)
      {
        return NotFound();
      }
      if (id != venue.VenueId)
      {
        return BadRequest();
      }
      venue = _mapper.Map(venueCreationDTO, venue);
      venue.UpdatedOn = DateTime.Now;
      _context.Entry(venue).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!VenueExists(id))
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

    // POST: api/Venue
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPost]
    public async Task<ActionResult<Venue>> PostVenue([FromForm] VenueCreationDTO venueCreationDTO)
    {
      var venue = _mapper.Map<VenueCreationDTO, Venue>(venueCreationDTO);
      _context.Venue.Add(venue);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetVenue", new { id = venue.VenueId }, venue);
    }

    // DELETE: api/Venue/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Venue>> DeleteVenue(int id)
    {
      var venue = await _context.Venue.FindAsync(id);
      if (venue == null)
      {
        return NotFound();
      }

      // _context.Venue.Remove(venue);
      venue.IsActive = false;
      _context.Entry(venue).State = EntityState.Modified;
      await _context.SaveChangesAsync();

      return venue;
    }

    // DELETE: api/unDeleteVenue/5
    [HttpDelete("UnDelete/{id}")]
    public async Task<ActionResult<Venue>> UnDeleteVenue(int id)
    {
      var venue = await _context.Venue.FindAsync(id);
      if (venue == null)
      {
        return NotFound();
      }

      venue.IsActive = true;
      venue.UpdatedOn = DateTime.Now;
      _context.Entry(venue).State = EntityState.Modified;
      await _context.SaveChangesAsync();

      return venue;
    }

    private bool VenueExists(int id)
    {
      return _context.Venue.Any(e => e.VenueId == id);
    }
       


    }
}

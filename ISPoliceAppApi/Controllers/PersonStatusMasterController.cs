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
  public class PersonStatusMasterController : ControllerBase
  {
    private readonly ISPoliceAppApiDbContext _context;
    private readonly IMapper _mapper;

    public PersonStatusMasterController(ISPoliceAppApiDbContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;
    }

    // GET: api/PersonStatusMaster/Dropdown
    [HttpGet("Dropdown")]
    public async Task<ActionResult<IEnumerable<PersonStatusDTO>>> GetPersonTypeDropdown()
    {
      var personStatus = await _context.PersonStatusMaster.ToListAsync();
      var personStatusDto = _mapper.Map<List<PersonStatusDTO>>(personStatus);
      return personStatusDto;
    }

    // GET: api/PersonStatusMaster
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PersonStatusMaster>>> GetPersonStatusMaster()
    {
      return await _context.PersonStatusMaster.ToListAsync();
    }

    // GET: api/PersonStatusMaster/5
    [HttpGet("{id}")]
    public async Task<ActionResult<PersonStatusMaster>> GetPersonStatusMaster(int id)
    {
      var personStatusMaster = await _context.PersonStatusMaster.FindAsync(id);

      if (personStatusMaster == null)
      {
        return NotFound();
      }

      return personStatusMaster;
    }

    // PUT: api/PersonStatusMaster/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPersonStatusMaster(int id, PersonStatusMaster personStatusMaster)
    {
      if (id != personStatusMaster.StatusId)
      {
        return BadRequest();
      }

      _context.Entry(personStatusMaster).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!PersonStatusMasterExists(id))
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

    // POST: api/PersonStatusMaster
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPost]
    public async Task<ActionResult<PersonStatusMaster>> PostPersonStatusMaster(PersonStatusMaster personStatusMaster)
    {
      _context.PersonStatusMaster.Add(personStatusMaster);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetPersonStatusMaster", new { id = personStatusMaster.StatusId }, personStatusMaster);
    }

    // DELETE: api/PersonStatusMaster/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<PersonStatusMaster>> DeletePersonStatusMaster(int id)
    {
      var personStatusMaster = await _context.PersonStatusMaster.FindAsync(id);
      if (personStatusMaster == null)
      {
        return NotFound();
      }

      _context.PersonStatusMaster.Remove(personStatusMaster);
      await _context.SaveChangesAsync();

      return personStatusMaster;
    }

    private bool PersonStatusMasterExists(int id)
    {
      return _context.PersonStatusMaster.Any(e => e.StatusId == id);
    }
  }
}

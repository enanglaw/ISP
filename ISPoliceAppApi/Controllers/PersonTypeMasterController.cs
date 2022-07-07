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
  public class PersonTypeMasterController : ControllerBase
  {
    private readonly ISPoliceAppApiDbContext _context;
    private readonly IMapper _mapper;

    public PersonTypeMasterController(ISPoliceAppApiDbContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;
    }

    // GET: api/PersonTypeMaster
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PersonTypeMaster>>> GetPersonTypeMaster()
    {
      return await _context.PersonTypeMaster.ToListAsync();
    }


    // GET: api/PersonTypeMaster/Dropdown
    [HttpGet("Dropdown")]
    public async Task<ActionResult<IEnumerable<PersonTypeDTO>>> GetPersonTypeDropdown()
    {
      var personType = await _context.PersonTypeMaster.Where(x => x.IsActive == true).ToListAsync();
      var personTypeDto = _mapper.Map<List<PersonTypeDTO>>(personType);
      return personTypeDto;
    }

    // GET: api/PersonTypeMaster/5
    [HttpGet("{id}")]
    public async Task<ActionResult<PersonTypeMaster>> GetPersonTypeMaster(int id)
    {
      var personTypeMaster = await _context.PersonTypeMaster.FindAsync(id);

      if (personTypeMaster == null)
      {
        return NotFound();
      }

      return personTypeMaster;
    }

    // PUT: api/PersonTypeMaster/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPersonTypeMaster(int id, PersonTypeMaster personTypeMaster)
    {
      if (id != personTypeMaster.PersonTypeId)
      {
        return BadRequest();
      }

      _context.Entry(personTypeMaster).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!PersonTypeMasterExists(id))
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

    // POST: api/PersonTypeMaster
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPost]
    public async Task<ActionResult<PersonTypeMaster>> PostPersonTypeMaster(PersonTypeMaster personTypeMaster)
    {
      _context.PersonTypeMaster.Add(personTypeMaster);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetPersonTypeMaster", new { id = personTypeMaster.PersonTypeId }, personTypeMaster);
    }

    // DELETE: api/PersonTypeMaster/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<PersonTypeMaster>> DeletePersonTypeMaster(int id)
    {
      var personTypeMaster = await _context.PersonTypeMaster.FindAsync(id);
      if (personTypeMaster == null)
      {
        return NotFound();
      }

      _context.PersonTypeMaster.Remove(personTypeMaster);
      await _context.SaveChangesAsync();

      return personTypeMaster;
    }

    private bool PersonTypeMasterExists(int id)
    {
      return _context.PersonTypeMaster.Any(e => e.PersonTypeId == id);
    }
  }
}

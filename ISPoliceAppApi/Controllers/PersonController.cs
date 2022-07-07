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
using ISPoliceAppApi.Helpers;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System.IO;

namespace ISPoliceAppApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PersonController : ControllerBase
  {
    private readonly ISPoliceAppApiDbContext _context;
    private readonly IMapper _mapper;
    private readonly IFileStorageService _fileStorageService;
    private readonly ILogger<PersonController> _logger;

    public PersonController(ISPoliceAppApiDbContext context, IMapper mapper, IFileStorageService fileStorageService, ILogger<PersonController> logger)
    {
      _logger = logger;
      _fileStorageService = fileStorageService;
      _mapper = mapper;
      _context = context;
    }

    // GET: api/Person
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Person>>> GetPerson()
    {
      return await _context.Person.ToListAsync();
    }

    // GET: api/person/Dropdown
    [HttpGet("Dropdown")]
    public async Task<ActionResult<IEnumerable<PersonDropdownDTO>>> GetPersonDropdown()
    {
      var persons = await _context.Person.ToListAsync();
      var personDto = _mapper.Map<List<PersonDropdownDTO>>(persons);
      return personDto;
    }

    // GET: api/Person/GridView
    [HttpGet("GridView")]
    public async Task<ActionResult<IEnumerable<PersonGridViewDTO>>> GetPersonGridView()
    {
      var persons = await _context.Person
                            .Include(x => x.PersonAliasName)
                            .Include(x => x.Status)
                            .ToListAsync();
      var personDto = _mapper.Map<List<PersonGridViewDTO>>(persons);
      return personDto;
    }

    // GET: api/Person/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Person>> GetPerson(int id)
    {
      var person = await _context.Person.FindAsync(id);

      if (person == null)
      {
        return NotFound();
      }

      return person;
    }

    // PUT: api/Person/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPerson(int id, Person person)
    {
      if (id != person.PersonId)
      {
        return BadRequest();
      }

      _context.Entry(person).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!PersonExists(id))
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

    // POST: api/Person
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    /* [HttpPost]
    public async Task<ActionResult<Person>> PostPerson(Person person)
    {
        _context.Person.Add(person);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetPerson", new { id = person.PersonId }, person);
    } */

    // POST: api/Person
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPost]
    public async Task<ActionResult<Person>> PostPerson([FromForm] PersonCreationDTO personCreationDTO)
    {
      _logger.LogInformation("Person creation starts...");
      try
      {
        var person = _mapper.Map<PersonCreationDTO, Person>(personCreationDTO);
        /* if (personCreationDTO.PersonMedia != null)
        {
          foreach (var media in personCreationDTO.PersonMedia)
          {
            var date = DateTime.Now;
            var filePath = "Media\\Person\\" + date.ToString("MMM") + date.Year.ToString();
            person.InputDataDocumentPath = Path.Combine(filePath, media.MediaLabel);
            var fileUrl = await _fileStorageService.SaveFile(filePath, media.Media);
            person.InputDataDocumentUrl = fileUrl;
          }
        } */
        if (personCreationDTO.PhotoDocument != null)
        {
          var date = DateTime.Now;
          var filePath = "Media\\Person\\" + date.ToString("MMM") + date.Year.ToString();
          person.PhotoPath = Path.Combine(filePath, personCreationDTO.PhotoDocument.FileName);
          var fileUrl = await _fileStorageService.SaveFile(filePath, personCreationDTO.PhotoDocument);
          person.PhotoUrl = fileUrl;
        }
        _logger.LogInformation("Creating Person & detail table starts...");
        _context.Person.Add(person);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Person created successfully...");
        _logger.LogInformation("Creating Person & detail table ends...");
        return CreatedAtAction("GetPerson", new { id = person.PersonId }, person);
      }
      catch (DbUpdateConcurrencyException ex)
      {
        _logger.LogError("Exception in Creating Person & detail table: ", ex);
        throw;
      }
    }

    // DELETE: api/Person/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Person>> DeletePerson(int id)
    {
      var person = await _context.Person.FindAsync(id);
      if (person == null)
      {
        return NotFound();
      }

      _context.Person.Remove(person);
      await _context.SaveChangesAsync();

      return person;
    }

    private bool PersonExists(int id)
    {
      return _context.Person.Any(e => e.PersonId == id);
    }
  }
}

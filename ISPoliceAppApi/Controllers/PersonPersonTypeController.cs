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
    public class PersonPersonTypeController : ControllerBase
    {
        private readonly ISPoliceAppApiDbContext _context;

        public PersonPersonTypeController(ISPoliceAppApiDbContext context)
        {
            _context = context;
        }

        // GET: api/PersonPersonType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonPersonType>>> GetPersonPersonType()
        {
            return await _context.PersonPersonType.ToListAsync();
        }

        // GET: api/PersonPersonType/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonPersonType>> GetPersonPersonType(int id)
        {
            var personPersonType = await _context.PersonPersonType.FindAsync(id);

            if (personPersonType == null)
            {
                return NotFound();
            }

            return personPersonType;
        }

        // PUT: api/PersonPersonType/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonPersonType(int id, PersonPersonType personPersonType)
        {
            if (id != personPersonType.PersonPersonTypeId)
            {
                return BadRequest();
            }

            _context.Entry(personPersonType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonPersonTypeExists(id))
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

        // POST: api/PersonPersonType
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PersonPersonType>> PostPersonPersonType(PersonPersonType personPersonType)
        {
            _context.PersonPersonType.Add(personPersonType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersonPersonType", new { id = personPersonType.PersonPersonTypeId }, personPersonType);
        }

        // DELETE: api/PersonPersonType/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PersonPersonType>> DeletePersonPersonType(int id)
        {
            var personPersonType = await _context.PersonPersonType.FindAsync(id);
            if (personPersonType == null)
            {
                return NotFound();
            }

            _context.PersonPersonType.Remove(personPersonType);
            await _context.SaveChangesAsync();

            return personPersonType;
        }

        private bool PersonPersonTypeExists(int id)
        {
            return _context.PersonPersonType.Any(e => e.PersonPersonTypeId == id);
        }
    }
}

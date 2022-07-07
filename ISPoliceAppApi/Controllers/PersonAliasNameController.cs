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
    public class PersonAliasNameController : ControllerBase
    {
        private readonly ISPoliceAppApiDbContext _context;

        public PersonAliasNameController(ISPoliceAppApiDbContext context)
        {
            _context = context;
        }

        // GET: api/PersonAliasName
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonAliasName>>> GetPersonAliasName()
        {
            return await _context.PersonAliasName.ToListAsync();
        }

        // GET: api/PersonAliasName/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonAliasName>> GetPersonAliasName(int id)
        {
            var personAliasName = await _context.PersonAliasName.FindAsync(id);

            if (personAliasName == null)
            {
                return NotFound();
            }

            return personAliasName;
        }

        // PUT: api/PersonAliasName/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonAliasName(int id, PersonAliasName personAliasName)
        {
            if (id != personAliasName.AliasNameId)
            {
                return BadRequest();
            }

            _context.Entry(personAliasName).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonAliasNameExists(id))
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

        // POST: api/PersonAliasName
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PersonAliasName>> PostPersonAliasName(PersonAliasName personAliasName)
        {
            _context.PersonAliasName.Add(personAliasName);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersonAliasName", new { id = personAliasName.AliasNameId }, personAliasName);
        }

        // DELETE: api/PersonAliasName/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PersonAliasName>> DeletePersonAliasName(int id)
        {
            var personAliasName = await _context.PersonAliasName.FindAsync(id);
            if (personAliasName == null)
            {
                return NotFound();
            }

            _context.PersonAliasName.Remove(personAliasName);
            await _context.SaveChangesAsync();

            return personAliasName;
        }

        private bool PersonAliasNameExists(int id)
        {
            return _context.PersonAliasName.Any(e => e.AliasNameId == id);
        }
    }
}

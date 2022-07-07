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
    public class PersonAddressController : ControllerBase
    {
        private readonly ISPoliceAppApiDbContext _context;

        public PersonAddressController(ISPoliceAppApiDbContext context)
        {
            _context = context;
        }

        // GET: api/PersonAddress
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonAddress>>> GetPersonAddress()
        {
            return await _context.PersonAddress.ToListAsync();
        }

        // GET: api/PersonAddress/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonAddress>> GetPersonAddress(int id)
        {
            var personAddress = await _context.PersonAddress.FindAsync(id);

            if (personAddress == null)
            {
                return NotFound();
            }

            return personAddress;
        }

        // PUT: api/PersonAddress/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonAddress(int id, PersonAddress personAddress)
        {
            if (id != personAddress.AddressId)
            {
                return BadRequest();
            }

            _context.Entry(personAddress).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonAddressExists(id))
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

        // POST: api/PersonAddress
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PersonAddress>> PostPersonAddress(PersonAddress personAddress)
        {
            _context.PersonAddress.Add(personAddress);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersonAddress", new { id = personAddress.AddressId }, personAddress);
        }

        // DELETE: api/PersonAddress/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PersonAddress>> DeletePersonAddress(int id)
        {
            var personAddress = await _context.PersonAddress.FindAsync(id);
            if (personAddress == null)
            {
                return NotFound();
            }

            _context.PersonAddress.Remove(personAddress);
            await _context.SaveChangesAsync();

            return personAddress;
        }

        private bool PersonAddressExists(int id)
        {
            return _context.PersonAddress.Any(e => e.AddressId == id);
        }
    }
}

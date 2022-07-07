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
    public class PersonRivalGangController : ControllerBase
    {
        private readonly ISPoliceAppApiDbContext _context;

        public PersonRivalGangController(ISPoliceAppApiDbContext context)
        {
            _context = context;
        }

        // GET: api/PersonRivalGang
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonRivalGang>>> GetPersonRivalGang()
        {
            return await _context.PersonRivalGang.ToListAsync();
        }

        // GET: api/PersonRivalGang/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonRivalGang>> GetPersonRivalGang(int id)
        {
            var personRivalGang = await _context.PersonRivalGang.FindAsync(id);

            if (personRivalGang == null)
            {
                return NotFound();
            }

            return personRivalGang;
        }

        // PUT: api/PersonRivalGang/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonRivalGang(int id, PersonRivalGang personRivalGang)
        {
            if (id != personRivalGang.RivalGangId)
            {
                return BadRequest();
            }

            _context.Entry(personRivalGang).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonRivalGangExists(id))
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

        // POST: api/PersonRivalGang
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PersonRivalGang>> PostPersonRivalGang(PersonRivalGang personRivalGang)
        {
            _context.PersonRivalGang.Add(personRivalGang);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersonRivalGang", new { id = personRivalGang.RivalGangId }, personRivalGang);
        }

        // DELETE: api/PersonRivalGang/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PersonRivalGang>> DeletePersonRivalGang(int id)
        {
            var personRivalGang = await _context.PersonRivalGang.FindAsync(id);
            if (personRivalGang == null)
            {
                return NotFound();
            }

            _context.PersonRivalGang.Remove(personRivalGang);
            await _context.SaveChangesAsync();

            return personRivalGang;
        }

        private bool PersonRivalGangExists(int id)
        {
            return _context.PersonRivalGang.Any(e => e.RivalGangId == id);
        }
    }
}

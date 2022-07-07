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
    public class PersonCaseHistoryController : ControllerBase
    {
        private readonly ISPoliceAppApiDbContext _context;

        public PersonCaseHistoryController(ISPoliceAppApiDbContext context)
        {
            _context = context;
        }

        // GET: api/PersonCaseHistory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonCaseHistory>>> GetPersonCaseHistory()
        {
            return await _context.PersonCaseHistory.ToListAsync();
        }

        // GET: api/PersonCaseHistory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonCaseHistory>> GetPersonCaseHistory(int id)
        {
            var personCaseHistory = await _context.PersonCaseHistory.FindAsync(id);

            if (personCaseHistory == null)
            {
                return NotFound();
            }

            return personCaseHistory;
        }

        // PUT: api/PersonCaseHistory/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonCaseHistory(int id, PersonCaseHistory personCaseHistory)
        {
            if (id != personCaseHistory.CaseHistoryId)
            {
                return BadRequest();
            }

            _context.Entry(personCaseHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonCaseHistoryExists(id))
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

        // POST: api/PersonCaseHistory
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PersonCaseHistory>> PostPersonCaseHistory(PersonCaseHistory personCaseHistory)
        {
            _context.PersonCaseHistory.Add(personCaseHistory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersonCaseHistory", new { id = personCaseHistory.CaseHistoryId }, personCaseHistory);
        }

        // DELETE: api/PersonCaseHistory/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PersonCaseHistory>> DeletePersonCaseHistory(int id)
        {
            var personCaseHistory = await _context.PersonCaseHistory.FindAsync(id);
            if (personCaseHistory == null)
            {
                return NotFound();
            }

            _context.PersonCaseHistory.Remove(personCaseHistory);
            await _context.SaveChangesAsync();

            return personCaseHistory;
        }

        private bool PersonCaseHistoryExists(int id)
        {
            return _context.PersonCaseHistory.Any(e => e.CaseHistoryId == id);
        }
    }
}

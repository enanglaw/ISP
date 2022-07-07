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
    public class PersonMediaController : ControllerBase
    {
        private readonly ISPoliceAppApiDbContext _context;

        public PersonMediaController(ISPoliceAppApiDbContext context)
        {
            _context = context;
        }

        // GET: api/PersonMedia
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonMedia>>> GetPersonMedia()
        {
            return await _context.PersonMedia.ToListAsync();
        }

        // GET: api/PersonMedia/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonMedia>> GetPersonMedia(int id)
        {
            var personMedia = await _context.PersonMedia.FindAsync(id);

            if (personMedia == null)
            {
                return NotFound();
            }

            return personMedia;
        }

        // PUT: api/PersonMedia/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonMedia(int id, PersonMedia personMedia)
        {
            if (id != personMedia.MediaId)
            {
                return BadRequest();
            }

            _context.Entry(personMedia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonMediaExists(id))
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

        // POST: api/PersonMedia
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PersonMedia>> PostPersonMedia(PersonMedia personMedia)
        {
            _context.PersonMedia.Add(personMedia);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersonMedia", new { id = personMedia.MediaId }, personMedia);
        }

        // DELETE: api/PersonMedia/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PersonMedia>> DeletePersonMedia(int id)
        {
            var personMedia = await _context.PersonMedia.FindAsync(id);
            if (personMedia == null)
            {
                return NotFound();
            }

            _context.PersonMedia.Remove(personMedia);
            await _context.SaveChangesAsync();

            return personMedia;
        }

        private bool PersonMediaExists(int id)
        {
            return _context.PersonMedia.Any(e => e.MediaId == id);
        }
    }
}

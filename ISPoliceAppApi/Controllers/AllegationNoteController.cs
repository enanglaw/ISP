using AutoMapper;
using ISPoliceAppApi.Data;
using ISPoliceAppApi.DTOs;
using ISPoliceAppApi.Helpers;
using ISPoliceAppApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using EntityState = Microsoft.EntityFrameworkCore.EntityState;
using DbUpdateConcurrencyException = System.Data.Entity.Infrastructure.DbUpdateConcurrencyException;

namespace ISPoliceAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllegationNoteController : ControllerBase
    {
        private readonly ISPoliceAppApiDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileStorageService _fileStorageService;
        private readonly ILogger<AllegationNoteController> _logger;

        public AllegationNoteController(ISPoliceAppApiDbContext context, IMapper mapper, IFileStorageService fileStorageService, ILogger<AllegationNoteController> logger)
        {
            _logger = logger;
            _fileStorageService = fileStorageService;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AllegationNote))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AllegationNote>> AllegationNote(int id)
        {


            try
            {
                var note = await _context.AllegationNotes.Where(a=>a.Id==id).ToListAsync();
                if (note == null)
                {
                    return BadRequest($"Could not find any note with provided Id");
                }
                   
                return Ok(note);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AllegationNote))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<AllegationNote>>> GetAllegationNotes()
        {
         
            try
            {
                var notes = await _context.AllegationNotes.ToListAsync();
                if (notes == null)
                    return BadRequest();
                return Ok(notes);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }

        }




        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutAllegationNote(int id, [FromBody] AllegationNote note)
        {

      
            if (id!= note.Id)
            {
                return BadRequest($"Could not find any note with provided Id");
            }
           
            try
            {
                var allegationTitle = await _context.Allegations.FirstOrDefaultAsync(x => x.Id == note.AllegationId);
                if (allegationTitle.Title != null)
                {
                    note.Title = allegationTitle.Title;
                }
                _context.Entry(note).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError("error", ex);
                if (!IsNoteExists(id))
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




        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<AllegationNote>> PostAllegationNote([FromBody] AllegationNote note)
        {

            _logger.LogInformation("Note creation starts...");
            try
            {
         
                _logger.LogInformation("Creating note & detail table starts...");
                var allegationTitle = await _context.Allegations.FirstOrDefaultAsync(x => x.Id == note.AllegationId);
                if (allegationTitle.Title != null)
                {
                    note.Title = allegationTitle.Title;
                }

                _context.AllegationNotes.Add(note);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Note created successfully...");
                _logger.LogInformation("Creating note & detail table ends...");
                return Ok();

            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError("Exception in Creating note & detail table: ", ex);
                throw;
            }

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AllegationNote>> DeleteAllegationNote(int id)
        {

           
            var note = await _context.AllegationNotes.FindAsync(id);
            if (note != null)
            {
                _context.AllegationNotes.Remove(note);
                await _context.SaveChangesAsync();
               
                
                return Ok();

            }

            return NotFound();
        }




private bool IsNoteExists(int id)
        {
            return _context.AllegationNotes.Any(e => e.Id == id);
        }


    }
}

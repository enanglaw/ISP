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
    public class MemorandumController : ControllerBase
    {
        private readonly ISPoliceAppApiDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileStorageService _fileStorageService;
        private readonly ILogger<MemorandumController> _logger;

        public MemorandumController(ISPoliceAppApiDbContext context, IMapper mapper, IFileStorageService fileStorageService, ILogger<MemorandumController> logger)
        {
            _logger = logger;
            _fileStorageService = fileStorageService;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Memorandum))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Memorandum>> GetMemorandum(int id)
        {


            try
            {
                var memorandum = await _context.Memoranda.Where(a=>a.Id==id).ToListAsync();
                if (memorandum == null)
                {
                    return BadRequest($"Could not find any memorandum  with provided Id");
                }
                   
                return Ok(memorandum);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Memorandum))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Memorandum>>> GetMemoranda()
        {
         
            try
            {
                var memoranda = await _context.Memoranda.ToListAsync();
                if (memoranda == null)
                    return BadRequest();
                return Ok(memoranda);
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
        public async Task<IActionResult> PutMemoranda(int id, [FromBody] Memorandum memorandum)
        {

      
            if (id!= memorandum.Id)
            {
                return BadRequest($"Could not find any memorandum with provided Id");
            }
          

            try
            {
                var allegationTitle = await _context.Allegations.FirstOrDefaultAsync(x => x.Id == memorandum.AllegationId);
                if (allegationTitle.Title != null)
                {
                    memorandum.Title = allegationTitle.Title;
                }
                _context.Entry(memorandum).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError("error", ex);
                if (!IsMemorandumExists(id))
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

        public async Task<ActionResult<Memorandum>> PostMemorandum([FromBody] Memorandum memorandum)
        {

            _logger.LogInformation("Memorandum creation starts...");
            try
            {
         
                _logger.LogInformation("Creating Memorandum & detail table starts...");
                var allegationTitle = await _context.Allegations.FirstOrDefaultAsync(x => x.Id == memorandum.AllegationId);
                if (allegationTitle.Title != null)
                {
                    memorandum.Title = allegationTitle.Title;
                }
                _context.Memoranda.Add(memorandum);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Memorandum created successfully...");
                _logger.LogInformation("Creating Memorandum & detail table ends...");
                return Ok();

            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError("Exception in Creating allegation & detail table: ", ex);
                throw;
            }

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Memorandum>> DeleteMemorandum(int id)
        {

            var memorandum = await _context.Memoranda.FindAsync(id);
            if (memorandum != null)
            {
                _context.Memoranda.RemoveRange(memorandum);
                await _context.SaveChangesAsync();             
               
                
                return Ok();

            }

            return NotFound();
        }




private bool IsMemorandumExists(int id)
        {
            return _context.Memoranda.Any(e => e.Id == id);
        }


    }
}

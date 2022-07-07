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
    public class AllegationController : ControllerBase
    {
        private readonly ISPoliceAppApiDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileStorageService _fileStorageService;
        private readonly ILogger<AllegationController> _logger;

        public AllegationController(ISPoliceAppApiDbContext context, IMapper mapper, IFileStorageService fileStorageService, ILogger<AllegationController> logger)
        {
            _logger = logger;
            _fileStorageService = fileStorageService;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Allegation))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Allegation>> GetAllegation(int id)
        {


            try
            {
                var allegation = await _context.Allegations.Where(a=>a.Id==id).ToListAsync();
                if (allegation == null)
                    return BadRequest($"Could not find any allegation Enquiry with provided Id");
                return Ok(allegation);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }

        }
        [Route("Dropdown/{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Allegation))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Allegation>> GetAllegationDropdown(int id)
        {


            try
            {
                var allegation = await _context.Allegations.Where(a => a.Id == id).OrderBy(x => x.Title).ToListAsync();
                if (allegation == null)
                {
                    return BadRequest($"Could not find any allegation Enquiry with provided Id");

                }
                return Ok(allegation);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Allegation))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Allegation>>> GetAllegations()
        {
         
            try
            {
                var allegation = await _context.Allegations.ToListAsync();
                if (allegation == null)
                    return BadRequest($"Could not find any allegation Enquiry with provided Id");
                return Ok(allegation);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }

        }



      

        [HttpPost("Upload")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<Allegation>> PostUpload()
        {


            try
            {
                var date = DateTime.Now;
                var filePath = "Resources\\Media\\Allegation\\" ;
                var file = Request.Form.Files[0];
                var folderName = Path.Combine(filePath);
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);

                    if (!Directory.Exists(folderName))
                    {
                        Directory.CreateDirectory(folderName);
                    }



                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutAllegation(int id, [FromBody] AllegationUpdateDTO allegationUpdateDTO)
        {

      
            if (id!= allegationUpdateDTO.Id)
            {
                return BadRequest($"Could not find any allegation with provided Id");
            }
           
           var allegation = _mapper.Map<AllegationUpdateDTO, Allegation>(allegationUpdateDTO);

            if (allegation != null)
            {

                _context.Entry(allegation).State = EntityState.Modified;
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError("error", ex);
                if (!IsAllegationExists(id))
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

        public async Task<ActionResult<Allegation>> PostAllegation([FromBody] Allegation allegation)
        {

            _logger.LogInformation("Allegation creation starts...");
            try
            {
              var custId=  allegation.PersonalProfileId;
                var customId = allegation.Id;
         
                _logger.LogInformation("Creating allegation & detail table starts...");
                _context.Allegations.Add(allegation);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Allegation created successfully...");
                _logger.LogInformation("Creating Allegation & detail table ends...");
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
        public async Task<ActionResult<Allegation>> DeleteAllegation(int id)
        {

            var filePath = "Resources\\Media\\Allegation\\";
            int length = filePath.Length;
            var allegation = await _context.Allegations.FindAsync(id);
            if (allegation != null)
            {
                _context.Allegations.Remove(allegation);
                await _context.SaveChangesAsync();
                if (allegation.AttachmentUrl != null)
                {
                    allegation.AttachmentPath = allegation.AttachmentUrl.Substring(length);
                    await _fileStorageService.DeleteFile(allegation.AttachmentUrl, allegation.AttachmentPath);

                }
                
                return Ok();

            }

            return NotFound();
        }




private bool IsAllegationExists(int id)
        {
            return _context.Allegations.Any(e => e.Id == id);
        }


    }
}

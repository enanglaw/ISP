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
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ISPoliceAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonnelPreviousAllegationController : ControllerBase
    {

        private readonly ISPoliceAppApiDbContext _context;
        private readonly IMapper _mapper;

        private readonly IFileStorageService _fileStorageService;
        private readonly ILogger<PersonnelPreviousAllegationController> _logger;

        public PersonnelPreviousAllegationController(ISPoliceAppApiDbContext context, IMapper mapper,
            ILogger<PersonnelPreviousAllegationController> logger, IFileStorageService fileStorageService )
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
            _fileStorageService = fileStorageService;
        }



      

        [HttpGet("{id}")]
        
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PersonnelPreviousAllegation))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PersonnelPreviousAllegation>> GetAllegation(int id)
        {


            try
            {
                var previousAllegation = await _context.PersonnelPreviousAllegations.FindAsync(id);
                if (previousAllegation == null)
                {
                    return BadRequest($"Could not find any allegation with provided Id");
                }
                   
               
                return Ok(previousAllegation);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }

        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PersonnelPreviousAllegation))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<PersonnelPreviousAllegation>>> GetAll()
        {


            try
            {
                var allegations = await _context.PersonnelPreviousAllegations.ToListAsync();
                if (allegations == null)
                {
                    return BadRequest($"Could not find any allegation with provided Id");
                }


                return Ok(allegations);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }

        }
        [HttpPost("{Id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<PersonnelPreviousAllegation>> PostAllegation(int Id,[FromForm] PersonnelPreviousAllegationCreationDTO previousAllegationCreationDTO)
        {



            var existingAllegation = await GetAllegation(Id);
            if (existingAllegation == null)
            {
                return BadRequest($"Could not find any previous allegation with provided Id");
            }
            var date = DateTime.Now;
            var fileRoute = "Resources\\Media\\PreviousAllegations\\" + date.ToString("MMM") + date.Year.ToString();
            var personnelPrevious = _mapper.Map<PersonnelPreviousAllegationCreationDTO, PersonnelPreviousAllegation>(previousAllegationCreationDTO);
                

           /* if (previousAllegationCreationDTO.DocumentFormFiles != null)
            {

                string fileUrl = await _fileStorageService.SaveFile(fileRoute, previousAllegationCreationDTO.DocumentFormFiles );
                personnelPrevious.AttachmentUrl = fileUrl;
                personnelPrevious.AttachmentPath = fileRoute;
            }*/

            personnelPrevious.Date = DateTime.Now;
            personnelPrevious.Description = personnelPrevious.Description;
            _context.PersonnelPreviousAllegations.Add(personnelPrevious);

            try
            {
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetAllegation), new { id = personnelPrevious.Id }, Id + " updated successfully!");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IspersonnelAllegationExists(Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<PersonnelPreviousAllegation>> PutPreviousAllegation(int Id,[FromForm] PersonnelPreviousAllegationUpdateDTO personnelPreviousAllegation)
        {
            var existingAllegation = await GetAllegation(Id);
            if (Id != existingAllegation.Value.Id)            
                return BadRequest($"Could not find any gender with provided Id");            

            if (existingAllegation == null)            
                return BadRequest($"Could not find any gender with provided Id");

            var  previousAllegation = _mapper.Map<PersonnelPreviousAllegationUpdateDTO, PersonnelPreviousAllegation>(personnelPreviousAllegation);
          /*  if (personnelPreviousAllegation.DocumentFormFiles != null)
            {

                string fileUrl = await _fileStorageService.EditFile(existingAllegation.Value.AttachmentPath, personnelPreviousAllegation.DocumentFormFiles, existingAllegation.Value.AttachmentUrl);
                existingAllegation.Value.AttachmentUrl = fileUrl;
            }*/
            existingAllegation.Value.Date= DateTime.Now;
            existingAllegation.Value.PersonnelId = previousAllegation.PersonnelId;
            existingAllegation.Value.Description = previousAllegation.Description;

            _context.Entry(existingAllegation).State = (Microsoft.EntityFrameworkCore.EntityState)EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetAllegation), new { Id = previousAllegation.Id }, previousAllegation);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IspersonnelAllegationExists(Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }



        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PersonnelPreviousAllegation>> DeleteAllegation(int id)
        {

            var personnelPrevious = await _context.PersonnelPreviousAllegations.FindAsync(id);
            if (personnelPrevious != null)
            {
                _context.PersonnelPreviousAllegations.Remove(personnelPrevious);
                await _context.SaveChangesAsync();
                if (personnelPrevious.AttachmentUrl != null)
                {
                    await _fileStorageService.DeleteFile(personnelPrevious.AttachmentUrl, personnelPrevious.AttachmentPath);

                }
              
                return CreatedAtAction(nameof(GetAllegation), new { id = personnelPrevious.Id }, id + " deleted successfully!");

            }

            return NotFound();
        }

        private Task<byte[]> GetFileBytesById(string id)
        {
            throw new NotImplementedException();
        }
        private bool IspersonnelAllegationExists(int id)
        {
            return _context.PersonnelPreviousAllegations.Any(e => e.Id == id);
        }
    }
}

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
    public class PersonnelWarningPunishmentController : ControllerBase
    {

        private readonly ISPoliceAppApiDbContext _context;
        private readonly IMapper _mapper;
        private readonly InAppStorageService _fileStorageService;

        public PersonnelWarningPunishmentController(ISPoliceAppApiDbContext context, IMapper mapper, InAppStorageService fileStorageService )
        {
            _mapper = mapper;
            _context = context;
            _fileStorageService = fileStorageService;
        }



      

        [HttpGet("{id}")]
        
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PersonnelWarningOrPunishment))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PersonnelWarningOrPunishment>> GetWarning(int id)
        {


            try
            {
                var warning = await _context.PersonnelWarningOrPunishments.FindAsync(id);
                if (warning == null)
                {
                    return BadRequest($"Could not find any warning with provided Id");
                }
                   
               
                return Ok(warning);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }

        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PersonnelWarningOrPunishment))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<PersonnelWarningOrPunishment>>> GetAll()
        {


            try
            {
                var warnings = await _context.PersonnelWarningOrPunishments.ToListAsync();
                if (warnings == null)
                {
                    return BadRequest($"Could not find any warning with provided Id");
                }


                return Ok(warnings);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }

        }
        [HttpPost("Warning")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<PersonnelWarningOrPunishment>> PostWarning([FromForm] PersonnelWarningOrPunishmentCreationDTO warningOrPunishmentCreationDTO)
        {


            var date = DateTime.Now;

            var fileRoute = "Resources\\Media\\WarningOrPunishment\\" + date.ToString("MMM") + date.Year.ToString();

            var warningOrPunishment = _mapper.Map<PersonnelWarningOrPunishmentCreationDTO, PersonnelWarningOrPunishment>(warningOrPunishmentCreationDTO);

          /*  if (warningOrPunishmentCreationDTO.DocumentFormFiles != null)
            {

                string fileUrl = await _fileStorageService.SaveFile(fileRoute, warningOrPunishmentCreationDTO.DocumentFormFiles);
                warningOrPunishment.AttachmentUrl = fileUrl;
                warningOrPunishment.AttachmentPath = fileRoute;
            }*/

            _context.PersonnelWarningOrPunishments.Add(warningOrPunishment);

            try
            {
                           

                await _context.SaveChangesAsync();


                return CreatedAtAction(nameof(GetWarning), new { id = warningOrPunishment.Id }, warningOrPunishment);

            }
            catch (DbUpdateConcurrencyException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
               
            }
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<PersonnelWarningOrPunishment>> PutPersonnelWarning(int Id,[FromForm] PersonnelWarningOrPunishmentUpdateDTO personnelWarningOrPunishment)
        {
            var existingWarning = await GetWarning(Id);
            if (Id != personnelWarningOrPunishment.Id)            
                return BadRequest($"Could not find any warning with provided Id");            

            if (existingWarning == null)            
                return BadRequest($"Could not find any warning with provided Id");

            var personnelWarning = _mapper.Map<PersonnelWarningOrPunishmentUpdateDTO,  PersonnelWarningOrPunishment>(personnelWarningOrPunishment);

           /* if (personnelWarningOrPunishment.DocumentFormFiles != null)
            {

                var fileUrl = await _fileStorageService.EditFile(existingWarning.Value.AttachmentPath, personnelWarningOrPunishment.DocumentFormFiles, existingWarning.Value.AttachmentUrl);
                personnelWarning.AttachmentUrl = fileUrl;

                existingWarning.Value.AttachmentUrl = personnelWarning.AttachmentUrl;
                existingWarning.Value.PersonnelId = personnelWarning.PersonnelId;
                existingWarning.Value.Title = personnelWarning.Title;

            }*/

            _context.Entry(existingWarning).State = (Microsoft.EntityFrameworkCore.EntityState)EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetWarning), new { Id = personnelWarning.Id }, personnelWarning);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IsPostingrExists(Id))
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
        public async Task<ActionResult<PersonnelWarningOrPunishment>> DeleteWarning(int id)
        {

            var personnelWarning = await _context.PersonnelWarningOrPunishments.FindAsync(id);
            if (personnelWarning != null)
            {
                _context.PersonnelWarningOrPunishments.Remove(personnelWarning);
                await _context.SaveChangesAsync();
                await _fileStorageService.DeleteFile(personnelWarning.AttachmentUrl, personnelWarning.AttachmentPath);
                
                return CreatedAtAction(nameof(GetWarning), new { id = personnelWarning.Id }, id + " deleted successfully!");

            }

            return NotFound();
        }

        private Task<byte[]> GetFileBytesById(string id)
        {
            throw new NotImplementedException();
        }
        private bool IsPostingrExists(int id)
        {
            return _context.PersonnelWarningOrPunishments.Any(e => e.Id == id);
        }
    }
}

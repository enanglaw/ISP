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
    public class PersonnelAllegationEnquiryController : ControllerBase
    {
        private readonly ISPoliceAppApiDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileStorageService _fileStorageService;
        private readonly ILogger<PersonnelAllegationEnquiryController> _logger;

        public PersonnelAllegationEnquiryController(ISPoliceAppApiDbContext context, IMapper mapper, IFileStorageService fileStorageService, ILogger<PersonnelAllegationEnquiryController> logger)
        {
            _logger = logger;
            _fileStorageService = fileStorageService;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PersonnelAllegationEnquiry))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PersonnelAllegationEnquiry>> GetEnquiry(int id)
        {


            try
            {
                var allegation = await _context.AllegationEnquiries.FindAsync(id);
                if (allegation == null)
                    return BadRequest($"Could not find any allegation with provided Id");
                var allegationDTO= _mapper.Map<List<AllegationCreationDTO>>(allegation);
                return Ok(allegationDTO);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Allegation>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<PersonnelAllegationEnquiryCreationDTO>>> GetAllegationEnquiries()
        {


            try
            {
                var allegations = await _context.AllegationEnquiries.ToListAsync();
                var allegationsDtos = _mapper.Map<List<PersonnelAllegationEnquiryCreationDTO>>(allegations);
                return allegationsDtos;
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<PersonnelAllegationEnquiry>> PostEnquiry([FromForm] PersonnelAllegationEnquiryCreationDTO personnelAllegationEnquiry)
        {

          

            _logger.LogInformation("Allegation creation starts...");
            try
            {
                /* var enquiry = _mapper.Map<PersonnelAllegationEnquiryCreationDTO, PersonnelAllegationEnquiry>(personnelAllegationEnquiry);
                 var date = DateTime.Now;
                 List<string> planPath = new List<string>();
                 var filePath = "Resources\\Media\\AllegationEnquiries\\" + date.ToString("MMM") + date.Year.ToString();

                 if (personnelAllegationEnquiry..MemorandumFormFiles != null)
                 {
                     var fileUrl = await _fileStorageService.SaveFile(filePath, personnelAllegationEnquiry.MemorandumFormFiles);
                     enquiry.MemorandumUrl = fileUrl;
                     enquiry.MemorandumPath = filePath;
                 }
                 if (personnelAllegationEnquiry.MOMFormFiles != null)
                 {
                     var fileUrl = await _fileStorageService.SaveFile(filePath, personnelAllegationEnquiry.MOMFormFiles);
                     enquiry.MOMUrl = fileUrl;
                     enquiry.MOMPath = filePath;
                 }
                 if (personnelAllegationEnquiry.NotesFormFiles != null)
                 {
                     var fileUrl = await _fileStorageService.SaveFile(filePath, personnelAllegationEnquiry.NotesFormFiles);
                     enquiry.NotesUrl = fileUrl;
                     enquiry.NotesPath = filePath;
                 }
                 if (personnelAllegationEnquiry.OutComeFormFiles != null)
                 {
                     var fileUrl = await _fileStorageService.SaveFile(filePath, personnelAllegationEnquiry.OutComeFormFiles);
                     enquiry.OutComeUrl = fileUrl;
                     enquiry.OutComePath = filePath;
                 }
                 if (personnelAllegationEnquiry.ParticipantFormFiles != null)
                 {
                     var fileUrl = await _fileStorageService.SaveFile(filePath, personnelAllegationEnquiry.ParticipantFormFiles);
                     enquiry.ParticipantUrl = fileUrl;
                     enquiry.ParticipantPath = filePath;

                 }
                 _logger.LogInformation("Creating allegation enqury & detail table starts...");

                 _context.AllegationEnquiries.Add(enquiry);
                 await _context.SaveChangesAsync();



                 _logger.LogInformation("Allegation enquiry created successfully...");
                 _logger.LogInformation("Creating Allegation & detail table ends...");*/
                return Ok();

            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError("Exception in Creating allegation enquiry & detail table: ", ex);
                throw;
            }
        }




        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutAllegationEnqury(int id, [FromForm] PersonnelAllegationEnquiryUpdateDTO personnelAllegationEnquiry)
        {

            var existingPersonnelAllegationEnquiry = await _context.AllegationEnquiries.FindAsync(id);
            if (existingPersonnelAllegationEnquiry == null)
            {
                return BadRequest($"Could not find any allegation with provided Id");
            }

           var allegationEnquiry = _mapper.Map<PersonnelAllegationEnquiryUpdateDTO, PersonnelAllegationEnquiry>(personnelAllegationEnquiry);

          /*  if (personnelAllegationEnquiry.MemorandumFormFiles != null)
            {

                var fileUrl = await _fileStorageService.EditFile(existingPersonnelAllegationEnquiry.MemorandumPath, personnelAllegationEnquiry.MemorandumFormFiles, existingPersonnelAllegationEnquiry.MemorandumUrl);
                allegationEnquiry.MemorandumUrl = fileUrl;
                existingPersonnelAllegationEnquiry.MemorandumUrl = allegationEnquiry.MemorandumUrl;
            }
            if (personnelAllegationEnquiry.MOMFormFiles != null)
            {
                var fileUrl = await _fileStorageService.EditFile(existingPersonnelAllegationEnquiry.MOMPath, personnelAllegationEnquiry.MOMFormFiles, existingPersonnelAllegationEnquiry.MOMUrl);
                allegationEnquiry.MOMUrl = fileUrl;
                existingPersonnelAllegationEnquiry.MOMUrl = allegationEnquiry.MOMUrl;

            }
            if (personnelAllegationEnquiry.NotesFormFiles != null)
            {
                var fileUrl = await _fileStorageService.EditFile(existingPersonnelAllegationEnquiry.NotesPath, personnelAllegationEnquiry.NotesFormFiles, existingPersonnelAllegationEnquiry.NotesUrl);
                allegationEnquiry.NotesUrl = fileUrl;
                existingPersonnelAllegationEnquiry.NotesUrl = allegationEnquiry.NotesUrl;

            }
            if (personnelAllegationEnquiry.OutComeFormFiles != null)
            {
                var fileUrl = await _fileStorageService.EditFile(existingPersonnelAllegationEnquiry.OutComePath, personnelAllegationEnquiry.OutComeFormFiles, existingPersonnelAllegationEnquiry.OutComeUrl);
                allegationEnquiry.OutComeUrl = fileUrl;
                existingPersonnelAllegationEnquiry.OutComeUrl = allegationEnquiry.OutComeUrl;

            }
            if (personnelAllegationEnquiry.ParticipantFormFiles != null)
            {
                var fileUrl = await _fileStorageService.EditFile(existingPersonnelAllegationEnquiry.ParticipantPath, personnelAllegationEnquiry.ParticipantFormFiles, existingPersonnelAllegationEnquiry.ParticipantUrl);
                allegationEnquiry.ParticipantUrl = fileUrl;
                existingPersonnelAllegationEnquiry.ParticipantUrl = allegationEnquiry.ParticipantUrl;

            }*/
            existingPersonnelAllegationEnquiry.AllegationId = allegationEnquiry.AllegationId;


            _context.Entry(existingPersonnelAllegationEnquiry).State = (Microsoft.EntityFrameworkCore.EntityState)EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetEnquiry), new { id = existingPersonnelAllegationEnquiry.Id }, id + " updated successfully!");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IsAllegationEnquiryExists(id))
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
        public async Task<ActionResult<PersonnelAllegationEnquiry>> DeleteAllegation(int id)
        {
            var allegation = await _context.AllegationEnquiries.FindAsync(id);
            if (allegation != null)
            {
                _context.AllegationEnquiries.Remove(allegation);
                await _context.SaveChangesAsync();
               

                if (allegation.MemorandumUrl != null)
                {

                     await _fileStorageService.DeleteFile(allegation.MemorandumUrl, allegation.MemorandumPath);
                   
                }
                if (allegation.MOMUrl != null)
                {
                    await _fileStorageService.DeleteFile(allegation.MOMUrl, allegation.MOMPath);
                 

                }
                if (allegation.NotesUrl != null)
                {
                    await _fileStorageService.DeleteFile(allegation.NotesUrl,allegation.NotesPath);

                }
                if (allegation.OutComeUrl != null)
                {
                     await _fileStorageService.DeleteFile(allegation.OutComeUrl,allegation.OutComePath);
                   

                }
                if (allegation.ParticipantUrl != null)
                {
                    await _fileStorageService.DeleteFile(allegation.ParticipantUrl,allegation.ParticipantPath);
                    ;

                }

                return CreatedAtAction(nameof(GetEnquiry), new { id = allegation.Id }, id + " deleted successfully!");

            }

            return NotFound();
        }

        private bool IsAllegationEnquiryExists(int id)
        {
            return _context.AllegationEnquiries.Any(e => e.Id == id);
        }


    }
}

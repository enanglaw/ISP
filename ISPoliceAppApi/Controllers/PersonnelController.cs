using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ISPoliceAppApi.Data;
using ISPoliceAppApi.Models;
using ISPoliceAppApi.DTOs;
using ISPoliceAppApi.Helpers;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ISPoliceAppApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PersonnelController : ControllerBase
  {
    private readonly ISPoliceAppApiDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<PersonnelController> _logger;
        private readonly IFileStorageService _fileStorageService;

        public PersonnelController(ISPoliceAppApiDbContext context, IFileStorageService fileStorageService, IMapper mapper, ILogger<PersonnelController> logger)
    {
      _logger = logger;
      _mapper = mapper;
            _fileStorageService = fileStorageService;
            _context = context;
    }

        [HttpPost("Upload")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<Personnel>> PostUpload()
        {


            try
            {
                var filePath = "Resources\\Media\\Personnel\\";                var file = Request.Form.Files[0];
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


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Personnel>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Personnel>>> GetPersonnels()
        {
            try
            {
                var personnel = await _context.Personnels.ToListAsync();
                if (personnel == null)
                    return BadRequest($"Could not find any personnel with provided Id");
                return Ok(personnel);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }

        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Personnel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Personnel>>> GetPersonnel(int id)
        {


            try
            {
                var personnel = await _context.Personnels
                                   
                                    .Include(x => x.PersonnelCaseDetails)
                                    .Include(x => x.PersonnelChildrens)
                                    .Include(x => x.PersonnelEducationalBackgrounds)
                                    .Include(x => x.PersonnelGallantryAwards)
                                    .Include(x => x.PersonnelPreviousAllegations)
                                    .Include(x => x.PersonnelWarningOrPunishments)
                                    .Include(x => x.PersonnelSpouses)
                                    .Include(x => x.PersonnelPostings).Where(x=>x.Id==id).ToListAsync();
                if (personnel == null)
                    return BadRequest($"Could not find any personnel with provided Id");
                return Ok(personnel);
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
        public async Task<IActionResult> PutPersonnel(int id, [FromBody] Personnel personnelCreationDTO)
        {
           
            if (id != personnelCreationDTO.Id)
            {
                return BadRequest($"Could not find any personnel with provided Id");

            }

            _context.Entry(personnelCreationDTO).State = EntityState.Modified;



            foreach (var posting in personnelCreationDTO.PersonnelPostings)
            {

                _context.Update(posting);
            }
            foreach (var personnelEducationBackground in personnelCreationDTO.PersonnelEducationalBackgrounds)
            {

                _context.Update(personnelEducationBackground);
            }
           
            foreach (var personnelCaseDetail in personnelCreationDTO.PersonnelCaseDetails)
            {

                _context.Update(personnelCaseDetail);
            }
            foreach (var personnelChild in personnelCreationDTO.PersonnelChildrens)
            {

                _context.Update(personnelChild);
            }
            foreach (var personnelSpouse in personnelCreationDTO.PersonnelSpouses)
            {

                _context.Update(personnelSpouse);
            }
            foreach (var personnelPreviousAllegation in personnelCreationDTO.PersonnelPreviousAllegations)
            {

                _context.Update(personnelPreviousAllegation);
            }
            foreach (var personnelGallantryAward in personnelCreationDTO.PersonnelGallantryAwards)
            {

                _context.Update(personnelGallantryAward);
            }
            foreach (var personnelWarningOrPunishment in personnelCreationDTO.PersonnelWarningOrPunishments)
            {

               
                _context.Update(personnelWarningOrPunishment);
            }

            try
            {
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IsOrgMediaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Personnel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostPersonnel([FromBody] Personnel personnel)
        {

            _logger.LogInformation("Personnel creation starts...");
            try
            {

                _logger.LogInformation("Creating Personnel & detail table starts...");
              foreach(var personnelCaseDetail in personnel.PersonnelCaseDetails)
                {
                    personnelCaseDetail.CaseCreatedDate =  DateTime.Now;
                }
                _context.Personnels.Add(personnel);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Personnel created successfully...");
                _logger.LogInformation("Creating Personnel & detail table ends...");
                return Ok();

            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError("Exception in Creating Personnel & detail table: ", ex);
                throw;
            }

        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Personnel>> DeleteOPersonnel(int id)
        {

            var folderPath = "Resources\\Media\\Personnel\\";
            var personnel = await _context.Personnels.FindAsync(id);
            if (personnel != null)
            {
                _context.Personnels.Remove(personnel);
                await _context.SaveChangesAsync();
                if (personnel.PersonnelPhotoUrl != null)
                {
                    await _fileStorageService.DeleteFile(personnel.PersonnelPhotoUrl, personnel.PersonnelPhotoPath);
                }
                if(personnel.PersonnelPreviousAllegations != null)
                {
                    foreach(var personnelPreviousAllegation in personnel.PersonnelPreviousAllegations)
                    {
                        await _fileStorageService.DeleteFile(personnelPreviousAllegation.AttachmentUrl, folderPath);
                    }
                   
                }
                if (personnel.PersonnelGallantryAwards != null)
                {
                    foreach (var personnelPersonnelGallantryAwards in personnel.PersonnelGallantryAwards)
                    {
                        await _fileStorageService.DeleteFile(personnelPersonnelGallantryAwards.AwardDocumentUrl, folderPath);
                        await _fileStorageService.DeleteFile(personnelPersonnelGallantryAwards.GallantryAwardUrl, folderPath);
                    }

                }
                if (personnel.PersonnelWarningOrPunishments != null)
                {
                    foreach (var personnelWarningOrPunishment in personnel.PersonnelWarningOrPunishments)
                    {
                        await _fileStorageService.DeleteFile(personnelWarningOrPunishment.AttachmentUrl, folderPath);
                    }

                }
                return Ok();

            }

            return NotFound();
        }

        private bool IsOrgMediaExists(int id)
    {
      return _context.Personnels.Any(e => e.Id == id);
    }
  }
}

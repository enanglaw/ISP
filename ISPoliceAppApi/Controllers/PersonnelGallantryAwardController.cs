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
    public class PersonnelGallantryAwardController : ControllerBase
    {

        private readonly ISPoliceAppApiDbContext _context;
        private readonly IMapper _mapper;
        private readonly InAppStorageService _fileStorageService;

        public PersonnelGallantryAwardController(ISPoliceAppApiDbContext context, IMapper mapper, InAppStorageService  fileStorageService )
        {
            _mapper = mapper;
            _context = context;
            _fileStorageService =fileStorageService;
        }



      

        [HttpGet("{id}")]
        
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PersonnelGallantryAward))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PersonnelGallantryAward>> GetAward(int id)
        {


            try
            {
                var  award = await _context.PersonnelGallantryAwards.FindAsync(id);
                if (award == null)
                {
                    return BadRequest($"Could not find any Awaard with provided Id");
                }
                   
               
                return Ok(award);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }

        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PersonnelGallantryAward))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<PersonnelGallantryAward>>> GetAll()
        {


            try
            {
                var awards = await _context.PersonnelGallantryAwards.ToListAsync();
                if (awards == null)
                {
                    return BadRequest($"Could not find any award with provided Id");
                }


                return Ok(awards);
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

        public async Task<ActionResult<PersonnelGallantryAward>> PostPersonnelAward([FromForm] PersonnelGallantryAwardCreationDTO gallantryAwardCreationDTO)
        {

            var date = DateTime.Now;

            var fileRoute = "Resources\\Media\\GallantryAwards\\" + date.ToString("MMM") + date.Year.ToString();
            var personnelGallantry = _mapper.Map<PersonnelGallantryAwardCreationDTO, PersonnelGallantryAward>(gallantryAwardCreationDTO);


           /* if (gallantryAwardCreationDTO.DocumentFormFiles != null)
            {

                string fileUrl = await _fileStorageService.SaveFile(fileRoute, gallantryAwardCreationDTO.DocumentFormFiles);
                personnelGallantry.GallantryAwardUrl = fileUrl;
                personnelGallantry.GallantryAwardPath = fileRoute;
            }*/
            _context.PersonnelGallantryAwards.Add(personnelGallantry);

            try
            {
              
                await _context.SaveChangesAsync();


                return CreatedAtAction(nameof(GetAward), new { id = personnelGallantry.Id }, personnelGallantry);

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

        public async Task<ActionResult<PersonnelGallantryAward>> PutPersonnelAward(int Id,[FromForm] PersonnelGallantryAwardUpdateDTO personnelGallantryAward)
        {
            var existingAward = await _context.PersonnelGallantryAwards.FindAsync(Id);            

            if (existingAward == null)            
                return BadRequest($"Could not find any gender with provided Id");

            var personnelGallantry = _mapper.Map<PersonnelGallantryAwardUpdateDTO, PersonnelGallantryAward>(personnelGallantryAward);

            /*if (personnelGallantryAward.DocumentFormFiles != null)
            {
            var fileUrl=  await  _fileStorageService.EditFile(existingAward.GallantryAwardPath, personnelGallantryAward.DocumentFormFiles, existingAward.GallantryAwardUrl);
                personnelGallantry.GallantryAwardUrl = fileUrl;
            }*/
            existingAward.GallantryAwardUrl = personnelGallantry.GallantryAwardUrl;
            existingAward.PersonnelId = personnelGallantry.PersonnelId;
            existingAward.IssueingAuthority =personnelGallantry.IssueingAuthority;
            existingAward.Title=personnelGallantry.Title;
            existingAward.IssuingDate = personnelGallantry.IssuingDate;

            _context.Entry(existingAward).State = (Microsoft.EntityFrameworkCore.EntityState)EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetAward), new { Id = personnelGallantry.Id }, personnelGallantry);
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
        public async Task<ActionResult<PersonnelGallantryAward>> DeleteAward(int id)
        {

            var personnelGallantry = await _context.PersonnelGallantryAwards.FindAsync(id);
            if (personnelGallantry != null)
            {
                _context.PersonnelGallantryAwards.Remove(personnelGallantry);
                await _context.SaveChangesAsync();
                await _fileStorageService.DeleteFile(personnelGallantry.GallantryAwardUrl, personnelGallantry.GallantryAwardPath);
                
                
                return CreatedAtAction(nameof(GetAward), new { id = personnelGallantry.Id }, id + " deleted successfully!");

            }

            return NotFound();
        }

        private Task<byte[]> GetFileBytesById(string id)
        {
            throw new NotImplementedException();
        }
        private bool IsPostingrExists(int id)
        {
            return _context.PersonnelGallantryAwards.Any(e => e.Id == id);
        }
    }
}

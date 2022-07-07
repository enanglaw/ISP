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
    public class PersonnelEducationController : ControllerBase
    {

        private readonly ISPoliceAppApiDbContext _context;
        private readonly IMapper _mapper;

        public PersonnelEducationController(ISPoliceAppApiDbContext context, Mapper mapper )
        {
            _mapper = mapper;
            _context = context;
        }



      

        [HttpGet("{id}")]
        
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PersonnelEducationalBackground))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PersonnelEducationalBackground>> GetEducation(int id)
        {


            try
            {
                var educationalBackground = await _context.PersonnelEducationalBackgrounds.FindAsync(id);
                if (educationalBackground == null)
                {
                    return BadRequest($"Could not find any allegation with provided Id");
                }
                   
               
                return Ok(educationalBackground);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }

        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PersonnelEducationalBackground))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<PersonnelEducationalBackground>>> GetAll()
        {


            try
            {
                var backgrounds = await _context.PersonnelEducationalBackgrounds.ToListAsync();
                if (backgrounds == null)
                {
                    return BadRequest($"Could not find any backgrounds with provided Id");
                }


                return Ok(backgrounds);
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

        public async Task<ActionResult<PersonnelEducationalBackground>> PostPersonnelEducation([FromForm] PersonnelEducationBackgroundCreationDTO educationBackgroundCreationDTO)
        {


            try
            {
                var educationalBackground = _mapper.Map<PersonnelEducationBackgroundCreationDTO, PersonnelEducationalBackground>(educationBackgroundCreationDTO);



                _context.PersonnelEducationalBackgrounds.Add(educationalBackground);
                await _context.SaveChangesAsync();


                return CreatedAtAction(nameof(GetEducation), new { id = educationalBackground.Id }, educationalBackground);

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

        public async Task<ActionResult<PersonnelEducationalBackground>> PutPersonnelEducation(int Id,[FromForm] PersonnelEducationBackgroundUpdateDTO educationBackgroundUpdateDTO)
        {
            var existingEducation = await GetEducation(Id);
            if (Id != existingEducation.Value.Id)            
                return BadRequest($"Could not find any gender with provided Id");            

            if (existingEducation == null)            
                return BadRequest($"Could not find any gender with provided Id");

            var educationalBackground = _mapper.Map<PersonnelEducationBackgroundUpdateDTO, PersonnelEducationalBackground>(educationBackgroundUpdateDTO);
            existingEducation.Value.AdmissionYear = educationalBackground.AdmissionYear;
            existingEducation.Value.PersonnelId = educationalBackground.PersonnelId;
            existingEducation.Value.CourseOfStudy = educationalBackground.CourseOfStudy;
            existingEducation.Value.GraduationYear = educationalBackground.GraduationYear;
            existingEducation.Value.InstitutionName = educationalBackground.InstitutionName;
            existingEducation.Value.QualificationName = educationalBackground.QualificationName;

            _context.Entry(existingEducation).State = (Microsoft.EntityFrameworkCore.EntityState)EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetEducation), new { Id = educationalBackground.Id }, educationalBackground);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IsEducationExists(Id))
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
        public async Task<ActionResult<PersonnelEducationalBackground>> DeleteGender(int id)
        {

            var educationalBackground = await _context.PersonnelEducationalBackgrounds.FindAsync(id);
            if (educationalBackground != null)
            {
                _context.PersonnelEducationalBackgrounds.Remove(educationalBackground);
                await _context.SaveChangesAsync();             
                
                return CreatedAtAction(nameof(GetEducation), new { id = educationalBackground.Id }, id + " deleted successfully!");

            }

            return NotFound();
        }

        private Task<byte[]> GetFileBytesById(string id)
        {
            throw new NotImplementedException();
        }
        private bool IsEducationExists(int id)
        {
            return _context.Genders.Any(e => e.Id == id);
        }
    }
}

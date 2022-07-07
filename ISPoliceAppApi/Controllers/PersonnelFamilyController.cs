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
    public class PersonnelFamilyController : ControllerBase
    {

        private readonly ISPoliceAppApiDbContext _context;
        private readonly IMapper _mapper;

        public PersonnelFamilyController(ISPoliceAppApiDbContext context, IMapper mapper )
        {
            _mapper = mapper;
            _context = context;
             }



      

        [HttpGet("{id}")]
        
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PersonnelPosting))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PersonnelFamily>> GetFamily(int id)
        {


            try
            {
                var personnelFamily = await _context.PersonnelFamilies.FindAsync(id);
                if (personnelFamily == null)
                {
                    return BadRequest($"Could not find any allegation with provided Id");
                }
                   
               
                return Ok(personnelFamily);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }

        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PersonnelFamily))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<PersonnelFamily>>> GetAll()
        {


            try
            {
                var personnelFamilies = await _context.PersonnelFamilies.ToListAsync();
                if (personnelFamilies == null)
                {
                    return BadRequest($"Could not find any family with provided Id");
                }


                return Ok(personnelFamilies);
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

        public async Task<ActionResult<PersonnelFamily>> PostFamily([FromForm] PersonnelFamilyCreationDTO personnelFamilyCreationDTO)
        {


            try
            {
                var personnelFamily = _mapper.Map<PersonnelFamilyCreationDTO, PersonnelFamily>(personnelFamilyCreationDTO);



                _context.PersonnelFamilies.Add(personnelFamily);
                await _context.SaveChangesAsync();


                return CreatedAtAction(nameof(GetFamily), new { id = personnelFamily.Id }, personnelFamily);

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

        public async Task<ActionResult<PersonnelFamily>> PutFamily(int Id,[FromForm] PersonnelFamilyUpdateDTO familyUpdateDTO)
        {
            var existingFamily = await GetFamily(Id);
            if (Id != familyUpdateDTO.Id)            
                return BadRequest($"Could not find any family with provided Id");            

            if (existingFamily == null)            
                return BadRequest($"Could not find any family with provided Id");

            var  personnelFamily  = _mapper.Map<PersonnelFamilyUpdateDTO, PersonnelFamily>(familyUpdateDTO);
            existingFamily.Value.FatherName = personnelFamily.FatherName;
            existingFamily.Value.MotherName = personnelFamily.MotherName;
            existingFamily.Value.Spouse = personnelFamily.Spouse;
            existingFamily.Value.ChildFullName = personnelFamily.ChildFullName;
            _context.Entry(existingFamily).State = (Microsoft.EntityFrameworkCore.EntityState)EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetFamily), new { Id = personnelFamily.Id }, personnelFamily);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IsFamilyExists(Id))
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
        public async Task<ActionResult<PersonnelFamily>> DeleteFamily(int id)
        {

            var personnelFamily = await _context.PersonnelFamilies.FindAsync(id);
            if (personnelFamily != null)
            {
                _context.PersonnelFamilies.Remove(personnelFamily);
                await _context.SaveChangesAsync();             
                
                return CreatedAtAction(nameof(GetFamily), new { id = personnelFamily.Id }, id + " deleted successfully!");

            }

            return NotFound();
        }

        private Task<byte[]> GetFileBytesById(string id)
        {
            throw new NotImplementedException();
        }
        private bool IsFamilyExists(int id)
        {
            return _context.PersonnelFamilies.Any(e => e.Id == id);
        }
    }
}

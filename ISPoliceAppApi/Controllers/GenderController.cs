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
using Microsoft.EntityFrameworkCore;

using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ISPoliceAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenderController : ControllerBase
    {

        private readonly ISPoliceAppApiDbContext _context;
        private readonly IMapper _mapper;

        public GenderController(ISPoliceAppApiDbContext context, IMapper mapper )
        {
            _mapper = mapper;
            _context = context;
        }



      

        [HttpGet("{id}")]
        
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Gender))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Gender>> GetGender(int id)
        {


            try
            {
                var gender = await _context.Genders.FindAsync(id);
                if (gender == null)
                {
                    return BadRequest($"Could not find any allegation with provided Id");
                }
                   
               
                return Ok(gender);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }

        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Gender))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<GlobalUpdateDTO>>> GetAll()
        {


            

            try
            {
               var gender =  await _context.Genders.ToListAsync();
               // var genderDTO = _mapper.Map<List<GlobalUpdateDTO>>(gender);

                if (gender == null)
                {
                    return BadRequest($"Could not find any allegation with provided Id");
                }


                return Ok(gender);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }

        }
        [HttpPost("Gender")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<Gender>> PostGender([FromBody] GlobalCreationDTO globalCreationDTO)
        {


            try
            {
                var gender = _mapper.Map<GlobalCreationDTO, Gender>(globalCreationDTO);



                _context.Genders.Add(gender);
                await _context.SaveChangesAsync();


                return CreatedAtAction(nameof(GetGender), new { id = gender.Id }, gender);

            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
               
            }
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<Gender>> PutGender(int Id,[FromForm] GlobalUpdateDTO globalUpdateDTO)
        {
            var existingGender = await GetGender(Id);
            if (Id != globalUpdateDTO.Id)            
                return BadRequest($"Could not find any gender with provided Id");            

            if (existingGender == null)            
                return BadRequest($"Could not find any gender with provided Id");

            var gender = _mapper.Map<GlobalUpdateDTO, Gender>(globalUpdateDTO);
            existingGender.Value.Name = gender.Name;

            _context.Entry(existingGender).State = (Microsoft.EntityFrameworkCore.EntityState)EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetGender), new { Id = gender.Id }, gender);
            }
            catch (Exception)
            {
                if (!IsGenderExists(Id))
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
        public async Task<ActionResult<Gender>> DeleteGender(int id)
        {

            var gender = await _context.Genders.FindAsync(id);
            if (gender != null)
            {
                _context.Genders.Remove(gender);
                await _context.SaveChangesAsync();             
                
                return CreatedAtAction(nameof(GetGender), new { id = gender.Id }, id + " deleted successfully!");

            }

            return NotFound();
        }

        private bool IsGenderExists(int id)
        {
            return _context.Genders.Any(e => e.Id == id);
        }
    }
}

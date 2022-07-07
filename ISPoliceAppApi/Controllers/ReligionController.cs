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
    public class ReligionController : ControllerBase
    {

        private readonly ISPoliceAppApiDbContext _context;
        private readonly IMapper _mapper;

        public ReligionController(ISPoliceAppApiDbContext context, IMapper mapper)
        {
            
            _mapper = mapper;
            _context = context;
        }





        [HttpGet("{id}")]

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Religion))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Gender>> GetReligion(int id)
        {


            try
            {
                var religion = await _context.Religions.FindAsync(id);
                if (religion == null)
                {
                    return BadRequest($"Could not find any allegation with provided Id");
                }


                return Ok(religion);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }

        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Religion))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Religion>>> GetAll()
        {


            try
            {
                var religion = await _context.Religions.ToListAsync();
                if (religion == null)
                {
                    return BadRequest($"Could not find any religion with provided Id");
                }


                return Ok(religion);
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

        public async Task<ActionResult<Religion>> PostReligion([FromBody] GlobalCreationDTO globalCreationDTO)
        {


            try
            {
                var religion = _mapper.Map<GlobalCreationDTO, Religion>(globalCreationDTO);



                _context.Religions.Add(religion);
                await _context.SaveChangesAsync();


                return CreatedAtAction(nameof(GetReligion), new { id = religion.Id }, religion);

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

        public async Task<ActionResult<Religion>> PutReligion(int Id, [FromBody] GlobalUpdateDTO globalUpdateDTO)
        {
            var existingReligion = await GetReligion(Id);
            if (Id != globalUpdateDTO.Id)
                return BadRequest($"Could not find any gender with provided Id");

            if (existingReligion == null)
                return BadRequest($"Could not find any gender with provided Id");

            var religion = _mapper.Map<GlobalUpdateDTO, Religion>(globalUpdateDTO);
            existingReligion.Value.Name = religion.Name;

            _context.Entry(existingReligion).State = (Microsoft.EntityFrameworkCore.EntityState)EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetReligion), new { Id = religion.Id }, religion);
            }
            catch (Exception)
            {
                if (!IsReligionExists(Id))
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
        public async Task<ActionResult<Religion>> DeleteReligion(int id)
        {

            var religion = await _context.Religions.FindAsync(id);
            if (religion != null)
            {
                _context.Religions.Remove(religion);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetReligion), new { id = religion.Id }, id + " deleted successfully!");

            }

            return NotFound();
        }

        
        private bool IsReligionExists(int id)
        {
            return _context.Religions.Any(e => e.Id == id);
        }
    }
}

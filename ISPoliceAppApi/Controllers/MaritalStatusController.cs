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
    public class MaritalStatusController : ControllerBase
    {

        private readonly ISPoliceAppApiDbContext _context;
        private readonly IMapper _mapper;

        public MaritalStatusController(ISPoliceAppApiDbContext context, IMapper mapper )
        {
            _mapper = mapper;
            _context = context;
        }



      

        [HttpGet("{id}")]
        
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MaritalStatus))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<MaritalStatus>> GetMaritalStatus(int id)
        {


            try
            {
                var maritalStatus = await _context.Maritals.FindAsync(id);
                if (maritalStatus == null)
                {
                    return BadRequest($"Could not find any allegation with provided Id");
                }
                   
               
                return Ok(maritalStatus);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }

        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MaritalStatus))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<MaritalStatus>>> GetAll()
        {


            try
            {
                var maritals = await _context.Maritals.ToListAsync();
                if (maritals == null)
                {
                    return BadRequest($"Could not find any Marital Status with provided Id");
                }


                return Ok(maritals);
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

        public async Task<ActionResult<MaritalStatus>> PostMaritalStatus([FromBody] GlobalCreationDTO globalCreationDTO)
        {


            try
            {
                var marital = _mapper.Map<GlobalCreationDTO, MaritalStatus>(globalCreationDTO);



                _context.Maritals.Add(marital);
                await _context.SaveChangesAsync();


                return CreatedAtAction(nameof(GetMaritalStatus), new { id = marital.Id }, marital);

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

        public async Task<ActionResult<MaritalStatus>> PutMaritalStatus(int Id,[FromForm] GlobalUpdateDTO globalUpdateDTO)
        {
            var existingMaritalStatus = await GetMaritalStatus(Id);
            if (Id != globalUpdateDTO.Id)            
                return BadRequest($"Could not find any gender with provided Id");            

            if (existingMaritalStatus == null)            
                return BadRequest($"Could not find any gender with provided Id");

            var marital = _mapper.Map<GlobalUpdateDTO, MaritalStatus>(globalUpdateDTO);
            existingMaritalStatus.Value.Name = marital.Name;

            _context.Entry(existingMaritalStatus).State = (Microsoft.EntityFrameworkCore.EntityState)EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetMaritalStatus), new { Id = marital.Id }, marital);
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
        public async Task<ActionResult<MaritalStatus>> DeleteMaritalStatus(int id)
        {

            var maritalStatus = await _context.Maritals.FindAsync(id);
            if (maritalStatus != null)
            {
                _context.Maritals.Remove(maritalStatus);
                await _context.SaveChangesAsync();             
                
                return CreatedAtAction(nameof(GetMaritalStatus), new { id = maritalStatus.Id }, id + " deleted successfully!");

            }

            return NotFound();
        }

       
        private bool IsGenderExists(int id)
        {
            return _context.Maritals.Any(e => e.Id == id);
        }
    }
}

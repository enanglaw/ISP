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
    public class LeaderPoliticalBackgroundController : ControllerBase
    {

        private readonly ISPoliceAppApiDbContext _context;
        private readonly IMapper _mapper;

        public LeaderPoliticalBackgroundController(ISPoliceAppApiDbContext context, Mapper mapper )
        {
            _mapper = mapper;
            _context = context;
        }



      

        [HttpGet("{id}")]
        
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LeaderPoliticalBackground))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LeaderPoliticalBackground>> GetPoliticalBackground(int id)
        {


            try
            {
                var leaderPoliticalBackground = await _context.LeaderPoliticalBackgrounds.FindAsync(id);
                if (leaderPoliticalBackground == null)
                {
                    return BadRequest($"Could not find any allegation with provided Id");
                }
                   
               
                return Ok(leaderPoliticalBackground);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }

        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LeaderPoliticalBackground))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<LeaderPoliticalBackground>>> GetAll()
        {


            try
            {
                var backgrounds = await _context.LeaderPoliticalBackgrounds.ToListAsync();
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

        public async Task<ActionResult<LeaderPoliticalBackground>> PostPoliticalBackground([FromForm] LeaderPoliticalBackgroundCreationDTO politicalBackgroundCreationDTO)
        {


            try
            {
                var leaderPoliticalBackground = _mapper.Map<LeaderPoliticalBackgroundCreationDTO, LeaderPoliticalBackground>(politicalBackgroundCreationDTO);



                _context.LeaderPoliticalBackgrounds.Add(leaderPoliticalBackground);
                await _context.SaveChangesAsync();


                return CreatedAtAction(nameof(GetPoliticalBackground), new { id = leaderPoliticalBackground.Id }, leaderPoliticalBackground);

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

        public async Task<ActionResult<LeaderPoliticalBackground>> PutPoliticalBackground(int Id,[FromForm] LeaderPoliticalBackgroundUpdateDTO politicalBackgroundUpdateDTO)
        {
            var existingPoliticalBackground = await GetPoliticalBackground(Id);
            if (Id != existingPoliticalBackground.Value.Id)            
                return BadRequest($"Could not find any political background with provided Id");            

            if (existingPoliticalBackground == null)            
                return BadRequest($"Could not find any political background with provided Id");

            var leaderPoliticalBackground = _mapper.Map<LeaderPoliticalBackgroundUpdateDTO, LeaderPoliticalBackground>(politicalBackgroundUpdateDTO);
            existingPoliticalBackground.Value.LeaderId = leaderPoliticalBackground.LeaderId;
            existingPoliticalBackground.Value.Position = leaderPoliticalBackground.Position;
            existingPoliticalBackground.Value.PositionYear = leaderPoliticalBackground.PositionYear;

            _context.Entry(existingPoliticalBackground).State = (Microsoft.EntityFrameworkCore.EntityState)EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetPoliticalBackground), new { Id = leaderPoliticalBackground.Id }, leaderPoliticalBackground);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IsBackgroundExists(Id))
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
        public async Task<ActionResult<LeaderPoliticalBackground>> DeleteGender(int id)
        {

            var leaderPoliticalBackground = await _context.LeaderPoliticalBackgrounds.FindAsync(id);
            if (leaderPoliticalBackground != null)
            {
                _context.LeaderPoliticalBackgrounds.Remove(leaderPoliticalBackground);
                await _context.SaveChangesAsync();             
                
                return CreatedAtAction(nameof(GetPoliticalBackground), new { id = leaderPoliticalBackground.Id }, id + " deleted successfully!");

            }

            return NotFound();
        }

        private Task<byte[]> GetFileBytesById(string id)
        {
            throw new NotImplementedException();
        }
        private bool IsBackgroundExists(int id)
        {
            return _context.LeaderPoliticalBackgrounds.Any(e => e.Id == id);
        }
    }
}

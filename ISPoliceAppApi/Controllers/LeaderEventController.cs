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

namespace ISPoliceAppApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class LeaderEventController : ControllerBase
  {
    private readonly ISPoliceAppApiDbContext _context;
        private readonly IFileStorageService _fileStorageService;
        private readonly IMapper _mapper;
    private readonly ILogger<LeaderEventController> _logger;

    public LeaderEventController(ISPoliceAppApiDbContext context, IFileStorageService fileStorageService, IMapper mapper, ILogger<LeaderEventController> logger)
    {
      _logger = logger;
      _mapper = mapper;
      _context = context;
            _fileStorageService = fileStorageService;
    }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<LeaderEvent>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<LeaderEvent>>> GetLeaderEvents()
        {

            try
            {
                var leaderEvents = await _context.LeaderEvents.ToListAsync();
                if (leaderEvents != null)
                {
                    return Ok(leaderEvents);
                }
                return BadRequest($"Could not find any leader events ");
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }

        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LeaderEvent))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LeaderEvent>> GetLeaderEvent(int id)
        {


            try
            {
                var leaderEvent = await _context.LeaderEvents.FindAsync(id);
                if (leaderEvent == null)
                    return BadRequest($"Could not find any leader event with provided Id");
                return Ok(leaderEvent);
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
        public async Task<IActionResult> PutLerderEvent(int id, [FromForm] LeaderEventUpdateDTO eventUpdateDTO)
        {
            var existingLederEvent = await GetLeaderEvent(id);
            if (existingLederEvent == null)
            {
                return BadRequest($"Could not find any leader event with provided Id");
            }
            var leaderEvent = _mapper.Map<LeaderEventUpdateDTO, LeaderEvent>(eventUpdateDTO);

            existingLederEvent.Value.LeaderId = leaderEvent.LeaderId;
            existingLederEvent.Value.Title = leaderEvent.Title;
            existingLederEvent.Value.EventDate = leaderEvent.EventDate;
            existingLederEvent.Value.Description = leaderEvent.Description;


            _context.Entry(existingLederEvent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetLeaderEvent), new { id = leaderEvent.Id }, leaderEvent);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IsOrgEventExists(id))
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LeaderEvent>> Post([FromForm] LeaderEventCreationDTO eventCreationDTO)
        {
            try
            {
                var leaderEvent = _mapper.Map<LeaderEventCreationDTO, LeaderEvent>(eventCreationDTO);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                await _context.LeaderEvents.AddAsync(leaderEvent);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetLeaderEvent), new { id = leaderEvent.Id }, leaderEvent);


            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }
        }



        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LeaderEvent>> DeleteLeaderEvent(int id)
        {

            var leaderEvent = await _context.LeaderEvents.FindAsync(id);
            if (leaderEvent != null)
            {
                _context.LeaderEvents.Remove(leaderEvent);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetLeaderEvent), new { id = leaderEvent.Id }, id + " deleted successfully!");

            }

            return NotFound();
        }

        private bool IsOrgEventExists(int id)
    {
      return _context.LeaderEvents.Any(e => e.Id == id);
    }
  }
}

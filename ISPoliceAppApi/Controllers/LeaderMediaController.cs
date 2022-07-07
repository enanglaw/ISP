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
  public class LeaderMediaController : ControllerBase
  {
    private readonly ISPoliceAppApiDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<LeaderMediaController> _logger;
        private readonly IFileStorageService _fileStorageService;

        public LeaderMediaController(ISPoliceAppApiDbContext context, IFileStorageService fileStorageService, IMapper mapper, ILogger<LeaderMediaController> logger)
    {
      _logger = logger;
      _mapper = mapper;
            _fileStorageService = fileStorageService;
            _context = context;
    }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OrganizationMedia>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<LeaderMedia>>> GetMedias()
        {

            try
            {
                var leaderMedias = await _context.LeaderMedias.ToListAsync();
                return Ok(leaderMedias);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }

        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LeaderMedia))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LeaderMedia>> GetMedia(int id)
        {


            try
            {
                var leaderMedia = await _context.LeaderMedias.FindAsync(id);
                if (leaderMedia == null)
                    return BadRequest($"Could not find any organization media with provided Id");
                return Ok(leaderMedia);
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
       /* public async Task<IActionResult> PutLeaderMedia(int id, LeaderMediaUpdateDTO mediaUpdateDTO)
        {

            var existingLeaderMedia = await GetMedia(id);
            if (existingLeaderMedia == null)
            {
                return BadRequest($"Could not find any leader media with provided Id for update");
            }
            var leaderMedia   = _mapper.Map<LeaderMediaUpdateDTO, LeaderMedia>(mediaUpdateDTO);
            if (mediaUpdateDTO.LeaderMediaUrl != null)
            {
                var fileUrl =await _fileStorageService.EditFile(existingLeaderMedia.Value.LeaderMediaPath, mediaUpdateDTO.LeaderMediaUrl, existingLeaderMedia.Value.LeaderMediaUrl);
                leaderMedia.LeaderMediaUrl = fileUrl;
                existingLeaderMedia.Value.LeaderMediaUrl = leaderMedia.LeaderMediaUrl;
            }

            existingLeaderMedia.Value.LeaderId = leaderMedia.LeaderId;
            existingLeaderMedia.Value.Title = leaderMedia.Title;
           


            _context.Entry(existingLeaderMedia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetMedia), new { id = leaderMedia.Id }, leaderMedia);
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
       */

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(LeaderMedia))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LeaderMedia>> DeleteLeaderMedia(int id)
        {

            var leaderMedia = await _context.LeaderMedias.FindAsync(id);
            if (leaderMedia != null)
            {
                _context.LeaderMedias.Remove(leaderMedia);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetMedia), new { id = leaderMedia.Id }, id + " deleted successfully!");

            }

            return NotFound();
        }

        private bool IsOrgMediaExists(int id)
    {
      return _context.LeaderMedias.Any(e => e.Id == id);
    }
  }
}

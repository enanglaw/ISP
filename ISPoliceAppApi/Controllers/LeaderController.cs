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

namespace ISPoliceAppApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class LeaderController : ControllerBase
  {
    private readonly ISPoliceAppApiDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<LeaderController> _logger;
        private readonly IFileStorageService _fileStorageService;

        public LeaderController(ISPoliceAppApiDbContext context, IFileStorageService fileStorageService, IMapper mapper, ILogger<LeaderController> logger)
    {
      _logger = logger;
      _mapper = mapper;
            _fileStorageService = fileStorageService;
            _context = context;
    }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Leader>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Leader>>> GetLeaders()
        {

            try
            {
                var leaders = await _context.Leaders.ToListAsync();
                return Ok(leaders);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }

        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Leader))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Leader>> GetLeader(int id)
        {


            try
            {
                var leader = await _context.Leaders.FindAsync(id);
                if (leader == null)
                    return BadRequest($"Could not find any organization media with provided Id");
                return Ok(leader);
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
        public async Task<IActionResult> PutLeader(int id, [FromForm] LeaderUpdateDTO leaderUpdateDTO)
        {
            var existingLeader = await GetLeader(id);
            if (existingLeader == null)
            {
                return BadRequest($"Could not find any leader with provided Id");
            }
            var leader= _mapper.Map<LeaderUpdateDTO, Leader>(leaderUpdateDTO);
            if (leader.Name != null)
            {
                existingLeader.Value.Name = leader.Name;
            }
            if (leader.NativeDistrict != null)
            {
                existingLeader.Value.NativeDistrict = leader.NativeDistrict;
            }
            if (leader.PermanentAddress != null)
            {
                existingLeader.Value.PermanentAddress = leader.PermanentAddress;
            }
            if (leader.PlaceOfBirth != null)
            {
                existingLeader.Value.PlaceOfBirth = leader.PlaceOfBirth;
            }
            if (leader.StrinkingPersonalityTrait != null)
            {
                existingLeader.Value.StrinkingPersonalityTrait = leader.StrinkingPersonalityTrait;
            }
            if (leader.PositionInTheParty != null)
            {
                existingLeader.Value.PositionInTheParty = leader.PositionInTheParty;
            }
            if (leader.Properties != null)
            {
                existingLeader.Value.Properties = leader.Properties;
            }
            if (leader.PresentAddress != null)
            {
                existingLeader.Value.PresentAddress = leader.PresentAddress;
            }
            if (leader.PresentPartyAffiliation != null)
            {
                existingLeader.Value.PresentPartyAffiliation = leader.PresentPartyAffiliation;
            }
            if (leader.ReligionId != null)
            {
                existingLeader.Value.ReligionId = leader.ReligionId;
            }
            if (leader.MaritalStatusId != null)
            {
                existingLeader.Value.MaritalStatusId = leader.MaritalStatusId;
            }
            if (leader.GenderId != null)
            {
                existingLeader.Value.GenderId = leader.GenderId;
            }
            if (leader.Alias != null)
            {
                existingLeader.Value.Alias = leader.Alias;
            }
            if (leader.Caste != null)
            {
                existingLeader.Value.Caste = leader.Caste;
            }

            _context.Entry(existingLeader).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetLeader), new { id = leader.Id }, leader);
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
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Leader))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostOrganizationMedia([FromBody] LeaderUpdateDTO leaderUpdateDTO)
        {


            var keys = HttpContext.Request.Query.Keys;

            LeaderEventCreationDTO eventCreationDTO = new LeaderEventCreationDTO();
            LeaderMediaCreationDTO mediaCreationDTO = new LeaderMediaCreationDTO();
            LeaderPoliticalBackgroundCreationDTO politicalBackgroundCreationDTO = new LeaderPoliticalBackgroundCreationDTO();
            List<LeaderEventCreationDTO> listOfEvents = new List<LeaderEventCreationDTO>();
            List<LeaderMediaCreationDTO> listOfMedia = new List<LeaderMediaCreationDTO>();
            List<LeaderPoliticalBackgroundCreationDTO> listOfPoliticalBackgrounds = new List<LeaderPoliticalBackgroundCreationDTO>();
            if (keys.Count > 0) {
            if (keys.Contains("LeaderPoliticalBackgrounds"))
            {
                var queryStringContents = HttpContext.Request.Query.Where(P => P.Key == "LeaderPoliticalBackgrounds").ToDictionary(O => O.Key, O => O.Value);
                foreach (var content in queryStringContents)
                {
                    var leaderPoliticalBackgrounds = content.Value.ToArray();
                    foreach (var leaderPoliticalBackground in leaderPoliticalBackgrounds)
                    {
                        LeaderPoliticalBackgroundCreationDTO creationDTO = JsonConvert.DeserializeObject<LeaderPoliticalBackgroundCreationDTO>(leaderPoliticalBackground);
                        listOfPoliticalBackgrounds.Add(creationDTO);
                    }


                }

            }

            if (keys.Contains("LeaderMedia"))
            {
                var queryStringContents = HttpContext.Request.Query.Where(P => P.Key == "LeaderMedia").ToDictionary(O => O.Key, O => O.Value);
                foreach (var content in queryStringContents)
                {
                    var leaderMedia = content.Value.ToArray();
                    foreach (var media in leaderMedia)
                    {
                        LeaderMediaCreationDTO creationDTO = JsonConvert.DeserializeObject<LeaderMediaCreationDTO>(media);
                        listOfMedia.Add(creationDTO);
                    }


                }

            }

            if (keys.Contains("LeaderEvents"))
            {
                var queryStringContents = HttpContext.Request.Query.Where(P => P.Key == "LeaderEvents").ToDictionary(O => O.Key, O => O.Value);
                foreach (var content in queryStringContents)
                {
                    var leaderEvents = content.Value.ToArray();
                    foreach (var @event in leaderEvents)
                    {
                        LeaderEventCreationDTO creationDTO = JsonConvert.DeserializeObject<LeaderEventCreationDTO>(@event);
                        listOfEvents.Add(creationDTO);
                    }


                }

            }
            }

            var leader = _mapper.Map<LeaderUpdateDTO, Leader>(leaderUpdateDTO);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
           

            await _context.Leaders.AddAsync(leader);
            try
            {
               
                await _context.SaveChangesAsync();
                var leaderPoliticalBackground = _mapper.Map<LeaderPoliticalBackgroundCreationDTO, LeaderPoliticalBackground>(politicalBackgroundCreationDTO);
                if (listOfMedia.Count > 0)
                    foreach (var politicalBackground in listOfPoliticalBackgrounds)
                    {
                        leaderPoliticalBackground.LeaderId = politicalBackground.LeaderId;
                        leaderPoliticalBackground.Position = politicalBackground.Position;
                        leaderPoliticalBackground.PositionYear = politicalBackground.PositionYear;
                        if (leaderPoliticalBackground.Id > 0)
                        {
                            leaderPoliticalBackground.Id = 0;
                        }
                        _context.LeaderPoliticalBackgrounds.Add(leaderPoliticalBackground);
                        await _context.SaveChangesAsync();
                    }
                var leaderMedia = _mapper.Map<LeaderMediaCreationDTO, LeaderMedia>(mediaCreationDTO);
                if (listOfMedia.Count > 0)
                    foreach (var media in listOfMedia)
                    {
                        leaderMedia.LeaderId = media.LeaderId;
                        leaderMedia.Title = media.Title;


                        if (leaderMedia.Id > 0)
                        {
                            leaderMedia.Id = 0;
                        }
                        _context.LeaderMedias.Add(leaderMedia);
                        await _context.SaveChangesAsync();
                    }
                var leaderEvent = _mapper.Map<LeaderEventCreationDTO, LeaderEvent>(eventCreationDTO);
            if(listOfEvents.Count>0)
                foreach (var @event in listOfEvents)
                {
                    leaderEvent.LeaderId = @event.LeaderId;
                    leaderEvent.Title = @event.Title;
                    leaderEvent.Description = @event.Description;
                    leaderEvent.EventDate = @event.EventDate;

                    if (leaderEvent.Id > 0)
                    {
                        leaderEvent.Id = 0;
                    }
                    _context.LeaderEvents.Add(leaderEvent);
                    await _context.SaveChangesAsync();
                }
                return CreatedAtAction(nameof(GetLeader), new { id = leader.Id }, leader);


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
        public async Task<ActionResult<Person>> DeleteLeader(int id)
        {

            var leader = await _context.Leaders.FindAsync(id);
            if (leader != null)
            {
                _context.Leaders.Remove(leader);
                await _context.SaveChangesAsync();
                
                return CreatedAtAction(nameof(GetLeader), new { id = leader.Id }, id + " deleted successfully!");

            }

            return NotFound();
        }

        private bool IsOrgMediaExists(int id)
    {
      return _context.OrganizationMedias.Any(e => e.Id == id);
    }
  }
}

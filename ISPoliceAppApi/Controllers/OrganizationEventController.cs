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
  public class OrganizationEventController : ControllerBase
  {
    private readonly ISPoliceAppApiDbContext _context;
        private readonly IFileStorageService _fileStorageService;
        private readonly IMapper _mapper;
    private readonly ILogger<OrganizationEventController> _logger;

    public OrganizationEventController(ISPoliceAppApiDbContext context, IFileStorageService fileStorageService, IMapper mapper, ILogger<OrganizationEventController> logger)
    {
      _logger = logger;
      _mapper = mapper;
      _context = context;
            _fileStorageService = fileStorageService;
    }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<EventsModel>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<EventsModel>>> GetOrganizationEvents()
        {
            EventsModel events=new EventsModel();
            List<EventsModel> allEvents = new List<EventsModel>();
            try
            {
                var organizationEvents = await _context.Events.ToListAsync();
                foreach (var org in organizationEvents)
                {

                    var organization = await _context.Organizations.FirstOrDefaultAsync(o => o.OrganizationId == org.OrganizationId);
                    var subOrganization = await _context.SubOrganizationCategories.FirstOrDefaultAsync(o => o.Id == org.SubOrganizationId);
                    events.Id = org.Id;
                    events.EventDate = org.EventDate;
                    if (organization== null)
                    {
                        events.OrganizationName = subOrganization.Name;
                        events.EventGroupName = subOrganization.Name;
                    }
                    if (organization != null)
                    {
                        events.OrganizationName = organization.FullName;
                        events.EventGroupName = organization.ShortName;
                    }
                    if (subOrganization == null)
                    {
                        events.SubOrganizationName = organization.FullName;
                        events.EventGroupName = organization.ShortName;
                    }
                    if (subOrganization != null)
                    {
                        events.SubOrganizationName = subOrganization.Name;
                        events.EventGroupName = subOrganization.Name;
                    }
                   
                    events.Description=org.Description;
                    events.Title = org.Title;
                    allEvents.Add(events);
                    events = new EventsModel();


                }
                return Ok(allEvents);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }

        }
        [Route("[action]/{eventId}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EventsModelDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<IEnumerable<EventsModelDTO>>> GetOrganizationEvent(string eventId)
        {

            int comparerId = 0;
            int.TryParse(eventId, out comparerId);
            EventsModelDTO events = new EventsModelDTO();
            List<EventsModelDTO> allEvents = new List<EventsModelDTO>();
            try
            {
                var organizationEvents = await _context.Events.Where(events => events.Id == comparerId).ToListAsync();
                if (organizationEvents == null || organizationEvents.Count == 0)
                    return BadRequest($"Could not find any organization event with provided Id");

                foreach (var org in organizationEvents)
                {
                    var organization = await _context.Organizations.FirstOrDefaultAsync(o => o.OrganizationId == org.OrganizationId);
                    var subOrganization = await _context.SubOrganizationCategories.FirstOrDefaultAsync(o => o.Id == org.SubOrganizationId);
                    
                    events.Id = org.Id;
                    events.EventDate = org.EventDate;
                   
                    if (organization != null)
                    {
                        events.OrganizationId = organization.OrganizationId;
                        events.OrganizationName = organization.FullName;
                        events.EventGroupName = organization.ShortName;
                        
                    }
                 
                    if (subOrganization != null)
                    {
                        events.SubOrganizationId = subOrganization.Id;
                        events.SubOrganizationName = subOrganization.Name;
                        events.EventGroupName = subOrganization.Name;
                    }
                    
                    events.Description = org.Description;
                    events.Title = org.Title;
                    allEvents.Add(events);

                    events = new EventsModelDTO();

                }

               return Ok(allEvents);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }

        }

        [HttpGet("{id}", Name = nameof(GetEvent))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EventsModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<IEnumerable<EventsModel>>> GetEvent(int id)
        {

            EventsModel events = new EventsModel();
            List<EventsModel> allEvents = new List<EventsModel>();
            try
            {
                var organizationEvents = await _context.Events.Where(o => o.OrganizationId == id).ToListAsync();
                if (organizationEvents == null|| organizationEvents.Count==0)
                    return BadRequest($"Could not find any organization event with provided Id");

                foreach (var org in organizationEvents)
                {
                    var organization = await _context.Organizations.FirstOrDefaultAsync(o => o.OrganizationId == org.OrganizationId);
                    var subOrganization = await _context.SubOrganizationCategories.FirstOrDefaultAsync(o => o.Id == org.SubOrganizationId);
                    var leader = await _context.LeadersGroups.FirstOrDefaultAsync(o => o.Id == org.LeaderId);
                    var subLeader = await _context.SubOrganizationLeaders.FirstOrDefaultAsync(o => o.Id == org.SubLeaderId);
                    events.Id = org.Id;
                    events.EventDate = org.EventDate;
                    if (organization != null)
                    {
                        events.OrganizationName = organization.FullName;
                        events.EventGroupName = organization.ShortName;
                    }
                    if (subOrganization != null)
                    {
                        events.SubOrganizationName = subOrganization.Name;
                        events.EventGroupName = subOrganization.Name;
                    }
                    if (leader != null)
                    {
                        events.LeaderName = leader.Name;
                    }
                    if (subLeader != null)
                    {
                        events.SubLeaderName = subLeader.Name;
                    }
                    events.Description = org.Description;
                    events.Title = org.Title;
                    allEvents.Add(events);

                    events = new EventsModel();

                }

                return Ok(allEvents);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }

        }
       
        /*[HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EventsModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<EventsModel>>> GetEvents(string eventGroupId)
        {
           
            int id=0;
            int.TryParse(eventGroupId, out id);
            
            EventsModel events = new EventsModel();
            List<EventsModel> allEvents = new List<EventsModel>();
            try
            {
                var organizationEvents = await _context.Events.Where(o=>o.OrganizationId==id).ToListAsync();
                if (organizationEvents == null)
                    return BadRequest($"Could not find any organization event with provided Id");

                foreach(var org in organizationEvents)
                {
                     var organization = await _context.Organizations.FirstOrDefaultAsync(o => o.OrganizationId == org.OrganizationId);
                    var subOrganization = await _context.SubOrganizationCategories.FirstOrDefaultAsync(o => o.Id == org.SubOrganizationId);
                    var leader = await _context.LeadersGroups.FirstOrDefaultAsync(o => o.Id == org.LeaderId);
                    var subLeader = await _context.SubOrganizationLeaders.FirstOrDefaultAsync(o => o.Id == org.SubLeaderId);
                    events.Id = org.Id;
                    events.EventDate = org.EventDate;
                    if (organization != null)
                    {
                        events.OrganizationName = organization.FullName;
                    }
                    if (subOrganization != null)
                    {
                        events.SubOrganizationName = subOrganization.Name;
                    }
                    if (leader != null)
                    {
                        events.LeaderName = leader.Name;
                    }
                    if (subLeader != null)
                    {
                        events.SubLeaderName = subLeader.Name;
                    }
                    events.Description = org.Description;
                    events.Title = org.Title;

                }
               
                return Ok(allEvents);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }

        }

        */

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutOrganizationEvent(int id,[FromBody] OrganizationEventCreationDTO organizationEventUpdateDTO)
        {
            if (organizationEventUpdateDTO.organizationId > 0 && organizationEventUpdateDTO.subOrganizationId ==0)
            {
                var existingOrganizationEvent = await _context.Events.FindAsync(id);

                if (existingOrganizationEvent == null)
                {
                    return BadRequest($"Could not find any organization event with provided Id for update");
                }
                var organizationEvent = _mapper.Map<OrganizationEventCreationDTO, OrganizationEvent>(organizationEventUpdateDTO);

                existingOrganizationEvent.OrganizationId = organizationEvent.OrganizationId;
                existingOrganizationEvent.Title = organizationEvent.Title;
                existingOrganizationEvent.EventDate = organizationEvent.EventDate;
                existingOrganizationEvent.Description = organizationEvent.Description;

                _context.Entry(existingOrganizationEvent).State = EntityState.Modified;

            }
            if (organizationEventUpdateDTO.subOrganizationId >0 && (organizationEventUpdateDTO.organizationId>0 || organizationEventUpdateDTO.organizationId==0))
            {
                var existingOrganizationEvent = await _context.Events.FindAsync(id);

                if (existingOrganizationEvent == null)
                {
                    return BadRequest($"Could not find any organization event with provided Id for update");
                }
                var organizationEvent = _mapper.Map<OrganizationEventCreationDTO, SubOrganizationEvent>(organizationEventUpdateDTO);

                existingOrganizationEvent.SubOrganizationId = organizationEvent.subOrganizationId;
                existingOrganizationEvent.Title = organizationEvent.Title;
                existingOrganizationEvent.EventDate = organizationEvent.EventDate;
                existingOrganizationEvent.Description = organizationEvent.Description;

                _context.Entry(existingOrganizationEvent).State = EntityState.Modified;

            }



            try
            {
                await _context.SaveChangesAsync();
                return Ok();
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
        [HttpPost("PostOrganizationEvent")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Events>> PostOrganizationEvent([FromBody] OrganizationEventCreationDTO @eventDTO)
       {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
               
                if ((@eventDTO.organizationId > 0 || @eventDTO.organizationId==0) && @eventDTO.subOrganizationId > 0)
                   {
                   var organizationEvent = _mapper.Map<OrganizationEventCreationDTO, Events>(@eventDTO);
                    Events eventsModel = new Events();
                    eventsModel.Title = organizationEvent.Title;
                    eventsModel.SubOrganizationId = organizationEvent.SubOrganizationId;
                    eventsModel.Description = organizationEvent.Description;
                    eventsModel.EventDate = organizationEvent.EventDate;
                    eventsModel.CreatedDate = DateTime.Now;
                     await _context.Events.AddAsync(eventsModel);
                       await _context.SaveChangesAsync();

                   }
                   else
                   {
                       var organizationEvent = _mapper.Map<OrganizationEventCreationDTO, Events>(@eventDTO);

                    Events eventsModel = new Events();
                    eventsModel.Title = organizationEvent.Title;
                    eventsModel.OrganizationId = organizationEvent.OrganizationId;
                    eventsModel.Description = organizationEvent.Description;
                    eventsModel.EventDate = organizationEvent.EventDate;
                    eventsModel.CreatedDate = DateTime.Now;
                    await _context.Events.AddAsync(eventsModel);
                    await _context.SaveChangesAsync();

                } 
               

                return Ok();


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
        public async Task<ActionResult<Person>> DeleteOrganizationEvent(int id)
        {

            var organizationEvent = await _context.Events.FindAsync(id);
            if (organizationEvent != null)
            {
                _context.Events.Remove(organizationEvent);
                await _context.SaveChangesAsync();
                return Ok();

            }

            return NotFound();
        }

        private bool IsOrgEventExists(int id)
    {
      return _context.Events.Any(e => e.Id == id);
    }
  }
}

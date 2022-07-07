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
  public class SubOrganizationEventController : ControllerBase
  {
    private readonly ISPoliceAppApiDbContext _context;
        private readonly IFileStorageService _fileStorageService;
        private readonly IMapper _mapper;
    private readonly ILogger<SubOrganizationEventController> _logger;

    public SubOrganizationEventController(ISPoliceAppApiDbContext context, IFileStorageService fileStorageService, IMapper mapper, ILogger<SubOrganizationEventController> logger)
    {
      _logger = logger;
      _mapper = mapper;
      _context = context;
            _fileStorageService = fileStorageService;
    }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<EventsModel>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<EventsModel>>> GetSubOrganizationEvents()
        {
            EventsModel events=new EventsModel();
            List<EventsModel> allEvents = new List<EventsModel>();
            try
            {
                var organizationEvents = await _context.SubOrganizationEvents.ToListAsync();
                if (organizationEvents==null)
                {
                    return NotFound(); ;
                }
                foreach (var org in organizationEvents)
                {
                    var result = await _context.SubOrganizationCategories.FirstOrDefaultAsync(o => o.Id == org.subOrganizationId);
                    if (result != null)
                    {
                        events.EventDate = org.EventDate;
                        events.Id = org.Id;
                        events.Description = result.Description;
                        events.Title = org.Title;
                         allEvents.Add(events);
                        events = new EventsModel();

                    }


                }
                return Ok(allEvents);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }

        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EventsModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<IEnumerable<EventsModel>>> GetEvent(int id)
        {

            EventsModel events = new EventsModel();
            List<EventsModel> allEvents = new List<EventsModel>();
            try
            {
                var organizationEvents = await _context.Events.Where(o =>o.SubOrganizationId == id).ToListAsync();
                if (organizationEvents == null || organizationEvents.Count == 0)
                    return BadRequest($"Could not find any organization event with provided Id");

                foreach (var org in organizationEvents)
                {
                  
                    var subOrganization = await _context.SubOrganizationCategories.FirstOrDefaultAsync(o => o.Id == org.SubOrganizationId);


                    var organization = await _context.Organizations.Where(p => p.OrganizationId == subOrganization.OrganizationId).FirstOrDefaultAsync();
                    events.Id = org.Id;
                    events.EventDate = org.EventDate;
                    
                    if (organization != null)
                    {
                        events.OrganizationName = organization.FullName;
                        events.OrganizationId = organization.OrganizationId;
                    }
                    
                    if (subOrganization != null)
                    {
                        events.SubOrganizationName = subOrganization.Name;
                        events.SubOrganizationId = subOrganization.Id;
                    }
                    events.EventGroupId = subOrganization.Id;
                    events.EventGroupName = subOrganization.Name;
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
        [Route("[action]/{subOrgId}")]
        [HttpGet]
         [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubOrganizationEvent))]
         [ProducesResponseType(StatusCodes.Status500InternalServerError)]

         public async Task<ActionResult<IEnumerable<EventsModel>>> GetOrganizationEvent(string subOrgId)
         {

            int id = 0;
            int.TryParse(subOrgId, out id);


             EventsModel orgEvent = new EventsModel();
             List<EventsModel> allEvents = new List<EventsModel>();


             try
             {
                 var organizationEvents = await _context.SubOrganizationEvents.Where(p=>p.subOrganizationId==id).ToListAsync();
                 if (organizationEvents == null)
                     return BadRequest($"Could not find any sub organization event with provided Id");
                 foreach (var organizationEvent in organizationEvents)
                 {
                     var subOrganizationName = await _context.SubOrganizationCategories.FirstOrDefaultAsync(o => o.Id == organizationEvent.subOrganizationId);
                     orgEvent.EventDate = organizationEvent.EventDate;
                     orgEvent.Id = organizationEvent.Id;
                     orgEvent.Title = organizationEvent.Title;
                     orgEvent.Description = subOrganizationName.Description;
                     orgEvent.EventGroupId = id;

                     allEvents.Add(orgEvent);
                     orgEvent = new EventsModel();
                 }

                 return Ok(allEvents);
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
        public async Task<IActionResult> PutOrganizationEvent(int id,[FromBody] OrganizationEventCreationDTO organizationEventUpdateDTO)
        {
           
            if (organizationEventUpdateDTO.subOrganizationId == id)
            {
                var existingOrganizationEvent = await _context.SubOrganizationEvents.FindAsync(id);

                if (existingOrganizationEvent == null)
                {
                    return BadRequest($"Could not find any organization event with provided Id for update");
                }
                var organizationEvent = _mapper.Map<OrganizationEventCreationDTO, SubOrganizationEvent>(organizationEventUpdateDTO);

                existingOrganizationEvent.subOrganizationId = organizationEvent.subOrganizationId;
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
        public async Task<ActionResult<OrganizationEvent>> PostOrganizationEvent([FromBody] OrganizationEventCreationDTO @eventDTO)
       {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                if (@eventDTO.subOrganizationId > 0 || @eventDTO.subOrganizationId==0)
                   {
                       var organizationEvent = _mapper.Map<OrganizationEventCreationDTO, SubOrganizationEvent>(@eventDTO);

                       organizationEvent.CreatedDate = DateTime.Now;
                       await _context.SubOrganizationEvents.AddAsync(organizationEvent);
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

            var organizationEvent = await _context.SubOrganizationEvents.FindAsync(id);
            if (organizationEvent != null)
            {
                _context.SubOrganizationEvents.Remove(organizationEvent);
                await _context.SaveChangesAsync();
                return Ok();

            }

            return NotFound();
        }

        private bool IsOrgEventExists(int id)
    {
      return _context.OrganizationEvents.Any(e => e.Id == id);
    }
  }
}

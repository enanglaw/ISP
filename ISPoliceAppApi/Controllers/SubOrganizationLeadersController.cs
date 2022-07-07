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
  public class SubOrganizationLeadersController : ControllerBase
  {
    private readonly ISPoliceAppApiDbContext _context;
        private readonly IFileStorageService _fileStorageService;
        private readonly IMapper _mapper;
    private readonly ILogger<SubOrganizationLeadersController> _logger;

    public SubOrganizationLeadersController(ISPoliceAppApiDbContext context, IFileStorageService fileStorageService, IMapper mapper, ILogger<SubOrganizationLeadersController> logger)
    {
      _logger = logger;
      _mapper = mapper;
      _context = context;
            _fileStorageService = fileStorageService;
    }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SubLeadersGroupModel>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<SubLeadersGroupModel>>> GetSubOrganizationLeaders()
        {
           
            try
            {
                var leadersGroup = await _context.SubOrganizationLeaders.ToListAsync();
                if (leadersGroup == null)
                {
                    return NotFound(); ;
                }
            
                return Ok(leadersGroup);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }

        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubLeadersGroupModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<SubLeadersGroupModel>>> GetSubOrganizationLeader(int id)
        {
            try
            {
                LeadersListDTO leaders = new LeadersListDTO();
                List<LeadersListDTO> allSubLeaders = new List<LeadersListDTO>();
                var subOrganizationleader = await _context.SubOrganizationCategories.FindAsync(id);
                if (subOrganizationleader == null)
                {
                    return BadRequest($"Could not find any sub organization leader with provided Id");
                }
                var subLeader = await _context.SubOrganizationLeaders.FirstOrDefaultAsync(s => s.SubOrganizationId == subOrganizationleader.Id);


                    var subOrganizations = await _context.Leaders.Where(p=>p.SubOrganizationLeaderId== subLeader.SubOrganizationId).ToListAsync();
                if (subOrganizations == null)
                {
                    return BadRequest($"Could not find any sub organization leader with provided Id");
                }

                foreach (var subOrganization in subOrganizations)
                {

                    leaders.GroupName = subOrganizationleader.Name;

                    leaders.Id = subOrganization.Id;
                    leaders.MobileNumber = subLeader.MobileNumber;
                    leaders.Designation = subLeader.Designation;
                    leaders.Name = subLeader.Name;
                    leaders.Address = subLeader.Address;
                    leaders.SubOrganizationId = subLeader.SubOrganizationId;
                    allSubLeaders.Add(leaders);
                    leaders = new LeadersListDTO();

                }





                return Ok(allSubLeaders);


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
        public async Task<IActionResult> PutSubOrganizationLeaders(int id,[FromBody] LeaderModelDTO organizationLeaderUpdateDTO)
        {
           
            if (organizationLeaderUpdateDTO.SubOrganizationId== 0 && organizationLeaderUpdateDTO.OrganizationId==id)
            {
             
                var existingOrganizationLeader = await _context.LeadersGroups.FindAsync(id);

                if (existingOrganizationLeader == null)
                {
                    return BadRequest($"Could not find any organization Leader with provided Id for update");
                }
                var organizationleader = _mapper.Map<LeaderModelDTO, LeadersGroupModel>(organizationLeaderUpdateDTO);

                existingOrganizationLeader.OrganizationId = organizationleader.OrganizationId;
                existingOrganizationLeader.MobileNumber = organizationleader.MobileNumber;
                existingOrganizationLeader.Designation = organizationleader.Designation;
                existingOrganizationLeader.Address = organizationleader.Address;
                existingOrganizationLeader.Name = organizationleader.Name;

                _context.Entry(existingOrganizationLeader).State = EntityState.Modified;

            }
            else
            {
                var existingSubOrganizationLeader = await _context.SubOrganizationLeaders.FindAsync(id);

                if (existingSubOrganizationLeader == null)
                {
                    return BadRequest($"Could not find any organization Leader with provided Id for update");
                }
                var organizationEvent = _mapper.Map<LeaderModelDTO, SubOrganizationLeaders>(organizationLeaderUpdateDTO);
                existingSubOrganizationLeader.SubOrganizationId = organizationEvent.SubOrganizationId;
                existingSubOrganizationLeader.MobileNumber = organizationEvent.MobileNumber;
                existingSubOrganizationLeader.Designation = organizationEvent.Designation;
                existingSubOrganizationLeader.Address = organizationEvent.Address;
                existingSubOrganizationLeader.Name = organizationEvent.Name;

                _context.Entry(existingSubOrganizationLeader).State = EntityState.Modified;



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
        [HttpPost("PostSubOrganizationLeaders")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PostSubOrganizationLeaders([FromBody] LeaderModelDTO leadersDTO)
       {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                if (leadersDTO.OrganizationId == 0 && leadersDTO.SubOrganizationId>0)
                   {
                     var subOrganizationleader = _mapper.Map<LeaderModelDTO, SubOrganizationLeaders>(leadersDTO);

                    subOrganizationleader.SubOrganizationId = leadersDTO.SubOrganizationId;
                    await _context.SubOrganizationLeaders.AddAsync(subOrganizationleader);
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
        public async Task<ActionResult<Person>> DeleteSubOrganizationleaders(int id)
        {

            var subOrganizationleader = await _context.SubOrganizationLeaders.FindAsync(id);
            if (subOrganizationleader != null)
            {
                _context.SubOrganizationLeaders.Remove(subOrganizationleader);
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

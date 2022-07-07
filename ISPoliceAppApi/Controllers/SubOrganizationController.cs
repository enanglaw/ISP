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
    public class SubOrganizationController : ControllerBase
    {

        private readonly ISPoliceAppApiDbContext _context;
        private readonly IMapper _mapper;

        public SubOrganizationController(ISPoliceAppApiDbContext context, IMapper mapper)
        {
            
            _mapper = mapper;
            _context = context;
        }


        [HttpGet("SubOrganizationList")]
        public async Task<ActionResult<IEnumerable<SubOrganizationModel>>> GetSubOrganizationList()
        {
            //SubOrganizationCategory SubOrganizationModel
            SubOrganizationModel subOrganization = new SubOrganizationModel();
            List<SubOrganizationModel> subOrganizations = new List<SubOrganizationModel>();
            var results = await _context.SubOrganizationCategories.ToListAsync();
             foreach( var result in results)
            {
                var organization = await _context.Organizations
                    .FirstOrDefaultAsync(p => p.OrganizationId == result.OrganizationId);
                subOrganization.SubOrganizationName = result.Name;
                subOrganization.OrganizationName = organization.ShortName;
                subOrganization.Description = result.Description;
                subOrganization.Id = result.Id;
                subOrganization.OrganizationId = result.OrganizationId;
                subOrganizations.Add(subOrganization);
                subOrganization = new SubOrganizationModel();

            }


            return Ok(subOrganizations);

        }

        [HttpGet("GetDetails")]

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubOrganizationCategory))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SubOrganizationCategory>> GetDetails(int id)
        {


            try
            {
                var subOrganization = await _context.SubOrganizationCategories.FindAsync(id);
                if (subOrganization == null)
                {
                    return BadRequest($"Could not find any sub-organization with provided Id");
                }
             var result=   _context.Organizations.Where(op => op.OrganizationId == subOrganization.OrganizationId);

                return Ok(result);
               // return Ok(subOrganization);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }

        }

        [HttpGet("SubOrganizationView")]

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrganizationViewList))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OrganizationViewList>> GetSubOrganizationView(int id)
        {


            try
            {
                var subOrganization = await _context.SubOrganizationCategories.FindAsync(id);
                if (subOrganization == null)
                {
                    return BadRequest($"Could not find any sub-organization with provided Id");
                }
           //     var result = _context.Organizations.Where(op => op.OrganizationId == subOrganization.OrganizationId);

               // var viewResult = _mapper.Map<List<OrganizationViewList>>(subOrganization);



                return Ok(subOrganization);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubOrganizationCategory>>> GetView(int id)
        {
         
            try
            {
                var subOrganization = await _context.SubOrganizationCategories.FindAsync(id);
                if (subOrganization == null)
                {
                    return BadRequest($"Could not find any sub-organization with provided Id");
                }
              //  var result = _context.Organizations.Where(op => op.OrganizationId == subOrganization.OrganizationId);

             //  var viewResult = _mapper.Map<List<SubOrganizationCategory>>(subOrganization);



                return Ok(subOrganization);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }
        }

        [HttpGet("{id}")]

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubOrganizationCategory))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SubOrganizationCategory>> GetSubOrganization(int id)
        {
            SubOrganizationModel subOrganization = new SubOrganizationModel();
            try
            {
                var subOrg = await _context.SubOrganizationCategories.FindAsync(id);
                if (subOrg == null)
                {
                    return BadRequest($"Could not find any sub-organization with provided Id");
                }

                var organization = await _context.Organizations
                        .FirstOrDefaultAsync(p => p.OrganizationId == subOrg.OrganizationId);
                if (organization == null)
                {
                    return BadRequest($"Could not find any sub-organization with provided Id");
                }

                subOrganization.SubOrganizationName = subOrg.Name;
                subOrganization.OrganizationName = organization.ShortName;
                subOrganization.Description = subOrg.Description;
                subOrganization.Id = subOrg.Id;
                subOrganization.OrganizationId = subOrg.OrganizationId;
                return Ok(subOrganization);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }

        }
        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubOrganizationCategory))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<SubOrganizationCategory>>> GetAll()
        {


            try
            {
                var subOrganizations = await _context.SubOrganizationCategories.ToListAsync();
                if (subOrganizations == null)
                {
                    return BadRequest($"Could not find any sub organization with provided Id");
                }


                return Ok(subOrganizations);
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

        public async Task<ActionResult<SubOrganizationList>> PostSubOrganization([FromBody] SubOrganizationList categoryCreationDTO)
        {


            try
            {
              

                foreach (var subOrganizationList in categoryCreationDTO.SubOrganizations)
                {
                    var subOrganization= _mapper.Map<SubOrganizationCategoryUpdateDTO, SubOrganizationCategory>(subOrganizationList);
                    subOrganization.OrganizationId = categoryCreationDTO.OrganizationId;
                    _context.SubOrganizationCategories.Add(subOrganization);
                    await _context.SaveChangesAsync();

                }

             


                return Ok();// CreatedAtAction(nameof(GetSubOrganization), new { id = subOrganization.Id }, subOrganization);

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

        public async Task<ActionResult<SubOrganizationCategory>> PutSubOrganization(int Id, [FromBody] OrganizationList categoryCreationDTO)
        {
            var existingOrganiation = await _context.SubOrganizationCategories.FindAsync(Id);

            if (existingOrganiation == null)
                return BadRequest($"Could not find any sub organiation with provided Id");
           
                foreach (var subOrganizationList in categoryCreationDTO.SubOrganizations)
                 {
                    var subOrganization = _mapper.Map<SubOrganizationCategoryCreationDTO, SubOrganizationCategory>(subOrganizationList);
                    subOrganization.OrganizationId = categoryCreationDTO.OrganizationId;               
                
                    if(subOrganization.Name!=null) {
                        existingOrganiation.Name = subOrganization.Name;

                    } 
                    if (subOrganization.Description != null)
                    {
                        existingOrganiation.Description = subOrganization.Description;
                    }
                    if (subOrganization.OrganizationId > 0)
                    {

                        var organiation = _context.Organizations.Where(p => p.OrganizationId == subOrganization.OrganizationId);
                        if (organiation != null)
                            existingOrganiation.OrganizationId = subOrganization.OrganizationId;
                    }

                    _context.Entry(existingOrganiation).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                  
                }
            return Ok();

        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SubOrganizationCategory>> DeleteSubOrganization(int id)
        {

            var subOrganization = await _context.SubOrganizationCategories.FindAsync(id);
            if (subOrganization != null)
            {
                _context.SubOrganizationCategories.Remove(subOrganization);
                await _context.SaveChangesAsync();

                return Ok();// CreatedAtAction(nameof(GetSubOrganization), new { id = subOrganization.Id }, id + " deleted successfully!");

            }

            return NotFound();
        }

      

        private bool IsSubOrganizationExists(int id)
        {
            return _context.SubOrganizationCategories.Any(e => e.Id == id);
        }
    }
}

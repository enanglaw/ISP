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
  public class OrganizationMediaController : ControllerBase
  {
    private readonly ISPoliceAppApiDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<OrganizationMediaController> _logger;
        private readonly IFileStorageService _fileStorageService;

        public OrganizationMediaController(ISPoliceAppApiDbContext context, IFileStorageService fileStorageService, IMapper mapper, ILogger<OrganizationMediaController> logger)
    {
      _logger = logger;
      _mapper = mapper;
            _fileStorageService = fileStorageService;
            _context = context;
    }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OrganizationMedia>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<OrganizationMedia>>> GetOrganizationMedia()
        {

            try
            {
                var organizations = await _context.OrganizationMedias.ToListAsync();
                return Ok(organizations);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }

        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrganizationMedia))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OrganizationMedia>> GetOrganizationMedia(int id)
        {


            try
            {
                var organizationMedia = await _context.OrganizationMedias.FindAsync(id);
                if (organizationMedia == null)
                    return BadRequest($"Could not find any organization media with provided Id");
                return Ok(organizationMedia);
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
        public async Task<IActionResult> PutOrganizationMedia(int id, [FromForm] OrganizationMediaCreationDTO organizationMediaCreationDTO)
        {
            var existingOrganization = await GetOrganizationMedia(id);
            if (existingOrganization==null)
            {
                return BadRequest($"Could not find any organization media with provided Id");
            }
            var organizationMedia= _mapper.Map<OrganizationMediaCreationDTO, OrganizationMedia>(organizationMediaCreationDTO);
            if (organizationMediaCreationDTO.MediaUrl != null)
            {
               
                existingOrganization.Value.MediaUrl = organizationMedia.MediaUrl;
            }
            existingOrganization.Value.OrganizationId = organizationMedia.OrganizationId;
            existingOrganization.Value.Name = organizationMedia.Name;

            _context.Entry(existingOrganization).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetOrganizationMedia), new { id = organizationMedia.Id }, organizationMedia);
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
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(OrganizationMedia))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostOrganizationMedia([FromBody] OrganizationMediaCreationDTO organizationMediaCreationDTO)
        {

            var date = DateTime.Now;
            var fileRouteContainer = "Resources\\Media\\OrganizationMedia\\" + date.ToString("MMM") + date.Year.ToString();
            var organizationMedia = _mapper.Map<OrganizationMediaCreationDTO, OrganizationMedia>(organizationMediaCreationDTO);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
           
            

            await _context.OrganizationMedias.AddAsync(organizationMedia);
            try
            {
               
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetOrganizationMedia), new { id = organizationMedia.Id }, organizationMedia);


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
        public async Task<ActionResult<Person>> DeleteOrganizationMedia(int id)
        {

            var organizationMedia = await _context.OrganizationMedias.FindAsync(id);
            if (organizationMedia != null)
            {
                _context.OrganizationMedias.Remove(organizationMedia);
                await _context.SaveChangesAsync();
                if (organizationMedia.MediaUrl != null)
                {
                    await _fileStorageService.DeleteFile(organizationMedia.MediaUrl, organizationMedia.MediaPath);
                }
                return CreatedAtAction(nameof(GetOrganizationMedia), new { id = organizationMedia.Id }, id + " deleted successfully!");

            }

            return NotFound();
        }

        private bool IsOrgMediaExists(int id)
    {
      return _context.OrganizationMedias.Any(e => e.Id == id);
    }
  }
}

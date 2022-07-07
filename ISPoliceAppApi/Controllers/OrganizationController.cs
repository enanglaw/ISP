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
using System.Net.Http.Headers;

namespace ISPoliceAppApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class OrganizationController : ControllerBase
  {
    private readonly ISPoliceAppApiDbContext _context;
    private readonly IMapper _mapper;
    private readonly IFileStorageService _fileStorageService;
    private readonly ILogger<OrganizationController> _logger;

    public OrganizationController(ISPoliceAppApiDbContext context, IMapper mapper, IFileStorageService fileStorageService, ILogger<OrganizationController> logger)
    {
      _logger = logger;
      _fileStorageService = fileStorageService;
      _mapper = mapper;
      _context = context;
    }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Organization>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Organization>>> GetOrganizations()
         {

            try
            {
                var organizations = await _context.Organizations.Include(p=>p.OrganizationEvent).Include(p=>p.SubOrganizationCategory).ToListAsync(); 

           

                return Ok(organizations);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }
            
         }
        
        [HttpGet("OrganizationView")]
        public async Task<ActionResult<IEnumerable<OrganizationViewDTO>>> OrganizationView()
        {
            var organizations = await _context.Organizations.Include(x => x.SubOrganizationCategory)
                .Include(x=>x.OrganizationEvent).OrderBy(x => x.FullName).ToListAsync();
         
            return Ok(organizations);
        }

        [HttpGet("OrganizationDropdown")]
        public async Task<ActionResult<IEnumerable<OrganizationDropdownDTO>>> OrganizationListDropdown()
        {
            var category = await _context.Organizations.Include(x => x.SubOrganizationCategory).OrderBy(x => x.FullName).ToListAsync();
            var categoryDto = _mapper.Map<List<OrganizationDropdownDTO>>(category);
            return categoryDto;
        }
        [HttpGet("SubOrganizationDropdown")]
        public async Task<ActionResult<IEnumerable<OrganizationAndSubOrganizationDropdown>>> SubOrganizationListDropdown()
        {
            
              
            var category = await _context.Organizations.Include(x => x.SubOrganizationCategory).OrderBy(x => x.FullName).ToListAsync();
   
        
          

            var organizationDto = _mapper.Map<List<OrganizationAndSubOrganizationDropdown>>(category);
          
           
            

            return Ok(organizationDto);
        }



      
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Organization>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<OrganizationAndSubOrganizationDropdown>>> GetOrganization(int Id)
        {


            try
            {
                //OrganizationCreationDTO
                var category = await _context.Organizations.Include(x => x.SubOrganizationCategory)
                    .OrderBy(x => x.FullName).Where(p => p.OrganizationId == Id).ToListAsync();

                var organizationDto = _mapper.Map<List<OrganizationAndSubOrganizationDropdown>>(category);
                return organizationDto;
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }
        }


        [Route("[action]/{orgId}")]
        [HttpGet]
       
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Organization))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Organization>> GetOrganization(string orgId)
            {


            int id = 0;
            int.TryParse(orgId, out id);
                    try
                    {

                      {

                          var organizations = await _context.Organizations.Include(x => x.SubOrganizationCategory)
                          .Include(x => x.OrganizationEvent).OrderBy(x => x.FullName).Where(p => p.OrganizationId == id).ToListAsync();                  

                         return Ok(organizations);


                       }

                    }
                    catch (Exception exception)
                    {
                        return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
                    }
            
            }
        [HttpPost("Upload")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<Organization>> OrganiationUpload()
        {


            try
            {
                var date = DateTime.Now;
                var filePath = "Resources\\Media\\Organization\\";
                var file = Request.Form.Files[0];
                var folderName = Path.Combine(filePath);
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    if (!Directory.Exists(folderName))
                    {
                        Directory.CreateDirectory(folderName);
                    }
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutOrganization(int id, [FromBody] Organization organization)
                {

            if (id != organization.OrganizationId)
            {
                return BadRequest();
            }

            _context.Entry(organization).State = EntityState.Modified;

           
            foreach (var organEvent in organization.OrganizationEvent)
            {

                _context.Update(organEvent);
            }
            foreach (var subOrganization in organization.SubOrganizationCategory)
            {
                _context.Update(subOrganization);
            }
           
           

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError("error", ex);
                if (!IsOrganizationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }




        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        
        public async Task<ActionResult<Organization>> PostOrganization([FromBody] Organization organizationCreationDTO)
                {
                 
          

            _logger.LogInformation("Organization creation starts...");
                  try
                  {
                    OrganizationDTO org = new OrganizationDTO();
                     org.FlagUrl = organizationCreationDTO.FlagUrl;
                     org.SymbolUrl = organizationCreationDTO.SymbolUrl;
                     org.ShortName = organizationCreationDTO.ShortName;
                     org.FullName = organizationCreationDTO.FullName;
                     org.Ideology = organizationCreationDTO.Ideology;
                    var organization = _mapper.Map<OrganizationDTO, Organization>(org);
                      _logger.LogInformation("Creating Organiation & detail table starts...");              
                      _context.Organizations.Add(organization);
                     await _context.SaveChangesAsync();
                    if (organizationCreationDTO.SubOrganizationCategory.Count > 0)
                    {
                        foreach (var category in organizationCreationDTO.SubOrganizationCategory)
                        {
                           
                            category.OrganizationId = organization.OrganizationId;
                            if (category.Id > 0)
                            {
                                category.Id = 0;
                            }
                            _context.SubOrganizationCategories.Add(category);
                            await _context.SaveChangesAsync();
                        }
                    }
                   if(organizationCreationDTO.OrganizationEvent.Count>0)
                    {
                      foreach (var @event in organizationCreationDTO.OrganizationEvent)
                         {
                           
                            @event.OrganizationId = organization.OrganizationId;
                            if (@event.Id > 0)
                            {
                            @event.Id = 0;
                            }
                            _context.OrganizationEvents.Add(@event);
                            await _context.SaveChangesAsync();
                        }
                     } 
                    _logger.LogInformation("Organizaation created successfully...");
                    _logger.LogInformation("Creating Organizaation & detail table ends...");
                    return CreatedAtAction(nameof(GetOrganization), new { id = organization.OrganizationId }, organization);
              
                        }
                  catch (DbUpdateConcurrencyException ex)
                  {
                    _logger.LogError("Exception in Creating Organization & detail table: ", ex);
                    throw;
                  }

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Organization>> DeleteOrganization(int id)
            {

            var organization = await _context.Organizations.FindAsync(id);
            if (organization != null)
            {
                _context.Organizations.Remove(organization);
                await _context.SaveChangesAsync();
                if (organization.SymbolUrl != null)
                {
                    organization.SymbolPath = organization.SymbolUrl.Substring(30);
                  await _fileStorageService.DeleteFile(organization.SymbolUrl,organization.SymbolPath);                 

                }
                if (organization.FlagUrl != null)
                {
                    organization.FlagPath = organization.FlagUrl.Substring(30);
                    await _fileStorageService.DeleteFile(organization.FlagUrl, organization.SymbolPath);

                }
                return Ok();
              
            }

            return NotFound();
        }

    private bool IsOrganizationExists(int id)
    {
      return _context.Organizations.Any(e => e.OrganizationId == id);
    }

     

    }
}

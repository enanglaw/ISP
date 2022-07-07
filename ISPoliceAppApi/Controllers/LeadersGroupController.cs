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
using Microsoft.AspNetCore.StaticFiles;

namespace ISPoliceAppApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class LeadersGroupController : ControllerBase
  {
    private readonly ISPoliceAppApiDbContext _context;
        private readonly IFileStorageService _fileStorageService;
        private readonly IMapper _mapper;
    private readonly ILogger<LeadersGroupController> _logger;

    public LeadersGroupController(ISPoliceAppApiDbContext context, IFileStorageService fileStorageService, IMapper mapper, ILogger<LeadersGroupController> logger)
    {
      _logger = logger;
      _mapper = mapper;
      _context = context;
            _fileStorageService = fileStorageService;
    }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<LeadersGroupModel>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<LeadersGroupModel>>> GetLeaders()
        {
            LeadersListDTO leader = new LeadersListDTO();
            List<LeadersListDTO> allSubLeaders = new List<LeadersListDTO>();
            try
            {
                var leaders = await _context.Leaders.Include(l=>l.LeaderMedia).Include(l=>l.LeaderEvents).Include(l=>l.LeaderPoliticalBackgrounds).ToListAsync();
                if (leaders == null)
                {
                    return NotFound(); ;
                }
               
                foreach(var leaderGroup in leaders)
                {
                    if (leaderGroup.OrganizationLeaderId > 0)
                    {

                        var currentOrgLeaders = await _context.LeadersGroups.Where(l=>l.LeaderId==leaderGroup.Id).ToListAsync();

                        foreach (var currentOrgLeader in currentOrgLeaders)
                        {

                            var customId = currentOrgLeader.Id;

                            var organization = await _context.Organizations.FindAsync(currentOrgLeader.OrganizationId);
                            leader.Id = leaderGroup.Id;
                            leader.Address = currentOrgLeader.Address;
                            leader.Designation = currentOrgLeader.Designation;
                            leader.MobileNumber = currentOrgLeader.MobileNumber;
                            leader.Name = currentOrgLeader.Name;
                            leader.OrganizationId = currentOrgLeader.OrganizationId;
                            if (organization != null)
                            {
                                leader.GroupName = organization.ShortName;
                            }
                            allSubLeaders.Add(leader);
                            leader = new LeadersListDTO();
                        }
                            
                                             
                          


                        
                    }
                    if (leaderGroup.SubOrganizationLeaderId > 0)
                    {
                        var currentSubOrgLeaders = await _context.SubOrganizationLeaders.Where(l=>l.LeaderId==leaderGroup.Id).ToListAsync();

                        foreach (var currentSubOrgLeader in currentSubOrgLeaders)
                        {
                            var organization = await _context.SubOrganizationCategories.FindAsync(currentSubOrgLeader.SubOrganizationId);
                            leader.Id = leaderGroup.Id;
                            leader.Address = currentSubOrgLeader.Address;
                            leader.Designation = currentSubOrgLeader.Designation;
                            leader.MobileNumber = currentSubOrgLeader.MobileNumber;
                            leader.Name = currentSubOrgLeader.Name;
                            leader.SubOrganizationId = currentSubOrgLeader.SubOrganizationId;
                            if (organization != null)
                            {
                                leader.GroupName = organization.Name;
                            }
                            allSubLeaders.Add(leader);
                            leader = new LeadersListDTO();
                        }

                    }

                   
                }
                         
                return Ok(allSubLeaders);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }

        }



        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LeadersGroupModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<LeadersGroupModel>>> GetOrganizationLeader(int id)
        {

            LeadersListDTO leader = new LeadersListDTO();
            List<LeadersListDTO> allSubLeaders = new List<LeadersListDTO>();
            try
            {
                var organization = await _context.Organizations.FindAsync(id);
                if (organization == null)
                {
                    return BadRequest($"Could not find any Leader with provided Id ");
                }

                var leadersInfo = await _context.LeadersGroups.FirstOrDefaultAsync(leader => leader.Id == organization.OrganizationId);

                if (leadersInfo == null)
                {
                    return BadRequest($"Could not find any Leader with provided Id ");
                }

              
                    var organizationLeaders = await _context.Leaders.Where(l=>l.OrganizationLeaderId==leadersInfo.Id).ToListAsync();
                    if (organizationLeaders == null)
                    {
                    return BadRequest($"Could not find any Leader with provided Id ");
                    }
                    foreach(var organizationLeader in organizationLeaders)

                        {
                        if (organization != null)
                        {
                            leader.GroupName = organization.ShortName;
                        }
                    
                            leader.Id = organizationLeader.Id;
                            leader.Name = leadersInfo.Name;
                            leader.MobileNumber = leadersInfo.MobileNumber;
                            leader.Designation = leadersInfo.Designation;
                            leader.Address = leadersInfo.Address;
                            leader.OrganizationId = leadersInfo.OrganizationId;
                            allSubLeaders.Add(leader);
                            leader = new LeadersListDTO();

                        }
             


                return Ok(allSubLeaders);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }

        }

        [Route("[action]/{leaderId}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LeaderCreationDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<IEnumerable<LeaderCreationDTO>>> GetLeader(string leaderId)
        {

            int comparerId = 0;
          
            LeaderCreationDTO allLeaderDetail=new LeaderCreationDTO();
            List<LeaderCreationDTO> allLeaderDetails = new List<LeaderCreationDTO>();
            LeadersListDTO leadersListDTO = new LeadersListDTO();
      

            int.TryParse(leaderId, out comparerId);


            try
            {
                
                var leadersInfo= await _context.Leaders.FindAsync(comparerId);

                if (leadersInfo==null)
                {
                    return BadRequest($"Could not find any Leader with provided Id ");
                }

                if (leadersInfo.SubOrganizationLeaderId > 0)
                {
                  var  subOrganizationLeaders = await _context.SubOrganizationLeaders.Where(l=>l.LeaderId==leadersInfo.Id).ToListAsync();
                    if (subOrganizationLeaders != null)
                    {
                      foreach(var subOrg in subOrganizationLeaders)
                        {
                            var subOrganization = await _context.SubOrganizationCategories.FindAsync(subOrg.SubOrganizationId);
                            allLeaderDetail.GroupName = subOrganization.Name;
                            allLeaderDetail.Id = leadersInfo.Id;
                            allLeaderDetail.MobileNumber = subOrg.MobileNumber;
                            allLeaderDetail.Designation = subOrg.Designation;
                            allLeaderDetail.Address = subOrg.Address;
                            allLeaderDetail.SubOrganizationLeaderId = subOrg.SubOrganizationId;
                            allLeaderDetail.Name = subOrg.Name;
                            allLeaderDetail.SubOrganizationName = subOrganization.Name;

                            var leader = await _context.Leaders.Include(events => events.LeaderEvents)
                                 .Include(media => media.LeaderMedia)
                                 .Include(x => x.LeaderPoliticalBackgrounds)
                                 .FirstOrDefaultAsync(leader => leader.Id == subOrg.LeaderId);


                            var leaderInfo = _mapper.Map<LeaderCreationDTO>(leader);

                            allLeaderDetail.LeaderId = leaderInfo.Id;
                            allLeaderDetail.LeaderName = leaderInfo.Name;
                            allLeaderDetail.MaritalStatusId = leaderInfo.MaritalStatusId;
                            allLeaderDetail.NativeDistrict = leaderInfo.NativeDistrict;
                            allLeaderDetail.PermanentAddress = leaderInfo.PermanentAddress;
                            allLeaderDetail.PlaceOfBirth = leaderInfo.PlaceOfBirth;
                            allLeaderDetail.PositionInTheParty = leaderInfo.PositionInTheParty;
                            allLeaderDetail.PresentAddress = leaderInfo.PresentAddress;
                            allLeaderDetail.PresentPartyAffiliation = leaderInfo.PresentPartyAffiliation;
                            allLeaderDetail.Properties = leaderInfo.Properties;
                            allLeaderDetail.ReligionId = leaderInfo.ReligionId;
                            allLeaderDetail.StrinkingPersonalityTrait = leaderInfo.StrinkingPersonalityTrait;
                            allLeaderDetail.Alias = leaderInfo.Alias;
                            allLeaderDetail.Caste = leaderInfo.Caste;
                            allLeaderDetail.DateOfBirth = leaderInfo.DateOfBirth;
                            var religion = await _context.Religions.FindAsync(leaderInfo.ReligionId);
                            if (religion != null)
                            {
                                allLeaderDetail.ReligionId = leaderInfo.ReligionId;
                                allLeaderDetail.ReligionName = leaderInfo.Name;
                            }
                            var gender = await _context.Genders.FindAsync(leaderInfo.GenderId);
                            if (gender != null)
                            {
                                allLeaderDetail.GenderId = leaderInfo.GenderId;
                                allLeaderDetail.GenderName = leaderInfo.Name;
                            }
                            var maritalStatus = await _context.Maritals.FindAsync(leaderInfo.MaritalStatusId);
                            if (maritalStatus != null)
                            {
                                allLeaderDetail.MaritalStatusId = leaderInfo.MaritalStatusId;
                                allLeaderDetail.MaritalStatusName = leaderInfo.Name;
                            }
                            allLeaderDetail.leaderPoliticalBackgrounds = leaderInfo.leaderPoliticalBackgrounds;
                            allLeaderDetail.leaderMedia = leaderInfo.leaderMedia;
                            allLeaderDetail.leaderEvents = leaderInfo.leaderEvents;
                            allLeaderDetails.Add(allLeaderDetail);
                            allLeaderDetail = new LeaderCreationDTO();
                        }



                    }
                }
                if (leadersInfo.OrganizationLeaderId > 0)
                {
                    var leadersMainGroups = await _context.LeadersGroups.Where(l=>l.LeaderId==leadersInfo.Id).ToListAsync();
                    if (leadersMainGroups!= null)
                    {
                        foreach (var org in leadersMainGroups)
                        {
                            var organization = await _context.Organizations.FindAsync(org.OrganizationId);
                            allLeaderDetail.GroupName = organization.ShortName;
                            allLeaderDetail.Id = leadersInfo.Id;
                            allLeaderDetail.MobileNumber = org.MobileNumber;
                            allLeaderDetail.Designation = org.Designation;
                            allLeaderDetail.Address = org.Address;
                            allLeaderDetail.OrganizationLeaderId = org.OrganizationId;
                            allLeaderDetail.Name = org.Name;
                            allLeaderDetail.OrganizationName = organization.FullName;
                            var leader = await _context.Leaders.Include(events => events.LeaderEvents)
                                .Include(media => media.LeaderMedia)
                                .Include(x => x.LeaderPoliticalBackgrounds)
                                .FirstOrDefaultAsync(leader => leader.Id == org.LeaderId);
                            var leaderInfo = _mapper.Map<LeaderCreationDTO>(leader);

                            allLeaderDetail.LeaderId = leaderInfo.Id;
                            allLeaderDetail.LeaderName = leaderInfo.Name;
                            allLeaderDetail.MaritalStatusId = leaderInfo.MaritalStatusId;
                            allLeaderDetail.NativeDistrict = leaderInfo.NativeDistrict;
                            allLeaderDetail.PermanentAddress = leaderInfo.PermanentAddress;
                            allLeaderDetail.PlaceOfBirth = leaderInfo.PlaceOfBirth;
                            allLeaderDetail.PositionInTheParty = leaderInfo.PositionInTheParty;
                            allLeaderDetail.PresentAddress = leaderInfo.PresentAddress;
                            allLeaderDetail.PresentPartyAffiliation = leaderInfo.PresentPartyAffiliation;
                            allLeaderDetail.Properties = leaderInfo.Properties;
                            allLeaderDetail.ReligionId = leaderInfo.ReligionId;
                            allLeaderDetail.StrinkingPersonalityTrait = leaderInfo.StrinkingPersonalityTrait;
                            allLeaderDetail.Alias = leaderInfo.Alias;
                            allLeaderDetail.Caste = leaderInfo.Caste;
                            allLeaderDetail.DateOfBirth = leaderInfo.DateOfBirth;
                            var religion = await _context.Religions.FindAsync(leaderInfo.ReligionId);
                            if (religion != null)
                            {
                                allLeaderDetail.ReligionId = leaderInfo.ReligionId;
                                allLeaderDetail.ReligionName = leaderInfo.Name;
                            }
                            var gender = await _context.Genders.FindAsync(leaderInfo.GenderId);
                            if (gender != null)
                            {
                                allLeaderDetail.GenderId = leaderInfo.GenderId;
                                allLeaderDetail.GenderName = leaderInfo.Name;
                            }
                            var maritalStatus = await _context.Maritals.FindAsync(leaderInfo.MaritalStatusId);
                            if (maritalStatus != null)
                            {
                                allLeaderDetail.MaritalStatusId = leaderInfo.MaritalStatusId;
                                allLeaderDetail.MaritalStatusName = leaderInfo.Name;
                            }
                            allLeaderDetail.leaderPoliticalBackgrounds = leaderInfo.leaderPoliticalBackgrounds;
                            allLeaderDetail.leaderMedia = leaderInfo.leaderMedia;
                            allLeaderDetail.leaderEvents = leaderInfo.leaderEvents;
                            allLeaderDetails.Add(allLeaderDetail);
                            allLeaderDetail = new LeaderCreationDTO();

                        }

                    }

                }
               

                return Ok(allLeaderDetails);
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

        public async Task<ActionResult<Plan>> PostUpload()
        {


            try
            {
                var date = DateTime.Now;
                var filePath = "Resources\\Media\\Leaders\\";
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
        public async Task<IActionResult> PutOrganizationLeaders(int id,[FromBody] LeaderUpdateDTO organizationLeaderUpdateDTO)
        {

            if (id!=organizationLeaderUpdateDTO.Id)
            {
                return BadRequest($"Could not find any leader with provided Id for update");
            }


            var leader = await _context.Leaders.Include(l=>l.LeaderEvents).Include(l=>l.LeaderMedia).Include(l=>l.LeaderPoliticalBackgrounds).AsNoTracking().FirstOrDefaultAsync(l=>l.Id==organizationLeaderUpdateDTO.Id);
                if (leader == null)
                {
                    return BadRequest($"Could not find any leader with provided Id for update");

                }
                if (leader.OrganizationLeaderId>0 && (leader.OrganizationLeaderId == organizationLeaderUpdateDTO.OrganizationLeaderId))
                {
                      var organizationLeader = await _context.LeadersGroups.FirstOrDefaultAsync(l=>l.LeaderId==leader.Id);

                        if(organizationLeader == null)
                        {

                        return BadRequest($"Could not find any leader with provided Id for update");
                        }
                        organizationLeader.OrganizationId = organizationLeaderUpdateDTO.OrganizationLeaderId;
                        organizationLeader.MobileNumber = organizationLeaderUpdateDTO.MobileNumber;
                        organizationLeader.Designation = organizationLeaderUpdateDTO.Designation;
                        organizationLeader.Address = organizationLeaderUpdateDTO.Address;
                        organizationLeader.Name = organizationLeaderUpdateDTO.Name;
                        organizationLeader.LeaderId = leader.Id;
                        _context.LeadersGroups.Update(organizationLeader);
                        //_context.SaveChanges();
            

                var leaderDetailInfo = _mapper.Map<LeaderUpdateDTO,LeaderDetailInfo>(organizationLeaderUpdateDTO);
                var leaderUpdateDTO = _mapper.Map<Leader>(leaderDetailInfo);
                               

                  _context.Entry(leaderUpdateDTO).State = EntityState.Modified;
              

                    foreach (var leaderEvent in leaderUpdateDTO.LeaderEvents.ToList())
                    {

                        var existingLeaderEvent = await _context.LeaderEvents.Where(l => l.LeaderId == leaderEvent.LeaderId).ToListAsync();

                        if (existingLeaderEvent.Count>0)
                        {
                            _context.RemoveRange(existingLeaderEvent);

                        }
                        _context.Update(leaderEvent);
                    }
                    foreach (var leaderMedia in leaderUpdateDTO.LeaderMedia.ToList())
                    {

                        var existingLeaderMedia = await _context.LeaderMedias.Where(l => l.LeaderId == leaderMedia.LeaderId).ToListAsync();

                        if (existingLeaderMedia.Count>0)
                        {
                            _context.RemoveRange(existingLeaderMedia);

                        }
                        _context.Update(leaderMedia);



                    }
                    foreach (var leaderPoliticalBackground in leaderUpdateDTO.LeaderPoliticalBackgrounds.ToList())
                    {
                        var existingLeaderPoliticalBackground = await _context.LeaderPoliticalBackgrounds.Where(l => l.LeaderId == leaderPoliticalBackground.LeaderId).ToListAsync();

                        if (existingLeaderPoliticalBackground.Count>0)
                        {
                            _context.RemoveRange(existingLeaderPoliticalBackground);

                        }
                        _context.Update(leaderPoliticalBackground);


                    }


                }


            if (leader.SubOrganizationLeaderId>0 &&(leader.SubOrganizationLeaderId == organizationLeaderUpdateDTO.SubOrganizationLeaderId))
                {
                var subOorganizationLeader = await _context.SubOrganizationLeaders.FirstOrDefaultAsync(l=>l.LeaderId==leader.Id);
              

                if (subOorganizationLeader == null)
                {

                    return BadRequest($"Could not find any leader with provided Id for update");
                }
                subOorganizationLeader.SubOrganizationId = organizationLeaderUpdateDTO.SubOrganizationLeaderId;
                subOorganizationLeader.MobileNumber = organizationLeaderUpdateDTO.MobileNumber;
                subOorganizationLeader.Designation = organizationLeaderUpdateDTO.Designation;
                subOorganizationLeader.Address = organizationLeaderUpdateDTO.Address;
                subOorganizationLeader.Name = organizationLeaderUpdateDTO.Name;
                subOorganizationLeader.LeaderId = leader.Id;
                _context.SubOrganizationLeaders.Update(subOorganizationLeader);
                



                var leaderDetailInfo = _mapper.Map<LeaderUpdateDTO, LeaderDetailInfo>(organizationLeaderUpdateDTO);
                var leaderUpdateDTO = _mapper.Map<Leader>(leaderDetailInfo);

                _context.Entry(leaderUpdateDTO).State = EntityState.Modified;


                foreach (var leaderEvent in leaderUpdateDTO.LeaderEvents.ToList())
                {

                    var existingLeaderEvent = await _context.LeaderEvents.Where(l => l.LeaderId == leaderEvent.LeaderId).ToListAsync();

                    if (existingLeaderEvent.Count > 0)
                    {
                        _context.RemoveRange(existingLeaderEvent);

                    }
                    _context.Update(leaderEvent);
                }
                foreach (var leaderMedia in leaderUpdateDTO.LeaderMedia.ToList())
                {

                    var existingLeaderMedia = await _context.LeaderMedias.Where(l => l.LeaderId == leaderMedia.LeaderId).ToListAsync();

                    if (existingLeaderMedia.Count > 0)
                    {
                        _context.RemoveRange(existingLeaderMedia);

                    }
                    _context.Update(leaderMedia);



                }
                foreach (var leaderPoliticalBackground in leaderUpdateDTO.LeaderPoliticalBackgrounds.ToList())
                {
                    var existingLeaderPoliticalBackground = await _context.LeaderPoliticalBackgrounds.Where(l => l.LeaderId == leaderPoliticalBackground.LeaderId).ToListAsync();

                    if (existingLeaderPoliticalBackground.Count > 0)
                    {
                        _context.RemoveRange(existingLeaderPoliticalBackground);

                    }
                    _context.Update(leaderPoliticalBackground);


                }





            }

            try
            {
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
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
        [HttpPost("PostOrganizationLeaders")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LeadersGroup>> PostOrganizationLeaders([FromBody] LeaderModelDTO leadersDTO)
       {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                


               if ((leadersDTO.OrganizationId>0 || leadersDTO.OrganizationId==0) && leadersDTO.SubOrganizationId>0)
                   {
                    var subOrganizationleader = _mapper.Map<LeaderModelDTO, SubOrganizationLeaders>(leadersDTO);                   
                    

                    LeaderCreationDTO subOrgleader = new LeaderCreationDTO();

                    var subleader = _mapper.Map<LeaderCreationDTO, Leader>(subOrgleader);

                    subleader.Name = subOrganizationleader.Name;
                    subleader.SubOrganizationLeaderId = subOrganizationleader.SubOrganizationId;

                    await _context.Leaders.AddAsync(subleader);
                    await _context.SaveChangesAsync();
                    subOrganizationleader.LeaderId = subleader.Id;
                    await _context.SubOrganizationLeaders.AddAsync(subOrganizationleader);
                    await _context.SaveChangesAsync();


                }
                   else
                   {
                    LeaderCreationDTO orgLeader = new LeaderCreationDTO();
                    var leader = _mapper.Map<LeaderCreationDTO, Leader>(orgLeader);
                    var organizationLeader = _mapper.Map<LeaderModelDTO, LeadersGroup>(leadersDTO);
                      
                    leader.Name = organizationLeader.Name;
                    leader.OrganizationLeaderId = organizationLeader.OrganizationId;
                    await _context.Leaders.AddAsync(leader);
                    await _context.SaveChangesAsync();
                    organizationLeader.LeaderId = leader.Id;
                    await _context.LeadersGroups.AddAsync(organizationLeader);
                    await _context.SaveChangesAsync();

                } 
              

                return Ok();


            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }
        }

        [Route("[action]/{id}")]
        [HttpGet]

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LeaderMedia))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LeaderMedia>> MediaDownload(int id)
        {

            var provider = new FileExtensionContentTypeProvider();
            var filePath = "Resources\\Media\\Leaders\\";
            var length = filePath.Length;
            try
            {
                var media = await _context.LeaderMedias.FindAsync(id);
                if (media == null)
                {
                    return BadRequest($"Could not find any allegation with provided Id");
                }
                var folderName = Path.Combine(media.LeaderMediaUrl);
                var file = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                string contentType;

                if (!provider.TryGetContentType(file, out contentType))
                {
                    contentType = "application/octet-stream";
                }
                byte[] fileBytes;
                if (System.IO.File.Exists(file))
                {
                    fileBytes = System.IO.File.ReadAllBytes(file);
                }
                else
                {
                    return NotFound();
                }

                return File(fileBytes, contentType, media.LeaderMediaUrl.Substring(length));
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
        public async Task<ActionResult<Leader>> Deletleaders(int id)
        {
            var leader = await _context.Leaders.Include(l => l.LeaderEvents).Include(l => l.LeaderMedia).Include(l => l.LeaderPoliticalBackgrounds).AsNoTracking().FirstOrDefaultAsync(l => l.Id == id);

            var filePath = "Resources\\Media\\Leaders\\";
            if (leader != null)
            {
                if (leader.LeaderMedia.Count > 0)
                {
                    foreach (var url in leader.LeaderMedia)
                    {
                        await _fileStorageService.DeleteFile(url.LeaderMediaUrl, filePath);
                    }
                        
                }
                if (leader.OrganizationLeaderId > 0)
                {
                    var leaderGroup = await _context.LeadersGroups.FirstOrDefaultAsync(p => p.LeaderId == leader.Id);
                    _context.LeadersGroups.Remove(leaderGroup);
                }
                if (leader.SubOrganizationLeaderId > 0)
                {
                    var leaderSubGroup = await _context.SubOrganizationLeaders.FirstOrDefaultAsync(p => p.LeaderId == leader.Id);
                    _context.SubOrganizationLeaders.Remove(leaderSubGroup);

                }

                _context.Leaders.Remove(leader);
                await _context.SaveChangesAsync();



                
                return Ok();

            }

            return NotFound();
        }

        private bool IsOrgEventExists(int id)
    {
      return _context.Leaders.Any(e => e.Id == id);
    }
  }
}

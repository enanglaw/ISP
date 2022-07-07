using AutoMapper;
using ISPoliceAppApi.Data;
using ISPoliceAppApi.DTOs;
using ISPoliceAppApi.Helpers;
using ISPoliceAppApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ISPoliceAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanController : ControllerBase
    {

        private readonly ISPoliceAppApiDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileStorageService _fileStorageService;
        private readonly ILogger<PlanController> _logger;

        public PlanController(ISPoliceAppApiDbContext context, IMapper mapper, IFileStorageService fileStorageService, ILogger<PlanController> logger)
        {
            _logger = logger;
            _fileStorageService = fileStorageService;
            _mapper = mapper;
            _context = context;
        }



      

        [HttpGet("{id}")]
        
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Plan))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Plan>> DownloadPlan(int id)
        {

            var provider = new FileExtensionContentTypeProvider();

            try
            {
                var plan = await _context.Plans.FindAsync(id);
               if (plan == null)
                {
                    return BadRequest($"Could not find any allegation with provided Id");
                }
                var folderName = Path.Combine(plan.PlanUrl);
                var file = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                string contentType;

                if (!provider.TryGetContentType(file, out contentType))
                {
                 contentType="application/octet-stream";
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
                
                return File(fileBytes,contentType,plan.PlanUrl.Substring(24));
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }

        }

        [HttpGet("Planview")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Plan>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Plan>>> GetPlanview()
        {


            try
            {
                var plan = await _context.Plans.ToListAsync();
                var planDTO = _mapper.Map<List<Plan>>(plan);
                return planDTO;
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
                var filePath = "Resources\\Media\\Plans\\";
                var file = Request.Form.Files[0];
                var folderName = Path.Combine(filePath);
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(new { dbPath });
                }
                else
                {
                    return  BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

    
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<Plan>> PostDocumentUpload([FromBody] PlanCreationDTO[] planCreationDTO)
        {



            _logger.LogInformation("Allegation creation starts...");
            try
            {
                _logger.LogInformation("Creating Plan & detail table starts...");

                 foreach(var newPlan in planCreationDTO)
                  {

                      var planDTO = _mapper.Map<PlanCreationDTO, Plan>(newPlan);
                         planDTO.PlanUrl = newPlan.documentUrl;
                         planDTO.PlanPath =  newPlan.documentUrl.Substring(0,30);
                         planDTO.CreatedDate = DateTime.Now;

                      _context.Plans.Add(planDTO);
                      await _context.SaveChangesAsync();
                  }
               

                _logger.LogInformation("Plan created successfully...");
                _logger.LogInformation("Creating Plan & detail table ends...");
                return Ok();

            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in Creating allegation & detail table: ", ex);
                throw;
            }
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Plan>> DeletePlan(int id)
        {

            var plan = await _context.Plans.FindAsync(id);
            if (plan != null)
            {
                _context.Plans.Remove(plan);
                await _context.SaveChangesAsync();
                     foreach(var url in plan.PlanUrl )
                    await _fileStorageService.DeleteFile(plan.PlanUrl, plan.PlanPath);

                
                
                return CreatedAtAction(nameof(DownloadPlan), new { id = plan.Id }, id + " deleted successfully!");

            }

            return NotFound();
        }
        private string SizeConverter(long bytes)
        {
            var fileSize = new decimal(bytes);
            var kilobyte = new decimal(1024);
            var megabyte = new decimal(1024 * 1024);
            var gigabyte = new decimal(1024 * 1024 * 1024);

            switch (fileSize)
            {
                case var _ when fileSize < kilobyte:
                    return $"Less then 1KB";
                case var _ when fileSize < megabyte:
                    return $"{Math.Round(fileSize / kilobyte, 0, MidpointRounding.AwayFromZero):##,###.##}KB";
                case var _ when fileSize < gigabyte:
                    return $"{Math.Round(fileSize / megabyte, 2, MidpointRounding.AwayFromZero):##,###.##}MB";
                case var _ when fileSize >= gigabyte:
                    return $"{Math.Round(fileSize / gigabyte, 2, MidpointRounding.AwayFromZero):##,###.##}GB";
                default:
                    return "n/a";
            }
        }
        private Task<byte[]> GetFileBytesById(string id)
        {
            throw new NotImplementedException();
        }
    }
}

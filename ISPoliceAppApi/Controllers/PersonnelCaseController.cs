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
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ISPoliceAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonnelCaseController : ControllerBase
    {

        private readonly ISPoliceAppApiDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<PersonnelCaseController> _logger;

        public PersonnelCaseController(ISPoliceAppApiDbContext context,  IMapper mapper, ILogger<PersonnelCaseController> logger )
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
        }



      

        [HttpGet("{id}")]
        
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PersonnelCaseDetail))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PersonnelCaseDetail>> GetCase(int id)
        {


            try
            {
                var personnelCase = await _context.PersonnelCaseDetails.FindAsync(id);
                if (personnelCase == null)
                {
                    return BadRequest($"Could not find any case with provided Id");
                }
                   
               
                return Ok(personnelCase);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }

        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PersonnelCaseDetail))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<PersonnelCaseDetail>>> GetAll()
        {


            try
            {
                var caseDetails = await _context.PersonnelCaseDetails.ToListAsync();
                if (caseDetails == null)
                {
                    return BadRequest($"Could not find any allegation with provided Id");
                }


                return Ok(caseDetails);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }

        }
        [HttpPost("Case")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<PersonnelCaseDetail>> PostCase([FromForm] PersonnelCaseDetailCreationDTO detailCreationDTO)
        {


            try
            {
                var personnelCase = _mapper.Map<PersonnelCaseDetailCreationDTO,  PersonnelCaseDetail>(detailCreationDTO);



                _context.PersonnelCaseDetails.Add(personnelCase);
                await _context.SaveChangesAsync();


                return CreatedAtAction(nameof(GetCase), new { id = personnelCase.Id }, personnelCase);

            }
            catch (DbUpdateConcurrencyException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
               
            }
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<Gender>> PutCase(int Id,[FromForm] PersonnelCaseDetailUpdateDTO  caseDetailUpdateDTOg)
        {
            var  existingCase = await GetCase(Id);
            if (Id != existingCase.Value.Id)            
                return BadRequest($"Could not find any case with provided Id");            

            if (existingCase == null)            
                return BadRequest($"Could not find any case with provided Id");

            var personnelCaseDetail = _mapper.Map<PersonnelCaseDetailUpdateDTO, PersonnelCaseDetail>(caseDetailUpdateDTOg);
            existingCase.Value.CaseNumber = personnelCaseDetail.CaseNumber;
            existingCase.Value.PersonnelId = personnelCaseDetail.PersonnelId;
            existingCase.Value.CaseSection = personnelCaseDetail.CaseSection;
            existingCase.Value.Title = personnelCaseDetail.Title;

            _context.Entry(existingCase).State = (Microsoft.EntityFrameworkCore.EntityState)EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetCase), new { Id = personnelCaseDetail.Id }, personnelCaseDetail);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IsCaseExists(Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }



        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PersonnelCaseDetail>> DeleteGender(int id)
        {

            var caseDetail = await _context.PersonnelCaseDetails.FindAsync(id);
            if (caseDetail != null)
            {
                _context.PersonnelCaseDetails.Remove(caseDetail);
                await _context.SaveChangesAsync();             
                
                return CreatedAtAction(nameof(GetCase), new { id = caseDetail.Id }, id + " deleted successfully!");

            }

            return NotFound();
        }

        private Task<byte[]> GetFileBytesById(string id)
        {
            throw new NotImplementedException();
        }
        private bool IsCaseExists(int id)
        {
            return _context.PersonnelCaseDetails.Any(e => e.Id == id);
        }
    }
}

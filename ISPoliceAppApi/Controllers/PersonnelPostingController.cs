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
    public class PersonnelPostingController : ControllerBase
    {

        private readonly ISPoliceAppApiDbContext _context;
        private readonly IMapper _mapper;
        public PersonnelPostingController(ISPoliceAppApiDbContext context, IMapper mapper )
        {
            _mapper = mapper;
            _context = context;
        }



      

        [HttpGet("{id}")]
        
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PersonnelPosting))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PersonnelPosting>> GetPosting(int id)
        {


            try
            {
                var posting = await _context.PersonnelPostings.FindAsync(id);
                if (posting == null)
                {
                    return BadRequest($"Could not find any allegation with provided Id");
                }
                   
               
                return Ok(posting);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }

        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PersonnelPosting))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<PersonnelPosting>>> GetAll()
        {


            try
            {
                var postings = await _context.PersonnelPostings.ToListAsync();
                if (postings == null)
                {
                    return BadRequest($"Could not find any allegation with provided Id");
                }


                return Ok(postings);
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

        public async Task<ActionResult<PersonnelPosting>> PostPersonnelPosting([FromForm] PersonnelPostingCreationDTO globalCreationDTO)
        {


            try
            {
                var personnelPosting = _mapper.Map<PersonnelPostingCreationDTO, PersonnelPosting>(globalCreationDTO);



                _context.PersonnelPostings.Add(personnelPosting);
                await _context.SaveChangesAsync();


                return CreatedAtAction(nameof(GetPosting), new { id = personnelPosting.Id }, personnelPosting);

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

        public async Task<ActionResult<Gender>> PutPersonnelPosting(int Id,[FromForm] PersonnelPostingUpdateDTO personnelPosting)
        {
            var existingPost = await GetPosting(Id);
            if (Id != personnelPosting.Id)            
                return BadRequest($"Could not find any gender with provided Id");            

            if (existingPost == null)            
                return BadRequest($"Could not find any gender with provided Id");

            var  posting = _mapper.Map<PersonnelPostingUpdateDTO, PersonnelPosting>(personnelPosting);
            existingPost.Value.From = posting.From;
            existingPost.Value.PersonnelId = posting.PersonnelId;
            existingPost.Value.To = posting.To;
            existingPost.Value.Place = posting.Place;
            existingPost.Value.Post = posting.Post;

            _context.Entry(existingPost).State = (Microsoft.EntityFrameworkCore.EntityState)EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetPosting), new { Id = posting.Id }, posting);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IsPostingrExists(Id))
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
        public async Task<ActionResult<PersonnelPosting>> DeleteGender(int id)
        {

            var personnelPosting = await _context.PersonnelPostings.FindAsync(id);
            if (personnelPosting != null)
            {
                _context.PersonnelPostings.Remove(personnelPosting);
                await _context.SaveChangesAsync();             
                
                return CreatedAtAction(nameof(GetPosting), new { id = personnelPosting.Id }, id + " deleted successfully!");

            }

            return NotFound();
        }

        private Task<byte[]> GetFileBytesById(string id)
        {
            throw new NotImplementedException();
        }
        private bool IsPostingrExists(int id)
        {
            return _context.PersonnelPostings.Any(e => e.Id == id);
        }
    }
}

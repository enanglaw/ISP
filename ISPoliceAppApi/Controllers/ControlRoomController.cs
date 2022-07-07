using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ISPoliceAppApi.Data;
using ISPoliceAppApi.DSR;
using ISPoliceAppApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ISPoliceAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControlRoomController : ControllerBase
    {
        private readonly ISPoliceAppApiDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ControlRoomController> _logger;

        public ControlRoomController(ISPoliceAppApiDbContext context, IMapper mapper, ILogger<ControlRoomController> logger)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }
        // GET: api/<ControlRoomController>
        [HttpGet("Get")]

        public async Task<ActionResult<IEnumerable<ControlRoomDSRModel>>> Get()
        {
            var controlRoomList = await _context.ControlRoomDSR.ToListAsync();
            var controlRoomListModel = _mapper.Map<List<ControlRoomDSRModel>>(controlRoomList);
            return controlRoomListModel;

        }
        [HttpGet("getCategoryDrop")]
        public async Task<ActionResult<IEnumerable<ControlRoomDSRCategory>>> getCategoryDrop()
        {
            return await _context.ControlRoomDSRCategory.ToListAsync();

        }



        [HttpGet("{id}")]
        public async Task<ActionResult<ControlRoomDSR>> GetControlRoom(int id)
        {
            //var controlRoomDSR = await _context.ControlRoomDSR.FindAsync(id);
            var controlRoomDSR = await _context.ControlRoomDSR.Include(x => x.ControlRoomDSRAccuseds).ThenInclude(y => y.ControlRoomDSRAccusedDetails).FirstOrDefaultAsync(a => a.ControlRoomId == id);
            if (controlRoomDSR == null)
            {
                return NotFound();
            }
            return controlRoomDSR;
        }

        [HttpPost("save")]
        public async Task<ActionResult<ControlRoomDSR>> SaveControlRoom(ControlRoomDSR controlRoomDSR)
        {
            try
            {
                controlRoomDSR.EntryDate = System.DateTime.Now;
                _context.ControlRoomDSR.Add(controlRoomDSR);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetControlRoom", new { id = controlRoomDSR.ControlRoomId }, controlRoomDSR);
            }
            catch (Exception ex)
            {
                _logger.LogError("error", ex);
                throw;
            }


        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutControlRoom(int id, ControlRoomDSR controlRoomDSR)
        {
            if (id != controlRoomDSR.ControlRoomId)
            {
                return BadRequest();
            }

            _context.Entry(controlRoomDSR).State = EntityState.Modified;
            ;
            foreach (var DSRAccuseds in controlRoomDSR.ControlRoomDSRAccuseds)
            {
                _context.UpdateRange(DSRAccuseds.ControlRoomDSRAccusedDetails);
                _context.Update(DSRAccuseds);
            }


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError("error", ex);
                if (!ControlRoomExists(id))
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
        private bool ControlRoomExists(int id)
        {
            return _context.ControlRoomDSR.Any(e => e.ControlRoomId == id);
        }
        // DELETE api/<ControlRoomController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
